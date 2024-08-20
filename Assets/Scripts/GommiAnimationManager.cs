using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GommiAnimationManager : MonoBehaviour
{
    [SerializeField] private GameObject silla;

    [SerializeField] private GameObject paraguas;

    [SerializeField] private GameObject zapatoIzq;
    [SerializeField] private GameObject zapatoDer;

    [SerializeField] private GameObject rain;

    public void ToggleSilla(float active)
    {
        bool isActive = active != 0;
        silla.SetActive(isActive);
    }

    public void ToggleParaguas(float active)
    {
        bool isActive = active != 0;
        paraguas.SetActive(isActive);
    }

    public void ToggleZapatos(float active)
    {
        bool isActive = active != 0;
        zapatoIzq.SetActive(isActive);
        zapatoDer.SetActive(isActive);
    }

    public void ToggleRain(float active)
    {
        bool isActive = active != 0;
        rain.SetActive(isActive);
    }
}
