using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmpathyObject", menuName = "EmpathyObjects/New Object")]
public class EmpathyObjectSO : ScriptableObject
{
    public string objectName;

    public string objectDescription;

    public Sprite objectSprite;

    public AudioClip objectAudio;

    public EmotionBehaviorType relatedBehaviorType;
}
