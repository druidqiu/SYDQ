using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace SYDQ.Infrastructure.Configuration
{
    public class AppConfigReader
    {
        private static Hashtable _configItems;

        static AppConfigReader()
        {
            Init();
        }

        private static void Init()
        {
            _configItems = new Hashtable();
            string configPath = GetConfigPath();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configPath);
            XmlNodeList itemNodes = xmlDoc.SelectNodes("/appconfig/item");
            if (itemNodes == null) return;
            foreach (XmlNode node in itemNodes)
            {
                if (node.Attributes == null) continue;
                string key = node.Attributes["key"].Value;
                string value = node.Attributes["value"].Value;
                _configItems.Add(key, value);
            }
        }

        private static string GetConfigPath()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\Configuration\\appconfig.xml");
            return configPath;
        }

        public static string GetConfig(string key)
        {
            return (string)_configItems[key];
        }
    }
}
