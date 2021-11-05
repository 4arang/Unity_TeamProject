using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_Attack : MonoBehaviour
{
  
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private void Satrt()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(-10.0f, 10.0f);
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

    }
    
}
