using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Enums.SettingsWindow;

namespace CIAdvanced.Views;

[SettingsPageInfo("CIAdvancedSettingsPage", "CIAdvanced 设置", SettingsPageCategory.External)]
public partial class SettingsPage : SettingsPageBase
{
    public SettingsPage()
    {
        InitializeComponent();
    }
}