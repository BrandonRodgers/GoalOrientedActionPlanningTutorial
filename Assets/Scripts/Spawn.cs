using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject patientPrefab;

    [FormerlySerializedAs("numPatients")] [SerializeField] private int initialNumPatients;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initialNumPatients; ++i)
        {
            Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
        }
        
        Invoke("SpawnPaitent", 5);
    }
    
    private void SpawnPatient()
    {
        Instantiate(patientPrefab, this.transform.position, Quaternion.identity);
        Invoke("SpawnPatient", Random.Range(2, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
