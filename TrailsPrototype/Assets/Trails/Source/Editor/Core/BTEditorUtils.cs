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
          if(string.IsNullOrEmpty(name)){
				throw new System.ArgumentException("Name is null or empty", "name");
          }

          else{
			mStringBuilder.Length = 0;
			for(int i = 0; i < name.Length; i++)
			{
				if(!char.IsLetterOrDigit(name[i]))
				{
					if(mStringBuilder.Length > 0)
						mStringBuilder.Append(" ");

					continue;
				}

				if(mStringBuilder.Length == 0)
				{
					mStringBuilder.Append(char.ToUpper(name[i]));
				}
				else
				{
					if(i > startIndex)
					{
						if((char.IsUpper(name[i]) && !char.IsUpper(name[i - 1])) ||
							(char.IsLetter(name[i]) && char.IsDigit(name[i - 1])) ||
							(char.IsDigit(name[i]) && char.IsLetter(name[i - 1])))
						{
							mStringBuilder.Append(" ");
						}
					}

					mStringBuilder.Append(name[i]);
				}
			}

			return mStringBuilder.Length > 0 ? mStringBuilder.ToString() : name;
            }
            return "";
        }

        public static string GetResourcePath(UnityEngine.Object target)
		{
            if(target!=null){
             string path=AssetDatabase.GetAssetPath(target);
             int i= path.IndexOf('.');
             if(i > 0){
                path=path.Substring(0,i);
             }
             return path;
            }
            else{

                return "";
            }

        }
    
    }
}
