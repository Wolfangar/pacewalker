using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController3D : MonoBehaviour
{
    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    public GameObject wallLeft, wallRight;

    public float smooth = 2.5F;
    public float camSpeed = 3.5f;

    float height, width;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;

        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    // LateUpdate is called after Update each frame
    void Update()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        Vector3 pos = player.transform.position + offset;

        Vector3 screenPos = Camera.main.WorldToViewportPoint(player.transform.position);
        if (screenPos.x > 0.55)
        {
            //float newX = Mathf.Lerp(transform.position.x, player.transform.position.x, (smooth * Time.fixedDeltaTime));


            //float sss = Mathf.Lerp(transform.position.x, transform.position.x + 0.5f, smooth * Time.deltaTime);
            //transform.position = new Vector3(sss, transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x + camSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

        if (screenPos.x < 0.45)
        {
            transform.position = new Vector3(transform.position.x - camSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

        //transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, wallLeft.transform.position.x + width / 2, wallRight.transform.position.x - width /2), transform.position.y, transform.position.z);

        
    }
}