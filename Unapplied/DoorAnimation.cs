using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour
{
    private Animator anim;   //instance of the animator object                    
    private HashIds hash;    //instance of the HashId object                     
    private GameObject player;  //instance of the player object               
    private int count; //counter for the doors opening and closing                         
    
    void Awake()
    {
        anim = GetComponent<Animator>();//setting up animator reference
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIds>();//setting up hash reference
        player = GameObject.FindGameObjectWithTag("Player");//setting up player reference
    }

    void Update()
    {
        anim.SetBool(hash.openBool, count > 0);//setting up reference
    }

    void OnTriggerEnter(Collider obj)// when enters the sphere collider's orbit
    {
        if (obj.gameObject == player)// if the gameobject is the player
        {
                count++;//open door
        }
    }

    void OnTriggerExit(Collider obj) // when exited the sphere collider's orbit
    {
        if (obj.gameObject == player)// if the gameobject is the player
        {
            count--; // close door
        }
            
    }


}