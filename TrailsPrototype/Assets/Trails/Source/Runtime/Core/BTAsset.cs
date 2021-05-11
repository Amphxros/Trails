using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    [CreateAssetMenu(menuName = "Trails/Behaviour Tree")]
    public class BTAsset : ScriptableObject
    {
        [System.Serializable]
        private class AssetIDPair
        {
            public BTAsset asset;
            public string assetID;
        }
       
        [SerializeField]
        [HideInInspector]
        private string mSerializedData_;
        [SerializeField]
        private Rect mCanvasArea_;
        [SerializeField]
        private List<AssetIDPair> mSubtrees_;

#if UNITY_EDITOR
		private BehaviourTree mEditModeTree_;
#endif

        public static Vector2 DEFAULT_CANVAS_SIZE
        {
            get { return new Vector2(1000, 1000); }
        }

        public Rect CanvasArea
        {
            get
            {
                return mCanvasArea_;
            }
            set
            {
                mCanvasArea_ = value;
            }
        }

//#if UNITY_EDITOR
//		private void OnEnable()
//		{
//			if(Mathf.Approximately(mCanvasArea_.width, 0) || Mathf.Approximately(mCanvasArea_.height, 0))
//			{
//				mCanvasArea_ = new Rect(-DEFAULT_CANVAS_SIZE.x / 2, -DEFAULT_CANVAS_SIZE.y / 2, DEFAULT_CANVAS_SIZE.x, DEFAULT_CANVAS_SIZE.y);
//			}
//			if(mSubtrees_ == null)
//			{
//				mSubtrees_ = new List<AssetIDPair>();
//			}
//		}
//
//        public void Serialize()
//		{
//			if(mEditModeTree_ != null)
//			{
//				mEditModeTree_.Root.OnBeforeSerialize(this);
//
//				string serializedData = BTUtils.SerializeTree(mEditModeTree_);
//				if(serializedData != null)
//				{
//					mSerializedData_ = serializedData;
//				}
//			}
//		}
//        
//        public void Dispose()
//		{
//			m_editModeTree = null;
//		}
//#endif

    }
}