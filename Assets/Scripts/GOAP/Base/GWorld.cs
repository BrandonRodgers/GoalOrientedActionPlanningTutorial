using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GOAP.Base
{
    public class ResourceQueue
    {
        public Queue<GameObject> que = new Queue<GameObject>();
        public string tag;
        public string modState;

        public ResourceQueue(string tag, string modState, WorldStates worldStates)
        {
            this.tag = tag;
            this.modState = modState;
            if (this.tag != "")
            {
                GameObject[] resources = GameObject.FindGameObjectsWithTag(this.tag);
                foreach (GameObject r in resources)
                {
                    que.Enqueue(r);
                }
            }

            if ( (que.Count > 0) &&
                 (this.modState != "") )
            {
                worldStates.ModifyState(this.modState, que.Count);
            }
        }

        public void AddResource(GameObject r)
        {
            que.Enqueue(r);
        }

        public GameObject RemoveResource()
        {
            if (que.Count == 0)
                return null;
            
            return que.Dequeue();
        }
    }
    
    public sealed class GWorld
    {
        private static readonly GWorld instance = new GWorld();
        private static WorldStates world;
        private static ResourceQueue patients;
        private static ResourceQueue cubicles;
        private static ResourceQueue offices;
        private static ResourceQueue toilets;

        private static Dictionary<string, ResourceQueue> resources = new Dictionary<string, ResourceQueue>();

        static GWorld()
        {
            world = new WorldStates();
            patients = new ResourceQueue("", "", world);
            cubicles = new ResourceQueue("Cubicle", "FreeCubicle", world);
            offices = new ResourceQueue("Office", "FreeOffice", world);
            toilets = new ResourceQueue("Toilet", "FreeToilet", world);
            
            // Add ResourceQueues to dictionary
            resources.Add("patients", patients);
            resources.Add("cubicles", cubicles);
            resources.Add("offices", offices);
            resources.Add("toilets", toilets);

            Time.timeScale = 5f;
        }

        public ResourceQueue GetQueue(string type)
        {
            return resources[type];
        }

        private GWorld()
        {
        }
        
        public static GWorld Instance
        {
            get { return instance; }
        }

        public WorldStates GetWorld()
        {
            return world;
        }
    }
}
