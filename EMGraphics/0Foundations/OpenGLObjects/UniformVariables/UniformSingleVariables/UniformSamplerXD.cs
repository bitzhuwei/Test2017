﻿using System;
using System.ComponentModel;

namespace EMGraphics
{
    /// <summary>
    /// uniform samplerXD variable;
    /// </summary>
    public class UniformSampler : UniformSingleVariable<samplerValue>
    {
        /// <summary>
        /// uniform samplerXD variable;
        /// </summary>
        /// <param name="varName"></param>
        public UniformSampler(string varName) : base(varName) { }

        /// <summary>
        /// uniform samplerXD variable;
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="value"></param>
        public UniformSampler(string varName, samplerValue value) : base(varName, value) { }

        private static OpenGL.glActiveTexture activeTexture;

        /// <summary>
        ///
        /// </summary>
        /// <param name="program"></param>
        protected override void DoSetUniform(ShaderProgram program)
        {
            if (activeTexture == null)
            { activeTexture = OpenGL.GetDelegateFor<OpenGL.glActiveTexture>(); }
            activeTexture(value.activeTextureIndex + OpenGL.GL_TEXTURE0);
            //OpenGL.BindTexture(OpenGL.GL_TEXTURE_2D, value.TextureId);
            OpenGL.BindTexture(value.target, value.TextureId);
            this.Location = program.SetUniform(VarName, (int)value.activeTextureIndex);
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="program"></param>
        //public override void ResetUniform(ShaderProgram program)
        //{
        //    if (activeTexture == null)
        //    { activeTexture = OpenGL.GetDelegateFor<OpenGL.glActiveTexture>(); }
        //    activeTexture(value.activeTextureIndex + OpenGL.GL_TEXTURE0);
        //    OpenGL.BindTexture(value.target, 0);
        //    //base.ResetUniform(program);
        //    //if (glActiveTexture == null)
        //    //{ glActiveTexture = OpenGL.GetDelegateFor<OpenGL.glActiveTexture>(); }
        //    //glActiveTexture(value.ActiveTextureIndex);
        //    ////OpenGL.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        //    //OpenGL.BindTexture(value.target, 0);
        //}
    }

    /// <summary>
    /// value for setting/resetting uniform samplerXD variable.
    /// </summary>
    [TypeConverter(typeof(StructTypeConverter<samplerValue>))]
    public struct samplerValue : IEquatable<samplerValue>, ILoadFromString
    {
        internal uint target;

        /// <summary>
        ///
        /// </summary>
        public TextureTarget Target
        {
            get { return (TextureTarget)target; }
            set { target = (uint)value; }
        }

        private uint textureId;

        /// <summary>
        ///
        /// </summary>
        public uint TextureId
        {
            get { return textureId; }
            set { textureId = value; }
        }

        internal uint activeTextureIndex;

        /// <summary>
        /// 0 means OpenGL.GL_TEXTURE0, 1 means OpenGL.GL_TEXTURE1, ...
        /// </summary>
        public uint ActiveTextureIndex
        {
            get { return activeTextureIndex; }
            set { activeTextureIndex = value; }
        }

        /// <summary>
        /// value for setting/resetting uniform samplerXD variable.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="textureId"></param>
        /// <param name="activeTextureIndex">0 means OpenGL.GL_TEXTURE0, 1 means OpenGL.GL_TEXTURE1 etc</param>
        public samplerValue(TextureTarget target, uint textureId, uint activeTextureIndex)
        {
            this.target = (uint)target;
            this.textureId = textureId;
            this.activeTextureIndex = activeTextureIndex;
        }

        private static readonly char[] separator = new char[] { '[', ']', };

        internal static samplerValue Parse(string value)
        {
            string[] parts = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            TextureTarget target = (TextureTarget)Enum.Parse(typeof(TextureTarget), parts[1]);
            uint textureId = uint.Parse(parts[3]);
            uint activeTextureIndex = uint.Parse(parts[5]);

            return new samplerValue(target, textureId, activeTextureIndex);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("texture target: [{0}] id:[{1}] active GL_TEXTURE{2}", target, textureId, activeTextureIndex);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(samplerValue left, samplerValue right)
        {
            //object leftObj = left, rightObj = right;
            //if (leftObj == null)
            //{
            //    if (rightObj == null) { return true; }
            //    else { return false; }
            //}
            //else
            //{
            //    if (rightObj == null) { return false; }
            //}

            return left.Equals(right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(samplerValue left, samplerValue right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is samplerValue) && (this.Equals((samplerValue)obj));
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return string.Format("{0}#{1}#{2}", target, textureId, activeTextureIndex).GetHashCode();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(samplerValue other)
        {
            return (
                this.target == other.target
                && this.textureId == other.textureId
                && this.activeTextureIndex == other.activeTextureIndex
                );
        }

        void ILoadFromString.Load(string value)
        {
            string[] parts = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            TextureTarget target = (TextureTarget)Enum.Parse(typeof(TextureTarget), parts[1]);
            uint textureId = uint.Parse(parts[3]);
            uint activeTextureIndex = uint.Parse(parts[5]);
            this.target = (uint)target;
            this.textureId = textureId;
            this.activeTextureIndex = activeTextureIndex;
        }
    }
}