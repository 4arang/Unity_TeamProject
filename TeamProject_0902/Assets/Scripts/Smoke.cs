using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//플레이어가 연기에 들어가는 동안 연기 투명해지도록 ->상대방에게는 전달하지않는정보로 똑같이 불투명?
public class Smoke : MonoBehaviour
{
    [SerializeField] private GameObject Smoke1;
    [SerializeField] private GameObject Smoke2;

    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Smoke1.SetActive(false);
            Smoke2.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))

        {

                Smoke1.SetActive(true);
                Smoke2.SetActive(true);
        }
    }
}
