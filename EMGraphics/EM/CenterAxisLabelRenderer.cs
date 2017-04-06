using System;
using System.Collections.Generic;
using System.IO;

namespace EMGraphics
{
    //public enum CenterAxisLabelDirection
    //{
    //    X,
    //    Y,
    //    Z,
    //}
    public class CenterAxisLabelRenderer : Renderer
    {
        //private CenterAxisLabelDirection direction;

        //public static CenterAxisLabelRenderer Create(CenterAxisLabelDirection direction)
        public float LabelSize { get; set; }

        public static CenterAxisLabelRenderer Create()
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\PointSprite.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\PointSprite.frag"), ShaderType.FragmentShader);
            var provider = new ShaderCodeArray(shaderCodes);
            var map = new AttributeMap();
            map.Add("position", PointSpriteModel.strposition);
            var model = new PointSpriteModel();
            var renderer = new CenterAxisLabelRenderer(model, provider, map, new PointSpriteState());
            renderer.ModelSize = model.Lengths;
            renderer.LabelSize = 50.0f;
            //renderer.direction = direction;

            return renderer;
        }

        public CenterAxisLabelRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, switches)
        {
        }

        private List<Texture> textures = new List<Texture>();

        protected override void DoInitialize()
        {
            base.DoInitialize();
            var array = new string[] { "X", "Y", "Z" };
            for (uint i = 0; i < array.Length; i++)
            {
                // This is the texture that the compute program will write into
                var bitmap = ManifestResourceLoader.LoadBitmap(string.Format(@"EM\Textures\{0}.png", array[i]));
                var texture = new Texture(TextureTarget.Texture2D, bitmap, new SamplerParameters());
                texture.ActiveTextureIndex = i;
                texture.Initialize();
                bitmap.Dispose();

                this.SetUniform(string.Format("spriteTexture{0}", i), texture);

                textures.Add(texture);
            }
        }

        protected override void DoRender(RenderEventArgs arg)
        {
            const float left = -1, bottom = -1, right = 1, top = 1, near = -1000, far = 1000;
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
            this.SetUniform("mvp", projection * view * model);
            this.SetUniform("labelSize", this.LabelSize);

            //// 把所有在此之前渲染的内容都推到最远。
            //// Push all rendered stuff to farest position.
            OpenGL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT);
            base.DoRender(arg);
        }

        protected override void DisposeUnmanagedResources()
        {
            base.DisposeUnmanagedResources();

            foreach (var item in this.textures)
            {
                item.Dispose();
            }
        }

        private class PointSpriteModel : IBufferable
        {
            public PointSpriteModel()
            {
            }

            public const string strposition = "position";
            private VertexBuffer positionBuffer = null;
            private IndexBuffer indexBuffer;
            private float factor = 1;

            public VertexBuffer GetVertexAttributeBuffer(string bufferName, string varNameInShader)
            {
                if (bufferName == strposition)
                {
                    if (this.positionBuffer == null)
                    {
                        const float factor = 1.10f;
                        var array = new vec3[3];
                        array[0] = new vec3(0.5f, 0, 0) * factor;
                        array[1] = new vec3(0, 0.5f, 0) * factor;
                        array[2] = new vec3(0, 0, 0.5f) * factor;
                        VertexBuffer buffer = array.GenVertexBuffer(VBOConfig.Vec3, varNameInShader, BufferUsage.StaticDraw);
                        this.positionBuffer = buffer;
                    }

                    return this.positionBuffer;
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            public IndexBuffer GetIndexBuffer()
            {
                if (this.indexBuffer == null)
                {
                    ZeroIndexBuffer buffer = ZeroIndexBuffer.Create(DrawMode.Points, 0, 3);
                    this.indexBuffer = buffer;
                }

                return indexBuffer;
            }

            /// <summary>
            /// Uses <see cref="ZeroIndexBuffer"/> or <see cref="OneIndexBuffer"/>.
            /// </summary>
            /// <returns></returns>
            public bool UsesZeroIndexBuffer() { return true; }

            public vec3 Lengths { get { return new vec3(2, 2, 2); } }
        }

    }
}