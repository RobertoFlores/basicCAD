using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelGraphicTool
{
    public class GraphicGlobals
    {
        public static double scaleDataBase;                             // Normalize elements from database to a different scale
        public static double scaleZoom;                                 // Rezise all elements (zoom)
        public static double scaleSymbols;                              // Resize non-scalable elements
        public static double scalePixel;                                // Length of a pixel in the openGL's space
        public static double scaleWindow;
        public static double radiusRange;                               // Maximum distance between the mouse and a target
        public static double radiusPolygon;                             // Maximum distance between the mouse and a target


        public static OpenTK.Vector2d mousePosition;                    // Mouse position in the window plane (in pixel coordinates)
        public static OpenTK.Vector2d mouseCartesian;                   // Mouse position in the openGL's space plane (in cartesian coordinates)
        public static OpenTK.Vector2d mouseGeographic;


        public static mgTools gTool;
    }
}
