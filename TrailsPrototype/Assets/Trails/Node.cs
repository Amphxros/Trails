using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trail
{
    public class Node : MonoBehaviour
    {
        enum State { Undefined,Current, Sucess, Failure};

        State mState;
        // Start is called before the first frame update
        void Start()
        {
            mState = State.Undefined;
        }

        // Update is called once per frame
        void Update()
        {
            switch (mState)
            {
                case State.Undefined:
                    break;
                case State.Current:
                    break;
                case State.Sucess:
                    break;
                case State.Failure:
                    break;
            }
        }
    }
}