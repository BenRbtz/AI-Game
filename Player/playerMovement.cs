using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour
{
    private Animator anim;
    private HashIds id;

    float speedDampTime = 1f;

    void Awake ()
    {
        anim = GetComponent<Animator>();//inistallises the animator 
        id = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIds>();//get hasid from gamecontroller id
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");//input that is horizontal key mapped is stored in h 
        float v = Input.GetAxis("Vertical");//input that is vertical key mapped is stored in h 

        MovementMangaement(h, v);// passed the two input variable to a method that handles movement
    }

    void MovementMangaement(float h, float v)
    {
        if(h != 0 || v != 0)
        {
            Rotating(h, v);// method works out the rotation when both h and v is pressed
            anim.SetFloat(id.speedFloat, 5.5f, speedDampTime, Time.deltaTime);// movement animation
        }
        else
        { 
            anim.SetFloat(id.speedFloat, 0);//idle animation
        }
    
    }
    void Rotating(float h,float v)
    {
        Vector3 direction = new Vector3(h, 0, v); //store direction in a vector3 object
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);//setting the player looking direction.
        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, rotation,10f* Time.deltaTime);// works out the player rotation.
        GetComponent<Rigidbody>().MoveRotation(newRotation);//rotates the rigibody with the player
    }
}
