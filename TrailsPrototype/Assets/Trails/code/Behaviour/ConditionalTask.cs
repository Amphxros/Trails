using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalTask : Task
{
    public override IEnumerator Run()
    {
        isFinished = false;
        bool r = false;
        // implement your behaviour here
        /*
         .
         .
         .
         */
        // define result (r) whether true or false
        //---------
        setResult(r);
        yield break;
    }
}
