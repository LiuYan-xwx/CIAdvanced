using CIAdvanced.Models;
using CIAdvanced.ViewModels;
using CIAdvanced.Views;
using ClassIsland.Core;
using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Controls;
using ClassIsland.Core.Extensions.Registry;
using ClassIsland.Shared;
using ClassIsland.Shared.Helpers;
using HarmonyLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CIAdvanced
{
    [PluginEntrance]
    public class Plugin : PluginBase
    {
        public static Settings Settings { get; set; } = new Settings();

        public override void Initialize(HostBuilderContext context, IServiceCollection services)
        {
            Settings = ConfigureFileHelper.LoadConfig<Settings>(Path.Combine(PluginConfigFolder, "Settings.json"));  // 加载配置文件
            Settings.PropertyChanged += (sender, args) =>
            {
                ConfigureFileHelper.SaveConfig(Path.Combine(PluginConfigFolder, "Settings.json"), Settings);  // 保存配置文件
            };
            services.AddSettingsPage<SettingsPage>();
            services.AddTransient<SettingsPageViewModel>();

            AppBase.Current.AppStarted += (_,_) =>
            {
                var harmony = new Harmony("ciadvanced");
                harmony.PatchAll();
            };
        }
    }
}