using UnityEngine;
using System.Collections;

public class EnemyAI2 : MonoBehaviour
{
    private Transform enemy; //current transform data of this enemy
    private Transform player;
    
    Vector3 velocity;
    Vector3 desired_velocity; 

    float max_velocity;// maximum velocity
    float time=0;

    public EnemyVision vision; // instance of the enemy vison script

    public enum State //enum of the different states
    {
        Wander,
        Seek,
    }

    private State _state;

    public State state
    {
        get
        {
            return _state;//getter for state
        }

        set
        {
            _state = value;//stter for stae
        }
    }

    void Awake()
    {
        max_velocity = 4 * Time.deltaTime; // working out the max velocity ;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = transform;
        state = State.Wander;//default state as wander
    }
  

    void Update()
    {
        switch(state)// state machine
        {
            case State.Wander:// wander state
                wander();//returns wander function
                break;
            case State.Seek:
                seek();
                break;
        }
    }
    
    void wander()
    {
        time += Time.deltaTime;
        if(vision.playerInSight==true)// if the player walking into the enemy field of view
        {
            state = State.Seek;//switch to seeking state
        }
 
        time += Time.deltaTime;//timer
        enemy.Translate(Vector3.forward * Time.deltaTime * 4);//move enemy forward

        if (time > 7)//every 7 seconds
        {
            velocity = enemy.position - new Vector3(Random.Range(-45.0F, 45.0F), 0, Random.Range(-45.0F, 45.0F));// rotate in a random x and z direction between -45 and 45
            enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(velocity), 10 * Time.deltaTime);//rotates enemy
            time = 0;//reset timer
        }
        AvoidObstacle();//function to avoid objects
    }

    void AvoidObstacle()
    {

        RaycastHit hit;

        if (Physics.Raycast(enemy.position, enemy.forward, out hit, 10))// if raycast hits something within ten infront
        {
            if (hit.collider.gameObject)//any object
            {

                Debug.DrawLine(enemy.position, hit.point, Color.white);//show a debug line
              
                enemy.Rotate(0, 180, 0);//rotate 180 degrees
            }
        }
        else if (Physics.Raycast(enemy.position, -enemy.right, out hit, 5))//if raycast hits something within 5 on the left
        {
            if (hit.collider.gameObject)
            {
                Debug.DrawLine(enemy.position, hit.point, Color.white);
                enemy.Rotate(0, 45, 0);//rotate right
                //  enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(velocity), 10 * Time.deltaTime);
            }
        }
        else if (Physics.Raycast(enemy.position, enemy.right, out hit, 5))//if raycast hits something within 5 on left
        {
            Debug.DrawLine(enemy.position, hit.point, Color.white);
            if (hit.collider.gameObject)
            {
                enemy.Rotate(0, -45, 0);
            }
        }
    }

    void seek()
    {
        velocity = player.position - enemy.position;// setting velocity as the left of distance of player and enemy
        velocity.y = 0;// set the y velcoity to 0 just in case 

        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(velocity), max_velocity);// rotates enemy with the velocity and max velocity

        if(velocity.magnitude>6) //if length of velocity is greater than 6
        {
            desired_velocity = velocity.normalized * max_velocity;
            enemy.position += desired_velocity;
        }
        
        if (vision.playerInSight == false)// if not in sight
        {
            state = State.Wander;//set to patrol state
        }
    }
}

