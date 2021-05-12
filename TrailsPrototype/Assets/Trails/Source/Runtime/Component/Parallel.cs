using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails{
    public class Parallel : Composite
    {
    private bool mFailOnAny_;
    private bool mSucceedOnAny_;
    private bool mFailOnTie_;

    public bool FailOnAny{
        get{
            return mFailOnAny_;
        }
        set{
            mFailOnAny_=value;
        }
    }

    public bool SucceedOnAny{
        get{
            return mSucceedOnAny_;
        }
        set{
            mSucceedOnAny_=value;
        }
    }
    public bool FailOnTie{
			get
			{
				return mFailOnTie_;
			}
			set
			{
				mFailOnTie_ = value;
			}
	}

    public Parallel(){
        mFailOnAny_=true;
        mSucceedOnAny_=false;
        mFailOnTie_=true;
    }

    }
}