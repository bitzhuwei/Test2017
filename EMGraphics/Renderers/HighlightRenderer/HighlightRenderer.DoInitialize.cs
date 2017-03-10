﻿using System;
using System.Collections.Generic;

namespace EMGraphics
{
    public partial class HighlightRenderer
    {
        /// <summary>
        /// <see cref="OneIndexBuffer"/>实际存在多少个元素。
        /// </summary>
        protected int maxElementCount = 0;

        /// <summary>
        ///
        /// </summary>
        protected override void DoInitialize()
        {
            // init shader program.
            ShaderProgram program = this.shaderProgramProvider.GetShaderProgram();

            // init property buffer objects.
            VertexBuffer positionBuffer = null;
            IBufferable model = this.DataSource;
            VertexBuffer[] vertexAttributeBuffers;
            {
                var list = new List<VertexBuffer>();
                foreach (AttributeMap.NamePair item in this.attributeMap)
                {
                    VertexBuffer buffer = model.GetVertexAttributeBuffer(
                               item.NameInIBufferable, item.VarNameInShader);
                    if (buffer == null) { throw new Exception(string.Format("[{0}] returns null buffer pointer!", model)); }
                    if (item.NameInIBufferable == positionNameInIBufferable)
                    {
                        if (positionBuffer != null)
                        { throw new Exception(string.Format("Duplicate position buffer is not allowed!")); }

                        positionBuffer = buffer.Clone() as VertexBuffer;
                        positionBuffer.VarNameInVertexShader = "in_Position";// in_Postion same with in the PickingShader.vert shader
                    }
                    list.Add(buffer);
                }
                vertexAttributeBuffers = list.ToArray();
            }

            // init index buffer
            OneIndexBuffer indexBuffer;
            {
                var mode = DrawMode.Points;//any mode is OK as we'll update it later in other place.
                indexBuffer = GLBuffer.Create(IndexBufferElementType.UInt, positionBuffer.ByteLength / (positionBuffer.Config.GetDataSize() * positionBuffer.Config.GetDataTypeByteLength()), mode, BufferUsage.StaticDraw);
                this.maxElementCount = indexBuffer.ElementCount;
                indexBuffer.ElementCount = 0;// 高亮0个图元
                // RULE: Renderer takes uint.MaxValue, ushort.MaxValue or byte.MaxValue as PrimitiveRestartIndex. So take care this rule when designing a model's index buffer.
                GLState glState = new PrimitiveRestartState(indexBuffer.ElementType);
                this.stateList.Add(glState);
            }

            // init VAO.
            var vertexArrayObject = new VertexArrayObject(indexBuffer, vertexAttributeBuffers);
            vertexArrayObject.Initialize(program);

            // sets fields.
            this.Program = program;
            this.vertexAttributeBuffers = vertexAttributeBuffers;
            this.indexBuffer = indexBuffer;
            this.vertexArrayObject = vertexArrayObject;

            this.positionBuffer = positionBuffer;
        }
    }
}