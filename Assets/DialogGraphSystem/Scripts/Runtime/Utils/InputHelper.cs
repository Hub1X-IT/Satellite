using System;
using UnityEngine;

namespace DialogSystem.Runtime.Utils
{
    public static class InputHelper
    {
        private static float _lastAdvanceTime;
        private const float ADVANCE_COOLDOWN = 0.15f;

        // Cached reflection types for new input & XR
        private static bool _checkedNewInput;
        private static bool _hasNewInput;
        private static Type _keyboardType;
        private static Type _mouseType;
        private static Type _touchType;
        private static Type _gamepadType;
        private static Type _xrControllerType;
        private static PropertyInfoCache _xrLeft, _xrRight;

        /// <summary>
        /// Checks if any generic "advance/confirm" input was triggered this frame.
        /// Works across keyboard, mouse, touch, controller, and VR.
        /// Fully safe even if the Input System or XR packages are missing.
        /// </summary>
        public static bool CheckGenericAdvanceInput()
        {
            if (Time.time - _lastAdvanceTime < ADVANCE_COOLDOWN)
                return false;

            // Lazy reflection detection once
            if (!_checkedNewInput)
            {
                _checkedNewInput = true;
                _keyboardType = Type.GetType("UnityEngine.InputSystem.Keyboard, Unity.InputSystem");
                _mouseType = Type.GetType("UnityEngine.InputSystem.Mouse, Unity.InputSystem");
                _touchType = Type.GetType("UnityEngine.InputSystem.Touchscreen, Unity.InputSystem");
                _gamepadType = Type.GetType("UnityEngine.InputSystem.Gamepad, Unity.InputSystem");
                _xrControllerType = Type.GetType("UnityEngine.InputSystem.XR.XRController, Unity.InputSystem");

                _hasNewInput = _keyboardType != null;
                if (_xrControllerType != null)
                {
                    _xrLeft = new PropertyInfoCache(_xrControllerType, "leftHand");
                    _xrRight = new PropertyInfoCache(_xrControllerType, "rightHand");
                }
            }

#if ENABLE_INPUT_SYSTEM
            // --- NEW INPUT SYSTEM (if available via reflection) ---
            try
            {
                // Keyboard
                var kb = _keyboardType.GetProperty("current")?.GetValue(null);
                if (kb != null)
                {
                    var anyKey = kb.GetType().GetProperty("anyKey")?.GetValue(kb);
                    var wasPressed = anyKey?.GetType().GetProperty("wasPressedThisFrame")?.GetValue(anyKey);
                    if (wasPressed is bool b && b) return RegisterAdvance();
                }

                // Mouse
                var mouse = _mouseType.GetProperty("current")?.GetValue(null);
                var leftBtn = mouse?.GetType().GetProperty("leftButton")?.GetValue(mouse);
                var leftPressed = leftBtn?.GetType().GetProperty("wasPressedThisFrame")?.GetValue(leftBtn);
                if (leftPressed is bool l && l) return RegisterAdvance();

                // Touch
                var touch = _touchType.GetProperty("current")?.GetValue(null);
                var press = touch?.GetType().GetProperty("primaryTouch")?.GetValue(touch);
                var pressed = press?.GetType().GetProperty("press")?.GetValue(press);
                var touchDown = pressed?.GetType().GetProperty("wasPressedThisFrame")?.GetValue(pressed);
                if (touchDown is bool t && t) return RegisterAdvance();

                // Gamepad
                var pad = _gamepadType.GetProperty("current")?.GetValue(null);
                if (pad != null)
                {
                    bool CheckButton(string prop)
                    {
                        var b = pad.GetType().GetProperty(prop)?.GetValue(pad);
                        return b != null && (bool)(b.GetType().GetProperty("wasPressedThisFrame")?.GetValue(b) ?? false);
                    }

                    if (CheckButton("buttonSouth") || CheckButton("buttonEast") || CheckButton("startButton"))
                        return RegisterAdvance();
                }

                // XR Controllers (Oculus / OpenXR)
                if (_xrControllerType != null)
                {
                    object left = _xrLeft?.GetValue();
                    object right = _xrRight?.GetValue();
                    bool CheckSelect(object hand)
                    {
                        if (hand == null) return false;
                        var act = hand.GetType().GetProperty("selectAction")?.GetValue(hand);
                        var action = act?.GetType().GetProperty("action")?.GetValue(act);
                        return (bool)(action?.GetType().GetMethod("WasPressedThisFrame")?.Invoke(action, null) ?? false);
                    }
                    if (CheckSelect(left) || CheckSelect(right))
                        return RegisterAdvance();
                }
            }
            catch
            {
                // if any reflection fails, silently fall back
            }
#endif

#if ENABLE_LEGACY_INPUT_MANAGER
            // --- OLD INPUT SYSTEM (always safe) ---
            if (Input.anyKeyDown)
                return RegisterAdvance();

            if (Input.GetMouseButtonDown(0))
                return RegisterAdvance();

            for (int i = 0; i < Input.touchCount; i++)
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                    return RegisterAdvance();

            if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
                return RegisterAdvance();
#endif

            return false;
        }

        private static bool RegisterAdvance()
        {
            _lastAdvanceTime = Time.time;
            return true;
        }

        // Small helper to cache XR property info
        private class PropertyInfoCache
        {
            private readonly System.Reflection.PropertyInfo _prop;
            private readonly Type _type;
            public PropertyInfoCache(Type type, string name)
            {
                _type = type;
                _prop = type.GetProperty(name);
            }
            public object GetValue()
            {
                return _prop?.GetValue(null);
            }
        }
    }
}
