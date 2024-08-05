using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlacementOnMesh_Character : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject placementObject;

    private List<GameObject> placedObjects = new List<GameObject>();
    private bool isPlaced = false;

    public static event Action characterPlaced;

    private InputActions gameInput;

    private void OnEnable()
    {
        gameInput = new InputActions();

        gameInput.Gameplay.Touch.performed += OnTouch;
        //gameInput.Gameplay.Click.performed += OnClick;

        gameInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        gameInput.Gameplay.Touch.performed -= OnTouch;
        //gameInput.Gameplay.Click.performed -= OnClick;

        gameInput.Gameplay.Disable();
    }

    private void OnTouch(InputAction.CallbackContext context)
    {
        if (isPlaced) return;

        Vector2 touchPosition = context.ReadValue<Vector2>();

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = touchPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {
            Debug.Log("We hit a UI Element");
            return;
        }

        TouchToRay(touchPosition);
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (isPlaced) return;

        Vector2 clickPosition = context.ReadValue<Vector2>();

        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UI Hit was recognized");
            return;
        }

        TouchToRay(clickPosition);
    }

    private void TouchToRay(Vector2 position)
    {
        Ray ray = mainCam.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            placedObjects.Add(Instantiate(placementObject, hit.point, Quaternion.FromToRotation(transform.up, hit.normal)));
            isPlaced = true;
            characterPlaced?.Invoke();
        }
    }
}