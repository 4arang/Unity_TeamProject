using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSpawner : MonoBehaviour
{
    [SerializeField]
    private List<ChampionAvatarData> champDatas;
    [SerializeField]
    private GameObject champAvatarPrefabs;

    private void Start()
    {
        var newChamp = SpawnChamp(0);
        newChamp.Print();
    }
    public Champion SpawnChamp(int classIdx)
    {
        var newChamp = Instantiate(champAvatarPrefabs).GetComponent<Champion>();

        return newChamp;
    }
}
