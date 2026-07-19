using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointAndClickManager : MonoBehaviour
{
    public GameObject clickedGameObject;
    private InputSystem_Actions inputSystemActions;

    // Start is called before the first frame update
    void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputSystemActions.UI.Click.performed += OnClick;
        inputSystemActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputSystemActions.UI.Click.performed -= OnClick;
        inputSystemActions.UI.Disable();
    }

    // Update is called once per frame
    void Update()
    {
       // PointAndClick();
    }

   /* public void PointAndClick() 
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        //if (Input.GetMouseButtonDown(0))// When clicked Mouse-Left-Button - Legacy Input !!!
        {
            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()); //instead of Input.mousePosition
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
                Debug.Log("clicked: " + clickedGameObject.name);
            }
        }
    }*/

    private void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        clickedGameObject = null;

        //Vector2 mousePosition = Mouse.current.position.ReadValue();
        //Vector2 pointPosition = pointAction.ReadValue<Vector2>();
        Vector2 pointPosition = inputSystemActions.UI.Point.ReadValue<Vector2>();
        SelectObject(pointPosition);
        
    }

    private void SelectObject(Vector2 pointPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(pointPosition); //instead of Input.mousePosition
        RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit2d)
        {
            clickedGameObject = hit2d.transform.gameObject;
            Debug.Log("clicked: " + clickedGameObject.name);
        }
    }
}
