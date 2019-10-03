using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform test;

    Quaternion initRot;

    //How quickly to turn the camera
    float turnSpeed = 60.0f;

    Vector3 offset;

    float msX;
    float msY;


    // Start is called before the first frame update
    void Start()
    {
        //Setup offset between character and camera
        initRot = transform.localRotation;
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        msX = Input.GetAxis("Mouse X");
        //When Z or C are pressed, camera will rotate in a certain direction
        if (msX != 0)
        {
            //Rotates over the Y axis
            offset = Quaternion.AngleAxis(msX * turnSpeed * Time.deltaTime, Vector3.up) * offset;
        }
        //Sets up the new camera position
        transform.position = player.transform.position + offset;
        //Looks at player, if it was rotated in this sceen
        transform.LookAt(test, Vector3.up);
    }
}
