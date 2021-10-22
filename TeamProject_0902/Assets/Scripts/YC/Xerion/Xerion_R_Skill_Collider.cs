using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xerion_R_Skill_Collider : MonoBehaviour
{
    [SerializeField] private GameObject Xerion_R_Explosion;



    void Start()
    {
        Xerion_R_Explosion.SetActive(false);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {

            Xerion_R_Explosion.SetActive(true);

            Destroy(gameObject, 1.0f);
        }
    }
}
