using UnityEngine;
using UnityEditor;
using System.Text;

namespace TrailsEditor{
    public static class BTEditorUtils 
    {
        private const int BEZIER_H_OFFSET = 250;
		private const int BEZIER_WIDTH = 3;
		private const float MIN_V_DISTANCE = 40.0f;
		private const float MIN_H_DISTANCE = 300.0f;
        private static StringBuilder mStringBuilder = new StringBuilder();
    
        public static void DrawBezierCurve(Rect a, Rect b, Color color)
		{
            DrawBezierCurve(a.center, b.center, color);
        }
    
        public static void DrawBezierCurve(Vector2 a, Vector2 b, Color color)
		{
	        Vector2 init = a.x <= b.x ? a : b;
			Vector2 final = a.x <= b.x ? b : a;
            
            float vDistance=Mathf.Abs(init.y - final.y);
            float hDistance=Mathf.Abs(init.x - final.x);
            float lerp=Mathf.Min(Mathf.Clamp01(vDistance/MIN_V_DISTANCE), Mathf.Clamp01(hDistance/MIN_H_DISTANCE));
            float offset= Mathf.Lerp(0.0f,BEZIER_H_OFFSET,lerp);

            Vector3 initTang= new Vector3(init.x+ offset, init.y,0);
            Vector3 finalTang= new Vector3(final.x-offset, final.y,0);

            Handles.DrawBezier(init, final, initTang, finalTang, color, null, BEZIER_WIDTH);

        }

        public static void DrawLine(Rect a, Rect b, Color color)
		{
            Handles.DrawBezier(a.center, b.center, a.center, b.center, color, null, BEZIER_WIDTH);
		}

        public static void DrawLine(Vector2 a, Vector2 b, Color color)
		{
            Handles.DrawBezier(a, b, a, b, color, null, BEZIER_WIDTH);
        }

        public static string MakePrettyName(string name)
		{
            return "";
        }

        public static string GetResourcePath(UnityEngine.Object target)
		{
            return "";

        }
    
    }
}
