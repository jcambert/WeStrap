using System;

namespace WeChart
{
    public struct LineStyle
    {
        private string _fill;
        /// <summary>
        /// Border Style of Line
        /// </summary>
        public BorderStyle? BorderStyle { get; set; }
        /// <summary>
        /// The line fill color.
        /// </summary>
        public RGBA? Background { get; set; }
        /// <summary>
        /// Cap style of the line
        /// </summary>
        public LineCap? CapStyle { get; set; }
        /// <summary>
        /// How to fill the area under the line
        /// </summary>
        public string Fill
        {
            get => _fill;
            set => _fill = ParseFill(value);
        }
        /// <summary>
        /// How to clip relative to chartArea. Positive value allows overflow, negative value clips that many pixels inside chartArea. 0 = clip at chartArea. Clipping can also be configured per side: clip: {left: 5, top: false, right: -2, bottom: 0}
        /// </summary>
        public Clip? Clip { get; set; }
        /// <summary>
        /// Bezier curve tension of the line. Set to 0 to draw straightlines. This option is ignored if monotone cubic interpolation is used.
        /// </summary>
        public double? Tension { get; set; }
        /// <summary>
        /// If false, the line is not drawn for this dataset.
        /// </summary>
        public bool? Show { get; set; }
        /// <summary>
        /// If true, lines will be drawn between points with no or null data. If false, points with NaN data will create a break in the line.
        /// </summary>
        public bool? SpanGaps { get; set; }

        #region Private function
        private string ParseFill(string @value)
        {

            if (Int32.TryParse(@value, out int result))
                return result.ToString();
            return @value switch
            {
                "start" => @value,
                "end" => @value,
                "origin" => @value,
                "false" => @value,
                _ => ""
            };



        }
        #endregion

    }
}
