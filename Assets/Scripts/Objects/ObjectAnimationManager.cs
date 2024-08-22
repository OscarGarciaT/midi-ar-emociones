using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimationManager : MonoBehaviour
{
    [SerializeField] private Vector3 targetScale = new Vector3(1, 1, 1);
    [SerializeField] private float animationDuration = 0.5f;

    // Method to enable the GameObject and scale it up from 0 to target scale
    public void EnableObject()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(targetScale, animationDuration).SetEase(Ease.OutBack);
    }

    // Method to scale down the GameObject from current scale to 0 and then disable it
    public void DisableObject()
    {
        transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
