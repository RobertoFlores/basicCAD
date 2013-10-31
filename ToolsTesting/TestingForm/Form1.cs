using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using OpenTK.Graphics;
using OpenGL = OpenTK.Graphics.OpenGL;
using GL = OpenTK.Graphics.OpenGL.GL;
using ModelGraphicTool;
using DrawingTools;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics.ES10;
using OpenTK;

    /******************************
         *      TESTING FORM
         *      
         *      This Form provides a proper initial OpenTK configuration of a spatial plane, animation and mouse/keyboard interaction
		 * 		for easy and fast test and tools development. Enjoy!	
         *      
         *      M.I. Luis Rodríguez Gil
         *      Jan / 25th / 2013
         *      
         * ***************************/
namespace TestingForm
{
    public partial class Form1 : Form
    {
        private readonly List<Line> _lines = new List<Line>();
        private Vector2d _currentStartPoint;
        private Vector2d _currentEndPoint;
        private Color _drawColor = Color.Yellow;
        
        bool glControlLoaded = false;
        double rotation = 0;
        double accumulator = 0;
        int idleCounter = 0;
        int FPS = 0;
        Stopwatch sw = new Stopwatch();
        
        mgTools gTool;
        VirtualRectangle zWindow = new VirtualRectangle();


        int dlTriangle = 0;
        int dlCircle = 0;
        int dlSquare = 0;
        int dlGrid = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            this.glControlLoaded = true;

            int zoomControl = 0;
            GraphicGlobals.scaleDataBase = 1000;
            double symbolsFactor = 3;

            double[] spaceCoords = new double[] {   -11000000 / GraphicGlobals.scaleDataBase, 
                                                    2000000 / GraphicGlobals.scaleDataBase,
                                                    -8000000 / GraphicGlobals.scaleDataBase,
                                                    4000000 / GraphicGlobals.scaleDataBase };

            int[] windowSize = new int[] {  glControl1.Width, 
                                            glControl1.Height };


            gTool = new mgTools( symbolsFactor, spaceCoords, windowSize, zoomControl);

            GL.ClearColor(Color.DarkGray);
            setupViewport();
            Application.Idle += Application_Idle;
            sw.Start();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            render();
            
           
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            setupViewport();
            glControl1.Invalidate();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            double milliseconds = ComputeTimeSlice();
            Accumulate(milliseconds);
            Animate(milliseconds);
        }


