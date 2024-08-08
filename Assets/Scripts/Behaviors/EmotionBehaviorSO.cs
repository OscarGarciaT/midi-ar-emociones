using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmotionBehaviorType
{
    DolorPies,
    TristezaLluvia,
    CansancioSilla,
}

[CreateAssetMenu(fileName = "EmotionBehavior", menuName = "Behaviors/New Emotion Behavior")]
public class EmotionBehaviorSO : ScriptableObject
{
    public string emotionEventName;

    public string animationState;

    public AudioClip audio;

    public EmotionBehaviorType type;
}
