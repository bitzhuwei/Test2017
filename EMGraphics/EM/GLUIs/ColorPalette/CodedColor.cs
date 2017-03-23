using System.Drawing;

namespace EMGraphics
{
    /// <summary>
    /// 用以描述色标的颜色、位置和数值。
    /// </summary>
    public class CodedColor
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="color"></param>
		/// <param name="coord">range in [0.0, 1.0]</param>
		/// <param name="propertyValue"></param>
        public CodedColor(Color color, float coord, float propertyValue)
        {
            this.DisplayColor = color; this.Coord = coord; this.PropertyValue = propertyValue;
        }

        /// <summary>
        /// Display color.
        /// </summary>
        public Color DisplayColor { get; set; }

        private float coord;

        /// <summary>
        /// position in coded color bar. Ranges from 0.0f to 1.0f.
        /// </summary>
        public float Coord
        {
            get { return coord; }
            set
            {
                if (value < 0) { coord = 0; }
                else if (value > 1.0f) { coord = 1.0f; }
                else { coord = value; }
            }
        }

        /// <summary>
        /// Display value.
        /// </summary>
        public float PropertyValue { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, Coord: {1}, Value: {2}", DisplayColor, Coord, PropertyValue);
        }
    }
}