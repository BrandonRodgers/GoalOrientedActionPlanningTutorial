using UnityEngine;
using GOAP.Base;

namespace GOAP.Agents
{
    public class Janitor : GAgent
    {
       
        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("clean", 1, false);
            goals.Add(s1, 1);
        }
    }
}
