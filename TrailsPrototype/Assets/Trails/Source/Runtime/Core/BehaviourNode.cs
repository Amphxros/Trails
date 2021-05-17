using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails.Serialization;

namespace Trails
{

    public enum BehaviourNodeStatus
	{
		Failure, Success, Running, None
	}
    public abstract class BehaviourNode
    {
        private string mUniqueID_;

        private Vector2 mPos_;
        private string mComment_;
        private string mName_;
        private float mWeight_;

        private BehaviourNodeStatus mStatus;
        //mas variables aqui
        private List<Constraint> mConstraints_;
		private List<Service> mServices_;
        public BehaviourNode(){
            mStatus= BehaviourNodeStatus.None;
            mConstraints_= new List<Constraint>();
            mServices_= new List<Service>();
        }

#if UNITY_EDITOR
		/// <summary>
		/// This node's list of Constraints. 
        // For use only in the editor code. DO NOT USE IN RUNTIME CODE!!!
		/// </summary>
		[BTIgnore]
		public List<Constraint> Constraints
		{
			get { return mConstraints_; }
		}

		/// <summary>
		/// This node's list of Services. 
        // For use only in the editor code. DO NOT USE IN RUNTIME CODE!!!
		/// </summary>
		[BTIgnore]
		public List<Service> Services
		{
			get { return mServices_; }
		}
#endif



        public Vector2 Position
        {
            get
            {
                return mPos_;
            }
            set
            {
                mPos_ = value;
            }
        }
        public string Name
        {
            get
            {
                return mName_;
            }
            set
            {
                mName_ = value;
            }
        }
        public string Comment
        {
            get
            {
                return mComment_;
            }
            set
            {
                mComment_ = value;
            }
        }

        public float Weight
        {
            get
            {
                return mWeight_;
            }
            set
            {
                mWeight_ = value;
            }
        }

        public virtual string Title
        {
            get { return GetType().Name; }
        }

        public string UniqueID
        {
            get { return mUniqueID_; }
        }

      
	    public virtual void OnBeforeSerialize(BTAsset btAsset)
		{
		    foreach(var c in mConstraints_){
            c.OnBeforeSerialize(btAsset);
            }

            foreach(var s in mServices_){
            s.OnBeforeSerialize(btAsset);
            }


        }
        public virtual void OnAfterDeserialize(BTAsset btAsset)
		{
        // constraints
        if(mConstraints_ == null){
            mConstraints_ = new List<Constraint>();
        }
        foreach(var c in mConstraints_){
            c.OnAfterDeserialize(btAsset);
        }
        // service
        if(mServices_==null){
            mServices_= new List<Service>();
        }

        foreach(var s in mServices_){
            s.OnAfterDeserialize(btAsset);
        }


        }

        public virtual void OnStart(AIAgent agent)
		{
		}

        public virtual void OnReset(){
        mStatus=BehaviourNodeStatus.None;
        }

        public BehaviourNodeStatus Run(AIAgent agent){
            if(mStatus!=BehaviourNodeStatus.Running){
                OnEnter(agent);
                foreach(var s in mServices_){
                    s.OnEnter(agent);

                    // añadir aqui el debug
                }
            }
            if(CheckConstraints(agent)){
                mStatus=OnExecute(agent);
                foreach(var s in mServices_){
                 s.OnExecute(agent);   
                }
            }
            else{
                if(mStatus!=BehaviourNodeStatus.Running){
                    OnExit(agent);
                    foreach(var s in mServices_){
                        s.OnExit(agent);
                    }
                    // mas debug aqui ???
                }
            }

            return mStatus;
        }

	    protected abstract BehaviourNodeStatus OnExecute(AIAgent agent);
        protected virtual void OnEnter(AIAgent agent)
		{
		}

		protected virtual void OnExit(AIAgent agent)
		{
		}
        private bool CheckConstraints(AIAgent agent)
		{
			foreach(var c in mConstraints_)
			{
				if(!c.OnExecute(agent)){
					return false;
                }
			}

			return true;
		}


    }
}