using System;
namespace EMGraphics
{
    public sealed unsafe partial class TempUnmanagedArray<T>
    {
        /// <summary>
        /// Dispose unmanaged resources
        /// </summary>
        protected override void DisposeUnmanagedResources()
        {
            this.Header = IntPtr.Zero;
            this.Length = 0;
        }
    }
}