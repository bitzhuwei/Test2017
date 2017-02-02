﻿using System;
using System.Collections.Generic;

namespace EMGraphics
{
    internal class PointsRecognizer : PrimitiveRecognizer
    {
        protected override void RecognizeUInt(uint lastVertexId, IntPtr pointer, OneIndexBuffer oneIndexBuffer, List<RecognizedPrimitiveInfo> primitiveInfoList)
        {
            int length = oneIndexBuffer.Length;
            unsafe
            {
                var array = (uint*)pointer.ToPointer();
                for (uint i = 0; i < length; i++)
                {
                    var value = array[i];
                    if (value == lastVertexId)
                    {
                        var item = new RecognizedPrimitiveInfo(i, value);
                        primitiveInfoList.Add(item);
                    }
                }
            }
        }

        protected override void RecognizeUShort(uint lastVertexId, IntPtr pointer, OneIndexBuffer oneIndexBuffer, List<RecognizedPrimitiveInfo> primitiveInfoList)
        {
            int length = oneIndexBuffer.Length;
            unsafe
            {
                var array = (ushort*)pointer.ToPointer();
                for (uint i = 0; i < length; i++)
                {
                    var value = array[i];
                    if (value == lastVertexId)
                    {
                        var item = new RecognizedPrimitiveInfo(i, value);
                        primitiveInfoList.Add(item);
                    }
                }
            }
        }

        protected override void RecognizeByte(uint lastVertexId, IntPtr pointer, OneIndexBuffer oneIndexBuffer, List<RecognizedPrimitiveInfo> primitiveInfoList)
        {
            int length = oneIndexBuffer.Length;
            unsafe
            {
                var array = (byte*)pointer.ToPointer();
                for (uint i = 0; i < length; i++)
                {
                    var value = array[i];
                    if (value == lastVertexId)
                    {
                        var item = new RecognizedPrimitiveInfo(i, value);
                        primitiveInfoList.Add(item);
                    }
                }
            }
        }

        protected override void RecognizeUInt(uint lastVertexId, IntPtr pointer, OneIndexBuffer oneIndexBuffer, List<RecognizedPrimitiveInfo> primitiveInfoList, uint primitiveRestartIndex)
        {
            int length = oneIndexBuffer.Length;
            unsafe
            {
                var array = (uint*)pointer.ToPointer();
                for (uint i = 0; i < length; i++)
                {
                    var value = array[i];
                    if (value == lastVertexId)
                    {
                        var item = new RecognizedPrimitiveInfo(i, value);
                        primitiveInfoList.Add(item);
                    }
                }
            }
        }

        protected override void RecognizeUShort(uint lastVertexId, IntPtr pointer, OneIndexBuffer oneIndexBuffer, List<RecognizedPrimitiveInfo> primitiveInfoList, uint primitiveRestartIndex)
        {
            int length = oneIndexBuffer.Length;
            unsafe
            {
                var array = (ushort*)pointer.ToPointer();
                for (uint i = 0; i < length; i++)
                {
                    var value = array[i];
                    if (value == lastVertexId)
                    {
                        var item = new RecognizedPrimitiveInfo(i, value);
                        primitiveInfoList.Add(item);
                    }
                }
            }
        }

        protected override void RecognizeByte(uint lastVertexId, IntPtr pointer, OneIndexBuffer oneIndexBuffer, List<RecognizedPrimitiveInfo> primitiveInfoList, uint primitiveRestartIndex)
        {
            int length = oneIndexBuffer.Length;
            unsafe
            {
                var array = (byte*)pointer.ToPointer();
                for (uint i = 0; i < length; i++)
                {
                    var value = array[i];
                    if (value == lastVertexId)
                    {
                        var item = new RecognizedPrimitiveInfo(i, value);
                        primitiveInfoList.Add(item);
                    }
                }
            }
        }
    }
}