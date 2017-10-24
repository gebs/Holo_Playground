// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.Audio;
=======
>>>>>>> Stashed changes

namespace HoloToolkit.Unity
{
    /// <summary>
    /// The different rules for how audio should be played back.
    /// </summary>
    public enum AudioContainerType
    {
        Random,
        Sequence,
        Simultaneous,
        ContinuousSequence,
        ContinuousRandom
    }

    /// <summary>
    /// Defines the behavior for when the instance limit is reached for a particular event.
    /// </summary>
    public enum AudioEventInstanceBehavior
    {
        KillOldest,
        KillNewest
    }

    /// <summary>
    /// The different types of spatial positioning.
    /// </summary>
    public enum SpatialPositioningType
    {
<<<<<<< Updated upstream
        /// <summary>
        /// Stereo
        /// </summary>
        TwoD,
        /// <summary>
        /// 3D Audio
        /// </summary>
        ThreeD,
        /// <summary>
        /// Microsoft Spatial Sound
        /// </summary>
        SpatialSound
=======
        TwoD,           // Stereo
        ThreeD,         // 3D audio
        SpatialSound,   // Microsoft Spatial Sound
>>>>>>> Stashed changes
    }

    /// <summary>
    /// The AudioEvent class is the main component of UAudioManager and contains settings and a container for playing audio clips.
    /// </summary>
<<<<<<< Updated upstream
    [Serializable]
    public class AudioEvent : IComparable, IComparable<AudioEvent>
    {
        [Tooltip("The name of this AudioEvent.")]
        public string Name = "_NewAudioEvent";

        [Tooltip("How this sound is to be positioned.")]
        public SpatialPositioningType Spatialization = SpatialPositioningType.TwoD;

        [Tooltip("The size of the Microsoft Spatial Sound room.  Only used when positioning is set to SpatialSound.")]
        public SpatialSoundRoomSizes RoomSize = SpatialSoundSettings.DefaultSpatialSoundRoom;

        [Tooltip("The minimum gain, in decibels.  Only used when positioning is set to SpatialSound.")]
        [Range(SpatialSoundSettings.MinimumGainDecibels, SpatialSoundSettings.MaximumGainDecibels)]
        public float MinGain = SpatialSoundSettings.DefaultMinGain;

        [Tooltip("The maximum gain, in decibels.  Only used when positioning is set to SpatialSound.")]
        [Range(SpatialSoundSettings.MinimumGainDecibels, SpatialSoundSettings.MaximumGainDecibels)]
        public float MaxGain = SpatialSoundSettings.DefaultMaxGain;

        [Tooltip("The volume attenuation curve for simple 3D sounds. Only used when positioning is set to 3D")]
        public AnimationCurve AttenuationCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f); // By default simple attenuation

        [Tooltip("The spatial attenuation curve for simple 3D sounds. Only used when positioning is set to 3D")]
        public AnimationCurve SpatialCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 1f); // by default Full 3D sound

        [Tooltip("The spread attenuation curve for simple 3D sounds. Only used when positioning is set to 3D")]
        public AnimationCurve SpreadCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 0f); // by default no spread

        [Tooltip("The lowpass attenuation curve for simple 3D sounds. Only used when positioning is set to 3D")]
        public AnimationCurve LowPassCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 0f); // by default no lowpass

        [Tooltip("The reverb attenuation curve for simple 3D sounds. Only used when positioning is set to 3D")]
        public AnimationCurve ReverbCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 0f); // by default no reverb

        [Tooltip("The maximum attenuation distance for simple 3D sounds. Only used when positioning is set to 3D")]
        [Range(1f, 500f)]
        public float MaxDistanceAttenuation3D = 100f;

        [Tooltip("The distance, in meters at which the gain is 0 decibels.  Only used when positioning is set to SpatialSound.")]
        [Range(SpatialSoundSettings.MinimumUnityGainDistanceMeters, SpatialSoundSettings.MaximumUnityGainDistanceMeters)]
        public float UnityGainDistance = SpatialSoundSettings.DefaultUnityGainDistance;

        [Tooltip("The AudioMixerGroup to use when playing.")]
        public AudioMixerGroup AudioBus;

        [Tooltip("The default or center pitch around which randomization can be done.")]
        [Range(-3.0f, 3.0f)]
        public float PitchCenter = 1.0f;
