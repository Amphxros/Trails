using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Trails{
    public class AIAgent : MonoBehaviour
    {
        public BTAsset mBehaviourTree_;
        public BehaviourTree mBTInstance_;

        private GameObject mBody_;
        

        public GameObject Body{
            get{
                return mBody_!=null ? mBody_:gameObject;
            }
        }

        

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}