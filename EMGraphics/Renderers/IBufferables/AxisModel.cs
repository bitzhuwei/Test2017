﻿using System;

namespace EMGraphics
{
    /// <summary>
    /// 3D坐标系
    /// </summary>
    internal class AxisModel
    {
        internal vec3[] positions;
        internal vec3[] colors;
        internal uint[] indexes;

        internal DrawMode mode = DrawMode.TriangleFan;

        public AxisModel(int partCount = 12, float radius = 1.0f)
        {
            this.positions = GeneratePositions(partCount);
            for (int i = 0; i < this.positions.Length; i++)
            {
                this.positions[i] *= radius;
            }
            this.colors = GenrateColors(partCount);
            this.indexes = GenerateIndexes(partCount);
        }

        private uint[] GenerateIndexes(int partCount)
        {
            var indexes = new uint[3 * (3 * (1 + (partCount + 1) + 1))];
            int index = 0;
            // x axis
            indexes[index++] = 0;
            for (uint i = 0; i < (partCount + 1); i++)
            {
                indexes[index++] = (uint)(i % partCount + 1);
            }
            indexes[index++] = uint.MaxValue;// seprate triangle fans.
            indexes[index++] = (uint)(1 + partCount);
            for (uint i = 0; i < (partCount + 1); i++)
            {
                indexes[index++] = (uint)(i % partCount + 1 + (1 + partCount));
            }
            indexes[index++] = uint.MaxValue;// seprate triangle fans.
            indexes[index++] = (uint)((3 + 2 * partCount - 1));
            for (uint i = 0; i < (partCount + 1); i++)
            {
                indexes[index++] = (uint)(i % partCount + 1 + (1 + partCount));
            }
            indexes[index++] = uint.MaxValue;// seprate triangle fans.
            // y axis
            for (uint i = 0; i < 3 * (1 + (partCount + 1) + 1); i++)
            {
                uint value = indexes[i];
                if (value == uint.MaxValue)
                { indexes[index++] = uint.MaxValue; }
                else
                { indexes[index++] = (uint)(value + 3 + 2 * partCount); }
            }
            // z axis
            for (uint i = 0; i < 3 * (1 + (partCount + 1) + 1); i++)
            {
                uint value = indexes[i];
                if (value == uint.MaxValue)
                { indexes[index++] = uint.MaxValue; }
                else
                { indexes[index++] = (uint)(value + 3 + 2 * partCount + 3 + 2 * partCount); }
            }

            return indexes;
        }

        private vec3[] GenrateColors(int partCount)
        {
            Random r = new Random();
            const float lightColor = 0.75f;
            var colors = new vec3[3 * (3 + 2 * partCount)];
            int index = 0;
            for (int i = 0; i < 3 + 2 * partCount; i++)
            {
                if (i == 1 + partCount)
                { colors[index++] = new vec3(1, 1, 1); }
                else if (partCount < i && i < 2 + 2 * partCount)// light red arrow except header.
                { colors[index++] = new vec3(1, lightColor, lightColor); }
                else if (i % (1 + partCount) == 0)// make sure pure red color exists.
                { colors[index++] = new vec3(1, 0, 0); }
                else// random red color.
                { colors[index++] = new vec3(0.5f + (float)r.NextDouble() / 2, 0, 0); }
            }
            for (int i = 0; i < 3 + 2 * partCount; i++)
            {
                if (i == 1 + partCount)
                { colors[index++] = new vec3(1, 1, 1); }
                else if (partCount < i && i < 2 + 2 * partCount)// light green arrow except header.
                { colors[index++] = new vec3(lightColor, 1, lightColor); }
                else if (i % (1 + partCount) == 0)// make sure pure green color exists.
                { colors[index++] = new vec3(0, 1, 0); }
                else// random green color.
                { colors[index++] = new vec3(0, 0.5f + (float)r.NextDouble() / 2, 0); }
            }
            for (int i = 0; i < 3 + 2 * partCount; i++)
            {
                if (i == 1 + partCount)
                { colors[index++] = new vec3(1, 1, 1); }
                else if (partCount < i && i < 2 + 2 * partCount)// light blue arrow except header.
                { colors[index++] = new vec3(lightColor, lightColor, 1); }
                else if (i % (1 + partCount) == 0)// make sure pure blue color exists.
                { colors[index++] = new vec3(0, 0, 1); }
                else// random blue color.
                { colors[index++] = new vec3(0, 0, 0.5f + (float)r.NextDouble() / 2); }
            }
            return colors;
        }

        private static vec3[] GeneratePositions(int partCount)
        {
            const float tail = -0.5f;// xyz cross
            var positions = new vec3[3 * (3 + 2 * partCount)];
            const float stickLength = 0.68f;
            const float r1 = 0.08f;
            const float r2 = 0.16f;
            int index = 0;
            {
                // x axis
                positions[index++] = new vec3(tail, 0, 0);
                for (int i = 0; i < partCount; i++)
                {
                    double angle = 2 * Math.PI * i / partCount;
                    float cos = r1 * (float)Math.Cos(angle);
                    float sin = r1 * (float)Math.Sin(angle);
                    positions[index++] = new vec3(stickLength, cos, sin);
                }
                positions[index++] = new vec3(stickLength, 0, 0);
                for (int i = 0; i < partCount; i++)
                {
                    double angle = 2 * Math.PI * i / partCount;
                    float cos = r2 * (float)Math.Cos(angle);
                    float sin = r2 * (float)Math.Sin(angle);
                    positions[index++] = new vec3(stickLength, cos, sin);
                }
                positions[index++] = new vec3(1.0f, 0, 0);
            }
            {
                // y axis
                positions[index++] = new vec3(0, tail, 0);
                for (int i = 0; i < partCount; i++)
                {
                    double angle = 2 * Math.PI * i / partCount;
                    float cos = r1 * (float)Math.Cos(angle);
                    float sin = r1 * (float)Math.Sin(angle);
                    positions[index++] = new vec3(cos, stickLength, sin);
                }
                positions[index++] = new vec3(0, stickLength, 0);
                for (int i = 0; i < partCount; i++)
                {
                    double angle = 2 * Math.PI * i / partCount;
                    float cos = r2 * (float)Math.Cos(angle);
                    float sin = r2 * (float)Math.Sin(angle);
                    positions[index++] = new vec3(cos, stickLength, sin);
                }
                positions[index++] = new vec3(0, 1.0f, 0);
            }
            {
                // z axis
                positions[index++] = new vec3(0, 0, tail);
                for (int i = 0; i < partCount; i++)
                {
                    double angle = 2 * Math.PI * i / partCount;
                    float cos = r1 * (float)Math.Cos(angle);
                    float sin = r1 * (float)Math.Sin(angle);
                    positions[index++] = new vec3(cos, sin, stickLength);
                }
                positions[index++] = new vec3(0, 0, stickLength);
                for (int i = 0; i < partCount; i++)
                {
                    double angle = 2 * Math.PI * i / partCount;
                    float cos = r2 * (float)Math.Cos(angle);
                    float sin = r2 * (float)Math.Sin(angle);
                    positions[index++] = new vec3(cos, sin, stickLength);
                }
                positions[index++] = new vec3(0, 0, 1.0f);
            }
            return positions;
        }
    }
}