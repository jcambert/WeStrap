using System.ComponentModel;

namespace WeChart
{
    /// <summary>
    /// @see https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineJoin
    /// </summary>
    public enum LineJoin
    {
        /// <summary>
        /// Rounds off the corners of a shape by filling an additional sector of disc centered at the common endpoint of connected segments. The radius for these rounded corners is equal to the line width.
        /// </summary>
        [Description("round")]
        Bevel,
        /// <summary>
        /// Fills an additional triangular area between the common endpoint of connected segments, and the separate outside rectangular corners of each segment.
        /// </summary>
        [Description("round")]
        Round,
        /// <summary>
        /// Connected segments are joined by extending their outside edges to connect at a single point, with the effect of filling an additional lozenge-shaped area. This setting is affected by the miterLimit property. Default value.
        /// </summary>
        [Description("miter")]
        Miter
    }
}
