using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeChart
{
    public interface IJsonConfiguration
    {
        void AddConverter(JsonConverter converter);
        JsonSerializerOptions SerializeOptions { get; }
    }
    public class JsonConfiguration : IJsonConfiguration
    {

        public JsonConfiguration()
        {
            //AddConverter(new DatasetConverter<Dataset>());
        }
        public void AddConverter(JsonConverter converter)
        {
            serializeOptions.Converters.Add(converter);
        }

        private readonly JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {

            IgnoreNullValues = true,

            MaxDepth = 32,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,

        };
        public JsonSerializerOptions SerializeOptions => serializeOptions;
    }
}
