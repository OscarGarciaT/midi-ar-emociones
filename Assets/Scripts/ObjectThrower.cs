using MIDI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject UIButton;
    [SerializeField] private float forwardForceMultiplier = 2f;
    [SerializeField] private float throwForceMultiplier = 2f;
    [SerializeField] private float ballDestroyDelay = 2f;

    private GameObject ball;
    private bool holding;
    private Vector3 newPosition;
    private Vector3 lastPosition;
    private float swipeStartTime;
    private float swipeEndTime;

    private void OnEnable()
    {
        TouchManager.Instance.OnTouchEnd += ThrowBall;
        GameManager.Instance.OnThrowObjectChange += (EmpathyObjectSO newObject) => BallPrefab = newObject.ball3DPrefab;
        AddEventTriggerListener(UIButton, EventTriggerType.PointerDown, OnUIButtonClick);
    }

    private void OnDisable()
    {
        //TouchManager.Instance.OnTouchEnd -= ThrowBall;
        //RemoveEventTriggerListener(UIButton, EventTriggerType.PointerDown, OnUIButtonClick);
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

        // Ensure the velocity is significant
        if (swipeVelocity.magnitude < 0.1f)
        {
            swipeVelocity = Camera.main.transform.forward;
        }

        // Adjust forward force based on swipe speed
        Vector3 forwardForce = Camera.main.transform.forward * swipeVelocity.magnitude * forwardForceMultiplier;

        // Combine swipe direction with forward force
        Vector3 throwDirection = (swipeVelocity + forwardForce).normalized * swipeVelocity.magnitude * throwForceMultiplier;

        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.useGravity = true;
        ballRigidbody.AddForce(throwDirection, ForceMode.VelocityChange);

        // Start coroutine to destroy ball after a delay
        StartCoroutine(DestroyBallAfterDelay(ballDestroyDelay));
    }

    private System.Collections.IEnumerator DestroyBallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (ball != null)
        {
            Destroy(ball);
            ball = null;
        }

        // Reset the UI button
        UIButton.SetActive(true);

        // Reset other necessary variables if any
        holding = false;
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
