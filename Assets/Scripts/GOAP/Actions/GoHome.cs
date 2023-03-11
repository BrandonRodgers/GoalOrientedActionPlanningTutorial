using GOAP.Base;
using UnityEngine;

namespace GOAP.Actions
{
    public class GoHome : GAction
    {
        public override bool PrePerform()
        {
            return true;
        }
    
        public override bool PostPerform()
        {
            GWorld.Instance.GetWorld().ModifyState("Cured", 1);
            //beliefs.ModifyState("atHospital", -1);
            beliefs.RemoveState("atHospital");
            Destroy(this.gameObject, 1f);
            return true;
        }
    }
}