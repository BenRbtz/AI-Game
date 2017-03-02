using UnityEngine;
using System.Collections;

public class EnemySquad : MonoBehaviour
{
    Transform leaderTransform;
    Transform playerTransform;

    GameObject[] follower;
    GameObject[] leader;
   
    Vector3 velocity;
    Vector3 desired_velocity;

    float time = 0;
    float max_velocity;// maximum velocity

    void Awake()
    {
        max_velocity = 3 * Time.deltaTime; // max velocity

        leaderTransform   = GameObject.FindGameObjectWithTag("Leader").transform;// get leader transforms
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        leader = GameObject.FindGameObjectsWithTag("Leader");//get leader object 
        follower = GameObject.FindGameObjectsWithTag("Follower");// get follower objects

        print("Number of enemy squad followers: " + (follower.Length));//display amount of followers
    }

    void Update()
    {
        leaderFollower();//continously follower a leader
    }


    void leaderFollower()
    {

        foreach (GameObject obj in leader)// all objects tagged at leader
        {
                wander(obj);//wander round
        }

        foreach (GameObject obj in follower) // all objects tagged as follower
        {
                follow(obj);
        }
    }

    void wander(GameObject obj)
    {
        time += Time.deltaTime;//timer
        obj.transform.Translate(Vector3.forward * Time.deltaTime * 4);//move forward
//
        if (time > 8)//after 8 seconds
        {
            velocity =  new Vector3(Random.Range(-45.0F, 45.0F),0, Random.Range(-45.0F, 45.0F));//rotate coordinates both in x and z 
            obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, Quaternion.LookRotation(velocity), 100 * Time.deltaTime);//rotate with new coordinates
            time = 0;//resets timer
        }
        obstacleAvoidance(obj);      // allow check for obstalces to avoid while wandering
    }

    void obstacleAvoidance(GameObject obj)
    {
        RaycastHit hit;

        if (Physics.Raycast(obj.transform.position, obj.transform.forward, out hit, 10))//front raycast
        {
            if (hit.collider.gameObject)
            {

                Debug.DrawLine(obj.transform.position, hit.point, Color.white);
                obj.transform.Rotate(0, 180, 0);
              //  velocity =  new Vector3(Random.Range(-45, 45),0, Random.Range(-45, 45));
            //    obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, Quaternion.LookRotation(velocity), 100 * Time.deltaTime);
            }
        }
        else if (Physics.Raycast(obj.transform.position, -obj.transform.right, out hit, 10))//left raycast
        {
            if (hit.collider.gameObject)
            {
                Debug.DrawLine(obj.transform.position, hit.point, Color.white);
                obj.transform.Rotate(0, 45, 0);
                //velocity =  new Vector3(0,Random.Range(45,90),0);
               // obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, Quaternion.LookRotation(velocity), 100 * Time.deltaTime);
            }
        }
        else if (Physics.Raycast(obj.transform.position, obj.transform.right, out hit, 10))// right raycast
        {
            if (hit.collider.gameObject)
            {
                Debug.DrawLine(obj.transform.position, hit.point, Color.white);
                obj.transform.Rotate(0, -45, 0);
              //  velocity =  new Vector3(0, Random.Range(-90,-45),0);
              //  obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, Quaternion.LookRotation(velocity), 100 * Time.deltaTime);
            }
        }
    }
    void follow(GameObject obj)
    {
        velocity = leaderTransform.position - obj.transform.position;// setting velocity as the left of distance of player and enemy
        

        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, Quaternion.LookRotation(velocity), max_velocity);// rotates enemy with the velocity and max velocity

        if (velocity.magnitude > 15 )
        {
            desired_velocity = velocity.normalized * max_velocity;
            obj.transform.position += desired_velocity;
        }
    }

}
