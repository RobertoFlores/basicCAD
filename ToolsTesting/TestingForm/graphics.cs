using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    
    class mgTools
    {        
        private int zoomControl;                            // Index of the current zoom level
        private int[] zoomLevels = new int[] { -5, 30 };    // Zoom levels avaiable
        private double scaleSymbolsFactor = 0.0025;         // Portion of the WindowSize.Y that a symbol will occupy
        private double ratioView;

        private OpenTK.Vector2d sizeSpace;                  // Size of the original spatial plane with any transformation
        private OpenTK.Vector2d sizeWindow;                 // Sise of the Spatial plane limited by the window
        private OpenTK.Vector2d sizeView;                   // Size of the View Port (Window) in pixels

        private OpenTK.Vector2d pointMinSpace;
        private OpenTK.Vector2d pointMaxSpace;
        private OpenTK.Vector2d pointMinWindow;
        private OpenTK.Vector2d pointMaxWindow;
        private OpenTK.Vector2d pointCenter;

        private OpenTK.Vector2d pointDragCoordinates;       // Initial mouse's coordinates for moveByDrag 

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
            get { return this.pointMinWindow.X; }
        }
        public double MinY
        {
            get { return this.pointMinWindow.Y; }
        }
        public double MaxX
        {
            get { return this.pointMaxWindow.X; }
        }
        public double MaxY
        {
            get { return this.pointMaxWindow.Y; }
        }


        public double SpaceWidth
        {
            get { return this.sizeWindow.X; }
        }
        public double SpaceHeight
        {
            get { return this.sizeWindow.Y; }
        }
        public double SpaceRatio
        {
            get { return this.ratioView; }
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
        

        // Constructors
        public mgTools(double mapScale, double[] coordinates, int[] window, int zControl)
        {
            GraphicGlobals.scaleDataBase = mapScale;
            this.sizeView = new OpenTK.Vector2d(window[0], window[1]);
            this.ratioView = this.sizeView.Y / this.sizeView.X;

            this.shiftMap = new OpenTK.Vector2d(0, 0);

            this.pointMinSpace = this.pointMinWindow = new OpenTK.Vector2d(coordinates[0], coordinates[1]);
            this.pointMaxSpace = new OpenTK.Vector2d(coordinates[2], coordinates[3]);

            this.sizeSpace = this.sizeWindow = this.pointMaxSpace - this.pointMinSpace;

            this.sizeSpace.Y = this.sizeWindow.Y = this.sizeSpace.X * this.ratioView;

            this.pointMaxWindow.Y = this.pointMaxSpace.Y = this.pointMinSpace.Y + this.sizeSpace.Y;
            //this.pointMaxWindow.Y = this.pointMinWindow.Y + this.sizeSpace.Y * this.ratio;

            //this.sizeWindow = this.sizeSpace = this.pointMaxWindow - this.pointMinWindow;

            setCenterPoint();

            this.zoomControl = zControl;

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

            this.pointMaxWindow = this.pointMinWindow + this.sizeWindow;;

            GraphicGlobals.scalePixel = this.sizeWindow.X / this.ViewWidth;

            GraphicGlobals.scaleSymbols = this.sizeWindow.Y * this.scaleSymbolsFactor;

            setCenterPoint();
        }
        
        public void setMousePosition(int x, int y)
        {
            GraphicGlobals.mousePosition = new OpenTK.Vector2d(x, this.ViewHieght - (y + 1));
            GraphicGlobals.mouseCartesian = GraphicGlobals.mousePosition * GraphicGlobals.scalePixel + this.pointMinWindow;
        }

        public void setCenterPoint()
        {
            this.pointCenter = this.pointMinWindow + this.sizeWindow / 2;
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
            OpenTK.Vector2d drag;

            drag = (GraphicGlobals.mouseCartesian - this.pointDragCoordinates);

            this.shiftMap += drag;
            this.pointMinWindow -= drag;
            this.pointMaxWindow -= drag;
            this.pointCenter -= drag;
        }

        public void moveByDrag(OpenTK.Vector2d iPos)
        {
            this.pointDragCoordinates = iPos;
        }

        public void moveTo()
        {
            centerOnPoint(GraphicGlobals.mouseCartesian);
        }

        public void moveTo(double[] newCenter)
        {
            centerOnPoint(newCenter);
        }

        public void moveTo(OpenTK.Vector2d newCenter)
        {
            centerOnPoint(newCenter);
        }

        private void moveLeft(double dsp)
        {
            this.shiftMap.X += dsp * this.sizeWindow.X;
            this.pointMinWindow.X -= dsp * this.sizeWindow.X;
            this.pointMaxWindow.X -= dsp * this.sizeWindow.X;
            setCenterPoint();
        }

        private void moveRight(double dsp)
        {
            this.shiftMap.X -= dsp * this.sizeWindow.X;
            this.pointMinWindow.X += dsp * this.sizeWindow.X;
            this.pointMaxWindow.X += dsp * this.sizeWindow.X;
            setCenterPoint();

        }

        private void moveUp(double dsp)
        {
            this.shiftMap.Y -= dsp * this.sizeWindow.Y;
            this.pointMinWindow.Y += dsp * this.sizeWindow.Y;
            this.pointMaxWindow.Y += dsp * this.sizeWindow.Y;
            setCenterPoint();

        }

        private void moveDown(double dsp)
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

        private void setZoomLevel()
        {
            switch (zoomControl)
            {
                case 30:
                    GraphicGlobals.scaleZoom = 6000;
                    break;
                case 29:
                    GraphicGlobals.scaleZoom = 5500;
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

            GraphicGlobals.scalePixel = this.sizeWindow.X / this.ViewWidth;
            GraphicGlobals.scaleSymbols = this.sizeWindow.Y * this.scaleSymbolsFactor;
        }



        private void centerOnPoint(double[] centerPoint)
        {
            OpenTK.Vector2d newCenter = new OpenTK.Vector2d(centerPoint[0], centerPoint[1]);
            this.pointMinWindow = newCenter - this.sizeWindow / 2;
            this.pointMaxWindow = newCenter + this.sizeWindow / 2;

            this.shiftWindow = this.pointMinWindow - this.pointMinSpace;
            this.shiftMap = (1 - GraphicGlobals.scaleZoom) * (this.pointMinSpace / GraphicGlobals.scaleZoom) - this.shiftWindow;
        }

        private void centerOnPoint(OpenTK.Vector2d newCenter)
        {
            this.pointMinWindow = newCenter - this.sizeWindow / 2;
            this.pointMaxWindow = newCenter + this.sizeWindow / 2;

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

    }

}
