using UnityEngine;
using GOAP.Base;

namespace GOAP.Agents
{
    public class Nurse : GAgent
    {
        [SerializeField] private int minWaitTired = 30;
        [SerializeField] private int maxWaitTired = 40;
        [SerializeField] private int minWaitRelief = 30;
        [SerializeField] private int maxWaitRelief = 40;
        
        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("treatPatient", 1, false);
            goals.Add(s1, 3);
            
            SubGoal s2 = new SubGoal("relief", 1, false);
            goals.Add(s2, 4);
            
            SubGoal s3 = new SubGoal("rested", 1, false);
            goals.Add(s3, 1);

            Invoke("GetTired", Random.Range(minWaitTired, maxWaitTired));
            Invoke("NeedRelief", Random.Range(minWaitRelief, maxWaitRelief));
        }

        void GetTired()
        {
            beliefs.ModifyState("exhausted", 0);
            Invoke("GetTired", Random.Range(minWaitTired, maxWaitTired));
        }
        
        void NeedRelief()
        {
            beliefs.ModifyState("needRelief", 0);
            Invoke("NeedRelief", Random.Range(minWaitRelief, maxWaitRelief));
        }
    }
}
