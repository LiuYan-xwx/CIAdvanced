namespace CIAdvanced.Models;

public record SettingsPageOption(string Id, string Name)
{
    public string DisplayName => string.IsNullOrEmpty(Id) ? Name : $"{Name}（{Id}）";
}
