using System;
using System.Collections.Generic;
using System.Text;

namespace WeCommon
{


    public class IconAttribute:Attribute
    {
        public IconAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}
