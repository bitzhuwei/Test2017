﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EMGraphics
{
    // check http://www.cnblogs.com/bitzhuwei/p/EMGraphics-18-Picking-of-OneIndexBuffer.html
    partial class OneIndexRenderer
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="stageVertexId"></param>
        /// <param name="x">mouse position(Left Down is (0, 0)).</param>
        /// <param name="y">mouse position(Left Down is (0, 0)).</param>
        /// <returns></returns>
        public override PickedGeometry GetPickedGeometry(RenderEventArgs arg, uint stageVertexId,
            int x, int y)
        {
            uint lastVertexId;
            if (!this.GetLastVertexIdOfPickedGeometry(stageVertexId, out lastVertexId))
            { return null; }

            // 找到 lastIndexId
            RecognizedPrimitiveInfo lastIndexId = this.GetLastIndexIdOfPickedGeometry(
                arg, lastVertexId, x, y);
            if (lastIndexId == null)
            {
                Debug.WriteLine(string.Format(
                    "Got lastVertexId[{0}] but no lastIndexId! Params are [{1}] [{2}] [{3}] [{4}]",
                    lastVertexId, arg, stageVertexId, x, y));
                { return null; }
            }

            PickingGeometryType geometryType = arg.PickingGeometryType;
            DrawMode mode = this.indexBuffer.Mode;
            PickingGeometryType typeOfMode = mode.ToGeometryType();

            if (geometryType == PickingGeometryType.Point)
            {
                // 获取pickedGeometry
                if (typeOfMode == PickingGeometryType.Point)
                { return PickWhateverItIs(arg, stageVertexId, lastIndexId, typeOfMode); }
                else if (typeOfMode == PickingGeometryType.Line)
                {
                    if (this.OnPrimitiveTest(lastVertexId, mode))
                    { return PickPoint(arg, stageVertexId, lastVertexId); }
                    else
                    { return null; }
                }
                else
                {
                    OneIndexPointSearcher searcher = GetPointSearcher(mode);
                    if (searcher != null)// line is from triangle, quad or polygon
                    { return SearchPoint(arg, stageVertexId, x, y, lastVertexId, lastIndexId, searcher); }
                    else
                    { throw new Exception(string.Format("Lack of searcher for [{0}]", mode)); }
                }
            }
            else if (geometryType == PickingGeometryType.Line)
            {
                // 获取pickedGeometry
                if (geometryType == typeOfMode)
                { return PickWhateverItIs(arg, stageVertexId, lastIndexId, typeOfMode); }
                else
                {
                    OneIndexLineSearcher searcher = GetLineSearcher(mode);
                    if (searcher != null)// line is from triangle, quad or polygon
                    { return SearchLine(arg, stageVertexId, x, y, lastVertexId, lastIndexId, searcher); }
                    else if (mode == DrawMode.Points)// want a line when rendering GL_POINTS
                    { return null; }
                    else
                    { throw new Exception(string.Format("Lack of searcher for [{0}]", mode)); }
                }
            }
            else
            {
                if (typeOfMode == geometryType)// I want what it is
                { return PickWhateverItIs(arg, stageVertexId, lastIndexId, typeOfMode); }
                else
                { return null; }
                //{ throw new Exception(string.Format("Lack of searcher for [{0}]", mode)); }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="stageVertexId"></param>
        /// <param name="x">mouse position(Left Down is (0, 0)).</param>
        /// <param name="y">mouse position(Left Down is (0, 0)).</param>
        /// <param name="lastVertexId"></param>
        /// <param name="primitiveInfo"></param>
        /// <param name="searcher"></param>
        /// <returns></returns>
        private PickedGeometry SearchPoint(RenderEventArgs arg, uint stageVertexId, int x, int y, uint lastVertexId, RecognizedPrimitiveInfo primitiveInfo, OneIndexPointSearcher searcher)
        {
            var vertexIds = new uint[] { searcher.Search(arg, x, y, primitiveInfo, this), };
            vec3[] positions = FillPickedGeometrysPosition(vertexIds);
            var pickedGeometry = new PickedGeometry(arg.UsingViewPort, PickingGeometryType.Point, positions, vertexIds, stageVertexId, this);

            return pickedGeometry;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="stageVertexId"></param>
        /// <param name="x">mouse position(Left Down is (0, 0)).</param>
        /// <param name="y">mouse position(Left Down is (0, 0)).</param>
        /// <param name="lastVertexId"></param>
        /// <param name="primitiveInfo"></param>
        /// <param name="searcher"></param>
        /// <returns></returns>
        private PickedGeometry SearchLine(RenderEventArgs arg, uint stageVertexId, int x, int y, uint lastVertexId, RecognizedPrimitiveInfo primitiveInfo, OneIndexLineSearcher searcher)
        {
            var vertexIds = searcher.Search(arg, x, y, primitiveInfo, this);
            vec3[] positions = FillPickedGeometrysPosition(vertexIds);
            var pickedGeometry = new PickedGeometry(arg.UsingViewPort, PickingGeometryType.Line, positions, vertexIds, stageVertexId, this);

            return pickedGeometry;
        }

        /// <summary>
        /// 是三角形，就pick一个三角形；是四边形，就pick一个四边形，是多边形，就pick一个多边形。
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="stageVertexId"></param>
        /// <param name="primitiveInfo"></param>
        /// <param name="typeOfMode"></param>
        /// <returns></returns>
        private PickedGeometry PickWhateverItIs(RenderEventArgs arg, uint stageVertexId, RecognizedPrimitiveInfo primitiveInfo, PickingGeometryType typeOfMode)
        {
            uint[] vertexIds = primitiveInfo.VertexIds;
            vec3[] positions = FillPickedGeometrysPosition(vertexIds);
            var pickedGeometry = new PickedGeometry(arg.UsingViewPort, typeOfMode, positions, vertexIds, stageVertexId, this);

            return pickedGeometry;
        }

        /// <summary>
        /// I don't know how to implement this method in a high effitiency way.
        /// So keep it like this.
        /// Also, why would someone use glDrawElements() when rendering GL_POINTS?
        /// </summary>
        /// <param name="lastVertexId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private bool OnPrimitiveTest(uint lastVertexId, DrawMode mode)
        {
            return true;
        }

        private PickedGeometry PickPoint(RenderEventArgs arg, uint stageVertexId, uint lastVertexId)
        {
            var vertexIds = new uint[] { lastVertexId, };
            vec3[] positions = FillPickedGeometrysPosition(vertexIds);
            var pickedGeometry = new PickedGeometry(arg.UsingViewPort, PickingGeometryType.Point, positions, vertexIds, stageVertexId, this);

            return pickedGeometry;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="lastVertexId"></param>
        /// <param name="x">mouse position(Left Down is (0, 0)).</param>
        /// <param name="y">mouse position(Left Down is (0, 0)).</param>
        /// <returns></returns>
        private RecognizedPrimitiveInfo GetLastIndexIdOfPickedGeometry(
            RenderEventArgs arg,
            uint lastVertexId, int x, int y)
        {
            List<RecognizedPrimitiveInfo> primitiveInfoList = GetLastIndexIdList(arg, lastVertexId);

            if (primitiveInfoList.Count == 0) { return null; }

            RecognizedPrimitiveInfo lastIndexId = GetLastIndexId(arg, primitiveInfoList, x, y);

            return lastIndexId;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="twoPrimitivesIndexBuffer"></param>
        /// <param name="x">mouse position(Left Down is (0, 0)).</param>
        /// <param name="y">mouse position(Left Down is (0, 0)).</param>
        /// <returns></returns>
        private uint Pick(RenderEventArgs arg, OneIndexBuffer twoPrimitivesIndexBuffer,
            int x, int y)
        {
            Render4InnerPicking(arg, twoPrimitivesIndexBuffer);

            uint pickedIndex = ColorCodedPicking.ReadStageVertexId(x, y);

            return pickedIndex;
        }

        /// <summary>
        /// 遍历以<paramref name="lastVertexId"/>为最后一个顶点的图元，
        /// 瞄准每个图元的索引（例如1个三角形有3个索引）中的最后一个索引，
        /// 将此索引在<see cref="IndexBuffer"/>中的索引（位置）收集起来。
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="lastVertexId"></param>
        /// <returns></returns>
        private List<RecognizedPrimitiveInfo> GetLastIndexIdList(RenderEventArgs arg, uint lastVertexId)
        {
            PrimitiveRecognizer recognizer = PrimitiveRecognizerFactory.Create(
                (arg.PickingGeometryType == PickingGeometryType.Point
                && this.indexBuffer.Mode.ToGeometryType() == PickingGeometryType.Line) ?
                DrawMode.Points : this.indexBuffer.Mode);

            PrimitiveRestartState glState = GetPrimitiveRestartState();

            var buffer = this.indexBuffer as OneIndexBuffer;
            IntPtr pointer = buffer.MapBuffer(MapBufferAccess.ReadOnly);
            List<RecognizedPrimitiveInfo> primitiveInfoList = null;
            if (glState == null)
            { primitiveInfoList = recognizer.Recognize(lastVertexId, pointer, this.indexBuffer as OneIndexBuffer); }
            else
            { primitiveInfoList = recognizer.Recognize(lastVertexId, pointer, this.indexBuffer as OneIndexBuffer, glState.RestartIndex); }
            buffer.UnmapBuffer();

            return primitiveInfoList;
        }

        private PrimitiveRestartState GetPrimitiveRestartState()
        {
            foreach (GLState item in this.stateList)
            {
                var target = item as PrimitiveRestartState;
                if (target != null)
                {
                    return target;
                }
            }

            return null;
        }
    }
}