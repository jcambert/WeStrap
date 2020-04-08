using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using WeReactive;

namespace WeReactiveApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var state = new State();
            state.Count.OnValueChanged.Subscribe(v => Console.WriteLine($"Counter has changed to {v}"));
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,

                MaxDepth = 32,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            new int[] { 1, 2, 3, 4 }.ToObservable().Delay(TimeSpan.FromSeconds(1)).Subscribe(i =>
           {
               state.Count.Value = i;
               state.Name.Value = $"My name is {i}";
               Console.WriteLine(JsonSerializer.Serialize(state, options));
           });
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }

    class State
    {
        [JsonConverter(typeof(ReactiveJsonConverter<int>))]
        public ReactiveProperty<int> Count { get; set; } = new ReactiveProperty<int>();

        [JsonConverter(typeof(ReactiveJsonConverter<double>))]
        public ReactiveProperty<double> CountDouble { get; set; } = new ReactiveProperty<double>();

        [JsonConverter(typeof(ReactiveJsonConverter<string>))]
        public ReactiveProperty<string> Name { get; set; } = new ReactiveProperty<string>();

        public List<string> ListeDeToto { get; set; } = new List<string>() { "toto", "titi", "tata" };

    }
}
