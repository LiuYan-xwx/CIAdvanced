using CIAdvanced.ViewModels;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Enums.SettingsWindow;

namespace CIAdvanced.Views;

[SettingsPageInfo("CIAdvancedSettingsPage", "CIAdvanced 设置", SettingsPageCategory.External)]
public partial class SettingsPage : SettingsPageBase
{
    private readonly SettingsPageViewModel ViewModel;

    public SettingsPage(SettingsPageViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        DataContext = ViewModel;
    }
}