=======
    [System.Serializable]
    public class AudioEvent : IComparable, IComparable<AudioEvent>
    {
        [Tooltip("The name of this AudioEvent.")]
        public string name = "_NewAudioEvent";

        [Tooltip("How this sound is to be positioned.")]
        public SpatialPositioningType spatialization = SpatialPositioningType.TwoD;

        [Tooltip("The size of the Microsoft Spatial Sound room.  Only used when positioning is set to SpatialSound.")]
        public SpatialSoundRoomSizes roomSize = SpatialSoundSettings.DefaultSpatialSoundRoom;

        [Tooltip("The minimum gain, in decibels.  Only used when positioning is set to SpatialSound.")]
        [Range(SpatialSoundSettings.MinimumGainDecibels, SpatialSoundSettings.MaximumGainDecibels)]
        public float minGain = SpatialSoundSettings.DefaultMinGain;

        [Tooltip("The maximum gain, in decibels.  Only used when positioning is set to SpatialSound.")]
        [Range(SpatialSoundSettings.MinimumGainDecibels, SpatialSoundSettings.MaximumGainDecibels)]
        public float maxGain = SpatialSoundSettings.DefaultMaxGain;

        [Tooltip("The volume attenuation curve for simple 3D sounds. Only used when positioning is set to 3D")]
        public AnimationCurve attenuationCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f); // By default simple attenuation

        [Tooltip("The spatial attenuation curve for simple 3D sounds. Only used when positioning is set to 3D")]
        public AnimationCurve spatialCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 1f); // by default Full 3D sound

        [Tooltip("The spread attenuation curve for simple 3D sounds. Only used when positioning is set to 3D")]
        public AnimationCurve spreadCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 0f); // by default no spread

        [Tooltip("The lowpass attenuation curve for simple 3D sounds. Only used when positioning is set to 3D")]
        public AnimationCurve lowPassCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 0f); // by default no lowpass

        [Tooltip("The reverb attenuation curve for simple 3D sounds. Only used when positioning is set to 3D")]
        public AnimationCurve reverbCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 0f); // by default no reverb

        [Tooltip("The maximum attenuation distance for simple 3D sounds. Only used when positioning is set to 3D")]
        [Range(1f, 500f)]
        public float maxDistanceAttenuation3D = 100f;

        [Tooltip("The distance, in meters at which the gain is 0 decibels.  Only used when positioning is set to SpatialSound.")]
        [Range(SpatialSoundSettings.MinimumUnityGainDistanceMeters, SpatialSoundSettings.MaximumUnityGainDistanceMeters)]
        public float unityGainDistance = SpatialSoundSettings.DefaultUnityGainDistance;

        [Tooltip("The AudioMixerGroup to use when playing.")]
        public UnityEngine.Audio.AudioMixerGroup bus = null;

        [Tooltip("The default or center pitch around which randomization can be done.")]
        [Range(-3.0f, 3.0f)]
        public float pitchCenter = 1.0f;
>>>>>>> Stashed changes

        /// <summary>
        /// The amount in either direction from Pitch Center that the pitch can randomly vary upon playing the event.
        /// </summary>
        /// <remarks>The supported range is 0.0f - 2.0f.</remarks>
        [HideInInspector]
<<<<<<< Updated upstream
        public float PitchRandomization;

        [Tooltip("The default or center volume level around which randomization can be done.")]
        [Range(0.0f, 1.0f)]
        public float VolumeCenter = 1.0f;
=======
        public float pitchRandomization = 0.0f;

        [Tooltip("The default or center volume level around which randomization can be done.")]
        [Range(0.0f, 1.0f)]
        public float volumeCenter = 1.0f;
>>>>>>> Stashed changes

        /// <summary>
        /// The amount in either direction from Volume Center that the volume can randomly vary upon playing the event.
        /// </summary>
        /// <remarks>The supported range is 0.0f - 0.5f.</remarks>
        [HideInInspector]
<<<<<<< Updated upstream
        public float VolumeRandomization;

        [Tooltip("The default or center panning. Only used when positioning is set to 2D.")]
        [Range(-1.0f, 1.0f)]
        public float PanCenter;
=======
        public float volumeRandomization = 0.0f;

        [Tooltip("The default or center panning. Only used when positioning is set to 2D.")]
        [Range(-1.0f, 1.0f)]
        public float panCenter = 0;
>>>>>>> Stashed changes

        /// <summary>
        /// The amount in either direction from Pan Center that panning can randomly vary upon playing the event.
        /// </summary>
        /// <remarks>The supported range is 0.0f - 0.5f.</remarks>
        [HideInInspector]
