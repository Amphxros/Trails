using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public abstract class BehaviourNode
    {
        private string mUniqueID_;

        private Vector2 mPos_;
        private string mComment_;
        private string mName_;
        private float mWeight_;
        //mas variables aqui

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
    }
}