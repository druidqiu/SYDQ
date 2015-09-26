using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SYDQ.Infrastructure.Configuration
{
    public class AppConfigReader
    {
        private static Hashtable configItems = null;

        static AppConfigReader()
        {
            Init();
        }

        private static void Init()
        {
            configItems = new Hashtable();
            string configPath = GetConfigPath();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configPath);
            XmlNodeList itemNodes = xmlDoc.SelectNodes("/appconfig/item");
            foreach (XmlNode node in itemNodes)
            {
                string key = node.Attributes["key"].Value;
                string value = node.Attributes["value"].Value;
                configItems.Add(key, value);
            }
        }

        private static string GetConfigPath()
        {
            string configPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin\\Configuration\\appconfig.xml");
            return configPath;
        }

        public static string GetConfig(string key)
        {
            return (string)configItems[key];
        }
    }
}
