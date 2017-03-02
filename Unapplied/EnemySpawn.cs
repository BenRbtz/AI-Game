using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy2;
    float x, y, z;
    void Start ()
    {
        createEnemy();
        createEnemy();
    }

	void createEnemy()
    {
        x = Random.Range(-120, -65);//random x value within boundaries
        y = 1;//on top of floor
        z= Random.Range(-10, 30);// rand z within boundaires
        Instantiate(enemy2, enemy2.transform.position = new Vector3(x, y, z), enemy2.transform.rotation);//makes a new enemy spawn
    }     
}
