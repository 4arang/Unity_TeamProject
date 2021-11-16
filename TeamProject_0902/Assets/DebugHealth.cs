using UnityEngine;
using UnityEngine.UI;
public class DebugHealth : MonoBehaviour
{
    public GameObject TargetEnemy;
    public Minion_Stats stats;
    public Text health;
    private void Start()
    {
        stats = TargetEnemy.GetComponent<Minion_Stats>();
        //health.text = stats.HP.ToString();
    }
    private void Update()
    {
        health.text = stats.HP.ToString();
        Debug.Log($"health.text{health.text}");
    }

}
