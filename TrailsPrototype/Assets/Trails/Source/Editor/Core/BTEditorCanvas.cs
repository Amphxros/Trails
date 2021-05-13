using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

namespace Trails.Editor{
public class BTEditorCanvas 
{
    private const int MOUSE_DRAG=2;
    public event UnityAction OnRepaint;

    private int mSnapSize;
    private static BTEditorCanvas mEditorCanvas_;

        public Vector2 Position { get; set; }
		public Rect Area { get; set; }
		public bool IsDebuging { get; set; }
		public string Clipboard { get; set; }

        public BTEditorCanvas()
		{
			Position = Vector2.zero;
			Area = new Rect();
			IsDebuging = false;
			mSnapSize = 10;
			Clipboard = null;
		}
        public static BTEditorCanvas Current{
            get{
            return mEditorCanvas_;
            }
            set{
            mEditorCanvas_=value;
            }
        }

        public void Repaint(){
            if(OnRepaint!=null)
                OnRepaint();
        }



}
}