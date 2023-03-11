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
            target = GWorld.Instance.RemovePatient();
            
            if (target != null)
            {
                resource = GWorld.Instance.RemoveCubicle();
                
                if (resource != null)
                {
                    inventory.AddItem(resource);
                    Debug.Log($"GetPatient::PrePerform agent name: {this.gameObject.name}");
                    GWorld.Instance.GetWorld().ModifyState("FreeCubicle", -1);
                    success = true;
                }
                else
                {
                    GWorld.Instance.AddPatient(target);
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
