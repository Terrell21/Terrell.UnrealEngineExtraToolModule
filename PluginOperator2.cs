using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrell.UnrealEngineExtraToolModule
{

    public partial class PluginOperator
    {
        private const string BUILD_CS_EXTEND = ".Build.cs";

        public static string[] GetPluginDirs(string uprojectRootPath)
        {
            //获取根目录 考虑情况：1、传入根目录 2、传入uproject路径
            string directoryPath = Path.GetDirectoryName(uprojectRootPath);
            //查找是否有该根目录
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("项目根路径无效!");
            }
            //查找Plugins路径
            string pluginsPath = directoryPath + "\\Plugins";
            if (!Directory.Exists(pluginsPath))
            {
                Console.WriteLine("查找不到有效的Plugins目录");
            }

            //这里获取的是绝对路径，且最后不带"\"
            string[] pluginDir = Directory.GetDirectories(pluginsPath);
            return pluginDir;
        }


        /// <summary>
        /// 获取项目的所有插件,并生成对象信息
        /// </summary>
        /// <param name="uprojectRootPath"></param>
        /// <returns></returns>
        public static List<PluginInfo> GetPlugins(string uprojectRootPath)
        {
            List<PluginInfo> pluginInfos = new List<PluginInfo>();
            //获取插件目录
            string[] pluginDir = GetPluginDirs(uprojectRootPath);
            foreach (string pDir in pluginDir)
            {
                //注意:有些插件会套多一层文件夹,可能需要考虑该情况
                Console.WriteLine(pDir);
                //获取uplugin文件,
                string[] upluginPaths= Directory.GetFiles(pDir, "*.uplugin");
                if (upluginPaths.Length > 0)
                {
                    //获取文件名
                    string upluginName = Path.GetFileNameWithoutExtension(upluginPaths[0]);
                    //存储信息
                    PluginInfo pluginInfo = new PluginInfo(upluginName);
                    pluginInfos.Add(pluginInfo);
                }
            }
            //string pluginName = Path.GetFileName(pluginRootPath);
            //string sourcePath = pluginRootPath + "/" + DIR_SOURCE;
            //string buildcsFileName = pluginName + BUILD_CS_EXTEND;
            //string buildcsFileAllPath = sourcePath + "/" + buildcsFileName;
            return pluginInfos;
        }




        public static List<PluginInfo> GetPluginsPreComplied(string uprojectRootPath)
        {
            List<PluginInfo> pluginInfos = new List<PluginInfo>();
            //获取插件目录
            string[] pluginDir = GetPluginDirs(uprojectRootPath);
            foreach (string pDir in pluginDir)
            {
                //注意:有些插件会套多一层文件夹,可能需要考虑该情况
                Console.WriteLine(pDir);
                //获取uplugin文件,
                string[] upluginPaths = Directory.GetFiles(pDir, "*.uplugin");
                if (upluginPaths.Length > 0)
                {
                    //获取文件名
                    string upluginName = Path.GetFileNameWithoutExtension(upluginPaths[0]);
                    //存储信息
                    PluginPreCompliedInfo pluginInfo = new PluginPreCompliedInfo(upluginName, pDir);
                    pluginInfos.Add(pluginInfo);
                }
            }
            return pluginInfos;
        }





    }





   



}
