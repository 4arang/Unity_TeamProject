using UnityEngine;
using UnityEngine.AI;
public class TestMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    private TestBasicAttack heroCombatScript;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        heroCombatScript = GetComponent<TestBasicAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heroCombatScript.targetedEnemy != null)
        {
            if (heroCombatScript.targetedEnemy.GetComponent<TestBasicAttack>() != null)
            {
                if (!heroCombatScript.targetedEnemy.GetComponent<TestBasicAttack>().isHeroAlive)
                {
                    heroCombatScript.targetedEnemy = null;
                }
            }

        }


        //When pressing the left mouse button
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            //Checking if the raycast shot hits something that uses the navmesh system.
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Floor")
                {
                    //MOVEMENT
                    agent.SetDestination(hit.point);
                    heroCombatScript.targetedEnemy = null;
                    agent.stoppingDistance = 0;

                    //ROTATION
                    Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        rotationToLookAt.eulerAngles.y,
                        ref rotateVelocity,
                        rotateSpeedMovement * (Time.deltaTime * 5));

                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }

            }
        }
    }
}
