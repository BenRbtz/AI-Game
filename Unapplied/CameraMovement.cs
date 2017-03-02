using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    private Transform player;           // Reference to the player's transform.
    private Vector3 cameraPos;       // The relative position of the camera from the player.

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;//instialises player transform
        cameraPos = transform.position - player.position;//insitalises camera postion with camera postion minus the player postion
    }

    void FixedUpdate()
    {
        Vector3 pos = player.position + cameraPos;//coordinates to follow player position with camera

        transform.position = Vector3.Lerp(transform.position, pos, 100*Time.deltaTime);//applies new camera position
    }
}   