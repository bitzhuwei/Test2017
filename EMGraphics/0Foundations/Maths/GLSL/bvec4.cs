using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace EMGraphics
{
    /// <summary>
    /// Represents a four dimensional vector.
    /// </summary>
    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 4 * 4)]
    [TypeConverter(typeof(StructTypeConverter<bvec4>))]
    [StructLayout(LayoutKind.Explicit)]
    public struct bvec4 : IEquatable<bvec4>, ILoadFromString
    {
        /// <summary>
        /// </summary>
        [FieldOffset(sizeof(bool) * 0)]
        public bool x;

        /// <summary>
        /// </summary>
        [FieldOffset(sizeof(bool) * 1)]
        public bool y;

        /// <summary>
        /// </summary>
        [FieldOffset(sizeof(bool) * 2)]
        public bool z;

        /// <summary>
        /// </summary>
        [FieldOffset(sizeof(bool) * 3)]
        public bool w;

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        public bvec4(bool s)
        {
            x = y = z = w = s;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public bvec4(bool x, bool y, bool z, bool w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="v"></param>
        public bvec4(bvec4 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.w = v.w;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xyz"></param>
        /// <param name="w"></param>
        public bvec4(bvec3 xyz, bool w)
        {
            this.x = xyz.x;
            this.y = xyz.y;
            this.z = xyz.z;
            this.w = w;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool this[int index]
        {
            get
            {
                if (index == 0) return x;
                else if (index == 1) return y;
                else if (index == 2) return z;
                else if (index == 3) return w;
                else throw new Exception("Out of range.");
            }
            set
            {
                if (index == 0) x = value;
                else if (index == 1) y = value;
                else if (index == 2) z = value;
                else if (index == 3) w = value;
                else throw new Exception("Out of range.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(bvec4 lhs, bvec4 rhs)
        {
            return (lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.w != rhs.w);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(bvec4 lhs, bvec4 rhs)
        {
            return (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is bvec4) && (this.Equals((bvec4)obj));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(bvec4 other)
        {
            return (this.x == other.x && this.y == other.y && this.z == other.z && this.w == other.w);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return string.Format("{0}#{1}#{2}#{3}", x, y, z, w).GetHashCode();
        }

        void ILoadFromString.Load(string value)
        {
            string[] parts = value.Split(VectorHelper.separator, StringSplitOptions.RemoveEmptyEntries);
            this.x = bool.Parse(parts[0]);
            this.y = bool.Parse(parts[1]);
            this.z = bool.Parse(parts[2]);
            this.w = bool.Parse(parts[3]);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool[] ToArray()
        {
            return new[] { x, y, z, w };
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", x, y, z, w);
        }
    }
}