using ClassIsland.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using HarmonyLib;

namespace CIAdvanced.Models
{
    public partial class Settings : ObservableObject
    {
        [ObservableProperty]
        private bool _patchSellingAnnouncement = true;

        [ObservableProperty]
        private bool _patchAutoDisableCorruptPlugins = true;

        [ObservableProperty]
        private bool _patchTutorial = true;


        private bool _debugMode = true;
        public bool DebugMode
        {
            get => (bool)Plugin.cisettings?.GetType().GetProperty("IsDebugOptionsEnabled")!.GetValue(Plugin.cisettings)!;
            set
            { 
                if(value == _debugMode)
                    return;
                _debugMode = value;
                Plugin.cisettings?.GetType().GetProperty("IsDebugOptionsEnabled")!.SetValue(Plugin.cisettings, value);
                OnPropertyChanged();
            }
        }
    }
}
