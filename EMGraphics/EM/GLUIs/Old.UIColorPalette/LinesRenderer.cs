﻿using System;
using System.IO;

namespace EMGraphics.OldColorPalette
{
    /// <summary>
    ///  /|\ y
    ///   |
    ///   |
    ///   |
    ///   ---------------&gt; x
    /// (0, 0)
    /// 0    2    4    6    8    10
    /// |    |    |    |    |    |
    /// |    |    |    |    |    |
    /// |    |    |    |    |    |
    /// |    |    |    |    |    |
    /// |    |    |    |    |    |
    /// |    |    |    |    |    |
    /// |    |    |    |    |    |
    /// 1    3    5    7    9    11
    /// side length is 1.
    /// </summary>
    internal class LinesRenderer : Renderer
    {
        private VertexBuffer positionBuffer;
        private int markerCount;

        public static LinesRenderer Create(LinesModel model)
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(File.ReadAllText(@"shaders\Lines.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(File.ReadAllText(@"shaders\Lines.frag"), ShaderType.FragmentShader);
			var provider = new ShaderCodeArray(shaderCodes);
            var map = new AttributeMap();
            map.Add("in_Position", LinesModel.position);
            var renderer = new LinesRenderer(model, provider, map);
            renderer.markerCount = model.markerCount;

            return renderer;
        }

        private LinesRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, switches)
        { }

        protected override void DoInitialize()
        {
            base.DoInitialize();

            this.positionBuffer = this.DataSource.GetVertexAttributeBuffer(LinesModel.position, null);
        }

        protected override void DoRender(RenderEventArgs arg)
        {
            base.DoRender(arg);
        }

        public void UpdateCodedColors(double axisMin, double axisMax, double step)
        {
            int lineCount = (int)((axisMax - axisMin) / step) + 1;
            IntPtr pointer = this.positionBuffer.MapBuffer(MapBufferAccess.ReadWrite);
            unsafe
            {
                var array = (vec3*)pointer.ToPointer();
                // valid lines.
                for (int i = 0; i < lineCount - 1; i++)
                {
                    array[i * 2 + 0] = new vec3(-0.5f
                        + (float)(step * i / (axisMax - axisMin)),
                        0.5f, 0);
                    array[i * 2 + 1] = new vec3(-0.5f
                        + (float)(step * i / (axisMax - axisMin)),
                        -0.5f, 0);
                }
                // last valid line.
                {
                    int i = lineCount - 1;
                    array[i * 2 + 0] = new vec3(0.5f, 0.5f, 0);
                    array[i * 2 + 1] = new vec3(0.5f, -0.5f, 0);
                }
                // invalid lines.
                for (int i = lineCount; i < this.markerCount; i++)
                {
                    array[i * 2 + 0] = new vec3(0.5f, 0.5f, 0);
                    array[i * 2 + 1] = new vec3(0.5f, -0.5f, 0);
                }
            }
            this.positionBuffer.UnmapBuffer();
        }

        //public void UpdateCodedColors(CodedColor[] codedColors)
        //{
        //    int lineCount = codedColors.Length;

        //    {
        //        OpenGL.BindBuffer(BufferTarget.ArrayBuffer, this.positionBuffer.BufferId);
        //        IntPtr pointer = OpenGL.MapBuffer(BufferTarget.ArrayBuffer, MapBufferAccess.ReadWrite);
        //        unsafe
        //        {
        //            var array = (vec3*)pointer.ToPointer();
        //            for (int i = 0; i < lineCount; i++)
        //            {
        //                //array[i * 2 + 0] = new vec3(-0.5f + (float)i / (float)(lineCount - 1), 0.5f, 0);
        //                //array[i * 2 + 1] = new vec3(-0.5f + (float)i / (float)(lineCount - 1), -0.5f, 0);
        //                array[i * 2 + 0] = new vec3(-0.5f + codedColors[i].Coord, 0.5f, 0);
        //                array[i * 2 + 1] = new vec3(-0.5f + codedColors[i].Coord, -0.5f, 0);
        //            }
        //            for (int i = lineCount; i < this.markerCount; i++)
        //            {
        //                array[i * 2 + 0] = new vec3(0.5f, 0.5f, 0);
        //                array[i * 2 + 1] = new vec3(0.5f, -0.5f, 0);
        //            }
        //        }
        //        OpenGL.UnmapBuffer(BufferTarget.ArrayBuffer);
        //        OpenGL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        //    }
        //}
    }
}