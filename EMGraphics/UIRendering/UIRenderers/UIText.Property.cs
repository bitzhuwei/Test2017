﻿using System.ComponentModel;
using System.Drawing;

namespace EMGraphics
{
    public partial class UIText
    {
        private TextModel textModel;

        private IFontTexture fontTexture;

        private string text = string.Empty;
        private UpdatingRecord textRecord = new UpdatingRecord();

        /// <summary>
        ///
        /// </summary>
        [Description("Displaying text.")]
        public string Text
        {
            get { return text; }
            set
            {
                if (value != this.text)
                {
                    this.text = value;
                    this.textRecord.Mark();
                }
            }
        }

		/// <summary>
		/// 
		/// </summary>
		[Description("Text alignment.")]
		public TextAlignment Alignment
		{
			get { return this.textModel.Alignment; }
			set { this.textModel.Alignment = value; }
		}

        private BlendState blendState = new BlendState(BlendingSourceFactor.SourceAlpha, BlendingDestinationFactor.OneMinusSourceAlpha);

        /// <summary>
        ///
        /// </summary>
        [Description("Blend mode.")]
        public BlendState BlendState
        {
            get { return blendState; }
        }

        private UpdatingRecord textColorRecord = new UpdatingRecord();
        private vec3 textColor = new vec3(1, 1, 1);

        /// <summary>
        /// Text's color.
        /// </summary>
        [Description("Text color.")]
        public Color TextColor
        {
            get { return textColor.ToColor(); }
            set
            {
                vec3 color = value.ToVec3();
                if (color != this.textColor)
                {
                    this.textColor = color;
                    this.textColorRecord.Mark();
                }
            }
        }
    }
}