        private void setupViewport()
        {
            if (!glControlLoaded)
            {
                return;
            }
            gTool.updateWindow(glControl1.Width, glControl1.Height);

            GL.MatrixMode(OpenGL.MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Ortho(gTool.oMinX, gTool.oMaxX, gTool.oMinY, gTool.oMaxY, -3, 3);
            GL.Viewport(0, 0, gTool.ViewWidth, gTool.ViewHieght);

            GL.MatrixMode(OpenGL.MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        private double ComputeTimeSlice()
        {
            sw.Stop();
            double timeslice = sw.Elapsed.TotalMilliseconds;
            sw.Reset();
            sw.Start();
            return timeslice;
        }

        private void Animate(double milliseconds)
        {
            float deltaRotation = (float)milliseconds / 20.0f;
            rotation += deltaRotation;
            glControl1.Invalidate();
        }

        private void Accumulate(double milliseconds)
        {
            idleCounter++;
            accumulator += milliseconds;
            console();
            if (accumulator > 1000)
            {
                FPS = idleCounter;

                accumulator -= 1000;
                idleCounter = 0;
            }
        }


        private void render()
        {
            if (!glControlLoaded)
                return;

            GL.Clear(OpenGL.ClearBufferMask.ColorBufferBit | OpenGL.ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(OpenGL.MatrixMode.Modelview);
            GL.LoadIdentity();
            Matrix4 lookat = Matrix4.LookAt(new Vector3(0.0f, 0.0f, 3.0f), Vector3.Zero, Vector3.UnitY);
   
            GL.LoadMatrix(ref lookat);

            GL.Disable(OpenGL.EnableCap.Texture2D);
            GL.PushMatrix();
            GL.Scale(GraphicGlobals.scaleZoom, GraphicGlobals.scaleZoom, 1);
            GL.Translate(gTool.Shift.X, gTool.Shift.Y, 0);

                renderGrid();
                GL.LineWidth(2.0f);


                foreach (var line in _lines)
                {
                    line.Draw();
                    
                }


                GL.Color3(_drawColor);


                GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Lines);
                {
                    GL.Vertex2(_currentStartPoint  / GraphicGlobals.scaleDataBase);
                    GL.Vertex2(_currentEndPoint / GraphicGlobals.scaleDataBase);
                }
                GL.End();

                GL.PushMatrix();

                    GL.Rotate(rotation, 0, 0, 1);

                    renderTriangle();

                GL.PopMatrix();

                renderCircle(1000 / GraphicGlobals.scaleDataBase, -1000 / GraphicGlobals.scaleDataBase, 2000 / GraphicGlobals.scaleDataBase, Color.Red);

                renderCircle(20000 / GraphicGlobals.scaleDataBase, -10000000 / GraphicGlobals.scaleDataBase, 2000000 / GraphicGlobals.scaleDataBase, Color.Red);

                renderCircle(2 / GraphicGlobals.scaleDataBase, -10000000 / GraphicGlobals.scaleDataBase, 2000000 / GraphicGlobals.scaleDataBase, Color.Black);

                renderSquare(2000 / GraphicGlobals.scaleDataBase, -1000 / GraphicGlobals.scaleDataBase, -1000 / GraphicGlobals.scaleDataBase, Color.Blue);

                zWindow.render(GraphicGlobals.scaleDataBase);

                GL.PushMatrix();

                    GL.Translate(-8000000 / GraphicGlobals.scaleDataBase, 3000000 / GraphicGlobals.scaleDataBase, 0);
                    GL.Scale(GraphicGlobals.scaleSymbols, GraphicGlobals.scaleSymbols, 1);
                
                    renderSquare(10000 / GraphicGlobals.scaleDataBase, 0, 0, Color.Blue);

                    renderSquare(2 / GraphicGlobals.scaleDataBase, 0, 0, Color.Red);
                
                GL.PopMatrix();

                drawScaleBar();

            GL.PopMatrix();
            
            GL.Flush();

            glControl1.SwapBuffers();

        }

        private void console()
        {

            slmouseCartesian.Text = Math.Truncate(gTool.MouseCartesian.X * 10000) / 10000 + ", " + Math.Truncate(gTool.MouseCartesian.Y * 10000) / 10000;
            slFPS.Text = this.FPS + "FPS";
            slZoom.Text = GraphicGlobals.scaleZoom + "x";
            slLimits.Text = "(" + gTool.MinX + ", " + gTool.MinY + " )" 
                + " (" + gTool.MaxX + ", " + gTool.MaxY + ")";

        }

        private void renderTriangle()
        {
            if (dlTriangle == 0)
            {
                dlTriangle = GL.GenLists(1);

                GL.NewList(dlTriangle, OpenGL.ListMode.CompileAndExecute);
                GL.Color3(Color.White);
                GL.Begin(OpenGL.BeginMode.Triangles);
                GL.Vertex2(0, 0);
                GL.Vertex2(1000 / GraphicGlobals.scaleDataBase, 2000 / GraphicGlobals.scaleDataBase);
                GL.Vertex2(1000 / GraphicGlobals.scaleDataBase, 5000 / GraphicGlobals.scaleDataBase);
                GL.End();
                GL.EndList();
            }
            else
            {
                GL.CallList(dlTriangle);
            }
        }

        private void renderCircle(double radius, double shiftX, double shiftY, Color fill)
        {
            
            {
      

                double angle;

                GL.Color3(fill);
                GL.Begin(OpenGL.BeginMode.Polygon);

                for (int i = 0; i <= 100; i += 2)
                {
                    angle = i * 2 * Math.PI / 100;
                    GL.Vertex2(Math.Cos(angle) * radius + shiftX, Math.Sin(angle) * radius + shiftY);
                }

                GL.End();
  
            }

        }

        private void renderSquare(double side, double shiftX, double shiftY, Color fill)
        {

            {
       
                GL.Color3(fill);
                GL.Begin(OpenGL.BeginMode.Polygon);

                GL.Vertex2(-side / 2 + shiftX, side / 2 + shiftY);
                GL.Vertex2(side / 2 + shiftX, side / 2 + shiftY);
                GL.Vertex2(side / 2 + shiftX, -side / 2 + shiftY);
                GL.Vertex2(-side / 2 + shiftX, -side / 2 + shiftY);
                
                GL.End();
     
            }

        }

        private void renderGrid()
        {
            if (dlGrid == 0)
            {

                dlGrid = GL.GenLists(1);

                GL.NewList(dlGrid, OpenGL.ListMode.CompileAndExecute);

                GL.Begin(OpenGL.BeginMode.Lines);
                GL.Color3(Color.LightGray);
                for (int i = -1000 * (int)GraphicGlobals.scaleDataBase; i < 1000 * (int)GraphicGlobals.scaleDataBase; i += (int)GraphicGlobals.scaleDataBase)
                {
                    GL.Vertex3(-10000 * GraphicGlobals.scaleDataBase, (-i * 1000) / GraphicGlobals.scaleDataBase, 0);
                    GL.Vertex3(10000 * GraphicGlobals.scaleDataBase, (-i * 1000) / GraphicGlobals.scaleDataBase, 0);

                    GL.Vertex3((-i * 1000) / GraphicGlobals.scaleDataBase, -10000 * GraphicGlobals.scaleDataBase, 0);
                    GL.Vertex3((-i * 1000) / GraphicGlobals.scaleDataBase, 10000 * GraphicGlobals.scaleDataBase, 0);

                }

                GL.Color3(Color.Black);
                GL.Vertex3(-1000 * (int)GraphicGlobals.scaleDataBase, 0, 0);
                GL.Vertex3(1000 * (int)GraphicGlobals.scaleDataBase, 0, 0);

                GL.Vertex3(0, 1000 * (int)GraphicGlobals.scaleDataBase, 0);
                GL.Vertex3(0, -1000 * (int)GraphicGlobals.scaleDataBase, 0);

                GL.End();
                GL.EndList();
            }
            else
            {
                GL.CallList(this.dlGrid);
            }

        }

        private void drawScaleBar()
        {
            OpenTK.Vector2d pointCorner1 = gTool.pointToCartesian(20, 20);
            OpenTK.Vector2d pointCorner2 = gTool.getScaleBarPoint();

            GL.Begin(OpenGL.BeginMode.Polygon);
            GL.Color3(Color.White);

            GL.Vertex2(pointCorner1);
            GL.Vertex2(pointCorner2.X, pointCorner1.Y);
            GL.Vertex2(pointCorner2);
            GL.Vertex2(pointCorner1.X, pointCorner2.Y);
            GL.End();

        }


        private void configTools(globals.toolCatalog currentTool)
        {
            switch (currentTool)
            {
                case globals.toolCatalog.none:
                    {
                        endTools();
                        break;
                    }

                case globals.toolCatalog.drag:
                    {
                        endTools();
                        globals.mouseModeDrag = true;
                        bDrag.Checked = true;
                        break;
                    }

                case globals.toolCatalog.zoomWindow:
                    {
                        endTools();
                        globals.mouseModeZoom = true;
                        bZoomWindow.Checked = true;
                        break;
                    }
                case globals.toolCatalog.line:
                    {
                        endTools();
                        globals.mouseModeLine = true;
                        lineToolStripButton.Checked = true;
                        break;
                    }
             }

         }

        private void endTools()
        {
            globals.mouseModeDrag = false;
            globals.mouseModeZoom = false;
            globals.mouseModeLine = false;

            lineToolStripButton.Checked = false;
            bDrag.Checked = false;
            bZoomWindow.Checked = false;
            zWindow.clearCoordinates();

        }


        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            gTool.setMousePosition(e.X, e.Y);
            globals.mouseGeographic = OpenTK.Vector2d.Zero;

            if (globals.mouseDown)
            {
                if (globals.mouseModeDrag)
                {
                    gTool.moveByDrag();
                }
                else if (globals.mouseModeZoom)
                {
                    zWindow.setLastPoint(gTool.MouseCartesian, globals.mouseGeographic);
                }

                else if (globals.mouseModeLine)
                {
                    _currentEndPoint = gTool.MouseCartesian;
                }  
            }
        }

        private void glControl1_MouseScroll(object sender, MouseEventArgs e)
        {
            gTool.zoomByScroll(e.Delta);

        }

        private void glControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
               globals.mouseDown = true;

             gTool.moveByDrag(GraphicGlobals.mouseCartesian);

               if (globals.mouseModeLine)
               {
                    
                    _currentEndPoint = gTool.MouseCartesian;
                }
            }
            
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                globals.mouseDown = true;

                gTool.moveByDrag(GraphicGlobals.mouseCartesian);

                if (globals.mouseModeZoom)
                {
                     zWindow = new VirtualRectangle(new VirtualPoint(gTool.MouseCartesian, globals.mouseGeographic));
                }
            
                else if (globals.mouseModeLine)
                {
                    _currentStartPoint = gTool.MouseCartesian;
                    _currentEndPoint = gTool.MouseCartesian;
                }
            }
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                globals.mouseDown = false;

