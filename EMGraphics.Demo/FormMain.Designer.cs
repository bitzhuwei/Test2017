﻿namespace EMGraphics.Demo
{
    partial class FormMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.新建NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.另存为AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.打印PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.打印预览VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.编辑EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.撤消UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.重复RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.剪切TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.复制CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.粘贴PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.全选AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.自定义CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.选项OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.内容CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.索引IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.搜索SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.openNASFile = new System.Windows.Forms.OpenFileDialog();
			this.glCanvas1 = new EMGraphics.GLCanvas();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnPickTriangle = new System.Windows.Forms.ToolStripButton();
			this.btnPickMesh = new System.Windows.Forms.ToolStripButton();
			this.btnPickModel = new System.Windows.Forms.ToolStripButton();
			this.btnEMGridRendererProperties = new System.Windows.Forms.ToolStripButton();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.reverseSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reverseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnCloud = new System.Windows.Forms.ToolStripButton();
			this.openCoudFile = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.glCanvas1)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.编辑EToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.帮助HToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(797, 28);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// 文件FToolStripMenuItem
			// 
			this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建NToolStripMenuItem,
            this.打开OToolStripMenuItem,
            this.toolStripSeparator,
            this.保存SToolStripMenuItem,
            this.另存为AToolStripMenuItem,
            this.toolStripSeparator1,
            this.打印PToolStripMenuItem,
            this.打印预览VToolStripMenuItem,
            this.toolStripSeparator2,
            this.退出XToolStripMenuItem});
			this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
			this.文件FToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
			this.文件FToolStripMenuItem.Text = "文件(&F)";
			// 
			// 新建NToolStripMenuItem
			// 
			this.新建NToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新建NToolStripMenuItem.Image")));
			this.新建NToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.新建NToolStripMenuItem.Name = "新建NToolStripMenuItem";
			this.新建NToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.新建NToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
			this.新建NToolStripMenuItem.Text = "新建(&N)";
			// 
			// 打开OToolStripMenuItem
			// 
			this.打开OToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripMenuItem.Image")));
			this.打开OToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
			this.打开OToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.打开OToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
			this.打开OToolStripMenuItem.Text = "打开(&O)";
			this.打开OToolStripMenuItem.Click += new System.EventHandler(this.打开OToolStripMenuItem_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(191, 6);
			// 
			// 保存SToolStripMenuItem
			// 
			this.保存SToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripMenuItem.Image")));
			this.保存SToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.保存SToolStripMenuItem.Name = "保存SToolStripMenuItem";
			this.保存SToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.保存SToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
			this.保存SToolStripMenuItem.Text = "保存(&S)";
			// 
			// 另存为AToolStripMenuItem
			// 
			this.另存为AToolStripMenuItem.Name = "另存为AToolStripMenuItem";
			this.另存为AToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
			this.另存为AToolStripMenuItem.Text = "另存为(&A)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
			// 
			// 打印PToolStripMenuItem
			// 
			this.打印PToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("打印PToolStripMenuItem.Image")));
			this.打印PToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.打印PToolStripMenuItem.Name = "打印PToolStripMenuItem";
			this.打印PToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.打印PToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
			this.打印PToolStripMenuItem.Text = "打印(&P)";
			// 
			// 打印预览VToolStripMenuItem
			// 
			this.打印预览VToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("打印预览VToolStripMenuItem.Image")));
			this.打印预览VToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.打印预览VToolStripMenuItem.Name = "打印预览VToolStripMenuItem";
			this.打印预览VToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
			this.打印预览VToolStripMenuItem.Text = "打印预览(&V)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
			// 
			// 退出XToolStripMenuItem
			// 
			this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
			this.退出XToolStripMenuItem.Size = new System.Drawing.Size(194, 26);
			this.退出XToolStripMenuItem.Text = "退出(&X)";
			// 
			// 编辑EToolStripMenuItem
			// 
			this.编辑EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.撤消UToolStripMenuItem,
            this.重复RToolStripMenuItem,
            this.toolStripSeparator3,
            this.剪切TToolStripMenuItem,
            this.复制CToolStripMenuItem,
            this.粘贴PToolStripMenuItem,
            this.toolStripSeparator4,
            this.全选AToolStripMenuItem});
			this.编辑EToolStripMenuItem.Name = "编辑EToolStripMenuItem";
			this.编辑EToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
			this.编辑EToolStripMenuItem.Text = "编辑(&E)";
			// 
			// 撤消UToolStripMenuItem
			// 
			this.撤消UToolStripMenuItem.Name = "撤消UToolStripMenuItem";
			this.撤消UToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.撤消UToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
			this.撤消UToolStripMenuItem.Text = "撤消(&U)";
			// 
			// 重复RToolStripMenuItem
			// 
			this.重复RToolStripMenuItem.Name = "重复RToolStripMenuItem";
			this.重复RToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.重复RToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
			this.重复RToolStripMenuItem.Text = "重复(&R)";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(187, 6);
			// 
			// 剪切TToolStripMenuItem
			// 
			this.剪切TToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("剪切TToolStripMenuItem.Image")));
			this.剪切TToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.剪切TToolStripMenuItem.Name = "剪切TToolStripMenuItem";
			this.剪切TToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.剪切TToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
			this.剪切TToolStripMenuItem.Text = "剪切(&T)";
			// 
			// 复制CToolStripMenuItem
			// 
			this.复制CToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("复制CToolStripMenuItem.Image")));
			this.复制CToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.复制CToolStripMenuItem.Name = "复制CToolStripMenuItem";
			this.复制CToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.复制CToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
			this.复制CToolStripMenuItem.Text = "复制(&C)";
			// 
			// 粘贴PToolStripMenuItem
			// 
			this.粘贴PToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("粘贴PToolStripMenuItem.Image")));
			this.粘贴PToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.粘贴PToolStripMenuItem.Name = "粘贴PToolStripMenuItem";
			this.粘贴PToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.粘贴PToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
			this.粘贴PToolStripMenuItem.Text = "粘贴(&P)";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(187, 6);
			// 
			// 全选AToolStripMenuItem
			// 
			this.全选AToolStripMenuItem.Name = "全选AToolStripMenuItem";
			this.全选AToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
			this.全选AToolStripMenuItem.Text = "全选(&A)";
			// 
			// 工具TToolStripMenuItem
			// 
			this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自定义CToolStripMenuItem,
            this.选项OToolStripMenuItem});
			this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
			this.工具TToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
			this.工具TToolStripMenuItem.Text = "工具(&T)";
			// 
			// 自定义CToolStripMenuItem
			// 
			this.自定义CToolStripMenuItem.Name = "自定义CToolStripMenuItem";
			this.自定义CToolStripMenuItem.Size = new System.Drawing.Size(149, 26);
			this.自定义CToolStripMenuItem.Text = "自定义(&C)";
			// 
			// 选项OToolStripMenuItem
			// 
			this.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem";
			this.选项OToolStripMenuItem.Size = new System.Drawing.Size(149, 26);
			this.选项OToolStripMenuItem.Text = "选项(&O)";
			// 
			// 帮助HToolStripMenuItem
			// 
			this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.内容CToolStripMenuItem,
            this.索引IToolStripMenuItem,
            this.搜索SToolStripMenuItem,
            this.toolStripSeparator5,
            this.关于AToolStripMenuItem});
			this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
			this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
			this.帮助HToolStripMenuItem.Text = "帮助(&H)";
			// 
			// 内容CToolStripMenuItem
			// 
			this.内容CToolStripMenuItem.Name = "内容CToolStripMenuItem";
			this.内容CToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
			this.内容CToolStripMenuItem.Text = "内容(&C)";
			// 
			// 索引IToolStripMenuItem
			// 
			this.索引IToolStripMenuItem.Name = "索引IToolStripMenuItem";
			this.索引IToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
			this.索引IToolStripMenuItem.Text = "索引(&I)";
			// 
			// 搜索SToolStripMenuItem
			// 
			this.搜索SToolStripMenuItem.Name = "搜索SToolStripMenuItem";
			this.搜索SToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
			this.搜索SToolStripMenuItem.Text = "搜索(&S)";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(144, 6);
			// 
			// 关于AToolStripMenuItem
			// 
			this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
			this.关于AToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
			this.关于AToolStripMenuItem.Text = "关于(&A)...";
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Location = new System.Drawing.Point(0, 666);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(797, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// openNASFile
			// 
			this.openNASFile.Filter = "*.nas|*.nas";
			// 
			// glCanvas1
			// 
			this.glCanvas1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.glCanvas1.Location = new System.Drawing.Point(0, 59);
			this.glCanvas1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.glCanvas1.Name = "glCanvas1";
			this.glCanvas1.RenderTrigger = EMGraphics.RenderTrigger.TimerBased;
			this.glCanvas1.Size = new System.Drawing.Size(797, 607);
			this.glCanvas1.TabIndex = 2;
			this.glCanvas1.DoubleClick += new System.EventHandler(this.glCanvas1_DoubleClick);
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPickTriangle,
            this.btnPickMesh,
            this.btnPickModel,
            this.btnEMGridRendererProperties,
            this.toolStripDropDownButton1,
            this.btnCloud});
			this.toolStrip1.Location = new System.Drawing.Point(0, 28);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(797, 27);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnPickTriangle
			// 
			this.btnPickTriangle.BackColor = System.Drawing.SystemColors.Control;
			this.btnPickTriangle.Image = ((System.Drawing.Image)(resources.GetObject("btnPickTriangle.Image")));
			this.btnPickTriangle.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPickTriangle.Name = "btnPickTriangle";
			this.btnPickTriangle.Size = new System.Drawing.Size(92, 24);
			this.btnPickTriangle.Text = "Triangle";
			this.btnPickTriangle.ToolTipText = "Pick Triangle";
			this.btnPickTriangle.Click += new System.EventHandler(this.btnPickTriangle_Click);
			// 
			// btnPickMesh
			// 
			this.btnPickMesh.Image = ((System.Drawing.Image)(resources.GetObject("btnPickMesh.Image")));
			this.btnPickMesh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPickMesh.Name = "btnPickMesh";
			this.btnPickMesh.Size = new System.Drawing.Size(73, 24);
			this.btnPickMesh.Text = "Mesh";
			this.btnPickMesh.ToolTipText = "Pick Mesh";
			this.btnPickMesh.Click += new System.EventHandler(this.btnPickMesh_Click);
			// 
			// btnPickModel
			// 
			this.btnPickModel.Image = ((System.Drawing.Image)(resources.GetObject("btnPickModel.Image")));
			this.btnPickModel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPickModel.Name = "btnPickModel";
			this.btnPickModel.Size = new System.Drawing.Size(81, 24);
			this.btnPickModel.Text = "Model";
			this.btnPickModel.ToolTipText = "Pick Model";
			this.btnPickModel.Click += new System.EventHandler(this.btnPickModel_Click);
			// 
			// btnEMGridRendererProperties
			// 
			this.btnEMGridRendererProperties.Image = ((System.Drawing.Image)(resources.GetObject("btnEMGridRendererProperties.Image")));
			this.btnEMGridRendererProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnEMGridRendererProperties.Name = "btnEMGridRendererProperties";
			this.btnEMGridRendererProperties.Size = new System.Drawing.Size(101, 24);
			this.btnEMGridRendererProperties.Text = "Renderer";
			this.btnEMGridRendererProperties.ToolTipText = "Renderer";
			this.btnEMGridRendererProperties.Click += new System.EventHandler(this.btnEMGridRendererProperties_Click);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reverseSelectedToolStripMenuItem,
            this.reverseAllToolStripMenuItem});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(34, 24);
			this.toolStripDropDownButton1.Text = "Reverse";
			// 
			// reverseSelectedToolStripMenuItem
			// 
			this.reverseSelectedToolStripMenuItem.Name = "reverseSelectedToolStripMenuItem";
			this.reverseSelectedToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
			this.reverseSelectedToolStripMenuItem.Text = "Reverse Selected";
			this.reverseSelectedToolStripMenuItem.Click += new System.EventHandler(this.reverseSelectedToolStripMenuItem_Click);
			// 
			// reverseAllToolStripMenuItem
			// 
			this.reverseAllToolStripMenuItem.Name = "reverseAllToolStripMenuItem";
			this.reverseAllToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
			this.reverseAllToolStripMenuItem.Text = "Reverse All";
			this.reverseAllToolStripMenuItem.Click += new System.EventHandler(this.reverseAllToolStripMenuItem_Click);
			// 
			// btnCloud
			// 
			this.btnCloud.Image = ((System.Drawing.Image)(resources.GetObject("btnCloud.Image")));
			this.btnCloud.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCloud.Name = "btnCloud";
			this.btnCloud.Size = new System.Drawing.Size(76, 24);
			this.btnCloud.Text = "Cloud";
			this.btnCloud.Click += new System.EventHandler(this.btnCloud_Click);
			// 
			// openCoudFile
			// 
			this.openCoudFile.Filter = "*.txt|*.txt";
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(797, 688);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.glCanvas1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EMGraphics DEMO";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.glCanvas1)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem 保存SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为AToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 打印PToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打印预览VToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 撤消UToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重复RToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 剪切TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴PToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 全选AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自定义CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 内容CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 索引IToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 搜索SToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private EMGraphics.GLCanvas glCanvas1;
        private System.Windows.Forms.OpenFileDialog openNASFile;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnPickTriangle;
        private System.Windows.Forms.ToolStripButton btnPickMesh;
        private System.Windows.Forms.ToolStripButton btnPickModel;
		private System.Windows.Forms.ToolStripButton btnEMGridRendererProperties;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem reverseSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reverseAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton btnCloud;
		private System.Windows.Forms.OpenFileDialog openCoudFile;
	}
}