<<<<<<< Updated upstream
        public float PanRandomization;


        [Tooltip("Time, in seconds, for the audio to fade from 0 to the selected volume.  Does not apply to continuous containers in which the Crossfade time property is used.")]
        [Range(0f, 20f)]
        public float FadeInTime;

        [Tooltip("Time, in seconds, for the audio to fade out from the selected volume to 0.  Does not apply to continuous containers in which the Crossfade time property is used.")]
        [Range(0f, 20f)]
        public float FadeOutTime;

        [Tooltip("The maximum number of instances that should be allowed at a time for this event. Any new instances will be suppressed.")]
        public int InstanceLimit;

        [Tooltip("The amount of time in seconds that an event will remain active past when the sound ends. Useful for limiting the instances of an event beyond the clip play time.")]
        public float InstanceTimeBuffer;

        [Tooltip("The behavior when the instance limit is reached.")]
        public AudioEventInstanceBehavior AudioEventInstanceBehavior = AudioEventInstanceBehavior.KillOldest;
=======
        public float panRandomization = 0.0f;


        [Tooltip("Time, in seconds, for the audio to fade from 0 to the selected volume.  Does not apply to continuous containers in which the Crossfade TGime property is used.")]
        [Range(0f, 20f)]
        public float fadeInTime = 0.0f;

        [Tooltip("Time, in seconds, for the audio to fade out from the selected volume to 0.  Does not apply to continuous containers in which the Crossfade TGime property is used.")]
        [Range(0f, 20f)]
        public float fadeOutTime = 0.0f;

        [Tooltip("The maximum number of instances that should be allowed at a time for this event. Any new instances will be suppressed.")]
        public int instanceLimit = 0;

        [Tooltip("The amount of time in seconds that an event will remain active past when the sound ends. Useful for limiting the instances of an event beyond the clip play time.")]
        public float instanceTimeBuffer = 0.0f;

        [Tooltip("The behavior when the instance limit is reached.")]
        public AudioEventInstanceBehavior instanceBehavior = AudioEventInstanceBehavior.KillOldest;
>>>>>>> Stashed changes

        /// <summary>
        /// Contains the sounds associated with this AudioEvent.
        /// </summary>
<<<<<<< Updated upstream
        public AudioContainer Container = new AudioContainer();
=======
        public AudioContainer container = new AudioContainer();
>>>>>>> Stashed changes

        /// <summary>
        /// Is this AudioEvent's container a continuous container?
        /// </summary>
        /// <returns>True if this AudioEvent's container is one of the continuous types (random or sequential), otherwise false.</returns>
        public bool IsContinuous()
        {
<<<<<<< Updated upstream
            return Container.ContainerType == AudioContainerType.ContinuousRandom ||
                   Container.ContainerType == AudioContainerType.ContinuousSequence;
=======
            return container.containerType == AudioContainerType.ContinuousRandom ||
                   container.containerType == AudioContainerType.ContinuousSequence;
>>>>>>> Stashed changes
        }

        /// <summary>
        /// Compares this AudioEvent with another object.
        /// </summary>
        /// <param name="obj">The object to compare against.</param>
        /// <returns>An integer that indicates whether this AudioEvent precedes (-1), follows (1),
        /// or appears in the same position (0) in the sort order as the AudioEvent being compared.</returns>
        /// <remarks>If the specified object is not an AudioEvent, the return value is 1.</remarks>
        public int CompareTo(object obj)
        {
<<<<<<< Updated upstream
            if (obj == null) { return 1; }

            var tempEvent = obj as AudioEvent;

=======
            if (obj == null) return 1;

            AudioEvent tempEvent = obj as AudioEvent;
>>>>>>> Stashed changes
            if (tempEvent != null)
            {
                return CompareTo(tempEvent);
            }
<<<<<<< Updated upstream

            throw new ArgumentException("Object is not an AudioEvent");
=======
            else
            {
                throw new ArgumentException("Object is not an AudioEvent");
            }
>>>>>>> Stashed changes
        }

        /// <summary>
        /// Compares this AudioEvent with another AudioEvent.
        /// </summary>
        /// <param name="other">The AudioEvent to compare against.</param>
        /// <returns>An integer that indicates whether this AudioEvent precedes (-1), follows (1),
        /// or appears in the same position (0) in the sort order as the AudioEvent being compared.</returns>
        public int CompareTo(AudioEvent other)
        {
<<<<<<< Updated upstream
            return other == null ? 1 : string.CompareOrdinal(Name, other.Name);
=======
            if (other == null) return 1;
            return string.Compare(name, other.name);
>>>>>>> Stashed changes
        }
    }
}