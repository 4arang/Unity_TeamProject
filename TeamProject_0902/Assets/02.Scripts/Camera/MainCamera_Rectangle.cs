using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_Rectangle : MonoBehaviour
{
   
    float RecPosY;
    Rigidbody2D rigidBody;
    
    void Start()
    {

       rigidBody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //RecPosY = GetComponentInParent<Transform>().position.y;
        //Vector3 position = rigidBody.transform.position;
        //position = new Vector3(position.x, position.y-RecPosY, position.z);
        //rigidBody.MovePosition(position);

    }
}
