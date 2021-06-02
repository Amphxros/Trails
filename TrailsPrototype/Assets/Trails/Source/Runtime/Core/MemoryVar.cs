using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails.Serialization;
namespace Trails
{
    public class MemoryVar  : IJsonSerializable
    {
        private string mValue;
        public string Value{
            get{
                return mValue;
            }
            set{
                mValue=value;
            }
        }

		private bool? mValueAsBool;
        public bool? AsBool{
            get{
                return mValueAsBool;
            }
        }

		private int? mValueAsInt;
		
        public int? AsInt{
            get{
                return mValueAsInt;
            }
        }
        private float? mValueAsFloat;
        public float? AsFloat{
            get{
                return mValueAsFloat;
            }
        }
		private string mValueAsVariableName;
		public string AsVarName{
            get{
                return mValueAsVariableName;
            }
        }
        private string mValueAsString;

        public string AsString{
            get{
                return mValueAsString;
            }
        }

        public MemoryVar(){
            mValue="";
        }

        public void Change(BlackBoard b, object value){
            if(string.IsNullOrEmpty(mValueAsVariableName)){
                return;
            }
            b.SetItem(mValueAsVariableName, value);
        }

        public T Evaluate<T>(BlackBoard b, T value){
             if(string.IsNullOrEmpty(mValueAsVariableName)){
                return value;
            }

            return b.GetItem<T>(mValueAsVariableName, value);
        }

        public bool HasValue<T>(BlackBoard b)
		{
			return b.HasItem<T>(mValueAsVariableName);
		}

        private void ParseContent()
		{
            bool bool_=false;
            int int_=0;
            float float_=0.0f;

            mValueAsBool=null;
            mValueAsInt=null;
            mValueAsFloat=null;
            mValueAsString=null;
            mValueAsVariableName=null;

            if(string.IsNullOrEmpty(mValue)){
                return;
            }
            else{
                if(mValue[0]=='"' && mValue[mValue.Length-1]=='"'){
                    mValueAsString= mValue.Substring(1,mValue.Length-2);
                }
                else{
                    mValueAsVariableName=mValue;
                    if(bool.TryParse(mValue, out bool_)){
                        mValueAsBool=bool_;
                    }
                    if(int.TryParse(mValue, out int_)){
                        mValueAsInt=int_;
                    }
                    if(float.TryParse(mValue, out float_)){
                        mValueAsFloat=float_;
                    }

                }
            }
        }


        public void ReadJson(JsonReader reader){
            mValue= (string)(reader.Read(typeof(string),false,false));
            ParseContent();

        }

        public void WriteJson(JsonWriter writer)
		{
			writer.Write(mValue);
		}

    }
}