using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;
public class TeamManager : MonoBehaviourPunCallbacks
{
    public int TeamID = 0;

    //public List<ChampionStats> Champions;
    
    public Transform MinionSpawnPoints;

    public GameObject MeleePrefab;
    public GameObject RangePrefab;
    public GameObject CannonPrefab;
    public GameObject SuperPrefab;

    public int WaveNumber = 0;
    public float WaveTimer = GameConsts.MINION_WAVESTART_TIME;          //countdown
    public bool Inhibitor = false;

    private void Update()
    {
        //Return until MINION _WAVESTART_TIME
        if (GameManager.Instance.GameTime < GameConsts.MINION_WAVESTART_TIME)       
        {
            return;
        }
        else
        {
            //Instantiate Minion After WAVESTART_TIME and Spawn every MINION_SPAWNINTERVAL_TIME
            if (WaveTimer <= 0f)
            {
                StartCoroutine(SpawnWave());
                WaveTimer = GameConsts.MINION_SPAWNINTERVAL_TIME;//Reset Timer          
                WaveNumber++;
            }
            else
            {
                WaveTimer -= Time.deltaTime;
            }
        }
        
        
    }

    IEnumerator SpawnWave()
    {
        Debug.Log($"Wave Number : {WaveNumber} has spawned at {GameManager.Instance.GameTime}");

        for (int m = 0; m < GameConsts.MELEE_COUNT; m++)
        {
            //spawn melee
            SpawnUnits(MeleePrefab);
            yield return new WaitForSeconds(0.2f);
        }

        for (int m = 0; m < GameConsts.RANGE_COUNT; m++)
        {
            //spawn range
            SpawnUnits(RangePrefab);
            yield return new WaitForSeconds(0.2f);
        }

        if (WaveNumber != 0 && WaveNumber % 3 == 0)
        {
            for (int m = 0; m < GameConsts.CANNON_COUNT; m++)
            {
                //spawn Cannon every 2 waves
                SpawnUnits(CannonPrefab);
                yield return new WaitForSeconds(0.2f);

            }
        }
        if (Inhibitor)
        {
            for (int m = 0; m < GameConsts.SUPER_ALL_COUNT; m++)
            {
                SpawnUnits(SuperPrefab);
                yield return new WaitForSeconds(0.2f);

            }
        }
        WaveNumber++;
    }

    private void SpawnUnits(GameObject prefab)
    {
        Instantiate(prefab, MinionSpawnPoints.transform.position, Quaternion.identity);
    }
}
