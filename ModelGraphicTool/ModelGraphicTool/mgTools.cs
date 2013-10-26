using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelGraphicTool
{
    /******************************
         *      MODEL'S GRAPHIC TOOL
         *      
         *      The objective of this class is to serve as support for the handling of spatial 
         *      coordinates of OpenGL's Cartesian plane. Its key features include Zoom, Shift 
         *      and Spatial Location
         *      
         *      M.I. Luis Rodríguez Gil
         *      Jan / 25th / 2013
         *      
         * ***************************/

    public class mgTools
    {
        private int zoomControl;                            // Index of the current zoom level
        private int[] zoomLevels = new int[] { -5, 35 };    // Zoom levels avaiable
        private double scaleSymbolsFactor;                  // Portion of the WindowSize that a symbol will occupy in pixelScale
        private double ratioView;

        private OpenTK.Vector2d sizeSpace;                  // Size of the original spatial plane with any transformation
        private OpenTK.Vector2d sizeWindow;                 // Sise of the Spatial plane limited by the window
        private OpenTK.Vector2d sizeView;                   // Size of the View Port (Window) in pixels

        private OpenTK.Vector2d pointMinSpace;
        private OpenTK.Vector2d pointMaxSpace;
        private OpenTK.Vector2d pointMinWindow;
        private OpenTK.Vector2d pointMaxWindow;
        private OpenTK.Vector2d pointCenterOriginal;
        private OpenTK.Vector2d pointCenter;

        private OpenTK.Vector2d pointDragCoordinates;       // Initial coordinates of the mouse for moveByDrag 
        private OpenTK.Vector2d pointZoomCoordinates;       // Initial coordinates of the mouse for zoomByWindow


        private OpenTK.Vector2d shiftMap;
        private OpenTK.Vector2d shiftWindow;


        // Properties
        public double oMinX
        {
            get { return this.pointMinSpace.X; }
        }
        public double oMinY
        {
            get { return this.pointMinSpace.Y; }
        }
        public double oMaxX
        {
            get { return this.pointMaxSpace.X; }
        }
        public double oMaxY
        {
            get { return this.pointMaxSpace.Y; }
        }


        public double MinX
        {
            get { return Math.Truncate(this.pointMinWindow.X * GraphicGlobals.scaleDataBase); }
        }
        public double MinY
        {
            get { return Math.Truncate(this.pointMinWindow.Y * GraphicGlobals.scaleDataBase); }
        }
        public double MaxX
        {
            get { return Math.Truncate(this.pointMaxWindow.X * GraphicGlobals.scaleDataBase); }
        }
        public double MaxY
        {
            get { return Math.Truncate(this.pointMaxWindow.Y * GraphicGlobals.scaleDataBase); }
        }


        public double WindowWidth
        {
            get { return Math.Truncate(this.sizeWindow.X * GraphicGlobals.scaleDataBase); }
        }
        public double WindowHeight
        {
            get { return Math.Truncate(this.sizeWindow.Y * GraphicGlobals.scaleDataBase); }
        }
        public double SpaceRatio
        {
            get { return this.ratioView; }
        }
        public double Ratio
        {
            get { return this.sizeView.X / this.sizeView.Y; }
        }
        public int ViewWidth
        {
            get { return (int)this.sizeView.X; }
        }
        public int ViewHieght
        {
            get { return (int)this.sizeView.Y; }
        }


        public OpenTK.Vector2d Shift
        {
            get { return this.shiftMap; }
        }
        public OpenTK.Vector2d MouseCartesian
        {
            get { return GraphicGlobals.mouseCartesian * GraphicGlobals.scaleDataBase; }
        }
        public OpenTK.Vector2d MouseGeographic
        {
            get { return GraphicGlobals.mouseGeographic; }
            set { GraphicGlobals.mouseGeographic = value; }
        }

        public double ScaleDB
        {
            get { return GraphicGlobals.scaleDataBase; }
        }
        public double ScaleZoom
        {
            get { return GraphicGlobals.scaleZoom; }
        }
        public double ScaleSymbol
        {
            get { return GraphicGlobals.scaleSymbols; }
        }
        public double ScaleWindow
        {
            get { return GraphicGlobals.scaleWindow; } 
        }

        // Constructors
        public mgTools(double sFactor, double[] coordinates, int[] window, int zControl)
        {
            this.scaleSymbolsFactor = sFactor;
            this.zoomControl = zControl;

            this.sizeView = new OpenTK.Vector2d(window[0], window[1]);
            this.ratioView = this.sizeView.Y / this.sizeView.X;

            this.pointMinSpace = this.pointMinWindow = new OpenTK.Vector2d(coordinates[0], coordinates[1]);
            this.pointMaxSpace = new OpenTK.Vector2d(coordinates[2], coordinates[3]);

            this.sizeSpace = this.sizeWindow = this.pointMaxSpace - this.pointMinSpace;

            this.sizeSpace.X = this.sizeWindow.X = Math.Abs(this.sizeSpace.X);
            this.sizeSpace.Y = this.sizeWindow.Y = Math.Abs(this.sizeSpace.Y);


            this.sizeSpace.Y = this.sizeWindow.Y = this.sizeSpace.X * this.ratioView;

            this.pointMaxWindow.Y = this.pointMaxSpace.Y = this.pointMinSpace.Y + this.sizeSpace.Y;

            setCenterPoint();
            this.pointCenterOriginal = this.pointCenter;


            this.shiftMap = OpenTK.Vector2d.Zero;
            this.pointDragCoordinates = OpenTK.Vector2d.Zero;
            this.pointZoomCoordinates = OpenTK.Vector2d.Zero;

            setZoomLevel();
        }


        // Methods
        public void updateWindow(int glcWidth, int glcHeight)
        {
            this.sizeSpace.X = (this.sizeSpace.X * glcWidth / this.sizeView.X);
            this.sizeSpace.Y = (this.sizeSpace.Y * glcHeight / this.sizeView.Y);

            this.pointMaxSpace = this.pointMinSpace + this.sizeSpace;

            this.sizeView = new OpenTK.Vector2d(glcWidth, glcHeight);

            this.ratioView = this.sizeView.Y / this.sizeView.X;

            this.sizeWindow = this.sizeSpace / GraphicGlobals.scaleZoom;

            this.pointMaxWindow = this.pointMinWindow + this.sizeWindow; ;

            setGlobalScales();

            setCenterPoint();

        }

        public void setMousePosition(int x, int y)
        {
            GraphicGlobals.mousePosition = new OpenTK.Vector2d(x, this.ViewHieght - (y + 1));
            GraphicGlobals.mouseCartesian = GraphicGlobals.mousePosition * GraphicGlobals.scalePixel + this.pointMinWindow;

        }

        private void setCenterPoint()
        {
            this.pointCenter = this.pointMinWindow + this.sizeWindow / 2;
        }
        
        public OpenTK.Vector2d pointToCartesian(int x, int y)
        {
            OpenTK.Vector2d tPoint = new OpenTK.Vector2d(x, y);
            tPoint = tPoint * GraphicGlobals.scalePixel + this.pointMinWindow;

            return tPoint;
        }



        public void moveByBorders()
        {
            if (GraphicGlobals.mousePosition.X <= (int)(this.sizeView.X * 0.10))
            {
                this.moveLeft(0.20);
            }
            else if (GraphicGlobals.mousePosition.X >= (int)(this.sizeView.X * 0.90))
            {
                this.moveRight(0.20);
            }
            else if (GraphicGlobals.mousePosition.Y <= (int)(this.sizeView.Y * 0.10))
            {
                this.moveDown(0.20);
            }
            else if (GraphicGlobals.mousePosition.Y >= (int)(this.sizeView.Y * 0.90))
            {
                this.moveUp(0.20);
            }

        }

        public void moveByDrag()
        {
            if (this.pointDragCoordinates != OpenTK.Vector2d.Zero)
            {
                OpenTK.Vector2d drag;

                drag = (GraphicGlobals.mouseCartesian - this.pointDragCoordinates);

                this.shiftMap += drag;
                this.pointMinWindow -= drag;
                this.pointMaxWindow -= drag;
                this.pointCenter -= drag;
            }
        }

        public void moveByDrag(OpenTK.Vector2d iPoint)
        {
            this.pointDragCoordinates = iPoint;
        }

        public void moveTo()
        {
            centerOnPoint(GraphicGlobals.mouseCartesian);
        }

        public void moveTo(double[] newCenter)
        {

            OpenTK.Vector2d newPoint = new OpenTK.Vector2d(newCenter[0] / GraphicGlobals.scaleDataBase, newCenter[1] / GraphicGlobals.scaleDataBase);

            centerOnPoint(newPoint);
        }

        public void moveTo(OpenTK.Vector2d newCenter)
        {
            centerOnPoint(newCenter / GraphicGlobals.scaleDataBase);
        }

        public void moveLeft(double dsp)
        {
            this.shiftMap.X += dsp * this.sizeWindow.X;
            this.pointMinWindow.X -= dsp * this.sizeWindow.X;
            this.pointMaxWindow.X -= dsp * this.sizeWindow.X;
            setCenterPoint();
        }

        public void moveRight(double dsp)
        {
            this.shiftMap.X -= dsp * this.sizeWindow.X;
            this.pointMinWindow.X += dsp * this.sizeWindow.X;
            this.pointMaxWindow.X += dsp * this.sizeWindow.X;
            setCenterPoint();

        }

        public void moveUp(double dsp)
        {
            this.shiftMap.Y -= dsp * this.sizeWindow.Y;
            this.pointMinWindow.Y += dsp * this.sizeWindow.Y;
            this.pointMaxWindow.Y += dsp * this.sizeWindow.Y;
            setCenterPoint();

        }

        public void moveDown(double dsp)
        {
            this.shiftMap.Y += dsp * this.sizeWindow.Y;
            this.pointMinWindow.Y -= dsp * this.sizeWindow.Y;
            this.pointMaxWindow.Y -= dsp * this.sizeWindow.Y;
            setCenterPoint();

        }


        public void zoomIn(OpenTK.Vector2d newCenter)
        {
            zoomControl++;

            if (zoomControl > zoomLevels[1])
            {
                zoomControl = zoomLevels[1];
            }

            setZoomLevel();

            centerOnPoint(newCenter);
        }

        public void zoomIn()
        {
            zoomControl++;

            if (zoomControl > zoomLevels[1])
            {
                zoomControl = zoomLevels[1];
            }

            setZoomLevel();

            centerOnPoint();
        }

        public void zoomOut(OpenTK.Vector2d newCenter)
        {
            zoomControl--;
            if (zoomControl < zoomLevels[0])
            {
                zoomControl = zoomLevels[0];
            }

            setZoomLevel();

            centerOnPoint(newCenter);
        }

        public void zoomOut()
        {
            zoomControl--;

            if (zoomControl < zoomLevels[0])
            {
                zoomControl = zoomLevels[0];
            }

            setZoomLevel();

            centerOnPoint();
        }

        public void zoomByScroll(int delta)
        {
            if (delta < 0)
            {
                zoomOut(GraphicGlobals.mouseCartesian);
            }
            else if (delta > 0)
            {
                zoomIn(GraphicGlobals.mouseCartesian);
            }
        }

        public void zoomExtend(OpenTK.Vector2d newCenter)
        {
            zoomControl = 0;

            setZoomLevel();

            centerOnPoint(newCenter);
        }

        public void zoomExtend()
        {
            zoomControl = 0;

            setZoomLevel();

            centerOnPoint(this.pointCenterOriginal);
        }

        public void zoomByWindow(OpenTK.Vector2d iPoint, OpenTK.Vector2d fPoint)
        {
            OpenTK.Vector2d newSize = new OpenTK.Vector2d();

            newSize.X = Math.Abs(fPoint.X - iPoint.X) / GraphicGlobals.scaleDataBase;
            newSize.Y = Math.Abs(fPoint.Y - iPoint.Y) / GraphicGlobals.scaleDataBase;

            while ((newSize.X < this.sizeWindow.X && newSize.Y < this.sizeWindow.Y) && zoomControl < zoomLevels[1])
            {
                zoomIn(getCenter(iPoint / GraphicGlobals.scaleDataBase, fPoint / GraphicGlobals.scaleDataBase, newSize / 2));
            };

        }

        public void zoomByWindow(OpenTK.Vector2d centralPoint, double distance)
        {
            OpenTK.Vector2d newSize = new OpenTK.Vector2d(distance, distance);
            
            while ((newSize.X < this.sizeWindow.X && newSize.Y < this.sizeWindow.Y) && zoomControl < zoomLevels[1])
            {
                zoomIn(centralPoint);
            };

        }

        private void setZoomLevel()
        {
            switch (zoomControl)
            {
                case 35:
                    GraphicGlobals.scaleZoom = 20000;
                    break;
                case 34:
                    GraphicGlobals.scaleZoom = 17000;
                    break;
                case 33:
                    GraphicGlobals.scaleZoom = 15000;
                    break;
                case 32:
                    GraphicGlobals.scaleZoom = 13000;
                    break;
                case 31:
                    GraphicGlobals.scaleZoom = 11000;
                    break;
                case 30:
                    GraphicGlobals.scaleZoom = 9000;
                    break;
                case 29:
                    GraphicGlobals.scaleZoom = 6000;
                    break;
                case 28:
                    GraphicGlobals.scaleZoom = 5000;
                    break;
                case 27:
                    GraphicGlobals.scaleZoom = 4500;
                    break;
                case 26:
                    GraphicGlobals.scaleZoom = 4000;
                    break;
                case 25:
                    GraphicGlobals.scaleZoom = 3600;
                    break;
                case 24:
                    GraphicGlobals.scaleZoom = 3200;
                    break;
                case 23:
                    GraphicGlobals.scaleZoom = 2800;
                    break;
                case 22:
                    GraphicGlobals.scaleZoom = 2400;
                    break;
                case 21:
                    GraphicGlobals.scaleZoom = 2000;
                    break;
                case 20:
                    GraphicGlobals.scaleZoom = 1800;
                    break;
                case 19:
                    GraphicGlobals.scaleZoom = 1600;
                    break;
                case 18:
                    GraphicGlobals.scaleZoom = 1400;
                    break;
                case 17:
                    GraphicGlobals.scaleZoom = 1200;
                    break;
                case 16:
                    GraphicGlobals.scaleZoom = 1000;
                    break;
                case 15:
                    GraphicGlobals.scaleZoom = 800;
                    break;
                case 14:
                    GraphicGlobals.scaleZoom = 600;
                    break;
                case 13:
                    GraphicGlobals.scaleZoom = 400;
                    break;
                case 12:
                    GraphicGlobals.scaleZoom = 200;
                    break;
                case 11:
                    GraphicGlobals.scaleZoom = 100;
                    break;
                case 10:
                    GraphicGlobals.scaleZoom = 80;
                    break;
                case 9:
                    GraphicGlobals.scaleZoom = 60;
                    break;
                case 8:
                    GraphicGlobals.scaleZoom = 40;
                    break;
                case 7:
                    GraphicGlobals.scaleZoom = 20;
                    break;
                case 6:
                    GraphicGlobals.scaleZoom = 10;
                    break;
                case 5:
                    GraphicGlobals.scaleZoom = 8;
                    break;
                case 4:
                    GraphicGlobals.scaleZoom = 6;
                    break;
                case 3:
                    GraphicGlobals.scaleZoom = 4;
                    break;
                case 2:
                    GraphicGlobals.scaleZoom = 2;
                    break;
                case 1:
                    GraphicGlobals.scaleZoom = 1.2;
                    break;
                case 0:
                    GraphicGlobals.scaleZoom = 1;
                    break;
                case -1:
                    GraphicGlobals.scaleZoom = 0.8;
                    break;
                case -2:
                    GraphicGlobals.scaleZoom = 0.5;
                    break;
                case -3:
                    GraphicGlobals.scaleZoom = 0.4;
                    break;
                case -4:
                    GraphicGlobals.scaleZoom = 0.2;
                    break;
                case -5:
                    GraphicGlobals.scaleZoom = 0.1;
                    break;
            }

            this.sizeWindow = this.sizeSpace / GraphicGlobals.scaleZoom;

            //GraphicGlobals.scalePixel = this.sizeWindow.X / this.ViewWidth;
            setGlobalScales();
            //GraphicGlobals.scaleSymbols = this.sizeWindow.X * this.scaleSymbolsFactor;
        }

        private void setGlobalScales()
        {
            GraphicGlobals.scalePixel = this.sizeWindow.X / this.ViewWidth;

            GraphicGlobals.scaleWindow = GraphicGlobals.scalePixel * GraphicGlobals.scaleDataBase * 1000;
            //GraphicGlobals.scaleSymbols = this.sizeWindow.X * this.scaleSymbolsFactor;
            GraphicGlobals.scaleSymbols = GraphicGlobals.scalePixel * this.scaleSymbolsFactor;
            GraphicGlobals.radiusRange = Math.Pow(GraphicGlobals.scalePixel * 500, 2);
            GraphicGlobals.radiusPolygon = Math.Pow(GraphicGlobals.scalePixel * 500, 2) * 5;
        }


        private void centerOnPoint(OpenTK.Vector2d newCenter)
        {
            this.pointCenter = newCenter;
            this.pointMinWindow = pointCenter - this.sizeWindow / 2;
            this.pointMaxWindow = pointCenter + this.sizeWindow / 2;

            this.shiftWindow = this.pointMinWindow - this.pointMinSpace;
            this.shiftMap = (1 - GraphicGlobals.scaleZoom) * (this.pointMinSpace / GraphicGlobals.scaleZoom) - this.shiftWindow;
        }

        private void centerOnPoint()
        {
            this.pointMinWindow = this.pointCenter - this.sizeWindow / 2;
            this.pointMaxWindow = this.pointCenter + this.sizeWindow / 2;

            this.shiftWindow = this.pointMinWindow - this.pointMinSpace;
            this.shiftMap = (1 - GraphicGlobals.scaleZoom) * (this.pointMinSpace / GraphicGlobals.scaleZoom) - this.shiftWindow;
        }

        private OpenTK.Vector2d getCenter(OpenTK.Vector2d iPoint, OpenTK.Vector2d fPoint, OpenTK.Vector2d halfSize)
        {
            OpenTK.Vector2d newCenter = new OpenTK.Vector2d();

            if (iPoint.X > fPoint.X)
            {
                newCenter.X = fPoint.X + halfSize.X;
            }
            else
            {
                newCenter.X = iPoint.X + halfSize.X;
            }

            if (iPoint.Y > fPoint.Y)
            {
                newCenter.Y = fPoint.Y + halfSize.Y;
            }
            else
            {
                newCenter.Y = iPoint.Y + halfSize.Y;
            }

            return newCenter;
        }
        


        public OpenTK.Vector2d getScaleBarPoint()
        {
            OpenTK.Vector2d pointCorner2 = this.pointToCartesian(20, 30);

            if (this.sizeWindow.X < 0.01)
            {
                pointCorner2.X += 0.001;
                return pointCorner2;
            }
            else if (this.sizeWindow.X >= 0.01 && this.sizeWindow.X < 0.1)
            {
                pointCorner2.X += 0.01;
                return pointCorner2;
            }
            else if (this.sizeWindow.X >= 0.1 && this.sizeWindow.X < 1)
            {
                pointCorner2.X += 0.1;
                return pointCorner2;
            }
            else if (this.sizeWindow.X >= 1 && this.sizeWindow.X < 10)
            {
                pointCorner2.X += 1;
                return pointCorner2;
            }
            else if (this.sizeWindow.X >= 10 && this.sizeWindow.X < 100)
            {
                pointCorner2.X += 10;
                return pointCorner2;
            }
            else if (this.sizeWindow.X >= 100 && this.sizeWindow.X < 1000)
            {
                pointCorner2.X += 100;
                return pointCorner2;
            }
            else if (this.sizeWindow.X >= 1000 && this.sizeWindow.X < 10000)
            {
                pointCorner2.X += 1000;
                return pointCorner2;
            }
            else //(this.sizeWindow.X >= 100 && this.sizeWindow.X <= 1000)
            {
                pointCorner2.X += 10000;
                return pointCorner2;
            }

        }

    
    }

}
