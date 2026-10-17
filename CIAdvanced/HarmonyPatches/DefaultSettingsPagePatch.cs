using CIAdvanced.Helpers;
using HarmonyLib;
using System.Reflection;
using System.Reflection.Emit;

namespace CIAdvanced.HarmonyPatches
{
    [HarmonyPatch]
    public class DefaultSettingsPagePatch
    {
        private static readonly Type? WindowType = AccessTools.TypeByName("ClassIsland.Views.SettingsWindowNew");
        private static readonly MethodInfo? OpenDefault = WindowType == null
            ? null : AccessTools.Method(WindowType, "Open", Type.EmptyTypes);
        private static readonly MethodInfo? OpenPage = WindowType == null
            ? null : AccessTools.Method(WindowType, "Open", [typeof(string), typeof(Uri)]);

        public static MethodBase TargetMethod()
        {
            return AccessTools.Method(AccessTools.TypeByName("ClassIsland.MainWindow"), "MenuItemSettings_OnClick");
        }

        public static bool Prepare()
        {
            return OpenDefault != null && OpenPage != null && TargetMethod() != null;
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var replacement = AccessTools.Method(typeof(DefaultSettingsPagePatch), nameof(OpenSettings));
            foreach (var instruction in instructions)
            {
                if (instruction.Calls(OpenDefault))
                {
                    // 保留原调用的标签和异常块，仅替换托盘入口处的 Open()。
                    instruction.opcode = OpCodes.Call;
                    instruction.operand = replacement;
                }

                yield return instruction;
            }
        }

        private static void OpenSettings(object window)
        {
            var key = Plugin.Settings.DefaultSettingsPageId;
            var viewModel = AccessTools.Property(WindowType, "ViewModel")?.GetValue(window);
            var selectedPage = viewModel == null ? null : AccessTools.Property(viewModel.GetType(), "SelectedPageInfo");
            var launchPage = AccessTools.Property(WindowType, "LaunchSettingsPage")?.GetValue(window);
            var startupPage = AccessTools.Field(WindowType, "StartupSettingsPage")?.GetValue(null);

            // 已经导航过或宿主指定了其他启动页（例如集控的“关于”页）时，沿用宿主行为。
            if (!string.IsNullOrWhiteSpace(key) && selectedPage != null &&
                selectedPage.GetValue(viewModel) == null &&
                AccessTools.Field(WindowType, "_isFirstNavigated")?.GetValue(window) is not true &&
                launchPage is string && Equals(launchPage, startupPage) &&
                SettingsPageRegistryHelper.GetRegisteredPages().Any(x => x.Id == key))
            {
                OpenPage!.Invoke(window, [key, null]);
                return;
            }

            OpenDefault!.Invoke(window, null);
        }
    }
}
