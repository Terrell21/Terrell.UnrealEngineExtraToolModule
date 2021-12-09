using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Terrell.UnrealEngineExtraToolModule
{
    //此处数据对应UnrealEngine插件文件 yourPluginName.uplugin;





    
    public class ModulesInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string LoadingPhase { get; set; }
    }


    /// <summary>
    /// 插件信息
    /// </summary>
    public class UPlugin
    {
        public int FileVersion { get; set; }
        /// <summary>
        /// Version number for the plugin.
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Name of the version for this plugin.
        /// </summary>
        public string VersionName { get; set; }
        public string FriendlyName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByURL { get; set; }
        public string DocsURL { get; set; }
        public string MarketplaceURL { get; set; }
        public string SupportURL { get; set; }
        public string EnabledByDefault { get; set; }
        /// <summary>
        /// Can this plugin contain content?
        /// </summary>
        public string CanContainContent { get; set; }
        public string IsBetaVersion { get; set; }
        /// <summary>
        /// Signifies that the plugin was installed on top of the engine
        /// </summary>
        public string Installed { get; set; }
        public List<ModulesInfo> Modules { get; set; }


     

    }




   








    //流程可以考虑是,选择根目录后,项目路径下的plugins有多少个插件?

    //然后以一个勾选框的形式显示出来,


}
