using System.Collections;
using System.Collections.Generic;
using GOAP.Base;
using UnityEngine;

[RequireComponent(typeof(GAgent))]
public class GStateMonitor : MonoBehaviour
{
    [SerializeField] private string state;
    [SerializeField] private float stateStrength;
    [SerializeField] private float stateDecayRate;
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private string queueName;
    [SerializeField] private string worldState;
    [SerializeField] private GAction action;

    private WorldStates beliefs;
    private bool stateFound = false;
    private float initialStrength;
    
    void Awake()
    {
        beliefs = this.GetComponent<GAgent>().beliefs;
        initialStrength = stateStrength;
    }
    
    void LateUpdate()
    {
        if (action.running)
        {
            stateFound = false;
            stateStrength = initialStrength;
        }
        
        if (!stateFound && beliefs.HasState(state))
        {
            stateFound = true;
        }

        if (stateFound)
        {;
            stateStrength -= stateDecayRate * Time.deltaTime;
            if (stateStrength <= 0)
            {
                var agentPosition = this.transform.position;
                Vector3 location = new Vector3(agentPosition.x, resourcePrefab.transform.position.y,
                    agentPosition.z);
                GameObject r = Instantiate(resourcePrefab, location, resourcePrefab.transform.rotation);
                stateFound = false;
                stateStrength = initialStrength;
                beliefs.RemoveState(state);
                GWorld.Instance.GetQueue(queueName).AddResource(r);
                GWorld.Instance.GetWorld().ModifyState(worldState, 1);
            }
        }
    }
}
