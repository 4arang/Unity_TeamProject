using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public List<List<GameObject>> LaneSpawnPoints;

    public GameObject MeleePrefab;
    public GameObject RangePrefab;
    public GameObject CannonPrefab;
    public GameObject SuperPrefab;
    
    public int waveNumber = 0;
    public float waveTimer = 0;

    public bool MidInhibitor = false;
    public bool TopInhibitor = false;
    public bool BottomInhibitor = false;

    private void Start()
    {
        waveTimer = GameConsts.MINION_WAVE_TIME;
    }
    void Update()
    {
        SpawnWave();
    }

    void SpawnWave()
    {
        if (GameManager.Instance.GameTime < GameConsts.MINION_SPAWN_TIME)
            return;
        if (waveTimer >= GameConsts.MINION_WAVE_TIME)       //일정 시간을 초과하면 미니언 나오기
        {
            ////Check minion spawn time
            //System.TimeSpan t = System.TimeSpan.FromSeconds(InGameManager.Instance.gameTime);

            //Debug.Log(string.Format("WaveNumber:{0}has sapwned at{1}",
            //    waveNumber, string.Format("{0:D2}:{1:D2}",
            //    t.Minutes, t.Seconds)));


            //////////////////
            ///MinionsSpawn///
            //////////////////
            SpawnUnit(MeleePrefab, GameConsts.SPAWN_MID);


            //for(int i=0;i<SpawnPoints.Count;i++)                
            //{

            #region Wave Spawner
            //Add Melee Minion
            //for (int m = 0; m < GameConsts.MELEE_COUNT; m++)
            //{
            //    SpawnUnit(MeleePrefab, GameConsts.SPAWN_MID);
            //}

            ////Add Cast Minion
            //for (int r = 0; r < GameConsts.RANGE_COUNT; r++)
            //{
            //    SpawnUnit(RangePrefab, GameConsts.SPAWN_MID);
            //}

            ////Add Cannon Minion
            //if(waveNumber!=0&&waveNumber%3==0)          //Cannon Minion spawn every 3 waves
            //{
            //    for (int c = 0; c < GameConsts.CANNON_COUNT; c++)
            //    {
            //        SpawnUnit(CannonPrefab, GameConsts.SPAWN_MID);
            //    }
            //}


            //Add Super Minion
            //if (BottomInhibitor||MidInhibitor||TopInhibitor)
            //{
            //    if(BottomInhibitor&&MidInhibitor&&TopInhibitor)
            //    {
            //        for(int s=0;s<GameConsts.SUPER_ALL_COUNT;s++)
            //        {
            //            SpawnUnit(SuperPrefab, GameConsts.SPAWN_MID);
            //            //SpawnUnit(SuperPrefab, GameConsts.SPAWN_BOTTOM);
            //            //SpawnUnit(SuperPrefab, GameConsts.SPAWN_TOP);
            //        }
            //    }

            //    else
            //    {
            //        for(int s=0;s<GameConsts.SUPER_COUNT;s++)
            //        {
            //            SpawnUnit(SuperPrefab, GameConsts.SPAWN_MID);
            //            //SpawnUnit(SuperPrefab, GameConsts.SPAWN_BOTTOM);
            //            //SpawnUnit(SuperPrefab, GameConsts.SPAWN_TOP);
            //        }
            //    }
            //}
            #endregion
            waveTimer = 0;
            waveNumber++;
        }

        else
        {
            waveTimer += Time.deltaTime;
        }
    }

    private void SpawnUnit(GameObject prefab,int spawnLoc)
    {
        GameObject gameObject = Instantiate(prefab, SpawnPoints[GameConsts.SPAWN_MID].transform.position, Quaternion.identity);

        Minion minion = gameObject.GetComponent<Minion>();

        //switch (spawnLoc)
        //{
        //    case 0:// Case0 would be only use on this prj
        //        {
        //            minion.path = MidSpawnPoints;
        //            minion.Target = MidSpawnPoints[0].transform;        //Initialize path
        //            break;
        //        }

        //    case 1:     //top
        //        {
        //            minion.path = TopSpawnPoints;
        //            minion.Target = MidSpawnPoints[0].transform;        //Initialize path

        //            break;
        //        }

        //    case 2:     //bottom
        //        {
        //            minion.path = BotSpawnPoints;
        //            minion.Target = MidSpawnPoints[0].transform;        //Initialize path

        //            break;
        //        }
        //}
    }

}
               
