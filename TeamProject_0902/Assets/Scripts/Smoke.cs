using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�÷��̾ ���⿡ ���� ���� ���� ������������ ->���濡�Դ� ���������ʴ������� �Ȱ��� ������?
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
