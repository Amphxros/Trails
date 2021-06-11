using Trails.Serialization;

namespace Trails
{
    public abstract class Constraint
    {
        [BTProperty("IsExpanded")]
		[BTHideInInspector]
        private bool mIsExpanded_ = true;
        [BTProperty("InvertResult")]
		[BTHideInInspector]
        private bool mInvertResult = false;
      
        [BTIgnore]
        public bool IsExpanded
        {
            get
            {
                return mIsExpanded_;
            }
            set
            {
                mIsExpanded_ = value;
            }
        }

        [BTIgnore]
        public bool InvertResult
        {
            get
            {
                return mInvertResult;
            }
            set
            {
                mInvertResult = value;
            }
        }

        public virtual void OnBeforeSerialize(BTAsset btAsset) { }
        public virtual void OnAfterDeserialize(BTAsset btAsset) { }
   
        public bool OnExecute(AIAgent agent)
        {
            bool result = Evaluate(agent);
            return InvertResult ? !result : result;
        }
   

        protected abstract bool Evaluate(AIAgent agent);

    }
}