using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace EMGraphics
{
    /// <summary>
    /// 
    /// </summary>
    public class CenterAxisRenderer : Renderer
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CenterAxisRenderer Create(CenterAxis model)
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\CenterAxis.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\CenterAxis.frag"), ShaderType.FragmentShader);
            var provider = new ShaderCodeArray(shaderCodes);
            var map = new AttributeMap();
            map.Add("inPosition", CenterAxis.strPosition);
            map.Add("inColor", CenterAxis.strColor);
            var renderer = new CenterAxisRenderer(model, provider, map);
            renderer.ModelSize = model.ModelSize;

            return renderer;
        }

        private CenterAxisRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, switches)
        {
        }

        LineStippleState lineStippleState = new LineStippleState();

        protected override void DoRender(RenderEventArgs arg)
        {
            const float left = -1, bottom = -1, right = 1, top = 1, near = -100, far = 100;
            mat4 projection = arg.Camera.GetProjectionMatrix();
            mat4 view = arg.Camera.GetViewMatrix();
            mat4 model = this.GetModelMatrix().Value;
            int[] viewport = OpenGL.GetViewport();
            vec3 windowCoord = glm.project(new vec3(0, 0, 0), view * model, projection, new vec4(viewport[0], viewport[1], viewport[2], viewport[3]));
            {
                IOrthoViewCamera camera = arg.Camera;
                float newBottom = (float)camera.Bottom;
                float newTop = (float)camera.Top;
                float newLeft = (float)camera.Left;
                float newRight = (float)camera.Right;
                float newWidth = newRight - newLeft;
                float newHeight = newTop - newBottom;
                if (newWidth >= newHeight)
                {
                    float leftPercent = (windowCoord.x - 0) / (float)arg.CanvasRect.Width;
                    float bottomPercent = (windowCoord.y - 0) / (float)arg.CanvasRect.Height;
                    float scale = newWidth / newHeight;
                    projection = glm.ortho(
                        -leftPercent * (top - bottom) * scale,
                        (1 - leftPercent) * (top - bottom) * scale,
                        -bottomPercent * (top - bottom),
                        (1 - bottomPercent) * (top - bottom),
                        near, far);
                }
                else
                {
                    float leftPercent = (windowCoord.x - 0) / (float)arg.CanvasRect.Width;
                    float bottomPercent = (windowCoord.y - 0) / (float)arg.CanvasRect.Height;
                    float scale = newHeight / newWidth;
                    projection = glm.ortho(
                        -leftPercent * (right - left),
                        (1 - leftPercent) * (right - left),
                        -bottomPercent * (right - left) * scale,
                        (1 - bottomPercent) * (right - left) * scale,
                        near, far);
                }
            }
            this.SetUniform("mvpMatrix", projection * view * model);
            base.DoRender(arg);

            //OpenGL.Flush();
            //OpenGL.Finish();

            lineStippleState.On();
            // 把所有在此之前渲染的内容都推到最远。
            // Push all rendered stuff to farest position.
            OpenGL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT);
            base.DoRender(arg);
            lineStippleState.Off();
        }
    }
}
