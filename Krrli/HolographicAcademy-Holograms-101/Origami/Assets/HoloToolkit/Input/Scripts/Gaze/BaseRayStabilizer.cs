// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    /// <summary>
<<<<<<< Updated upstream
    /// A base class for a stabilizer that takes an input position and rotation, and performs operations on them
=======
    /// A base class for a stabilizer that takes an input position and orientation, and performs operations on them
>>>>>>> Stashed changes
    /// to stabilize, or smooth deltas, in the data. 
    /// </summary>
    public abstract class BaseRayStabilizer : MonoBehaviour
    {
        /// <summary>
        /// The stabilized position.
        /// </summary>
        public abstract Vector3 StablePosition { get; }

        /// <summary>
        /// The stabilized rotation.
        /// </summary>
        public abstract Quaternion StableRotation { get; }

        /// <summary>
<<<<<<< Updated upstream
        /// A ray representing the stable position and rotation
=======
        /// A ray representing the stable position and orientation
>>>>>>> Stashed changes
        /// </summary>
        public abstract Ray StableRay { get; }

        /// <summary>
<<<<<<< Updated upstream
        /// Call this each frame to smooth out changes to a position and rotation, if supported.
        /// </summary>
        /// <param name="position">Input position to smooth.</param>
        /// <param name="rotation">Input rotation to smooth.</param>
        public virtual void UpdateStability(Vector3 position, Quaternion rotation)
        {
            UpdateStability(position, (rotation * Vector3.forward));
        }

        /// <summary>
        /// Call this each frame to smooth out changes to a position and direction, if supported.
        /// </summary>
        /// <param name="position">Input position to smooth.</param>
        /// <param name="direction">Input direction to smooth.</param>
        public abstract void UpdateStability(Vector3 position, Vector3 direction);
=======
        /// Call this each frame to smooth out changes to a position and orientation.
        /// </summary>
        /// <param name="position">Input position to smooth.</param>
        /// <param name="rotation">Input orientation to smooth.</param>
        public abstract void UpdateStability(Vector3 position, Quaternion rotation);
>>>>>>> Stashed changes
    }
}