using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public class TrailsManager : MonoBehaviour
    {
        public List<Task> tasks;
        Task init;
        public TrailsManager()
        {
            tasks = new List<Task>();
            init = new Task();
            tasks.Add(init); //init point
            
        }

        public void AddRepeater()
        {
            init.children.Add(new Task());
        }

        public void AddSelector()
        {

            tasks.Add(new Selector());
        }
        public void AddSequence()
        {
            tasks.Add(new Sequence());
        }


        public void Clear()
        {
            tasks.Clear();
        }

        public void AddCustomTask(ActionTask t)
        {

        }

        public void AddMessageInTask(Task t, string msg)
        {
            t.setInfo(msg);
        }

        public string getInfoInTask(Task t)
        {
            return t.getInfo();
        }
    }
}