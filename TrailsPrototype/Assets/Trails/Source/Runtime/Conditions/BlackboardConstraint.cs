using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trails.Serialization;

namespace Trails
{
    // Las palabras estaban reservadas y nos pusimos creativos
    enum ConditionValueType
    {
        isBool, isInt, isFloat, isGameObject, isUnityObject
    }
    enum BoolCompare
    {
        isTrue, isFalse
    }
    enum NumCompare
    {
        Equals, Less, Greater, LessOrEqual, GreaterOrEqual
    }
    enum referenceCompare
    {
        isNull, notNull
    }
    public class BlackboardConstraint : Constraint
    {
        MemoryVar firstValue;
        MemoryVar secondValue;
        ConditionValueType cosa;
        BoolCompare cosa2;
        NumCompare cosa3;
        referenceCompare cosa4;

        public override string Title
        {
            get
            {
                return "Blackboard";
            }
        }

        public BlackboardConstraint()
        {

        }

        protected override bool Evaluate(AIAgent agent)
        {

        }

        private bool CompareBool(AIAgent agent)
        { 
        }
        private bool CompareInteger(AIAgent agent)
        { 
        }
        private bool CompareFloat(AIAgent agent)
        { 
        }
        private bool CompareGameObject(AIAgent agent)
        {
        }
        private bool CompareUnityGameObject(AIAgent agent)
        {
        }
    }
} 
    
