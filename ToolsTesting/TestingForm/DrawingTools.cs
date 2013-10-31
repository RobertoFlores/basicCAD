using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

    /******************************
         *      DRAWINGTOOLS
         *      
         *      Classes set for drawing tools development 
         *      
         *      M.I. Luis Rodríguez Gil
         *      Jan / 25th / 2013
         *      
         * ***************************/
namespace DrawingTools
{
    class VirtualPoint
    {
        private Vector2d pointCartesian;
        private Vector2d pointGeographic;
        private Color symbolColor = Color.Black;

        public double X
        {
            get { return this.pointCartesian.X; }
        }
        public double Y
        {
            get { return this.pointCartesian.Y; }
        }
        public double Lat
        {
            get { return this.pointGeographic.Y; }
        }
        public double Lon
        {
            get { return this.pointGeographic.X; }
        }
        public Vector2d Cartesian
        {
            get { return this.pointCartesian; }
            set { this.pointCartesian = value; }
        }
        public Vector2d Geographic
        {
            get { return this.pointGeographic; }
            set { this.pointGeographic = value; }
        }


        public VirtualPoint()
        {
            this.pointCartesian = Vector2d.Zero;
            this.pointGeographic = Vector2d.Zero;
        }

        public VirtualPoint(Vector2d mouseCartesian, Vector2d mouseGeographic)
        {
            this.pointCartesian = mouseCartesian;
            this.pointGeographic = mouseGeographic;
        }

        public VirtualPoint(double x, double y, double lon, double lat)
        {
            this.pointCartesian = new Vector2d(x, y);
            this.pointGeographic = new Vector2d(lon, lat);
        }
    }

    class VirtualRectangle
    {
        private List<VirtualPoint> pointList;
        private Color lineColor;
        private float lineWidth;

        public OpenTK.Vector2d FirstPoint
        {
            get { return this.pointList[0].Cartesian; }
        }
        public OpenTK.Vector2d LastPoint
        {
            get { return this.pointList[2].Cartesian; }
        }


        public VirtualRectangle()
        {
            this.pointList = new List<VirtualPoint>();
            this.pointList.Add(new VirtualPoint());
            this.pointList.Add(new VirtualPoint());
            this.pointList.Add(new VirtualPoint());
            this.pointList.Add(new VirtualPoint());
            this.lineColor = Color.LawnGreen;
            this.lineWidth = 1.0f;
        }

        public VirtualRectangle(VirtualPoint firstPoint)
        {
            this.pointList = new List<VirtualPoint>();

            this.pointList.Add(firstPoint);
            this.pointList.Add(firstPoint);
            this.pointList.Add(firstPoint);
            this.pointList.Add(firstPoint);

            this.lineColor = Color.LawnGreen;
            this.lineWidth = 1.0f;
        }

        public void setLastPoint(Vector2d mouseCartesian, Vector2d mouseGeographic)
        {
            this.pointList[1] = new VirtualPoint(mouseCartesian.X, this.pointList[0].Y, mouseGeographic.X, this.pointList[0].Lat);
            this.pointList[2] = new VirtualPoint(mouseCartesian, mouseGeographic);
            this.pointList[3] = new VirtualPoint(this.pointList[0].X, mouseCartesian.Y, this.pointList[0].Lon, mouseGeographic.Y);
        }

        public void clearCoordinates()
        {
            for (int i = 0; i < this.pointList.Count; i++)
            {
                this.pointList[i] = new VirtualPoint();
            }
        }

        public void render(double scaleDataBase)
        {
            GL.PushMatrix();
                GL.Color3(this.lineColor);
                GL.LineWidth(this.lineWidth);

                GL.Begin(BeginMode.LineLoop);

                for (int i = 0; i < this.pointList.Count; i++)
                {
                    GL.Vertex2(this.pointList[i].X / scaleDataBase, this.pointList[i].Y / scaleDataBase);
                }

                GL.End();
            GL.PopMatrix();
        }

        ~VirtualRectangle()
        {
            pointList.Clear();
        }
    }
}
