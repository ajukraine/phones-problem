using System.IO;
using System.Text.Json;
using Dahomey.Json;
using Dahomey.Json.Serialization.Conventions;

namespace Phone.App
{
    public class JsonIntegration
    {
        private JsonSerializerOptions _options;

        public JsonIntegration()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            _options.SetupExtensions();

            var registry = _options.GetDiscriminatorConventionRegistry();
            registry.ClearConventions();
            registry.RegisterConvention(new DefaultDiscriminatorConvention<string>(_options, "_type"));
            registry.RegisterType<MobilePhone>();
            registry.RegisterType<RadioPhone>();
        }

        public T Read<T>(string jsonPath)
        {
            var jsonString = File.ReadAllText(jsonPath);
            var jsonModel = JsonSerializer.Deserialize<T>(jsonString, _options);

            return jsonModel;
        }

        public void Write<T>(string jsonPath, T obj)
        {
            var jsonString = JsonSerializer.Serialize(obj, _options);
            File.WriteAllText(jsonPath, jsonString);
        }
    }
}