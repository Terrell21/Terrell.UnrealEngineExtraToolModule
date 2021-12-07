using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrell.UnrealEngineExtraToolModule
{
    public class ConfigOperator
    {
        //可以考虑从外部某段文字 或者某个文件读取， 读取之后保存到本地一份。

        //考虑确认格式后,从外部读取
        public static List<string> ProjectInfo = new List<string>()
        {
            "照明协会1.0",
            "慧城集合版1.0",
            "彩域慧城演示版",
            "节日大道演示版",
            "顺通大道演示版",
            "南山智园演示版(未实装)",
            "彩域慧城+群华沙盘",
            "节日大道+群华沙盘",
            "顺通大道+群华沙盘",
            "南山智园+群华沙盘",
        };

        public static List<string> LinkInfos = new List<string>()
        {
            TEST_CLIENT,
            PROD_CLIENT,
            LIGHT_CLIENT,

        };

        public static Dictionary<string, string> LinkDict = new Dictionary<string, string>()
        {
            {"测试环境",TEST_CLIENT },
            {"生产环境", PROD_CLIENT},
            {"照明协会(唯一)",LIGHT_CLIENT },

        };


        public const string TEST_CLIENT = "wss://emulate-city-v3-ws.seekway.cn/test-client/";
        public const string PROD_CLIENT = "wss://emulate-city-v3-ws.seekway.cn/prod-client/";
        public const string LIGHT_CLIENT = "wss://emulate-city-v3-ws.seekway.cn/cies2021-client/";


    }
}
