using DG.Tweening;
using UnityEngine;

public class StartGameUIAnimation : MonoBehaviour
{
    [Header("Glove")]
    [SerializeField] private Transform gloveTransform;
    [SerializeField] private float gloveDuration = 0.5f;
    [SerializeField] private Vector3 targetPosition = Vector3.zero;


    [Header("Texto")]
    [SerializeField] private Transform textTransform;
    [SerializeField] private float textScaleDuration = 0.5f;
    [SerializeField] private Vector3 minScale = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private Vector3 maxScale = new Vector3(2.0f, 2.0f, 2.0f);

    private void OnEnable()
    {
        StartGloveAnimation();
        StartTextScaleAnimation();
    }

    private void StartGloveAnimation()
    {
        // Ensure the initial position is set
        Vector3 initialPosition = gloveTransform.position;
        Vector3 targetPosition1 = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
        Vector3 targetPosition2 = targetPosition;

        // Create the animation sequence
        Sequence gloveSequence = DOTween.Sequence();
        gloveSequence.Append(gloveTransform.DOMove(targetPosition1, gloveDuration).SetEase(Ease.InOutSine))
                     .Append(gloveTransform.DOMove(targetPosition2, gloveDuration).SetEase(Ease.InOutSine))
                     .SetLoops(-1, LoopType.Yoyo); // Loop indefinitely
    }

    private void StartTextScaleAnimation()
    {
        // Create the animation sequence
        Sequence textScaleSequence = DOTween.Sequence();
        textScaleSequence.Append(textTransform.DOScale(maxScale, textScaleDuration).SetEase(Ease.InOutSine))
                         .Append(textTransform.DOScale(minScale, textScaleDuration).SetEase(Ease.InOutSine))
                         .SetLoops(-1, LoopType.Yoyo); // Loop indefinitely
    }
}
