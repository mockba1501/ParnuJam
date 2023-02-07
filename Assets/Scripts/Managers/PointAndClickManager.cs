using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickManager : MonoBehaviour
{
    public GameObject clickedGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PointAndClick();
    }

    public void PointAndClick() 
    {
        if (Input.GetMouseButtonDown(0))// When clicked Mouse-Left-Button
        {

            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
                Debug.Log("clicked: " + clickedGameObject.name);
                /*
                if (clickedGameObject.tag == "Root")
                {
                    playerActions.Collect();

                    (clickedGameObject);
                }
                else if (clickedGameObject.tag == "Fertilizer")
                {
                    if (clickedGameObject.tag == "PlayerChamber")
                    {
                        whichChamber = "PlayerChamber";
                    }
                    else if (clickedGameObject.tag == "EnemyChamber")
                    {
                        whichChamber = "EnemyChamber";
                    }

                    playerActions.Throw();
                }*/
            }
        }
    }
}
