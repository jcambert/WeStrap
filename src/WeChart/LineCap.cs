using System.ComponentModel;

namespace WeChart
{
    /// <summary>
    /// @see https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/lineCap
    /// </summary>
    public enum LineCap
    {
        //"butt" || "round" || "square"
        /// <summary>
        /// The ends of lines are squared off at the endpoints. Default value
        /// </summary>
        [Description("butt")]
        Butt,
        /// <summary>
        /// The ends of lines are rounded.
        /// </summary>
        [Description("round")]
        Round,
        /// <summary>
        /// The ends of lines are squared off by adding a box with an equal width and half the height of the line's thickness.
        /// </summary>
        [Description("square")]
        Square

    }
}
