using CommunityToolkit.Mvvm.ComponentModel;

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
    }
}
