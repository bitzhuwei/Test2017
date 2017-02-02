﻿namespace EMGraphics
{
    /// <summary>
    /// Renders a model provided by 
    /// </summary>
    public partial class SimpleRenderer : PickableRenderer
    {
        /// <summary>
        /// create an BigDipper's renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Chain model)
        {
            return Create(model, model.Lengths, Chain.position);
        }

        /// <summary>
        /// create an BigDipper's renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(BigDipper model)
        {
            return Create(model, model.Lengths, BigDipper.position);
        }

        /// <summary>
        /// create an Axis' renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Axis model)
        {
            return Create(model, model.ModelSize, Axis.strPosition);
        }

        /// <summary>
        /// create an Cube' renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Cube model)
        {
            return Create(model, model.Lengths, Cube.strPosition);
        }

        /// <summary>
        /// create an Tetrahedron' renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Tetrahedron model)
        {
            return Create(model, model.Lengths, Tetrahedron.strPosition);
        }

        /// <summary>
        /// create an Sphere' renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Sphere model)
        {
            return Create(model, model.Lengths, Sphere.strPosition);
        }

        /// <summary>
        /// create an Teapot' renderer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleRenderer Create(Teapot model)
        {
            return Create(model, model.Size, Teapot.strPosition);
        }

        internal static SimpleRenderer Create(IBufferable model, vec3 lengths, string positionNameInIBufferable)
        {
            var shaderCodes = new ShaderCode[2];
            shaderCodes[0] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"Resources\Simple.vert"), ShaderType.VertexShader);
            shaderCodes[1] = new ShaderCode(ManifestResourceLoader.LoadTextFile(@"Resources\Simple.frag"), ShaderType.FragmentShader);
            var map = new AttributeMap();
            map.Add("in_Position", "position");
            map.Add("in_Color", "color");
            var renderer = new SimpleRenderer(model, shaderCodes, map, positionNameInIBufferable);
            renderer.ModelSize = lengths;
            return renderer;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <param name="shaderCodes"></param>
        /// <param name="attributeMap"></param>
        /// <param name="positionNameInIBufferable"></param>
        /// <param name="switches"></param>
        private SimpleRenderer(IBufferable model, ShaderCode[] shaderCodes,
            AttributeMap attributeMap, string positionNameInIBufferable, params GLState[] switches)
            : base(model, shaderCodes, attributeMap, positionNameInIBufferable, switches)
        {
        }

        private long modelTicks;

        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        protected override void DoRender(RenderEventArgs arg)
        {
            MarkableStruct<mat4> model = this.GetModelMatrix();
            if (this.modelTicks != model.UpdateTicks)
            {
                this.SetUniform("modelMatrix", model.Value);
                this.modelTicks = model.UpdateTicks;
            }

            mat4 projection = arg.Camera.GetProjectionMatrix();
            mat4 view = arg.Camera.GetViewMatrix();
            this.SetUniform("projectionMatrix", projection);
            this.SetUniform("viewMatrix", view);

            base.DoRender(arg);
        }
    }
}