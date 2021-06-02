using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails.Serialization;

namespace Trails
{
    // Las palabras estaban reservadas y nos pusimos creativos
    public enum ConditionCompare
    {
        isBool, isInt, isFloat, isGameObject, isUnityObject
    }
    public enum BoolCompare
    {
        isTrue, isFalse
    }
    public enum NumCompare
    {
        Equals, Less, Greater, LessOrEqual, GreaterOrEqual
    }
    public enum ReferenceCompare
    {
        isNull, notNull
    }
    public class BlackboardConstraint : Constraint
    {
        MemoryVar mFirstValue;
        [BTIgnore]
		public MemoryVar FirstValue
		{
			get { return mFirstValue; }
			set { mFirstValue = value; }
		}
        MemoryVar mSecondValue;
        [BTIgnore]
		public MemoryVar SecondValue
		{
			get { return mSecondValue; }
			set { mSecondValue = value; }
		}

        ConditionCompare mCondition;
        [BTIgnore]
		public ConditionCompare Condition
		{
			get { return mCondition; }
			set { mCondition = value; }
		}
        BoolCompare mBoolean;
        [BTIgnore]
		public BoolCompare BooleanCompare
		{
			get { return mBoolean; }
			set { mBoolean = value; }
		}
        NumCompare mNumeric;

		[BTIgnore]
		public NumCompare NumericCompare
		{
			get { return mNumeric; }
			set { mNumeric = value; }
		}

        ReferenceCompare mReference;
		[BTIgnore]
		public ReferenceCompare ReferenceComparison
		{
			get { return mReference; }
			set { mReference = value; }
		}

      
        public BlackboardConstraint()
        {
            mFirstValue= new MemoryVar();
            mSecondValue= new MemoryVar();
            mCondition= ConditionCompare.isBool;
            mBoolean= BoolCompare.isTrue;
            mNumeric= NumCompare.Equals;
            mReference= ReferenceCompare.notNull;
        }

        protected override bool Evaluate(AIAgent agent)
        {
            switch(mCondition){
                case ConditionCompare.isBool:
                return CompareBool(agent);
                break;
                case ConditionCompare.isInt:
                return CompareInteger(agent);
                break;
                case ConditionCompare.isFloat:
                return CompareFloat(agent);
                break;
                case ConditionCompare.isGameObject:
                return CompareGameObject(agent);
                break;
                case ConditionCompare.isUnityObject:
                return CompareUnityGameObject(agent);
                break;

            }
            return false;


        }

        private bool CompareBool(AIAgent agent)
        { 
            return false;
        }
        private bool CompareInteger(AIAgent agent)
        { 
            
            return false;
        }
        private bool CompareFloat(AIAgent agent)
        { 
            
            return false;
        }
        private bool CompareGameObject(AIAgent agent)
        {
            
            return false;
        }
        private bool CompareUnityGameObject(AIAgent agent)
        {
            
            return false;
        }
    }
} 
    
