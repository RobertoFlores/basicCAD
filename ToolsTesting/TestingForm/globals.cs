using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingForm
{
    class globals
    {
        public static bool mouseModeZoom = false;
        public static bool mouseModePivot = false;
        public static bool mouseModeDrag = false;
        public static bool mouseModeRule = false;
        public static bool mouseModeLine = false;
        public static bool mouseDown = false;


        public enum toolCatalog { none = 0, drag, zoomWindow, line };
        public static OpenTK.Vector2d mouseGeographic;

        //internal static float mouseGeographic(int p, int p_2)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
