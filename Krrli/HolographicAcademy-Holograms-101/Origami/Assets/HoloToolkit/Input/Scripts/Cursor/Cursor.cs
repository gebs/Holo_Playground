// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    /// <summary>
    /// Object that represents a cursor in 3D space controlled by gaze.
    /// </summary>
    public abstract class Cursor : MonoBehaviour, ICursor
    {
<<<<<<< Updated upstream
        public CursorStateEnum CursorState { get { return cursorState; } }
        private CursorStateEnum cursorState = CursorStateEnum.None;

        [SerializeField]
        [Tooltip("Set this in the editor to an object with a component that implements IPointerSource to tell this cursor which pointer to follow. To set the pointer programmatically, set Pointer directly.")]
        protected GameObject LoadPointer;

        /// <summary>
        /// The pointer that this cursor should follow and process input from.
        /// </summary>
        public IPointingSource Pointer { get; set; }
=======
        /// <summary>
        /// Enum for current cursor state
        /// </summary>
        public enum CursorStateEnum
        {
            /// <summary>
            /// Useful for releasing external override.
            /// See <c>CursorStateEnum.Contextual</c>
            /// </summary>
            None = -1,
            /// <summary>
            /// Not IsHandVisible
            /// </summary>
            Observe,
            /// <summary>
            /// Not IsHandVisible AND not IsInputSourceDown AND TargetedObject exists
            /// </summary>
            ObserveHover,
            /// <summary>
            /// IsHandVisible AND not IsInputSourceDown AND TargetedObject is NULL
            /// </summary>
            Interact,
            /// <summary>
            /// IsHandVisible AND not IsInputSourceDown AND TargetedObject exists
            /// </summary>
            InteractHover,
            /// <summary>
            /// IsHandVisible AND IsInputSourceDown
            /// </summary>
            Select,
            /// <summary>
            /// Available for use by classes that extend Cursor.
            /// No logic for setting Release state exists in the base Cursor class.
            /// </summary>
            Release,
            /// <summary>
            /// Allows for external override
            /// </summary>
            Contextual
        }

        public CursorStateEnum CursorState { get { return cursorState; } }
        private CursorStateEnum cursorState = CursorStateEnum.None;
>>>>>>> Stashed changes

        /// <summary>
        /// Minimum distance for cursor if nothing is hit
        /// </summary>
<<<<<<< Updated upstream
        [Header("Cursor Distance")]
=======
        [Header("Cusor Distance")]
>>>>>>> Stashed changes
        [Tooltip("The minimum distance the cursor can be with nothing hit")]
        public float MinCursorDistance = 1.0f;

        /// <summary>
        /// Maximum distance for cursor if nothing is hit
        /// </summary>
        [Tooltip("The maximum distance the cursor can be with nothing hit")]
        public float DefaultCursorDistance = 2.0f;

        /// <summary>
        /// Surface distance to place the cursor off of the surface at
        /// </summary>
        [Tooltip("The distance from the hit surface to place the cursor")]
        public float SurfaceCursorDistance = 0.02f;

        [Header("Motion")]
        [Tooltip("When lerping, use unscaled time. This is useful for games that have a pause mechanism or otherwise adjust the game timescale.")]
        public bool UseUnscaledTime = true;

        /// <summary>
        /// Blend value for surface normal to user facing lerp
        /// </summary>
        public float PositionLerpTime = 0.01f;

        /// <summary>
        /// Blend value for surface normal to user facing lerp
        /// </summary>
        public float ScaleLerpTime = 0.01f;

        /// <summary>
        /// Blend value for surface normal to user facing lerp
        /// </summary>
        public float RotationLerpTime = 0.01f;

        /// <summary>
        /// Blend value for surface normal to user facing lerp
        /// </summary>
        [Range(0, 1)]
        public float LookRotationBlend = 0.5f;

        /// <summary>
        /// Visual that is displayed when cursor is active normally
        /// </summary>
<<<<<<< Updated upstream
        [Header("Transform References")]
=======
        [Header("Tranform References")]
>>>>>>> Stashed changes
        public Transform PrimaryCursorVisual;

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public Quaternion Rotation
        {
            get { return transform.rotation; }
        }

        public Vector3 LocalScale
        {
            get { return transform.localScale; }
        }

        /// <summary>
        /// Indicates if hand is current in the view
        /// </summary>
        protected bool IsHandVisible;

        /// <summary>
        /// Indicates air tap down
        /// </summary>
        protected bool IsInputSourceDown;

        protected GameObject TargetedObject;
        protected ICursorModifier TargetedCursorModifier;

        private uint visibleHandsCount = 0;
        private bool isVisible = true;

<<<<<<< Updated upstream
=======
        private GazeManager gazeManager;

>>>>>>> Stashed changes
        /// <summary>
        /// Position, scale and rotational goals for cursor
        /// </summary>
        private Vector3 targetPosition;
        private Vector3 targetScale;
        private Quaternion targetRotation;

        /// <summary>
        /// Indicates if the cursor should be visible
        /// </summary>
        public bool IsVisible
        {
            set
            {
                isVisible = value;
<<<<<<< Updated upstream
                SetVisibility(isVisible);
=======
                SetVisiblity(isVisible);
>>>>>>> Stashed changes
            }
        }

        #region MonoBehaviour Functions

        private void Awake()
        {
            // Use the setter to update visibility of the cursor at startup based on user preferences
            IsVisible = isVisible;
<<<<<<< Updated upstream
            SetVisibility(isVisible);
=======
            SetVisiblity(isVisible);
>>>>>>> Stashed changes
        }

        private void Start()
        {
<<<<<<< Updated upstream
            RegisterManagers();
            TryLoadPointerIfNeeded();
=======
            gazeManager = GazeManager.Instance;
            RegisterManagers();
>>>>>>> Stashed changes
        }

        private void Update()
        {
            UpdateCursorState();
            UpdateCursorTransform();
        }

        /// <summary>
        /// Override for enable functions
        /// </summary>
        protected virtual void OnEnable()
        {
<<<<<<< Updated upstream
            if (FocusManager.IsInitialized && Pointer != null)
            {
                OnPointerSpecificFocusChanged(Pointer, null, FocusManager.Instance.GetFocusedObject(Pointer));
=======
            if (gazeManager)
            {
                OnFocusedObjectChanged(null, gazeManager.HitObject);
>>>>>>> Stashed changes
            }
            OnCursorStateChange(CursorStateEnum.None);
        }

        /// <summary>
        /// Override for disable functions
        /// </summary>
        protected virtual void OnDisable()
        {
            TargetedObject = null;
            TargetedCursorModifier = null;
            visibleHandsCount = 0;
            IsHandVisible = false;
            OnCursorStateChange(CursorStateEnum.Contextual);
        }

        private void OnDestroy()
        {
            UnregisterManagers();
        }

        #endregion

        /// <summary>
        /// Register to events from the managers the cursor needs.
        /// </summary>
        protected virtual void RegisterManagers()
        {
<<<<<<< Updated upstream
=======
            // Register to gaze events
            gazeManager.FocusedObjectChanged += OnFocusedObjectChanged;

>>>>>>> Stashed changes
            // Register the cursor as a global listener, so that it can always get input events it cares about
            InputManager.Instance.AddGlobalListener(gameObject);

            // Setup the cursor to be able to respond to input being globally enabled / disabled
            if (InputManager.Instance.IsInputEnabled)
            {
                OnInputEnabled();
            }
            else
            {
                OnInputDisabled();
            }

            InputManager.Instance.InputEnabled += OnInputEnabled;
            InputManager.Instance.InputDisabled += OnInputDisabled;
<<<<<<< Updated upstream

            FocusManager.Instance.PointerSpecificFocusChanged += OnPointerSpecificFocusChanged;
=======
>>>>>>> Stashed changes
        }

        /// <summary>
        /// Unregister from events from the managers the cursor needs.
        /// </summary>
        protected virtual void UnregisterManagers()
        {
<<<<<<< Updated upstream
            if (InputManager.IsInitialized)
            {
                InputManager.Instance.InputEnabled -= OnInputEnabled;
                InputManager.Instance.InputDisabled -= OnInputDisabled;
                InputManager.Instance.RemoveGlobalListener(gameObject);
            }

            if (FocusManager.IsInitialized)
            {
                FocusManager.Instance.PointerSpecificFocusChanged -= OnPointerSpecificFocusChanged;
            }
        }

        private void TryLoadPointerIfNeeded()
        {
            if (Pointer != null)
            {
                // Nothing to do. Keep the pointer that must have been set programmatically.
            }

            else if (LoadPointer != null)
            {
                Pointer = LoadPointer.GetComponent<IPointingSource>();

                if (Pointer == null)
                {
                    Debug.LogErrorFormat("Load pointer object \"{0}\" is missing its {1} component.",
                        LoadPointer.name,
                        typeof(IPointingSource).Name
                        );
                }
            }
            else if (FocusManager.IsInitialized)
            {
                // For backward-compatibility, if a pointer wasn't specified, but there's exactly one
                // pointer currently registered with FocusManager, we use it.
                IPointingSource pointingSource;
                if (FocusManager.Instance.TryGetSinglePointer(out pointingSource))
                {
                    Pointer = pointingSource;
                }
            }
            else
            {
                // No options available, so we leave Pointer unset. It will need to be set programmatically later.
=======
            if (gazeManager != null)
            {
                gazeManager.FocusedObjectChanged -= OnFocusedObjectChanged;
            }

            if (InputManager.Instance != null)
            {
                InputManager.Instance.RemoveGlobalListener(gameObject);
                InputManager.Instance.InputEnabled -= OnInputEnabled;
                InputManager.Instance.InputDisabled -= OnInputDisabled;
>>>>>>> Stashed changes
            }
        }

        /// <summary>
        /// Updates the currently targeted object and cursor modifier upon getting
        /// an event indicating that the focused object has changed.
        /// </summary>
<<<<<<< Updated upstream
        /// <param name="pointer">The pointer associated with this focus change.</param>
        /// <param name="oldFocusedObject">Object that was previously being focused.</param>
        /// <param name="newFocusedObject">New object being focused.</param>
        protected virtual void OnPointerSpecificFocusChanged(IPointingSource pointer, GameObject oldFocusedObject, GameObject newFocusedObject)
        {
            if (pointer == Pointer)
            {
                TargetedObject = newFocusedObject;

                CursorModifier newModifier = (newFocusedObject == null)
                    ? null
                    : newFocusedObject.GetComponent<CursorModifier>();

                OnActiveModifier(newModifier);
=======
        /// <param name="previousObject">Object that was previously being focused.</param>
        /// <param name="newObject">New object being focused.</param>
        protected virtual void OnFocusedObjectChanged(GameObject previousObject, GameObject newObject)
        {
            TargetedObject = newObject;
            if (newObject != null)
            {
                OnActiveModifier(newObject.GetComponent<CursorModifier>());
>>>>>>> Stashed changes
            }
        }

        /// <summary>
        /// Override function when a new modifier is found or no modifier is valid
        /// </summary>
        /// <param name="modifier"></param>
        protected virtual void OnActiveModifier(CursorModifier modifier)
        {
            TargetedCursorModifier = modifier;
        }

        /// <summary>
        /// Update the cursor's transform
        /// </summary>
        protected virtual void UpdateCursorTransform()
        {
<<<<<<< Updated upstream
            FocusDetails focusDetails = FocusManager.Instance.GetFocusDetails(Pointer);
            GameObject newTargetedObject = focusDetails.Object;

            // Get the forward vector looking back along the pointing ray.
            Vector3 lookForward = -Pointer.Ray.direction;
=======
            // Get the necessary info from the gaze source
            RaycastHit hitResult = gazeManager.HitInfo;
            GameObject newTargetedObject = gazeManager.HitObject;

            // Get the forward vector looking back at camera
            Vector3 lookForward = -gazeManager.GazeNormal;
>>>>>>> Stashed changes

            // Normalize scale on before update
            targetScale = Vector3.one;

            // If no game object is hit, put the cursor at the default distance
            if (newTargetedObject == null)
            {
<<<<<<< Updated upstream
                TargetedObject = null;
                TargetedCursorModifier = null;
                targetPosition = Pointer.Ray.origin + Pointer.Ray.direction * DefaultCursorDistance;
=======
                this.TargetedObject = null;
                this.TargetedCursorModifier = null;
                targetPosition = gazeManager.GazeOrigin + gazeManager.GazeNormal * DefaultCursorDistance;
>>>>>>> Stashed changes
                targetRotation = lookForward.magnitude > 0 ? Quaternion.LookRotation(lookForward, Vector3.up) : transform.rotation;
            }
            else
            {
                // Update currently targeted object
<<<<<<< Updated upstream
                TargetedObject = newTargetedObject;
=======
                this.TargetedObject = newTargetedObject;
>>>>>>> Stashed changes

                if (TargetedCursorModifier != null)
                {
                    TargetedCursorModifier.GetModifiedTransform(this, out targetPosition, out targetRotation, out targetScale);
                }
                else
                {
                    // If no modifier is on the target, just use the hit result to set cursor position
<<<<<<< Updated upstream
                    targetPosition = focusDetails.Point + (lookForward * SurfaceCursorDistance);
                    Vector3 lookRotation = Vector3.Slerp(focusDetails.Normal, lookForward, LookRotationBlend);
                    targetRotation = Quaternion.LookRotation(lookRotation == Vector3.zero ? lookForward : lookRotation, Vector3.up);
=======
                    targetPosition = hitResult.point + (lookForward * SurfaceCursorDistance);
                    targetRotation = Quaternion.LookRotation(Vector3.Lerp(hitResult.normal, lookForward, LookRotationBlend), Vector3.up);
>>>>>>> Stashed changes
                }
            }

            float deltaTime = UseUnscaledTime
                ? Time.unscaledDeltaTime
                : Time.deltaTime;

            // Use the lerp times to blend the position to the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, deltaTime / PositionLerpTime);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, deltaTime / ScaleLerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, deltaTime / RotationLerpTime);
        }

        /// <summary>
        /// Updates the visual representation of the cursor.
        /// </summary>
<<<<<<< Updated upstream
        public virtual void SetVisibility(bool visible)
=======
        public void SetVisiblity(bool visible)
>>>>>>> Stashed changes
        {
            if (PrimaryCursorVisual != null)
            {
                PrimaryCursorVisual.gameObject.SetActive(visible);
            }
        }

        /// <summary>
        /// Disable input and set to contextual to override input
        /// </summary>
        public virtual void OnInputDisabled()
        {
            // Reset visible hands on disable
            visibleHandsCount = 0;
            IsHandVisible = false;

            OnCursorStateChange(CursorStateEnum.Contextual);
        }

        /// <summary>
        /// Enable input and set to none to reset cursor
        /// </summary>
        public virtual void OnInputEnabled()
        {
            OnCursorStateChange(CursorStateEnum.None);
        }

        /// <summary>
        /// Function for consuming the OnInputUp events
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnInputUp(InputEventData eventData)
        {
<<<<<<< Updated upstream
            if (Pointer.OwnsInput(eventData))
            {
                IsInputSourceDown = false;
            }
=======
            IsInputSourceDown = false;
>>>>>>> Stashed changes
        }

        /// <summary>
        /// Function for receiving OnInputDown events from InputManager
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnInputDown(InputEventData eventData)
        {
<<<<<<< Updated upstream
            if (Pointer.OwnsInput(eventData))
            {
                IsInputSourceDown = true;
            }
=======
            IsInputSourceDown = true;
>>>>>>> Stashed changes
        }

        /// <summary>
        /// Function for receiving OnInputClicked events from InputManager
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnInputClicked(InputClickedEventData eventData)
        {
            // Open input socket for other cool stuff...
        }


        /// <summary>
        /// Input source detected callback for the cursor
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnSourceDetected(SourceStateEventData eventData)
        {
<<<<<<< Updated upstream
            if (Pointer.OwnsInput(eventData))
            {
                visibleHandsCount++;
                IsHandVisible = true;
            }
=======
            visibleHandsCount++;
            IsHandVisible = true;
>>>>>>> Stashed changes
        }


        /// <summary>
        /// Input source lost callback for the cursor
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnSourceLost(SourceStateEventData eventData)
        {
<<<<<<< Updated upstream
            if (Pointer.OwnsInput(eventData))
            {
                visibleHandsCount--;
                if (visibleHandsCount == 0)
                {
                    IsHandVisible = false;
                    IsInputSourceDown = false;
                }
=======
            visibleHandsCount--;
            if (visibleHandsCount == 0)
            {
                IsHandVisible = false;
                IsInputSourceDown = false;
>>>>>>> Stashed changes
            }
        }

        /// <summary>
        /// Internal update to check for cursor state changes
        /// </summary>
        private void UpdateCursorState()
        {
            CursorStateEnum newState = CheckCursorState();
            if (cursorState != newState)
            {
                OnCursorStateChange(newState);
            }
        }

        /// <summary>
        /// Virtual function for checking state changess.
        /// </summary>
        public virtual CursorStateEnum CheckCursorState()
        {
            if (cursorState != CursorStateEnum.Contextual)
            {
                if (IsInputSourceDown)
                {
                    return CursorStateEnum.Select;
                }
                else if (cursorState == CursorStateEnum.Select)
                {
                    return CursorStateEnum.Release;
                }

                if (IsHandVisible)
                {
                    return TargetedObject != null ? CursorStateEnum.InteractHover : CursorStateEnum.Interact;
                }
                return TargetedObject != null ? CursorStateEnum.ObserveHover : CursorStateEnum.Observe;
            }
            return CursorStateEnum.Contextual;
        }

        /// <summary>
        /// Change the cursor state to the new state.  Override in cursor implementations.
        /// </summary>
        /// <param name="state"></param>
        public virtual void OnCursorStateChange(CursorStateEnum state)
        {
            cursorState = state;
        }
    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
