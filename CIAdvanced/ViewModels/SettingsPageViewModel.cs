using CIAdvanced.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CIAdvanced.ViewModels
{
    public class SettingsPageViewModel : ObservableObject
    {
        public Settings Settings { get; set; } = Plugin.Settings;
    }
}
