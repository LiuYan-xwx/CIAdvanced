using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CIAdvanced.HarmonyPatches
{
    public class AutoDisableCorruptPluginsPatch
    {
        public static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName("ClassIsland.Models.Settings");
            return AccessTools.PropertyGetter(type, "AutoDisableCorruptPlugins");
        }

        [HarmonyPostfix]
        public static void Postfix(ref bool __result)
        {
            if (Plugin.Settings.PatchAutoDisableCorruptPlugins)
            {
                __result = false;
            }
        }
    }
}
