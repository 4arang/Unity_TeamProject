using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_InputTargeting : MonoBehaviour
{
    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;

    void Start()
    {
        if(selectedHero==null)//�߰�
        selectedHero = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Minion Targeting
        if (Input.GetMouseButtonDown(1))
        {

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                
                if (hit.collider.GetComponent<Targetable>() != null)
                {
                    //If the minion is targetable
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
                    {
                        selectedHero.GetComponent<Player_Combat>().targetedEnemy = hit.collider.gameObject;
                    }
                    
                    //If the Champion is targetable
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Champion)
                    {
                        Debug.Log("è�Ǿ� Ŭ��");
                        selectedHero.GetComponent<Player_Combat>().targetedEnemy = hit.collider.gameObject;
                    }
                }

                else if (hit.collider.gameObject.GetComponent<Targetable>() == null)
                {
                    //Debug.Log("�ٸ��� Ŭ��");
                    selectedHero.GetComponent<Player_Combat>().targetedEnemy = null;
                }
            }
        }
    }
}
