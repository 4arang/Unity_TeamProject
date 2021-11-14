using UnityEngine;

public class TestSetTarget : MonoBehaviour
{
    public GameObject myChampion;
    RaycastHit hit;

    void Start()
    {
        if (myChampion == null)//추가
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
                        Debug.Log($"{hit.collider.gameObject}로 타겟이 설정되었다");
                    }
                    
                    //If the Champion is targetable
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Champion)
                    {
                        Debug.Log("챔피언 클릭");
                       // myChampion.GetComponent<Player_Combat>().targetedEnemy = hit.collider.gameObject;
                    }

                    //If the Other Obstacle is targeted
                    if (hit.collider.gameObject.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Champion)
                    {
                        Debug.Log("챔피언 클릭");
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
