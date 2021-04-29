using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public List<Task> children;
    protected bool result = false;
    protected bool isFinished = false;

    public virtual void setResult(bool res) { 
        result = res;
        isFinished = true;
    }

    public IEnumerator PrintNumber(int n)
    {
        Debug.Log(n);
        yield break;
    }

    public virtual IEnumerator Run()
    {
        setResult(true);
        yield break;
    }

    public virtual IEnumerator RunTask()
    {
        yield return StartCoroutine(Run());
    }
}
