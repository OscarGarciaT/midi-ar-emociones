using MIDI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject UIButton;

    private GameObject ball;
    private bool holding;
    private Vector3 newPosition;
    private Vector3 lastPosition;
    private float swipeStartTime;
    private float swipeEndTime;

    private void OnEnable()
    {
        TouchManager.Instance.OnTouchEnd += ThrowBall;
        AddEventTriggerListener(UIButton, EventTriggerType.PointerDown, OnUIButtonClick);
    }

    private void OnDisable()
    {
        TouchManager.Instance.OnTouchEnd -= ThrowBall;
        RemoveEventTriggerListener(UIButton, EventTriggerType.PointerDown, OnUIButtonClick);
    }

    private void Update()
    {
        if (holding)
        {
            PickupBall();
        }
    }

    private void OnUIButtonClick(BaseEventData eventData)
    {
        UIButton.SetActive(false);
        SpawnBall();
    }

    private void SpawnBall()
    {
        Vector3 touchPos = TouchManager.Instance.TouchPosition;

        holding = true;

        touchPos.z = Camera.main.nearClipPlane * 3f;
        newPosition = Camera.main.ScreenToWorldPoint(touchPos);
        ball = Instantiate(BallPrefab, newPosition, Quaternion.identity);

        ball.GetComponent<Rigidbody>().useGravity = false;

        lastPosition = newPosition;
        swipeStartTime = Time.time;
    }

    private void PickupBall()
    {
        Vector3 touchPos = TouchManager.Instance.TouchPosition;
        touchPos.z = Camera.main.nearClipPlane * 3f;
        newPosition = Camera.main.ScreenToWorldPoint(touchPos);
        ball.transform.position = Vector3.Lerp(ball.transform.position, newPosition, 80f * Time.deltaTime);

        // Update the last position and swipe time
        lastPosition = newPosition;
        swipeEndTime = Time.time;
    }

    private void ThrowBall()
    {
        if (!holding) return;

        holding = false;

        // Calculate swipe direction and velocity
        Vector3 swipeVelocity = (newPosition - lastPosition) / (swipeEndTime - swipeStartTime);

        // Normalize swipe direction
        swipeVelocity = swipeVelocity.normalized;

        // Add a forward force to ensure the ball goes forward in 3D space
        Vector3 forwardForce = Camera.main.transform.forward * 5f; // Adjust this multiplier to control forward force

        // Combine swipe direction with forward force
        Vector3 throwDirection = (swipeVelocity + forwardForce).normalized * 10f; // Adjust this multiplier to control throw force

        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.useGravity = true;
        ballRigidbody.AddForce(throwDirection, ForceMode.VelocityChange);

        // Reset variables
        ball = null;
    }

    private void AddEventTriggerListener(GameObject target, EventTriggerType eventType, Action<BaseEventData> callback)
    {
        EventTrigger trigger = target.GetComponent<EventTrigger>() ?? target.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(callback));
        trigger.triggers.Add(entry);
    }

    private void RemoveEventTriggerListener(GameObject target, EventTriggerType eventType, Action<BaseEventData> callback)
    {
        EventTrigger trigger = target.GetComponent<EventTrigger>();
        if (trigger == null) return;
        var entry = trigger.triggers.Find(e => e.eventID == eventType && e.callback.GetPersistentEventCount() > 0);
        if (entry != null)
        {
            entry.callback.RemoveListener(new UnityEngine.Events.UnityAction<BaseEventData>(callback));
        }
    }
}
