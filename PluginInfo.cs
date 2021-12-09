using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrell.UnrealEngineExtraToolModule
{

    //此处数据对应UnrealEngine插件文件 yourPluginName.build.cs


    /// <summary>
    /// 插件信息 build.cs
    /// </summary>
    public class PluginInfo
    {
        public string name;


        public PluginInfo(string name) { this.name = name; }
    }


















    /// <summary>
    /// 插件预编译信息 build.cs
    /// </summary>
    public class PluginPreCompliedInfo : PluginInfo
    {
        //默认全字匹配,若需要改动,请继承

        //考虑到要修改字符串,这里需要注意字符串匹配的情况
        //1)语句被注释
        //2)语句空格不匹配

        /// <summary>
        /// 启用预编译
        /// </summary>
        public bool bPrecompile;

        /// <summary>
        /// 使用预编译数据
        /// </summary>
        public bool bUsePrecompiled;

        public PluginPreCompliedInfo(string name) : base(name) {}

        public PluginPreCompliedInfo(string name ,string pluginPath):base(name)
        {
            bPrecompile= GetPrecompile(pluginPath);
            bUsePrecompiled = GetUsePrecompiled(pluginPath);
        }


        private const string DISABLE_BPRECOMPILE = "bPrecompile = false;";
        private const string ENABLE_BPRECOMPILE = "bPrecompile = true;";
        private const string ENABLE_BUESDPRECOMPILE = "bUsePrecompiled = true;";
        private const string DISABLE_BUESDPRECOMPILE = "bUsePrecompiled = false;";

        /// <summary>
        /// 从文件获取bprecompile的指
        /// </summary>
        /// <param name="pluginPath"></param>
        /// <returns></returns>
        public virtual bool GetPrecompile(string pluginPath)
        {
            string buildcsFileAllPath = PluginOperator.FindBuildCSFile(pluginPath);
            FileStream fs = File.Open(buildcsFileAllPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string content = sr.ReadToEnd();

            bool bprecompile =  content.Contains(ENABLE_BPRECOMPILE);

            fs.Dispose();
            return bprecompile;
        }

        /// <summary>
        /// 从文件获取buseprecompiled的值
        /// </summary>
        /// <param name="pluginPath"></param>
        /// <returns></returns>
        public virtual bool GetUsePrecompiled(string pluginPath)
        {
            string buildcsFileAllPath = PluginOperator.FindBuildCSFile(pluginPath);
            FileStream fs = File.Open(buildcsFileAllPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string content = sr.ReadToEnd();

            bool bUsePrecompiled = content.Contains(ENABLE_BUESDPRECOMPILE);

            fs.Dispose();

            return bUsePrecompiled;
        }

        public virtual void SetUsePrecompiled(string pluginPath)
        {
            string buildcsFileAllPath = PluginOperator.FindBuildCSFile(pluginPath);
            FileStream fs = File.Open(buildcsFileAllPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string content = sr.ReadToEnd();

            if (bUsePrecompiled)
                content = content.Replace(DISABLE_BUESDPRECOMPILE, ENABLE_BUESDPRECOMPILE);       //打开使用预编译。
            else
                content = content.Replace(ENABLE_BUESDPRECOMPILE, DISABLE_BUESDPRECOMPILE);       //关闭使用预编译。

            fs.Dispose();
            File.WriteAllText(buildcsFileAllPath, content, Encoding.UTF8);
        }

        public virtual void SetPrecompile(string pluginPath)
        {
            string buildcsFileAllPath = PluginOperator.FindBuildCSFile(pluginPath);
            FileStream fs = File.Open(buildcsFileAllPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string content = sr.ReadToEnd();

            

            if (bPrecompile)
                content = content.Replace(DISABLE_BPRECOMPILE ,ENABLE_BPRECOMPILE);               //关闭预编译设置，
            else
                content = content.Replace(ENABLE_BPRECOMPILE, DISABLE_BPRECOMPILE);               //关闭预编译设置，

            fs.Dispose();
            File.WriteAllText(buildcsFileAllPath, content, Encoding.UTF8);
        }



        public virtual void EnableUsedPrecompile(string pluginPath, bool enable)
        {
            if (enable)
            {
                bUsePrecompiled = true;
                bPrecompile = false;
            }
            else
            {
                bUsePrecompiled = false;
                bPrecompile = true;
            }
            SetUsePrecompiled(pluginPath);
            SetPrecompile(pluginPath);
        }


        //一行行读
        //判断该行是否被注释
        //  注释有两种情况
        //          前面有//   这时候需要考虑是否包含其他代码行
        //          或者 /*   这时候需要找回*/


        //全部读完
        //          这个只能进行全字匹配修改了
        //


        //操作:
        //1）查找是否有匹配项
        //2）


    }


}
