// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

<<<<<<< Updated upstream
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
=======
using System;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{ 
>>>>>>> Stashed changes
    /// <summary>
    /// Base class for an input source.
    /// </summary>
    public abstract class BaseInputSource : MonoBehaviour, IInputSource
    {
<<<<<<< Updated upstream
=======
        protected InputManager inputManager;
  
        protected virtual void Start()
        {
            inputManager = InputManager.Instance;
        }

>>>>>>> Stashed changes
        public abstract SupportedInputInfo GetSupportedInputInfo(uint sourceId);

        public bool SupportsInputInfo(uint sourceId, SupportedInputInfo inputInfo)
        {
<<<<<<< Updated upstream
            return ((GetSupportedInputInfo(sourceId) & inputInfo) == inputInfo);
        }

        public abstract bool TryGetSourceKind(uint sourceId, out InteractionSourceInfo sourceKind);

        public abstract bool TryGetPointerPosition(uint sourceId, out Vector3 position);

        public abstract bool TryGetPointerRotation(uint sourceId, out Quaternion rotation);

        public abstract bool TryGetPointingRay(uint sourceId, out Ray pointingRay);

        public abstract bool TryGetGripPosition(uint sourceId, out Vector3 position);

        public abstract bool TryGetGripRotation(uint sourceId, out Quaternion rotation);

        public abstract bool TryGetThumbstick(uint sourceId, out bool isPressed, out Vector2 position);

        public abstract bool TryGetTouchpad(uint sourceId, out bool isPressed, out bool isTouched, out Vector2 position);

        public abstract bool TryGetSelect(uint sourceId, out bool isPressed, out double pressedValue);

        public abstract bool TryGetGrasp(uint sourceId, out bool isPressed);

        public abstract bool TryGetMenu(uint sourceId, out bool isPressed);
=======
            return (GetSupportedInputInfo(sourceId) & inputInfo) != 0;
        }

        public abstract bool TryGetPosition(uint sourceId, out Vector3 position);

        public abstract bool TryGetOrientation(uint sourceId, out Quaternion orientation);
>>>>>>> Stashed changes
    }
}
