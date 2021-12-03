using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Nexus_Spawn : MonoBehaviour
{
    [SerializeField] private GameObject minion1;
    [SerializeField] private GameObject minion2;
    [SerializeField] private GameObject minion3;
    [SerializeField] private GameObject minion4;
   
    private byte checkSpawnTimes=0;
    private Vector3 MonsterSpawnPos;

    private int spawnOffset = 1;
    void Start()
    {
        MonsterSpawnPos = new Vector3(-7,0,25);
        InvokeRepeating("SpawnMinion", 1.0f, 30.0f);
        if(transform.position.x<0) InvokeRepeating("SpawnMonster", 100.0f, 30.0f);
    }

    

    void SpawnMinion()
    {
        checkSpawnTimes++;
        if (transform.position.x < 0)
        {
            PhotonNetwork.Instantiate("minion1", new Vector3(transform.position.x + spawnOffset * 2, transform.position.y, transform.position.z), Quaternion.identity);
            PhotonNetwork.Instantiate("minion1", new Vector3(transform.position.x + spawnOffset * 1, transform.position.y, transform.position.z), Quaternion.identity);

            PhotonNetwork.Instantiate("minion2", new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion2, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion3, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion4, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            if (checkSpawnTimes == 3)
            {
                PhotonNetwork.Instantiate("minion3", transform.position, Quaternion.identity);
                PhotonNetwork.Instantiate("minion4", transform.position, Quaternion.identity);
                checkSpawnTimes = 0;
            }
        }
        else
        {
            PhotonNetwork.Instantiate("minion1", new Vector3(transform.position.x - spawnOffset * 2, transform.position.y, transform.position.z), Quaternion.identity);
            PhotonNetwork.Instantiate("minion1", new Vector3(transform.position.x - spawnOffset * 1, transform.position.y, transform.position.z), Quaternion.identity);

            PhotonNetwork.Instantiate("minion2", new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion2, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion3, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion4, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            if (checkSpawnTimes == 3)
            {
                PhotonNetwork.Instantiate("minion3", transform.position, Quaternion.identity);
                PhotonNetwork.Instantiate("minion4", transform.position, Quaternion.identity);
                checkSpawnTimes = 0;
            }
        }
    }

    void SpawnMonster()
    {
 
        PhotonNetwork.Instantiate("Monster1", MonsterSpawnPos, Quaternion.identity);
    }
}
