using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_CameraRoam : MonoBehaviour
{
    //space ют╥б╫ц event////
    public Transform player;
    public Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smoothness = 0.5f;
    ///////////////////////
    
    public float camWidthLeft = -25;
    public float camWidthRight = 25;
    public float camHeightUp = 20;
    public float camHeightDown = -30;

    public float camSpeed = 20;
    public float screenSizeThickness = 10;

    public float camHeight;

    private void Start()
    {
        //player = GameObject.FindWithTag("Player").transform;
        camHeight = transform.position.y;
        //if (player == null)
        //{
        //    player = GameObject.FindWithTag("Player").transform;
        //}
    }

    public void SetCameraTarget(Transform playertransform)
    {
        player = playertransform;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {

            cameraOffset = player.transform.position;
            cameraOffset.y = camHeight;
            cameraOffset.z -= 6;
            transform.position = Vector3.Slerp(transform.position, cameraOffset, smoothness);
        }

        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - screenSizeThickness)
        {
            if (pos.z <= camHeightUp)
            {
                pos.z += camSpeed * Time.deltaTime;
                pos.x += camSpeed * Time.deltaTime;
            }
        }

        //Down
        if (Input.mousePosition.y <= screenSizeThickness)
        {
            if (pos.z >= camHeightDown)
            {
                pos.z -= camSpeed * Time.deltaTime;
                pos.x -= camSpeed * Time.deltaTime;
            }
        }

        //Right
        if (Input.mousePosition.x >= Screen.width - screenSizeThickness)
        {
            if (pos.x <= camWidthRight)
            {
                pos.x += camSpeed * Time.deltaTime;
                pos.z -= camSpeed * Time.deltaTime;
            }

        }

        //Left
        if (Input.mousePosition.x <= screenSizeThickness)
        {
            if (pos.x >= camWidthLeft)
            {
                pos.x -= camSpeed * Time.deltaTime;
                pos.z += camSpeed * Time.deltaTime;

            }

        }

        transform.position = pos;
    }
}
