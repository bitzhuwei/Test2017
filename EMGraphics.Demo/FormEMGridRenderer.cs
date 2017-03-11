using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMGraphics.Demo
{
	public partial class FormEMGridRenderer : Form
	{
		private Scene scene;
		private string ambient;
		private string directionalLight;
		private string regularColor;
		private string highlightColor;
		private string regularLineColor;
		private string highlightLineColor;
		private bool flat;
		private bool renderFaces;
		private bool renderLines;

		public FormEMGridRenderer(Scene scene)
		{
			InitializeComponent();

			this.scene = scene;
		}

		private void FormEMGridRenderer_Load(object sender, EventArgs e)
		{
			this.ambient = this.txtAmbient.Text;
			this.directionalLight = this.txtDirectionalLight.Text;
			this.regularColor = this.txtRegularColor.Text;
			this.highlightColor = this.txtHighlightColor.Text;
			this.regularLineColor = this.txtRegularLineColor.Text;
			this.highlightLineColor = this.txtHighlightLineColor.Text;
			this.flat = this.rdoFlat.Checked;
			this.renderFaces = this.chkRenderFaces.Checked;
			this.renderLines = this.chkRenderLines.Checked;
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			this.txtAmbient.Text = this.ambient;
			this.txtDirectionalLight.Text = this.directionalLight;
			this.txtRegularColor.Text = this.regularColor;
			this.txtHighlightColor.Text = this.highlightColor;
			this.txtRegularLineColor.Text = this.regularLineColor;
			this.txtHighlightLineColor.Text = this.highlightLineColor;
			this.rdoFlat.Checked = this.flat;
			this.rdoSmooth.Checked = !this.flat;
			this.chkRenderFaces.Checked = this.renderFaces;
			this.chkRenderLines.Checked = this.renderLines;
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			if (this.scene == null)
			{
				MessageBox.Show("没有场景可供设置！");
				return;
			}
			Color ambient, directinalLight, regular, highlight,
				regularLine, highlightLine;
			try
			{
				ambient = Str2Color(this.txtAmbient.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("ambient颜色设置错误，无法解析！");
				return;
			}
			try
			{
				directinalLight = Str2Color(this.txtDirectionalLight.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("directinalLight颜色设置错误，无法解析！");
				return;
			}
			try
			{
				regular = Str2Color(this.txtRegularColor.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("regular颜色设置错误，无法解析！");
				return;
			}
			try
			{
				highlight = Str2Color(this.txtHighlightColor.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("highlight颜色设置错误，无法解析！");
				return;
			}
			try
			{
				regularLine = Str2Color(this.txtRegularLineColor.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("regularLine颜色设置错误，无法解析！");
				return;
			}
			try
			{
				highlightLine = Str2Color(this.txtHighlightLineColor.Text);
			}
			catch (Exception)
			{
				MessageBox.Show("highlightLine颜色设置错误，无法解析！");
				return;
			}

			var properties = new EMGridProperties(ambient, directinalLight,
				regular, highlight, regularLine, highlightLine,
				this.rdoFlat.Checked,
				this.chkRenderFaces.Checked, this.chkRenderLines.Checked,
				this.chkRenderNormal.Checked);

			UpdateProperties(this.scene.RootObject, properties);
		}

		private void UpdateProperties(SceneObject sceneObject, EMGridProperties properties)
		{
			{
				var renderer = sceneObject.Renderer as EMGridRenderer;
				if (renderer != null)
				{
					renderer.AmbientLightColor = properties.ambientColor;
					renderer.DirectionalLightColor = properties.directionalLightColor;
					renderer.RegularColor = properties.regularColor;
					renderer.HighlightColor = properties.highlightColor;
					renderer.RegularLineColor = properties.regularLineColor;
					renderer.HighlightLineColor = properties.highlightLineColor;
					renderer.FlatMode = properties.flatMode;
					renderer.RenderFaces = properties.renderFaces;
					renderer.RenderLines = properties.renderLines;
				}
			}
			{
				var renderer = sceneObject.Renderer as NormalLineRenderer;
				if (renderer != null)
				{
					sceneObject.RenderingEnabled = properties.rendernormal;
				}
			}

			foreach (SceneObject item in sceneObject.Children)
			{
				UpdateProperties(item, properties);
			}
		}

		private static readonly char[] separator = new char[] { ',', ' ' };
		private Color Str2Color(string text)
		{
			string[] parts = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			int r = int.Parse(parts[0]);
			int g = int.Parse(parts[1]);
			int b = int.Parse(parts[2]);

			return Color.FromArgb(255, r, g, b);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}

	class EMGridProperties
	{
		public Color ambientColor;
		public Color directionalLightColor;
		public Color regularColor;
		public Color highlightColor;
		public Color regularLineColor;
		public Color highlightLineColor;
		public bool flatMode;
		public bool renderFaces;
		public bool renderLines;
		public bool rendernormal;

		public EMGridProperties(Color ambient, Color directionalLight,
			Color regular, Color highlight,
			Color regularLine, Color highlightLine,
			bool flatMode, bool renderFaces, bool renderLines,
			bool renderNormal)
		{
			this.ambientColor = ambient;
			this.directionalLightColor = directionalLight;
			this.regularColor = regular;
			this.highlightColor = highlight;
			this.regularLineColor = regularLine;
			this.highlightLineColor = highlightLine;
			this.flatMode = flatMode;
			this.renderFaces = renderFaces;
			this.renderLines = renderLines;
			this.rendernormal = renderNormal;
		}
	}
}
