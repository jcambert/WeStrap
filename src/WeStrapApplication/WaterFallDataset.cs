using System;
using WeChart;

namespace WeStrapApplication
{
    [AutoMapper.AutoMap(typeof(Dataset))]
    public class WaterFallDataset : Dataset
    {
        public WaterFallDataset() : base()
        {
            Console.WriteLine(" WaterFall Dataset");
        }
        public string Stack { get; set; } = "stack 1";
        public int StackIndex { get; set; } = 1;
    }
}
