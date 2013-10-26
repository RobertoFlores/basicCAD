using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ModelGraphicTool;
using DrawingTools;

namespace TestingForm
{
    class Line
    {
        public Vector2d StartPoint;
        public Vector2d EndPoint;
        public Color Color;
        public Vector2d startpoint
        {
            set
            {
                StartPoint = value;
            }
            get
            {
                return StartPoint;
            }
        }
        public Vector2d endpoint
        {
            set
            {
                EndPoint = value;
            }
            get
            {
                return EndPoint;
            }
        }
        

        public Line(Vector2d startPoint, Vector2d endPoint, Color color)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Color = color;
        }

        public void Draw()
        {
            GL.Color3(Color);

            GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Lines);
            {
                GL.Vertex2(StartPoint / GraphicGlobals.scaleDataBase);
                GL.Vertex2(EndPoint / GraphicGlobals.scaleDataBase);
            }
            GL.End();
        }
    }
}