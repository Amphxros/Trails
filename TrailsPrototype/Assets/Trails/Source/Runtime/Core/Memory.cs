using System;
using UnityEngine;

namespace Trails{

//	The enum values should start at zero and have incremental values so they can be easily retreived
	//	from a SerializedProperty in editor scripts.
public enum MemoryType {Boolean, Int, Float, String, Vector2, Vector3, GO, Asset}


[System.Serializable]
public class Memory
 {
    [SerializeField]
    private string mName_;

    [SerializeField]
    private MemoryType mType;
    
    [SerializeField]
    private bool mValueBool_;

    [SerializeField]
    private int mValueInt_;
    
    [SerializeField]
    private float mValueFloat_;

    [SerializeField]
    private string mValueString_;

    [SerializeField]
    private Vector2 mVector2_;
    
    [SerializeField]
    private Vector3 mVector3_;


    [SerializeField]
    private GameObject mGameObj_;
    
    [SerializeField]
    private UnityEngine.Object mObject_;
    public string Name
		{
			get
			{
				return mName_;
			}
		}
    

    public MemoryType Type
	{
			get
			{
				return mType;
			}
	}

    

    public object GetValue(){

        switch (mType)
        {
            case MemoryType.Boolean:
            return mValueBool_;
            break;
            
            case MemoryType.Int:
            return mValueInt_;
            break;
            
            case MemoryType.Float:
            return mValueFloat_;
            break;
            
            case MemoryType.String:
            return mValueString_;
            break;
            
            case MemoryType.Vector2:
            return mVector2_;
            break;
            
            case MemoryType.Vector3:
            return mVector3_;
           
            case MemoryType.GO:
            return mGameObj_;
            break;

            case MemoryType.Asset:
            return mObject_;
            break;
        
        }
        return null;
    }
        public void SetValue(object value, MemoryType type, bool canOverrideType)
		{
			if(canOverrideType)
			{
				mType = type;
				ResetValues();
				SetValueInternal(value, type);
			}
			else
			{
				if(mType == type)
					SetValueInternal(value, type);
			}
		}

        private void SetValueInternal(object value, MemoryType type)
		{
			switch(type)
			{
			case MemoryType.Boolean:
				mValueBool_ = (bool)value;
				break;
			case MemoryType.Int:
				mValueInt_ = (int)value;
				break;
			case MemoryType.Float:
				mValueFloat_ = (float)value;
				break;
			case MemoryType.String:
				mValueString_ = (string)value;
				break;
			case MemoryType.Vector2:
				mVector2_ = (Vector2)value;
				break;
			case MemoryType.Vector3:
				mVector3_ = (Vector3)value;
				break;
			case MemoryType.GO:
				mGameObj_ = (GameObject)value;
				break;
			case MemoryType.Asset:
				mObject_ = (UnityEngine.Object)value;
				break;
			}
		}

        private void ResetValues()
		{
			mValueBool_ = false;
			mValueInt_ = 0;
			mValueFloat_ = 0.0f;
			mValueString_ = null;
			mVector2_.Set(0, 0);
			mVector3_.Set(0, 0, 0);
			mGameObj_ = null;
			mObject_ = null;
		}


    }   

}