using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowSpawner : MonoBehaviour
{

    public GameObject toSpawn;
    public GameObject sourceObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown (0))
        {
            RaycastHit2D hitObject = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);      

       
        if (hitObject.collider != null && hitObject.collider.gameObject.name == sourceObject.gameObject.name)
        {
            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPos[2] = 0.0f;
            GameObject thisInstance = Instantiate(toSpawn, spawnPos, Quaternion.identity);
            
        }        
        }


    }
}
