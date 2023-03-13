using GOAP.Base;
using Unity.VisualScripting;

namespace GOAP.Actions
{
    public class GoToWaitingRoom : GAction
    {
        public override bool PrePerform()
        {
            return true;
        }
    
        public override bool PostPerform()
        {
            GWorld.Instance.GetWorld().ModifyState("Waiting", 1);
            GWorld.Instance.GetQueue("patients").AddResource(this.gameObject);
            //agentBeliefs.ModifyState("atHospital", 1);
            return true;
        }
    }
}
