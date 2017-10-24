// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity
{
    /// <summary>
    /// A Tagalong that stays at a fixed distance from the camera and always
    /// seeks to stay on the edge or inside a sphere that is straight in front of the camera.
    /// </summary>
    public class SphereBasedTagalong : MonoBehaviour
    {
        [Tooltip("Sphere radius.")]
        public float SphereRadius = 1.0f;

        [Tooltip("How fast the object will move to the target position.")]
        public float MoveSpeed = 2.0f;

<<<<<<< Updated upstream
        /// <summary>
        /// When moving, use unscaled time. This is useful for games that have a pause mechanism or otherwise adjust the game timescale.
        /// </summary>
        [SerializeField]
        [Tooltip("When moving, use unscaled time. This is useful for games that have a pause mechanism or otherwise adjust the game timescale.")]
        private bool useUnscaledTime = true;

        /// <summary>
        /// Used to initialize the initial position of the SphereBasedTagalong before being hidden on LateUpdate.
        /// </summary>
        [SerializeField]
        [Tooltip("Used to initialize the initial position of the SphereBasedTagalong before being hidden on LateUpdate.")]
        private bool hideOnStart;

        [SerializeField]
        [Tooltip("Display the sphere in red wireframe for debugging purposes.")]
        private bool debugDisplaySphere;

        [SerializeField]
        [Tooltip("Display a small green cube where the target position is.")]
        private bool debugDisplayTargetPosition;
=======
        [Tooltip("When moving, use unscaled time. This is useful for games that have a pause mechanism or otherwise adjust the game timescale.")]
        public bool UseUnscaledTime = true;

        [Tooltip("Display the sphere in red wireframe for debugging purposes.")]
        public bool DebugDisplaySphere = false;

        [Tooltip("Display a small green cube where the target position is.")]
        public bool DebugDisplayTargetPosition = false;
>>>>>>> Stashed changes

        private Vector3 targetPosition;
        private Vector3 optimalPosition;
        private float initialDistanceToCamera;

<<<<<<< Updated upstream
        private void Start()
        {
            initialDistanceToCamera = Vector3.Distance(transform.position, CameraCache.Main.transform.position);
        }

        private void Update()
        {
            optimalPosition = CameraCache.Main.transform.position + CameraCache.Main.transform.forward * initialDistanceToCamera;
            Vector3 offsetDir = transform.position - optimalPosition;

=======
        void Start()
        {
            initialDistanceToCamera = Vector3.Distance(this.transform.position, Camera.main.transform.position);
        }

        void Update()
        {
            optimalPosition = Camera.main.transform.position + Camera.main.transform.forward * initialDistanceToCamera;

            Vector3 offsetDir = this.transform.position - optimalPosition;
>>>>>>> Stashed changes
            if (offsetDir.magnitude > SphereRadius)
            {
                targetPosition = optimalPosition + offsetDir.normalized * SphereRadius;

<<<<<<< Updated upstream
                float deltaTime = useUnscaledTime
                    ? Time.unscaledDeltaTime
                    : Time.deltaTime;

                transform.position = Vector3.Lerp(transform.position, targetPosition, MoveSpeed * deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (hideOnStart)
            {
                hideOnStart = !hideOnStart;
                gameObject.SetActive(false);
=======
                float deltaTime = UseUnscaledTime
                    ? Time.unscaledDeltaTime
                    : Time.deltaTime;

                this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, MoveSpeed * deltaTime);
>>>>>>> Stashed changes
            }
        }

        public void OnDrawGizmos()
        {
<<<<<<< Updated upstream
            if (Application.isPlaying == false) { return; }

            Color oldColor = Gizmos.color;

            if (debugDisplaySphere)
=======
            if (Application.isPlaying == false) return;

            Color oldColor = Gizmos.color;

            if (DebugDisplaySphere)
>>>>>>> Stashed changes
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(optimalPosition, SphereRadius);
            }

<<<<<<< Updated upstream
            if (debugDisplayTargetPosition)
=======
            if (DebugDisplayTargetPosition)
>>>>>>> Stashed changes
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(targetPosition, new Vector3(0.1f, 0.1f, 0.1f));
            }

            Gizmos.color = oldColor;
        }
    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
