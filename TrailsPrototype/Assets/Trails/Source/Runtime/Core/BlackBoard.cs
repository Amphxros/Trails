using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails{
public class BlackBoard : MonoBehaviour
{
    [SerializeField]
    private Memory[] mMemory_;

    private Dictionary<string,object> mValues_;

    void Awake()
    {
        mValues_= new Dictionary<string, object>();
     
        foreach(Memory m in mMemory_){
         
         SetItem(m.Name, m.GetValue());
        }   
    }

    public void SetItem(string name, object item){
        if(!string.IsNullOrEmpty(name)){
            if(mValues_.ContainsKey(name))
                mValues_[name]=item;
            
            else
                mValues_.Add(name,item);
        }
    }
    public object GetItem(string name, object obj=null){
         if(!string.IsNullOrEmpty(name)){
        object v= null;
        if(mValues_.TryGetValue(name, out v))
            return v;

        }

        return obj;
    }

    public T GetItem<T>(string name, T obj)
	{
		object value = GetItem(name);
		return (value != null && value is T) ? (T)value : obj;
	}

        
	public bool HasItem<T>(string name)
	{
		object value = GetItem(name);
		return (value != null && value is T);
	}
}
}