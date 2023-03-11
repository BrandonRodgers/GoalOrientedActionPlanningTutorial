using GOAP.Base;
using UnityEngine;

namespace GOAP.Actions
{
    public class GoToToilet : GAction
    {
        public override bool PrePerform()
        {
            bool success = false;
            target = GWorld.Instance.GetQueue("toilets").RemoveResource();

            if (target != null)
            {
                inventory.AddItem(target);
                GWorld.Instance.GetWorld().ModifyState("FreeToilet", -1);
                success = true;
            }

            return success;
        }

        public override bool PostPerform()
        {
            GWorld.Instance.GetQueue("toilets").AddResource(target);
            inventory.RemoveItem(target);
            GWorld.Instance.GetWorld().ModifyState("FreeToilet", 1);
            beliefs.RemoveState("needRelief");
            return true;
        }
    }
}