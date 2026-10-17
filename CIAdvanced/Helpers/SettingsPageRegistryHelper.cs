using System.Collections;
using CIAdvanced.Models;
using HarmonyLib;

namespace CIAdvanced.Helpers;

internal static class SettingsPageRegistryHelper
{
    public static IEnumerable<SettingsPageOption> GetRegisteredPages()
    {
        var registryType = AccessTools.TypeByName("ClassIsland.Core.Services.Registry.SettingsWindowRegistryService");
        if (registryType == null ||
            AccessTools.Property(registryType, "Registered")?.GetValue(null) is not IEnumerable registered)
        {
            yield break;
        }

        foreach (var page in registered)
        {
            if (page == null || AccessTools.Property(page.GetType(), "Id")?.GetValue(page) is not string id ||
                string.IsNullOrWhiteSpace(id))
            {
                continue;
            }

            var name = AccessTools.Property(page.GetType(), "Name")?.GetValue(page) as string;
            yield return new SettingsPageOption(id, string.IsNullOrWhiteSpace(name) ? id : name);
        }
    }
}
