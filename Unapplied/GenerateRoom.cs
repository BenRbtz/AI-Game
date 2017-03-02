using UnityEngine;
using System.Collections;

public class GenerateRoom : MonoBehaviour {

    public GameObject room; // room object
    float x, y, z;// coordinates for a room
    float dis;// distance between both rooms
    Vector3 pos; // first room position
    Vector3 pos2; // second room position

    void Start()
    {
        createRoom();  // create a room 
    }

    void createRoom()
    {
        x = Random.Range(90, 140); // x coordinate within the room
        y = 5;
        z = Random.Range(-20, 20); // z coordinate within the room

        pos = new Vector3(x, y, z); // creates a postion to place the room
        Instantiate(room, pos, room.transform.rotation); // makes an instance of the room prefab


        x = Random.Range(90,140); // new x coordinate
        z = Random.Range(-20, 20);// new z coordinate

        pos2 = new Vector3(x, y, z);// creates second position to place the room
        dis = Vector3.Distance(pos, pos2); // checks the distance between both coordinates

        while(dis<30)// if the distance is less than 30 create another coordinate
        {
            x = Random.Range(90, 140);//new x position
            z = Random.Range(0, 20);//new z position

            pos2 = new Vector3(x, y, z);//assigns new x and z coordinates
            dis = Vector3.Distance(pos, pos2);//check if the position is to close to the other position
        }
        Instantiate(room, pos2, room.transform.rotation); // second room instance
    }
}
