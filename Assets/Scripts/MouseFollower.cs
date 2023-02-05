using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPos[2] = 0.0f;
        gameObject.transform.position = spawnPos;
    }
}
