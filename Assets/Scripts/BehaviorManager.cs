using System.Collections.Generic;
using UnityEngine;

public enum BehaviorState
{
    Idle,
    HasBehavior,
}

public class BehaviorManager : MonoBehaviour
{
    public static BehaviorManager Instance;

    public BehaviorState BehaviorState = BehaviorState.Idle;

    public bool DisableBehaviors = false;

    [SerializeField] private List<EmotionBehaviorSO> behaviors;
    [SerializeField] private EmotionBehaviorSO currentBehavior;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform soundOrigin;
    [SerializeField] private float minTimeBetweenBehaviors = 5f;
    [SerializeField] private float maxTimeBetweenBehaviors = 10f;
    [SerializeField] private string victoryStateName = "Victory";


    private HashSet<EmotionBehaviorSO> triggeredBehaviors = new HashSet<EmotionBehaviorSO>();
    private float nextBehaviorTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ScheduleNextBehavior();
    }

    private void Update()
    {
        if (Time.time >= nextBehaviorTime && currentBehavior == null && !DisableBehaviors)
        {
            TriggerRandomBehavior();
        }
    }

    private void ScheduleNextBehavior()
    {
        BehaviorState = BehaviorState.Idle;
        nextBehaviorTime = Time.time + UnityEngine.Random.Range(minTimeBetweenBehaviors, maxTimeBetweenBehaviors);
    }

    private void TriggerRandomBehavior()
    {
        List<EmotionBehaviorSO> untriggeredBehaviors = behaviors.FindAll(b => !triggeredBehaviors.Contains(b));

        if (untriggeredBehaviors.Count > 0)
        {
            currentBehavior = untriggeredBehaviors[UnityEngine.Random.Range(0, untriggeredBehaviors.Count)];
        }
        else
        {
            currentBehavior = behaviors[UnityEngine.Random.Range(0, behaviors.Count)];
        }

        Debug.Log($"Triggering {currentBehavior.emotionEventName}");

        BehaviorState = BehaviorState.HasBehavior;
        triggeredBehaviors.Add(currentBehavior);
        PlayAnimatorState(currentBehavior.animationState);
    }

    public void PlayAnimatorState(string stateName)
    {
        animator.Play(stateName);
    }

    public void CheckEmpathyObject(EmpathyObjectSO empathyObject)
    {
        Debug.Log("Checking empathy object");
        Debug.Log($"Object: {empathyObject.relatedBehaviorType}, Current Behavior: {currentBehavior.type}");

        if (currentBehavior != null && empathyObject.relatedBehaviorType == currentBehavior.type)
        {
            PlayAnimatorState(victoryStateName);
            currentBehavior = null;
            triggeredBehaviors.Clear();
            ScheduleNextBehavior();
        }
    }
}
