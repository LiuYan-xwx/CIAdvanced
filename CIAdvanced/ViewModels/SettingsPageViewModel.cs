using CIAdvanced.Helpers;
using CIAdvanced.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CIAdvanced.ViewModels
{
    public class SettingsPageViewModel : ObservableObject
    {
        public Settings Settings { get; set; } = Plugin.Settings;

        public IReadOnlyList<SettingsPageOption> SettingsPages { get; }

        public SettingsPageViewModel()
        {
            List<SettingsPageOption> pages = [new("", "使用 ClassIsland 默认页面")];
            pages.AddRange(SettingsPageRegistryHelper.GetRegisteredPages());

            // 保留已卸载插件的页面配置，避免打开下拉框时丢失原来的选择。
            var selectedId = Settings.DefaultSettingsPageId;
            if (!string.IsNullOrWhiteSpace(selectedId) && pages.All(x => x.Id != selectedId))
            {
                pages.Add(new SettingsPageOption(selectedId, "页面未注册，将使用默认页面"));
            }

            SettingsPages = pages;
        }
    }
}
