using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails.Serialization;
namespace Trails{
public abstract class Service
{
    [BTProperty("IsExpanded")]
		[BTHideInInspector]
		private bool mIsExpanded = true;

		[BTIgnore]
		public virtual string Title
		{
			get { return GetType().Name; }
		}

		[BTIgnore]
		public bool IsExpanded
		{
			get
			{
				return mIsExpanded;
			}
			set
			{
				mIsExpanded = value;
			}
		}

        public virtual void OnBeforeSerialize(BTAsset btAsset) { }
		public virtual void OnAfterDeserialize(BTAsset btAsset) { }
        
		public virtual void OnEnter(AIAgent agent) { }
		public virtual void OnExit(AIAgent agent) { }
		public abstract void OnExecute(AIAgent agent);

}
}