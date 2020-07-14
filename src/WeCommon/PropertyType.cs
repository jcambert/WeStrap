using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace WeCommon
{

    public class PropertyType
    {
        [JsonConstructor]
        public PropertyType()
        {

        }
        public Guid Id { get; set; }

        [BsonSerializer(typeof(DynamicSerializer<dynamic,string>))]
        public dynamic Description { get; set; } = new Property<string>();

        [BsonSerializer(typeof(DynamicSerializer<dynamic, string>))]
        public dynamic Location { get; set; } = new Property<string>();

        [BsonSerializer(typeof(DynamicSerializer<dynamic, int>))]
        public dynamic Prices { get; set; } = new Property<int>();

        [BsonSerializer(typeof(DynamicSerializer<dynamic, double>))]
        public dynamic Taxes { get; set; } = new Property<double>();
        public string Test { get; set; }
        public DateTime Date { get; set; }

        
        public Location GPS { get; set; }
       
    }
}
