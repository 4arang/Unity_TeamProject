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

    private bool spawnMinion3 = false;
    private byte checkSpawnTimes=0;
    private Vector3 MonsterSpawnPos;

    private int spawnOffset = 1;
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            MonsterSpawnPos = new Vector3(-7, 0, 25);
            InvokeRepeating("SpawnMinion", 20.0f, 45.0f);
            if (transform.position.x < 0) SpawnMonster();
        }
    }

    

    void SpawnMinion()
    {
        checkSpawnTimes++;
        if (transform.position.x < 0)
        {
            PhotonNetwork.Instantiate("Minion1_Blue", new Vector3(transform.position.x + spawnOffset * 3, transform.position.y, transform.position.z), Quaternion.identity);
            PhotonNetwork.Instantiate("Minion1_Blue", new Vector3(transform.position.x + spawnOffset * 2, transform.position.y, transform.position.z), Quaternion.identity);
            PhotonNetwork.Instantiate("Minion1_Blue", new Vector3(transform.position.x + spawnOffset * 1, transform.position.y, transform.position.z), Quaternion.identity);

            PhotonNetwork.Instantiate("Minion2_Blue", new Vector3(transform.position.x + spawnOffset * 0, transform.position.y, transform.position.z), Quaternion.identity);
            PhotonNetwork.Instantiate("Minion2_Blue", new Vector3(transform.position.x - spawnOffset * 1, transform.position.y, transform.position.z), Quaternion.identity);
           // PhotonNetwork.Instantiate("Minion2_Blue", new Vector3(transform.position.x - spawnOffset * 2, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion2, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion3, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion4, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            if (checkSpawnTimes == 3)
            {
                PhotonNetwork.Instantiate("Minion3_Blue", transform.position, Quaternion.identity);
               // PhotonNetwork.Instantiate("Minion4_Blue", transform.position, Quaternion.identity);
                checkSpawnTimes = 0;
            }

            if(spawnMinion3)
            {
                PhotonNetwork.Instantiate("Minion4_Blue", transform.position, Quaternion.identity);
            }
        }
        else
        {
            PhotonNetwork.Instantiate("Minion1_Red", new Vector3(transform.position.x - spawnOffset * 3, transform.position.y, transform.position.z), Quaternion.identity);
            PhotonNetwork.Instantiate("Minion1_Red", new Vector3(transform.position.x - spawnOffset * 2, transform.position.y, transform.position.z), Quaternion.identity);
            PhotonNetwork.Instantiate("Minion1_Red", new Vector3(transform.position.x - spawnOffset * 1, transform.position.y, transform.position.z), Quaternion.identity);

            PhotonNetwork.Instantiate("Minion2_Red", new Vector3(transform.position.x - spawnOffset * 0, transform.position.y, transform.position.z), Quaternion.identity);
            PhotonNetwork.Instantiate("Minion2_Red", new Vector3(transform.position.x + spawnOffset * 1, transform.position.y, transform.position.z), Quaternion.identity);
           // PhotonNetwork.Instantiate("Minion2_Red", new Vector3(transform.position.x + spawnOffset * 2, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion2, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion3, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            // Instantiate(minion4, new Vector3(transform.position.x + 50, transform.position.y, transform.position.z), Quaternion.identity);
            if (checkSpawnTimes == 3)
            {
                PhotonNetwork.Instantiate("Minion3_Red", transform.position, Quaternion.identity);
              //  PhotonNetwork.Instantiate("Minion4_Red", transform.position, Quaternion.identity);
                checkSpawnTimes = 0;
            }
            if (spawnMinion3)
            {
                PhotonNetwork.Instantiate("Minion4_Red", transform.position, Quaternion.identity);
            }
        }
    }

    void SpawnMonster()
    {
        StartCoroutine("spawnMonster");

    }

    IEnumerator spawnMonster()
    {
       
        yield return new WaitForSeconds(60.0f);
        PhotonNetwork.Instantiate("Monster1", MonsterSpawnPos, Quaternion.identity);
    }

    public void SpawnMinion2()
    {
        spawnMinion3 = true;
    }
}
