using UnityEngine;

public class TestInfo : MonoBehaviour
{
    public static TestInfo PI;
    public int mySelectedChampion;
    public int myTeam;
    public GameObject[] allCharacters;

    private void OnEnable()
    {
        if (TestInfo.PI == null)
        {
            TestInfo.PI = this;
        }
        else
        {
            if (TestInfo.PI != this)
            {
                Destroy(TestInfo.PI.gameObject);
                TestInfo.PI = this;
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedChampion = PlayerPrefs.GetInt("MyCharacter");
            Debug.Log("MySelectedChampion=" + mySelectedChampion);
        }
        else
        {
            mySelectedChampion = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedChampion);
            Debug.Log("MySelectedChampion=" + mySelectedChampion);
        }
    }
}
