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
    
        private static GUIStyle mBreadcrumbLeftStyle_;
        private static GUIStyle mBreadcrumbLeftActiveStyle_;
		private static GUIStyle mBreadcrumbMidStyle_;
		private static GUIStyle mBreadcrumbMidActiveStyle_;
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
        




    


    }
}