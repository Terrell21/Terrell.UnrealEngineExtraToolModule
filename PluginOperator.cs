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
        /// <summary>
        /// 需要操作build.cs文件的插件名          "考虑从外部读取"
        /// </summary>
        public static List<string> OperatePluginNames = new List<string>()
        {
            "YUtility",
            "YConfigTool",
            "TextureAssistant",
            "ReflectAssistant",
            "QRCode",
            "NetworkAssistant",
        };

        
        private const string BUILD_CS_EXTEND = ".Build.cs";
        private const string DIR_SOURCE = "Source";


        //进入Source文件
        //找到pluginName.Build.cs文件
        //打开，读取里面的所有文字
        //匹配查找 bUsedCompiled=true;
        //匹配查找 ...
        //修改
        //写回去，保存

        //插件根目录

        /// <summary>
        /// 查找Build.cs文件的全路径信息
        /// </summary>
        /// <param name="pluginRootPath"></param>
        /// <returns></returns>
        public  static  string FindBuildCSFile(string pluginRootPath)
        {
            string pluginName = Path.GetFileName(pluginRootPath);
            string sourcePath = pluginRootPath + "/"+ DIR_SOURCE;
            string buildcsFileName = pluginName + BUILD_CS_EXTEND;
            string buildcsFileAllPath = sourcePath + "/" + buildcsFileName;

            //考虑到部分插件的结构还会出现一层插件名
            if (!File.Exists(buildcsFileAllPath))
            {
                buildcsFileAllPath = sourcePath + "/" + pluginName + "/" + buildcsFileName;
            }

            return buildcsFileAllPath;
        }

        //考虑改 true or false

        private const string DISABLE_BPRECOMPILE = "bPrecompile = false;";
        private const string ENABLE_BPRECOMPILE = "bPrecompile = true;";
        private const string ENABLE_BUESDPRECOMPILE = "bUsePrecompiled = true;";
        private const string DISABLE_BUESDPRECOMPILE = "bUsePrecompiled = false;";
        //private const string DISABLE_PrecompileForTargets = "//PrecompileForTargets = PrecompileTargetsType.Any;";
        //private const string ENABLE_PrecompileForTargets = "PrecompileForTargets = PrecompileTargetsType.Any;";


        /// <summary>
        /// 设置激活"使用预编译"
        /// </summary>
        /// <param name="buildcsFileAllPath"></param>
        public static void SetbUsedPrecompiled(string buildcsFileAllPath)
        {
            FileStream fs = File.Open(buildcsFileAllPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string content = sr.ReadToEnd();

            //可能需要先查找是否有无

            content = content.Replace(DISABLE_BUESDPRECOMPILE, ENABLE_BUESDPRECOMPILE);       //打开使用预编译。
            content = content.Replace(ENABLE_BPRECOMPILE, DISABLE_BPRECOMPILE);               //关闭预编译设置，
            //content = content.Replace(ENABLE_PrecompileForTargets, DISABLE_PrecompileForTargets); //关闭预编译设置

            fs.Dispose();
            File.WriteAllText(buildcsFileAllPath, content,Encoding.UTF8);


        }


        /// <summary>
        /// 设置取消"使用预编译"，设置可预编译。
        /// </summary>
        /// <param name="buildcsFileAllPath"></param>
        public static void CanelbUsedPrecompiled(string buildcsFileAllPath)
        {
            FileStream fs = File.Open(buildcsFileAllPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string content = sr.ReadToEnd();

            content = content.Replace(ENABLE_BUESDPRECOMPILE, DISABLE_BUESDPRECOMPILE);       //打开使用预编译。
            content = content.Replace(DISABLE_BPRECOMPILE, ENABLE_BPRECOMPILE);               //关闭预编译设置，
            //content = content.Replace(DISABLE_PrecompileForTargets, ENABLE_PrecompileForTargets); //关闭预编译设置

            fs.Dispose();
            File.WriteAllText(buildcsFileAllPath, content, Encoding.UTF8);

        }






        /// <summary>
        /// 激活使用预编译(目标路径为项目根路径)
        /// </summary>
        /// <param name="path"></param>
        public static void ActiveUsedPrecompiled(string path,bool isActived)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Missing Path");
                return;
            }

            string[] plugins = FindUETargetFolder(path);

            foreach (string p in plugins)
            {

                foreach (string targetPlugin in OperatePluginNames)
                {
                    if (Path.GetFileName(p).Equals(targetPlugin))
                    {
                        string p2 = FindBuildCSFile(p);
                        if(isActived)
                            SetbUsedPrecompiled(p2);            //激活使用预编译数据的选项
                        else
                            CanelbUsedPrecompiled(p2);          //禁用使用预编译数据的选项
                    }

                }

            }
        }













        /// <summary>
        /// 查找UE4目标文件夹下的插件路径
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        public static string[] FindUETargetFolder(string projectPath)
        {
            //尝试查找 "Plugins"目录下的所有文件夹， 
            string[] dirAllPaths = Directory.GetDirectories(projectPath + "/" + "Plugins");

            foreach (string path in dirAllPaths)
            {
                Console.WriteLine(path);
            }
            return dirAllPaths;
        }

       
    }


















    /// <summary>
    /// 清除插件缓存
    /// </summary>
    public partial class PluginOperator
    {
        /// <summary>
        /// 清除插件所有缓存
        /// </summary>
        /// <param name="pluginPaths"></param>
        public static void DeletePluginCache(string[] pluginPaths)
        {
            foreach (string pluginPath in pluginPaths)
            {
                DeletePluginCache(pluginPath);
            }
        }

        /// <summary>
        /// 清除插件所有缓存
        /// </summary>
        /// <param name="pluginPath"></param>
        public static void DeletePluginCache(string pluginPath)
        {
            DeleteBinaries(pluginPath);
            DeleteIntermediate(pluginPath);
        }


        /// <summary>
        /// 删除插件Binaries文件
        /// </summary>
        /// <param name="pluginPath"></param>
        /// <returns></returns>
        public static bool DeleteBinaries(string pluginPath)
        {

            if (Directory.Exists(pluginPath))
            {
                string binariesPath = pluginPath + "/" + "Binaries";
                if (Directory.Exists(binariesPath))
                {
                    Directory.Delete(binariesPath, true);
                    return true;

                }
            }
            return false;
        }


        /// <summary>
        /// 删除插件Intermediate文件
        /// </summary>
        /// <param name="pluginPath"></param>
        /// <returns></returns>
        public static bool DeleteIntermediate(string pluginPath)
        {
            if (Directory.Exists(pluginPath))
            {
                string intermediatePath = pluginPath + "/" + "Intermediate";
                if (Directory.Exists(intermediatePath))
                {
                    Directory.Delete(intermediatePath, true);

                }
                return true;
            }
            return false;

        }



    }



}
