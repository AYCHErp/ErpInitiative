using System.IO;
using System.Text;
using Frapid.Configuration;
using Newtonsoft.Json;

namespace InitiativeNepal
{
    public sealed class ConfigurationManager
    {
        private const string ConfigFile = "/Tenants/{tenant}/Configs/SMS/InitiativeNepal.json";

        public static void Set(string tenant, Config config)
        {
            if (config == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(config.FromName))
            {
                config.FromName = config.SenderId;
            }

            string path = ConfigFile.Replace("{tenant}", tenant);
            path = PathMapper.MapPath(path);

            if (!File.Exists(path))
            {
                string directory = new FileInfo(path).Directory?.FullName;
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }

            string contents = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(path, contents, new UTF8Encoding(false));
        }

        public static Config Get(string tenant)
        {
            string path = ConfigFile.Replace("{tenant}", tenant);
            path = PathMapper.MapPath(path);

            if (!File.Exists(path))
            {
                return new Config();
            }

            string contents = File.ReadAllText(path, Encoding.UTF8);

            return string.IsNullOrWhiteSpace(contents) ? new Config() : JsonConvert.DeserializeObject<Config>(contents);
        }
    }
}