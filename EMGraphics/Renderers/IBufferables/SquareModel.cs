﻿namespace EMGraphics
{
    /// <summary>
    /// 一个正方形的模型。
    /// </summary>
    internal class SquareModel
    {
        internal vec3[] positions = new vec3[4]
        {
            new vec3(0.5f, 0.5f, 0),
            new vec3(0.5f, -0.5f, 0),
            new vec3(-0.5f, -0.5f, 0),
            new vec3(-0.5f, 0.5f, 0),
        };

        internal vec2[] texCoords = new vec2[4]
        {
            new vec2(1, 0),
            new vec2(1, 1),
            new vec2(0, 1),
            new vec2(0, 0),
        };

        internal DrawMode GetDrawModel()
        {
            return DrawMode.Quads;
        }
    }
}