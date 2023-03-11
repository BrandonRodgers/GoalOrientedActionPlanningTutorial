using GOAP.Base;
using UnityEngine;

namespace GOAP.Actions
{
    public class GetPatient : GAction
    {
        private GameObject resource;
        
        public override bool PrePerform()
        {
            bool success = false;
            target = GWorld.Instance.GetQueue("patients").RemoveResource();
            
            if (target != null)
            {
                resource = GWorld.Instance.GetQueue("cubicles").RemoveResource();
                
                if (resource != null)
                {
                    inventory.AddItem(resource);
                    GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
                    success = true;
                }
                else
                {
                    GWorld.Instance.GetQueue("patients").AddResource(target);
                    target = null;
                }
            }

            return success;
        }
    
        public override bool PostPerform()
        {
            GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
            if (target)
            {
                target.GetComponent<GAgent>().inventory.AddItem(resource);
            }
            
            return true;
        }
    }
}
