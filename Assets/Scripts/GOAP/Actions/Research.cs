using GOAP.Base;
using UnityEngine;

namespace GOAP.Actions
{
    public class Research : GAction
    {
        public override bool PrePerform()
        {
            bool success = false;
            target = GWorld.Instance.GetQueue("offices").RemoveResource();

            if (target != null)
            {
                inventory.AddItem(target);
                GWorld.Instance.GetWorld().ModifyState("FreeOffice", -1);
                success = true;
            }

            return success;
        }

        public override bool PostPerform()
        {
            GWorld.Instance.GetQueue("offices").AddResource(target);
            inventory.RemoveItem(target);
            GWorld.Instance.GetWorld().ModifyState("FreeOffice", 1);
            return true;
        }
    }
}