using CommunityToolkit.Mvvm.ComponentModel;

namespace CIAdvanced.Models
{
    public partial class Settings : ObservableObject
    {
        [ObservableProperty]
        private bool _patchSellingAnnouncement = true;
    }
}
