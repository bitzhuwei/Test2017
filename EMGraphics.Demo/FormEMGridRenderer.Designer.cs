namespace EMGraphics.Demo
{
    partial class FormEMGridRenderer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.label1 = new System.Windows.Forms.Label();
			this.txtAmbient = new System.Windows.Forms.TextBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btnReset = new System.Windows.Forms.Button();
			this.txtDirectionalLight = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtRegularColor = new System.Windows.Forms.TextBox();
			this.txtHighlightColor = new System.Windows.Forms.TextBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtRegularLineColor = new System.Windows.Forms.TextBox();
			this.txtHighlightLineColor = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.rdoFlat = new System.Windows.Forms.RadioButton();
			this.rdoSmooth = new System.Windows.Forms.RadioButton();
			this.chkRenderFaces = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.chkRenderLines = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(390, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "环境光(Ambient)：调节场景的整体亮度（范围0 - 255）";
			// 
			// txtAmbient
			// 
			this.txtAmbient.Location = new System.Drawing.Point(12, 27);
			this.txtAmbient.Name = "txtAmbient";
			this.txtAmbient.Size = new System.Drawing.Size(124, 25);
			this.txtAmbient.TabIndex = 1;
			this.txtAmbient.Text = "76, 76, 76";
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.Location = new System.Drawing.Point(567, 432);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "应用";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(648, 432);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(530, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "平行光光源(Directional Light)：照射到模型是的光的颜色（范围0 - 255）";
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnReset.Location = new System.Drawing.Point(12, 432);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(114, 23);
			this.btnReset.TabIndex = 2;
			this.btnReset.Text = "恢复默认值";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// txtDirectionalLight
			// 
			this.txtDirectionalLight.Location = new System.Drawing.Point(12, 73);
			this.txtDirectionalLight.Name = "txtDirectionalLight";
			this.txtDirectionalLight.Size = new System.Drawing.Size(124, 25);
			this.txtDirectionalLight.TabIndex = 1;
			this.txtDirectionalLight.Text = "255, 255, 255";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(453, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "模型底色(Regular Color)：模型本身默认的颜色（范围0 - 255）";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 147);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(514, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "模型高亮色(Highlight Color)：模型被选中高亮时的底色（范围0 - 255）";
			// 
			// txtRegularColor
			// 
			this.txtRegularColor.Location = new System.Drawing.Point(12, 119);
			this.txtRegularColor.Name = "txtRegularColor";
			this.txtRegularColor.Size = new System.Drawing.Size(124, 25);
			this.txtRegularColor.TabIndex = 1;
			this.txtRegularColor.Text = "202, 122, 0";
			// 
			// txtHighlightColor
			// 
			this.txtHighlightColor.Location = new System.Drawing.Point(12, 165);
			this.txtHighlightColor.Name = "txtHighlightColor";
			this.txtHighlightColor.Size = new System.Drawing.Size(124, 25);
			this.txtHighlightColor.TabIndex = 1;
			this.txtHighlightColor.Text = "204, 203, 0";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(9, 193);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(493, 15);
			this.label5.TabIndex = 0;
			this.label5.Text = "线条底色(Regular Line Color)：线条本身默认的颜色（范围0 - 255）";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(9, 239);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(584, 15);
			this.label6.TabIndex = 0;
			this.label6.Text = "线条高亮色(Highlight Line Color)：模型被选中高亮时线条的底色（范围0 - 255）";
			// 
			// txtRegularLineColor
			// 
			this.txtRegularLineColor.Location = new System.Drawing.Point(9, 211);
			this.txtRegularLineColor.Name = "txtRegularLineColor";
			this.txtRegularLineColor.Size = new System.Drawing.Size(124, 25);
			this.txtRegularLineColor.TabIndex = 1;
			this.txtRegularLineColor.Text = "211, 211, 211";
			// 
			// txtHighlightLineColor
			// 
			this.txtHighlightLineColor.Location = new System.Drawing.Point(9, 257);
			this.txtHighlightLineColor.Name = "txtHighlightLineColor";
			this.txtHighlightLineColor.Size = new System.Drawing.Size(124, 25);
			this.txtHighlightLineColor.TabIndex = 1;
			this.txtHighlightLineColor.Text = "100, 100, 100";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(9, 285);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(389, 15);
			this.label7.TabIndex = 0;
			this.label7.Text = "渲染方式(Render Mode)：扁平(flat) 或 平滑(smooth)";
			// 
			// rdoFlat
			// 
			this.rdoFlat.AutoSize = true;
			this.rdoFlat.Checked = true;
			this.rdoFlat.Location = new System.Drawing.Point(15, 303);
			this.rdoFlat.Name = "rdoFlat";
			this.rdoFlat.Size = new System.Drawing.Size(106, 19);
			this.rdoFlat.TabIndex = 4;
			this.rdoFlat.TabStop = true;
			this.rdoFlat.Text = "扁平(flat)";
			this.rdoFlat.UseVisualStyleBackColor = true;
			// 
			// rdoSmooth
			// 
			this.rdoSmooth.AutoSize = true;
			this.rdoSmooth.Location = new System.Drawing.Point(145, 303);
			this.rdoSmooth.Name = "rdoSmooth";
			this.rdoSmooth.Size = new System.Drawing.Size(122, 19);
			this.rdoSmooth.TabIndex = 4;
			this.rdoSmooth.Text = "顺滑(smooth)";
			this.rdoSmooth.UseVisualStyleBackColor = true;
			// 
			// chkRenderFaces
			// 
			this.chkRenderFaces.AutoSize = true;
			this.chkRenderFaces.Checked = true;
			this.chkRenderFaces.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRenderFaces.Location = new System.Drawing.Point(9, 343);
			this.chkRenderFaces.Name = "chkRenderFaces";
			this.chkRenderFaces.Size = new System.Drawing.Size(130, 19);
			this.chkRenderFaces.TabIndex = 5;
			this.chkRenderFaces.Text = "渲染面(Faces)";
			this.chkRenderFaces.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 325);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(120, 15);
			this.label8.TabIndex = 0;
			this.label8.Text = "是否渲染面\\线条";
			// 
			// chkRenderLines
			// 
			this.chkRenderLines.AutoSize = true;
			this.chkRenderLines.Checked = true;
			this.chkRenderLines.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRenderLines.Location = new System.Drawing.Point(166, 343);
			this.chkRenderLines.Name = "chkRenderLines";
			this.chkRenderLines.Size = new System.Drawing.Size(145, 19);
			this.chkRenderLines.TabIndex = 5;
			this.chkRenderLines.Text = "渲染线条(Lines)";
			this.chkRenderLines.UseVisualStyleBackColor = true;
			// 
			// FormEMGridRenderer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(735, 467);
			this.Controls.Add(this.chkRenderLines);
			this.Controls.Add(this.chkRenderFaces);
			this.Controls.Add(this.rdoSmooth);
			this.Controls.Add(this.rdoFlat);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.txtHighlightLineColor);
			this.Controls.Add(this.txtHighlightColor);
			this.Controls.Add(this.txtDirectionalLight);
			this.Controls.Add(this.txtRegularLineColor);
			this.Controls.Add(this.txtRegularColor);
			this.Controls.Add(this.txtAmbient);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Name = "FormEMGridRenderer";
			this.Text = "FormEMGridRenderer";
			this.Load += new System.EventHandler(this.FormEMGridRenderer_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAmbient;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.TextBox txtDirectionalLight;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtRegularColor;
		private System.Windows.Forms.TextBox txtHighlightColor;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtRegularLineColor;
		private System.Windows.Forms.TextBox txtHighlightLineColor;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RadioButton rdoFlat;
		private System.Windows.Forms.RadioButton rdoSmooth;
		private System.Windows.Forms.CheckBox chkRenderFaces;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox chkRenderLines;
	}
}