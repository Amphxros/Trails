using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TrailsEditor{
    public class BTEditorGrid 
    {
        private Texture mTexture_;
        public BTEditorGrid(Texture gridTexture){
            mTexture_=gridTexture;
        }

        public void DrawGUI(Vector2 size){
            BTEditorCanvas mCanvas_=BTEditorCanvas.Current;

            Rect pos= new Rect(0,0,size.x, size.y);
            Rect mTextureCoords= new Rect(-mCanvas_.Position.x/mTexture_.width, //x
            (1.0f- size.y /mTexture_.height) + mCanvas_.Position.y / mTexture_.height, //y
            size.x/mTexture_.width, //w
            size.y/mTexture_.height); //h
            
            GUI.DrawTextureWithTexCoords(pos, mTexture_, mTextureCoords);

        }
    
    }
}