                if (globals.mouseModeZoom)
                {
                    gTool.zoomByWindow(zWindow.FirstPoint, zWindow.LastPoint);
                    zWindow.clearCoordinates();
                    endTools();
                }

                if (globals.mouseModeDrag)
                {
                    configTools(globals.toolCatalog.drag);
                }
                if (globals.mouseModeLine)
                {
                    Line newLine = new Line(_currentStartPoint, _currentEndPoint, _drawColor);
                    _lines.Add(newLine);


              
                }
            }
        }



        private void bZoomExtent_Click(object sender, EventArgs e)
        {
            gTool.zoomExtend();
        }
        
        private void bZoomIn_Click(object sender, EventArgs e)
        {
            gTool.zoomIn();
        }

        private void bZoomOut_Click(object sender, EventArgs e)
        {
            gTool.zoomOut();
        }

        private void bDrag_Click(object sender, EventArgs e)
        {
            if (globals.mouseModeDrag)
            {
                endTools();
            }
            else
            {
                configTools(globals.toolCatalog.drag);
            }
        }

        private void bZoomWindow_Click(object sender, EventArgs e)
        {
            if (globals.mouseModeZoom)
            {
                endTools();
            }
            else
            {
                configTools(globals.toolCatalog.zoomWindow);
            }
        }


        private void UpdateColorButton()
        {

            var bitmap = new Bitmap(10, 10);

            using (var gfx = Graphics.FromImage(bitmap))
            using (var brush = new SolidBrush(_drawColor))
            {

                gfx.FillRectangle(brush, 0, 0, 10, 10);
                gfx.Dispose();
            }

            colorToolStripButton.Image = bitmap;
        }


        private void ColorToolStripButtonClick(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog { AllowFullOpen = true, Color = _drawColor };

           
            colorDialog.ShowDialog(this);

             _drawColor = colorDialog.Color;

            UpdateColorButton();
        }

        private void lineToolStripButton_Click(object sender, EventArgs e)
        {
            if (globals.mouseModeLine)
            {
                endTools();
            }
            else
            {
                configTools(globals.toolCatalog.line);
            }
        }
            
    }
}