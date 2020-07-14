using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace WeCommon
{
    public class JsonLocationSerializer : JsonConverter<Location>
    {
        public override Location ReadJson(JsonReader reader, Type objectType, [AllowNull] Location existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            
            
            double lon = jo["longitude"].Value<double>();
            var lat = jo["latitude"].Value<double>();
            return new Location(lon,lat);
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] Location value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("longitude");
            writer.WriteValue(value.Longitude);
            writer.WritePropertyName("latitude");
            writer.WriteValue(value.Latitude);
            writer.WriteEndObject();
        }
    }
}
