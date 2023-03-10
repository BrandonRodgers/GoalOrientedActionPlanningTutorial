using GOAP.Base;
using UnityEngine;

namespace GOAP.Actions
{
    public class GetTreated : GAction
    {
        public override bool PrePerform()
        {
            target = inventory.FindItemWithTag("Cubicle");

            if (target == null)
            {
                return false;
            }

            return true;
        }

        public override bool PostPerform()
        {
            GWorld.Instance.GetWorld().ModifyState("Treated", 1);
            agentBeliefs.ModifyState("isCured", 1);
            inventory.RemoveItem(target);
            return true;
        }
    }
}