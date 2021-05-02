using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// clase base de la que se podra implementar las tareas que el usuario quiera
/// </summary>
public class ActionTask : Task
{
    public override IEnumerator Run()
    {
        isFinished = false;
        Action(); 
        return base.Run();
    }

    protected virtual void Action()
    {
        // implement your behaviour here
        //-------------------------//
    }
}
