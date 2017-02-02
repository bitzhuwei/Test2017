﻿using System;
using System.Collections.Generic;

namespace EMGraphics
{
    internal class ClockPinRenderer : RendererBase, IModelSpace
    {
        private readonly List<vec3> secondPosition = new List<vec3>();
        private readonly List<vec3> secondColor = new List<vec3>();
        private readonly LineWidthState secondLineWidthState = new LineWidthState(1);
        private readonly List<vec3> minutePosition = new List<vec3>();
        private readonly List<vec3> minuteColor = new List<vec3>();
        private readonly LineWidthState minuteLineWidthState = new LineWidthState(3);
        private readonly List<vec3> hourPosition = new List<vec3>();
        private readonly List<vec3> hourColor = new List<vec3>();
        private readonly LineWidthState hourLineWidthState = new LineWidthState(8);

        public ClockPinRenderer()
        {
            this.Scale = new vec3(1, 1, 1);
            this.RotationAxis = new vec3(0, 0, -1);
            this.ModelSize = new vec3(2, 2, 2);
        }

        protected override void DoInitialize()
        {
            // second
            secondColor.Add(new vec3(1.0f, 0.0f, 0.0f));
            secondPosition.Add(new vec3(0.0f, 0.0f, 0.0f));
            secondColor.Add(new vec3(1.0f, 0.0f, 0.0f));
            secondPosition.Add(new vec3(0.0f, 1.0f, 0.0f) * 0.85f);
            // minite
            minuteColor.Add(new vec3(0.0f, 1.0f, 0.0f));
            minutePosition.Add(new vec3(0.0f, 0.0f, 0.0f));
            minuteColor.Add(new vec3(0.0f, 1.0f, 0.0f));
            minutePosition.Add(new vec3(0.0f, 1.0f, 0.0f) * 0.7f);
            // hour
            hourColor.Add(new vec3(0.0f, 0.0f, 1.0f));
            hourPosition.Add(new vec3(0.0f, 0.0f, 0.0f));
            hourColor.Add(new vec3(0.0f, 0.0f, 1.0f));
            hourPosition.Add(new vec3(0.0f, 1.0f, 0.0f) * 0.5f);
        }

        protected override void DoRender(RenderEventArgs arg)
        {
            DateTime now = DateTime.Now;
            const float speed = 1.0f;

            {
                float secondAngle = ((float)now.Second) / 60.0f * 360.0f * speed;
                this.RotationAngleDegree = secondAngle;
                OpenGL.LoadIdentity();
                this.LegacyTransform();

                secondLineWidthState.On();
                OpenGL.Begin(OpenGL.GL_LINES);
                for (int i = 0; i < secondPosition.Count; i++)
                {
                    vec3 color = secondColor[i];
                    OpenGL.Color3f(color.x, color.y, color.z);
                    vec3 position = secondPosition[i];
                    OpenGL.Vertex3f(position.x, position.y, position.z);
                }
                OpenGL.End();
                secondLineWidthState.Off();
            }
            {
                float minuteAngle = ((float)(now.Minute * 60 + now.Second)) / (60.0f * 60.0f) * 360.0f * speed;
                this.RotationAngleDegree = minuteAngle;
                OpenGL.LoadIdentity();
                this.LegacyTransform();

                minuteLineWidthState.On();
                OpenGL.Begin(OpenGL.GL_LINES);
                for (int i = 0; i < minutePosition.Count; i++)
                {
                    vec3 color = minuteColor[i];
                    OpenGL.Color3f(color.x, color.y, color.z);
                    vec3 position = minutePosition[i];
                    OpenGL.Vertex3f(position.x, position.y, position.z);
                }
                OpenGL.End();
                minuteLineWidthState.Off();
            }
            {
                float hourAngle = ((float)((now.Hour * 60 + now.Minute) * 60 + now.Second)) / (12.0f * 60.0f * 60.0f) * 360.0f * speed;
                this.RotationAngleDegree = hourAngle;
                OpenGL.LoadIdentity();
                this.LegacyTransform();

                hourLineWidthState.On();
                OpenGL.Begin(OpenGL.GL_LINES);
                for (int i = 0; i < hourPosition.Count; i++)
                {
                    vec3 color = hourColor[i];
                    OpenGL.Color3f(color.x, color.y, color.z);
                    vec3 position = hourPosition[i];
                    OpenGL.Vertex3f(position.x, position.y, position.z);
                }
                OpenGL.End();
                hourLineWidthState.Off();
            }
        }
    }
}