using HarmonyLib;
using System.Reflection;

namespace CIAdvanced.HarmonyPatches
{
    [HarmonyPatch]
    public class SellingAnnouncementPatch
    {
        public static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName("ClassIsland.Models.Settings");
            return AccessTools.PropertyGetter(type, "ShowSellingAnnouncement");
        }

        [HarmonyPostfix]
        public static void Postfix(ref bool __result)
        {
            __result = false;
        }
    }
}
