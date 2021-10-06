using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun;
public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private Text feedbackText;
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    [SerializeField]
    private LoaderAnimation loaderAnime;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
