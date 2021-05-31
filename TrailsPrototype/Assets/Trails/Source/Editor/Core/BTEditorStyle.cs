using UnityEditor;
using UnityEngine;

namespace TrailsEditor{
    public static class BTEditorStyle{

        private static GUISkin mEditorSkin_;
        private static Texture mArrowUP_;  
        private static Texture mArrowDOWN_;
        private static Texture mOptionsIcon_;

        enum NodeStyles{CompositeStyle, DecoratorStyle, ActionStyle,NodeGroup}
        private static BTGraphNodeStyle [] mStyles_;
    
        private static GUIStyle mHeaderLabel_;
        private static GUIStyle mBoldLabel_;
        private static GUIStyle mEditorFooter_;
        private static GUIStyle mSelectionBoxStyle_;
        private static GUIStyle mMultilineTextArea_;
       
        private static GUIStyle mListHeader_; 
        private static GUIStyle mListBackGround_;
        private static GUIStyle mListButton_;
        private static GUIStyle mListDragHandle_;

    
        private static GUIStyle mArrowUPButton_;
        private static GUIStyle mArrowDownButton_;

		private static GUIStyle mSeparatorStyle_;
		private static GUIStyle mRegionBackground_;


        public static Texture ArrowUP{
            get{
                return mArrowUP_;
            }
        }
        
        public static Texture ArrowDown{
            get{
                return mArrowDOWN_;
            }
        }
        public static Texture OptionsIcon{
            get{
                return mOptionsIcon_;
            }
        }

        public static GUIStyle HeaderLabel{
            get{
                return mHeaderLabel_;
            }
        }

        public static GUIStyle BoldLabel{
            get{
                return mBoldLabel_;
            }
        }
        public static GUIStyle SelectionBox{
            get{
                return mSelectionBoxStyle_;
            }
        }
        public static GUIStyle MultilineTextArea
		{
			get
			{
				return mMultilineTextArea_;
			}
		}
        public static GUIStyle ListHeader
		{
			get
			{
				return mListHeader_;
			}
		}
        
        public static GUIStyle ListBackground
		{
			get
			{
				return mListBackGround_;
			}
		}

        public static GUIStyle ListButton
		{
			get
			{
				return mListButton_;
			}
		}

        public static GUIStyle mListDragHandle{
            get{
                return mListDragHandle_;
            }
        }
        public static  GUIStyle ArrowUPButton{
            get{
                return mArrowUPButton_;
            }
        }
        public static  GUIStyle ArrowDOWNButton {
            get{
                return mArrowDownButton_;
            }
        }
        public static  GUIStyle EditorFooter
        {
            get{
                return mEditorFooter_;
            }
        }
        
        public static  GUIStyle SeparatorStyle
        {
            get{
                return mSeparatorStyle_;
            }
        }

        public static  GUIStyle RegionBG
        {
            get{
                return mRegionBackground_;
            }
        }

        public static BTEditorLayout TreeLayout{
            get{
                return null;
            }
            set{
               
                // EditorPrefs.SetInt("Trails.Editor.TreeLayout", (int)value);
            }
        }
        
        public static void EnsureStyle()
		{
			LoadGUISkin();
			LoadTextures();
			CreateNodeStyles();
			CreateGUIStyles();
		}

        private static void LoadGUISkin()
		{
            if(mEditorSkin_!=null){
                return;
            }
            else{
            mEditorSkin_= Resources.Load<GUISkin>("Trails/EditorGUI/editor_style");
            }
        }
    
        private static void LoadTextures()
		{
            if(mArrowUP_==null){

            }
            if(mArrowDOWN_==null){

            }
            if(mOptionsIcon_==null){
                
            }

        }
        private static void CreateNodeStyles()
		{

        }
        private static void CreateGUIStyles()
		{

        }

    }
}