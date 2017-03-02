using UnityEngine;
using System.Collections;

public class PartnerAI : MonoBehaviour
{
    Transform partnerTransform;

    public Transform[] enemy;     //instance of a player
    private Transform player;

    float moveSpeed = 8f; //inital speed
    float rotationSpeed = 3f; //rotation speeed
    float max_velocity; 

    Vector3 velocity;   
    Vector3 desired_velocity;

    void Awake()
    {
        partnerTransform = transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;//setting up the player 

        max_velocity = moveSpeed * Time.deltaTime;
    }

	void Update ()
    {

        if (Vector3.Distance(transform.position, enemy[0].position) > 5)
        {
            Flee(enemy[0].position);
        }

        if (Vector3.Distance(transform.position, enemy[1].position) > 5)
        {
            Flee(enemy[1].position);
        }

        if (Vector3.Distance(transform.position, enemy[2].position) > 5)
        {
            Flee(enemy[2].position);
        }

        if (Vector3.Distance(transform.position, enemy[3].position) > 5)
        {
            Flee(enemy[3].position);
        }

        if (Vector3.Distance(transform.position, player.position) > 5)
        {
            followPartner();
        }

    }

    void followPartner()
    {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            partnerTransform.rotation = Quaternion.Slerp(partnerTransform.rotation, Quaternion.LookRotation(player.position - partnerTransform.position), rotationSpeed * Time.deltaTime);
        
    }

    void Flee(Vector3 enemy)
    {
        velocity = partnerTransform.position - enemy;

        if (velocity.magnitude < 30)//if within 30 
        {
            partnerTransform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), rotationSpeed * Time.deltaTime);//rotate way
            desired_velocity = velocity.normalized * max_velocity;//desire velocity
            partnerTransform.position += desired_velocity;//moves partner
        }
    }
}

