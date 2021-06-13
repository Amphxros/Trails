using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails{
public class Root : BehaviourNode
{
    public override string Title
		{
			get
			{
				return "Root";
			}
		}


		public Root()
		{

		}


		public Root(BehaviourNode node)
			: base()
		{
			
		}

}
}