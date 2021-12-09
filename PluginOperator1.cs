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
        public static string FindBuildCSFile(string pluginRootPath)
        {
            string pluginName = Path.GetFileName(pluginRootPath);
            string sourcePath = pluginRootPath + "/" + DIR_SOURCE;
            string buildcsFileName = pluginName + BUILD_CS_EXTEND;
            string buildcsFileAllPath = sourcePath + "/" + buildcsFileName;

            //考虑到部分插件的结构还会出现一层插件名
            if (!File.Exists(buildcsFileAllPath))
            {
                buildcsFileAllPath = sourcePath + "/" + pluginName + "/" + buildcsFileName;
            }

            return buildcsFileAllPath;
        }

    }
}
