using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MIDI;

public class PlacementOnMesh_Character : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject placementObject;

    private List<GameObject> placedObjects = new List<GameObject>();
    private bool isPlaced = false;

    public static event Action characterPlaced;

    private void OnEnable()
    {
        TouchManager.Instance.OnTouchStart += OnTouch;
    }

    private void OnDisable()
    {
        TouchManager.Instance.OnTouchStart -= OnTouch;
    }

    private void OnTouch()
    {
        if (isPlaced) return;

        Vector2 touchPosition = TouchManager.Instance.TouchPosition;

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = touchPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {
            return;
        }

        TouchToRay(touchPosition);
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