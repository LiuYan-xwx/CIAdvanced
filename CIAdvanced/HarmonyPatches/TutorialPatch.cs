using HarmonyLib;
using System.Collections;
using System.Reflection;

namespace CIAdvanced.HarmonyPatches
{
    public class TutorialPatch
    {
        public static HashSet<string> _completedTutorials = [];

        public static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName("ClassIsland.Models.Tutorial.TutorialSettings");
            return AccessTools.PropertyGetter(type, "CompletedTutorials");
        }

        public static void Prepare()
        {
            var type = AccessTools.TypeByName("ClassIsland.Core.Abstractions.Services.ITutorialService");
            var property = AccessTools.Property(type, "RegisteredTutorialGroups");
            IEnumerable groups = (IEnumerable)property?.GetValue(null)!;
            foreach (var group in groups)
            {
                IEnumerable tutorials = (IEnumerable)group.GetType().GetProperty("Tutorials")!.GetValue(group)!;
                foreach (var tutorial in tutorials)
                {
                    var tutorialType = tutorial.GetType();
                    string tutorialId = (string)tutorialType.GetProperty("Id")!.GetValue(tutorial)!;
                    IEnumerable paragraphs = (IEnumerable)tutorialType.GetProperty("Paragraphs")!.GetValue(tutorial)!;
                    foreach (var paragraph in paragraphs)
                    {
                        var paragraphType = paragraph.GetType();
                        string paragraphId = (string)paragraphType.GetProperty("Id")!.GetValue(paragraph)!;
                        _completedTutorials.Add($"{tutorialId}/{paragraphId}");
                    }
                }
            }
        }

        public static void Postfix(ref HashSet<string> __result)
        {
            if (Plugin.Settings.PatchTutorial)
            {
                __result = _completedTutorials;
            }
        }

        public static bool CheckTutorial()
        {
            var tutorialServiceType = AccessTools.TypeByName("ClassIsland.Core.Abstractions.Services.ITutorialService");
            if (tutorialServiceType is not null)
            {
                Plugin.Settings.CanYouAcuallyPatchTutorial = true;
                return true;
            }
            return false;
        }
    }
}
