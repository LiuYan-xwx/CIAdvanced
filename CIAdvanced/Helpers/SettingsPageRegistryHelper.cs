using CIAdvanced.Models;
using HarmonyLib;
using System.Collections;

namespace CIAdvanced.Helpers;

internal static class SettingsPageRegistryHelper
{
    private static IReadOnlyList<SettingsPageOption>? _cachedPages;

    public static IReadOnlyList<SettingsPageOption> GetRegisteredPages()
    {
        if (_cachedPages != null)
        {
            return _cachedPages;
        }

        var registryType = AccessTools.TypeByName("ClassIsland.Core.Services.Registry.SettingsWindowRegistryService");
        if (registryType == null ||
            AccessTools.Property(registryType, "Registered")?.GetValue(null) is not IEnumerable registered)
        {
            return Array.Empty<SettingsPageOption>();
        }

        List<SettingsPageOption> pages = [];
        foreach (var page in registered)
        {
            if (page == null || AccessTools.Property(page.GetType(), "Id")?.GetValue(page) is not string id ||
                string.IsNullOrWhiteSpace(id))
            {
                continue;
            }

            var name = AccessTools.Property(page.GetType(), "Name")?.GetValue(page) as string;
            pages.Add(new SettingsPageOption(id, string.IsNullOrWhiteSpace(name) ? id : name));
        }

        return _cachedPages = pages.AsReadOnly();
    }
}
