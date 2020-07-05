using Microsoft.AspNetCore.Components;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeStrap.Components.Base
{
    public abstract class WeDatePickerBase:WeTag
    {


        [Parameter] public Date Min{ get; set; }
        [Parameter] public Date Max { get; set; }
        [Parameter] public int Firstday { get; set; }
    }
}
