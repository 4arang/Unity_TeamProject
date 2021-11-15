using UnityEngine;

public class TestSetTarget : MonoBehaviour
{
    public GameObject myChampion;
    RaycastHit hit;

    void Start()
    {
        if (myChampion == null)//�߰�
            myChampion = this.gameObject;
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
                        myChampion.GetComponent<TestBasicAttack>().targetedEnemy = hit.collider.gameObject;
                        Debug.Log($"{hit.collider.gameObject}�� Ÿ���� �����Ǿ���");
                    }
                    
                    //If the Champion is targetable
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Champion)
                    {
                        Debug.Log("è�Ǿ� Ŭ��");
                       // myChampion.GetComponent<Player_Combat>().targetedEnemy = hit.collider.gameObject;
                    }

                    //If the Other Obstacle is targeted
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Champion)
                    {
                        Debug.Log("è�Ǿ� Ŭ��");
                        // myChampion.GetComponent<Player_Combat>().targetedEnemy = hit.collider.gameObject;
                    }
                }

                else
                {
                    return;
                }
            }
        }
    }

    void DetectTeam()
    {
        //hit.collider.gameObject.GetComponent<Stats>()
        //return whichTeam;
    }
}
