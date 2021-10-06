using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColD_E_grenade : MonoBehaviour
{ 
    private Rigidbody rigidBody;
    public float maxSpeed = 10.0f;

    private Vector3 direct;

    void Start()
    {
        direct = GetComponentInParent<Transform>().position;
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 3.0f);

    }

    void Update()
    {
        Vector3 position = rigidBody.transform.position;
  
        position = new Vector3(position.x + maxSpeed*Time.deltaTime,
            position.y, position.z + maxSpeed * Time.deltaTime);

        rigidBody.MovePosition(position);

    }
    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}
}
