using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class TeamManager : MonoBehaviour
{
    public int TeamID = 0;

  //  public List<ChampionStats> Champions;
    
    public Transform MinionSpawnPoints;

    public GameObject MeleePrefab;
    public GameObject RangePrefab;
    public GameObject CannonPrefab;
    public GameObject SuperPrefab;

    public int WaveNumber = 0;
    public float WaveTimer = 0;

    public bool MidInhibitor = false;
    public bool TopInhibitor = false;
    public bool BotInhibitor = false;

    private void Start()
    {
        WaveTimer = GameConsts.MINION_WAVE_TIME;
    }

    private void Update()
    {
        if (GameManager.Instance.GameTime < GameConsts.MINION_SPAWN_TIME) return;
        if (WaveTimer>=GameConsts.MINION_SPAWN_TIME)
        {
            Debug.Log($"Wave Number : {WaveNumber} has spawned at {GameManager.Instance.GameTime}");

            WaveTimer = 0;
            WaveNumber++;

            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        SpawnUnits(CannonPrefab);
        yield return null;     

        #region MinionWave Patern
        //    //if super spawn minions
        //    if(BotInhibitor||MidInhibitor||TopInhibitor)
        //    {
        //        if (BotInhibitor && MidInhibitor && TopInhibitor)
        //        {
        //            for(int m=0;m<GameConsts.SUPER_ALL_COUNT;m++)
        //            {
        //                SpawnUnits(SuperPrefab, GameConsts.SPAWN_TOP);
        //                SpawnUnits(SuperPrefab, GameConsts.SPAWN_MID);
        //                SpawnUnits(SuperPrefab, GameConsts.SPAWN_BOT);
        //            }
        //        }
        //        else
        //        {
        //            for(int m=0;m<GameConsts.SUPER_COUNT;m++)
        //            {

        //                SpawnUnits(SuperPrefab, GameConsts.SPAWN_TOP);
        //                SpawnUnits(SuperPrefab, GameConsts.SPAWN_MID);
        //                SpawnUnits(SuperPrefab, GameConsts.SPAWN_BOT);
        //            }
        //        }
        //    }
        //    for (int m = 0; m < 3; m++) 
        //    {
        //        //spawn melee
        //        SpawnUnits(MeleePrefab, GameConsts.SPAWN_TOP);
        //        SpawnUnits(MeleePrefab, GameConsts.SPAWN_BOT);
        //        SpawnUnits(MeleePrefab, GameConsts.SPAWN_MID);
        //    }

        //    for (int m = 0; m < 3; m++)
        //    {
        //        //spawn range
        //        SpawnUnits(RangePrefab, GameConsts.SPAWN_TOP);
        //        SpawnUnits(RangePrefab, GameConsts.SPAWN_BOT);
        //        SpawnUnits(RangePrefab, GameConsts.SPAWN_MID);
        //    }

        //    if(WaveNumber!=0&&WaveNumber%3==0)
        //    {
        //        //for(int m=0;m<GameConsts.CANNON_COUNT-1;m++)
        //        //{
        //        //    //spawn Cannon every 2 waves
        //        //    SpawnUnits(CannonPrefab, GameConsts.SPAWN_TOP);
        //        //    SpawnUnits(CannonPrefab, GameConsts.SPAWN_BOT);
        //        //    SpawnUnits(CannonPrefab, GameConsts.SPAWN_MID);
        //        //}
        //    }


        //}
        //else
        //{
        //}
        #endregion Minion wave pattern
    }

    private void SpawnUnits(GameObject prefab)
    {
        #region Wave Spawner

        Instantiate(prefab, MinionSpawnPoints.transform.position, Quaternion.identity);

        
        #endregion WaveSpawner
    }
}
