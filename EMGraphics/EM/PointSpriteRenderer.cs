using System;
using System.IO;

namespace EMGraphics
{
    public class PointSpriteRenderer : Renderer
    {
        private Texture spriteTexture;

        public static PointSpriteRenderer Create()
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\PointSprite.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"EM\shaders\PointSprite.frag"), ShaderType.FragmentShader);
            var provider = new ShaderCodeArray(shaderCodes);
            var map = new AttributeMap();
            map.Add("position", PointSpriteModel.strposition);
            var model = new PointSpriteModel();
            var renderer = new PointSpriteRenderer(model, provider, map, new PointSpriteState());
            renderer.ModelSize = model.Lengths;

            return renderer;
        }

        public PointSpriteRenderer(IBufferable model, IShaderProgramProvider shaderProgramProvider,
            AttributeMap attributeMap, params GLState[] switches)
            : base(model, shaderProgramProvider, attributeMap, switches)
        {
        }

        protected override void DoInitialize()
        {
            {
                // This is the texture that the compute program will write into
                var bitmap = ManifestResourceLoader.LoadBitmap(@"EM\Textures\PointSprite.png");
                var texture = new Texture(TextureTarget.Texture2D, bitmap, new SamplerParameters());
                texture.Initialize();
                bitmap.Dispose();
                this.spriteTexture = texture;
            }
            base.DoInitialize();
            this.SetUniform("spriteTexture", this.spriteTexture);
            this.SetUniform("factor", 50.0f);
        }

        protected override void DoRender(RenderEventArgs arg)
        {
            const float left = -1, bottom = -1, right = 1, top = 1, near = int.MinValue, far = int.MaxValue;
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

            // 把所有在此之前渲染的内容都推到最远。
            // Push all rendered stuff to farest position.
            OpenGL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT);
            base.DoRender(arg);
        }

        protected override void DisposeUnmanagedResources()
        {
            base.DisposeUnmanagedResources();

            this.spriteTexture.Dispose();
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
                        var array = new vec3[1];
                        array[0] = new vec3(0.5f, 0, 0);
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
                    ZeroIndexBuffer buffer = ZeroIndexBuffer.Create(DrawMode.Points, 0, 1);
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

        internal void UpdateTexture(string filename)
        {
            // This is the texture that the compute program will write into
            var bitmap = new System.Drawing.Bitmap(filename);
            var texture = new Texture(TextureTarget.Texture2D, bitmap, new SamplerParameters());
            texture.Initialize();
            bitmap.Dispose();
            Texture old = this.spriteTexture;
            this.spriteTexture = texture;
            this.SetUniform("sprite_texture", texture);

            old.Dispose();
        }
    }
}