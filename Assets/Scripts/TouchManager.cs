using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using System;

namespace MIDI
{
    public class TouchManager : MonoBehaviour
    {
        public static TouchManager Instance { get; private set; }

        private InputActions touchControls;

        public bool IsTouching { get; private set; }
        public Vector2 TouchPosition { get; private set; }

        public Action OnTouchStart;
        public Action OnTouchEnd;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            touchControls = new InputActions();
        }

        private void OnEnable()
        {
            touchControls.Gameplay.TouchPress.started += OnTouchStarted;
            touchControls.Gameplay.TouchPress.canceled += OnTouchCanceled;
            touchControls.Enable();
        }

        private void OnDisable()
        {
            touchControls.Gameplay.TouchPress.started -= OnTouchStarted;
            touchControls.Gameplay.TouchPress.canceled -= OnTouchCanceled;
            touchControls.Disable();
        }

        private void Update()
        {
            if (IsTouching)
            {
                TouchPosition = touchControls.Gameplay.TouchPosition.ReadValue<Vector2>();
            }
        }

        private void OnTouchStarted(InputAction.CallbackContext context)
        {
            IsTouching = true;
            OnTouchStart?.Invoke();
        }

        private void OnTouchCanceled(InputAction.CallbackContext context)
        {
            IsTouching = false;
            OnTouchEnd?.Invoke();
        }
    }
}
