using System.Collections;
using System.Collections.Generic;
using GOAP.Base;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class WInterface : MonoBehaviour
{
    private GameObject focusObject;
    private ResourceData focalObjectData;
    private GameObject newResourcePrefab;
    public GameObject[] allResources;
    public GameObject resources;
    private Vector3 goalPos;
    public NavMeshSurface surface;
    private Camera _camera;
    private Vector3 clickOffset = Vector3.zero;
    private bool offsetCalc = false;
    private bool shouldDeleteResource = false;

    // Start is called before the first frame update
    private void Awake()
    {
        _camera = Camera.main;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (_camera != null)
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out hit))
                    return;

                offsetCalc = false;
                clickOffset = Vector3.zero;

                Resource r = hit.transform.gameObject.GetComponent<Resource>();
                
                if (r != null)
                {
                    focusObject = hit.transform.gameObject;
                    focalObjectData = r.info;
                }
                else if (newResourcePrefab != null)
                {
                    goalPos = hit.point;
                    focusObject = Instantiate(newResourcePrefab, goalPos, newResourcePrefab.transform.rotation);
                    focalObjectData = focusObject.GetComponent<Resource>().info;
                }

                if (focusObject != null)
                {
                    focusObject.GetComponent<Collider>().enabled = false;    
                }
            }
        }
        else if (focusObject && Input.GetMouseButtonUp(0))
        {
            if (shouldDeleteResource)
            {
                GWorld.Instance.GetQueue(focalObjectData.resourceQueue).RemoveResource(focusObject);
                GWorld.Instance.GetWorld().ModifyState(focalObjectData.resourceState, -1);
                Destroy(focusObject);
            }
            else
            {
                focusObject.transform.parent = resources.transform;
                GWorld.Instance.GetQueue(focalObjectData.resourceQueue).AddResource(focusObject);
                GWorld.Instance.GetWorld().ModifyState(focalObjectData.resourceState, 1);
                focusObject.GetComponent<Collider>().enabled = true;
                
            }
            
            surface.BuildNavMesh();
            focusObject = null;
        }
        else if (focusObject && Input.GetMouseButton(0))
        {
            
            if (_camera != null)
            {
                RaycastHit hitMove;
                int layerMask = LayerMask.GetMask("Floor");

                Ray rayMove = _camera.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(rayMove, out hitMove, Mathf.Infinity, layerMask))
                    return;

                if (!offsetCalc)
                {
                    clickOffset = hitMove.point - focusObject.transform.position;
                    offsetCalc = true;
                }
                
                goalPos = hitMove.point - clickOffset;
                focusObject.transform.position = goalPos;
            }
        }

        if (focusObject)
        {
            if (Input.GetKeyDown(KeyCode.Less) || Input.GetKeyDown(KeyCode.Comma))
            {
                focusObject.transform.Rotate(0f, 90f, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.Greater) || Input.GetKeyDown(KeyCode.Period))
            {
                focusObject.transform.Rotate(0f, -90f, 0f);
            }
        }
    }

    public void MouseOnHoverTrash()
    {
        shouldDeleteResource = true;
    }

    public void MouseOutHoverTrash()
    {
        shouldDeleteResource = false;
    }

    public void ActivateToilet()
    {
        newResourcePrefab = allResources[0];
    }
    
    public void ActivateCubicle()
    {
        newResourcePrefab = allResources[1];
    }
}