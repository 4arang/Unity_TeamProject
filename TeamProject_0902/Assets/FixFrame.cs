using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixFrame : MonoBehaviour
{
    public Camera camera;

    private void Start()
    {
        if (this.CompareTag("Player"))
        {
            camera = GetComponentInParent<Player_Return>().MainCamera;
        }
        else
            camera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + camera.transform.forward);
    }
}
