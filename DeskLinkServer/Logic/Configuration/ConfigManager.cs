using System;
using System.IO;
using Newtonsoft.Json;

namespace DeskLinkServer.Logic.Configuration
{
    public static class ConfigManager
    {
        private static readonly string configFileName = "config.json";

        public static void SaveConfig(Config config)
        {
            try
            {
                File.WriteAllText(configFileName, JsonConvert.SerializeObject(config, typeof(Config), new JsonSerializerSettings()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static Config LoadConfig()
        {
            try
            {
                if (!File.Exists(configFileName))
                    return new Config();
                string json = File.ReadAllText(configFileName);
                return JsonConvert.DeserializeObject<Config>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new Config();
        }
    }
}
