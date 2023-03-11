using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject patientPrefab;

    [SerializeField] private int initialNumPatients;

    [SerializeField] private bool keepSpawning = false;

    private int patientNum = 3;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initialNumPatients; ++i)
        {
            Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
        }

        if (keepSpawning)
        {
            Invoke("SpawnPatient", 5);    
        }
    }
    
    private void SpawnPatient()
    {
        GameObject newPatient = Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
        newPatient.GetComponent<NavMeshAgent>().avoidancePriority = patientNum;
        patientNum++;
        Invoke("SpawnPatient", Random.Range(2, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
