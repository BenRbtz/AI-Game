using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour
{
    float viewAngle = 90f;           // Number of degrees, centred on forward, for the enemy see.
    public bool playerInSight = false;      // Whether or not the player is currently sighted.
    
    private SphereCollider col;             // Reference to the sphere collider trigger component.
    private GameObject player;              // Reference to the player.

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");//setting up the player 
        col = GetComponent<SphereCollider>(); //gets collider
        playerInSight = false;//sets player in sight as false 
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)// if the player comes into the enemy collider
        {
            Vector3 dir = other.transform.position - transform.position;// vision in front
            float angle = Vector3.Angle(dir, transform.forward);//vision angle 

            if (angle < viewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, dir.normalized, out hit, col.radius*10))// ray cast consisting of a field of view
                {
                    if (hit.collider.gameObject == player)// if the player is within the enemy view
                    {
                        playerInSight = true;// set the player as in sight.
                    }

                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)//if player leaves the collider
            playerInSight = false;//sets to false
    }
}