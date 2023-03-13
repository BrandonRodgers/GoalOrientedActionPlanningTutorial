using GOAP.Base;
using UnityEngine;

namespace GOAP.Actions
{
    public class CleanPuddle : GAction
    {
        public override bool PrePerform()
        {
            bool success = false;
            target = GWorld.Instance.GetQueue("puddles").RemoveResource();

            if (target != null)
            {
                inventory.AddItem(target);
                GWorld.Instance.GetWorld().ModifyState("FreePuddle", -1);
                success = true;
            }

            return success;
        }

        public override bool PostPerform()
        {
            inventory.RemoveItem(target);
            Destroy(target);
            return true;
        }
    }
}