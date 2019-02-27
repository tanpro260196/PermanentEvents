using Newtonsoft.Json;
using System.IO;

namespace PermanentEvents
{
    public class ConfigFile
    {
        #region ConfigVars

        public bool sandstorm = true;
        public bool rain = true;
        public bool bloodmoon = false;
        public bool eclipse = false;

        #endregion ConfigVars

        public static ConfigFile Read(string path)
        {
            if (!File.Exists(path))
            {
                ConfigFile config = new ConfigFile();

                File.WriteAllText(path, JsonConvert.SerializeObject(config, Formatting.Indented));
                return config;
            }
            return JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText(path));
        }
    }
}