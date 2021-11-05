using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus_Spawn : MonoBehaviour
{
    [SerializeField] private GameObject minion1;
    [SerializeField] private GameObject minion2;
    [SerializeField] private GameObject minion3;
    [SerializeField] private GameObject minion4;
    private byte checkSpawnTimes=0;
    void Start()
    {
        InvokeRepeating("SpawnMinion", 1.0f, 15.0f);
    }

    void SpawnMinion()
    {
        checkSpawnTimes++;
        Instantiate(minion1, new Vector3(transform.position.x , transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(minion1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
  
        Instantiate(minion2, transform.position, Quaternion.identity);
        if (checkSpawnTimes == 3)
        {
            Instantiate(minion3, transform.position, Quaternion.identity);
            Instantiate(minion4, transform.position, Quaternion.identity);
            checkSpawnTimes = 0;
        }
    }
}
