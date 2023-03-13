using GOAP.Base;
using UnityEngine;

namespace GOAP.Actions
{
    public class Rest : GAction
    {
        public override bool PrePerform()
        {
            return true;
        }
    
        public override bool PostPerform()
        {
            agentBeliefs.RemoveState("exhausted");
            return true;
        }
    }
}