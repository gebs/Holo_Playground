// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace HoloToolkit.Unity
{
    /// <summary>
    /// Currently active AudioEvents along with their AudioSource components for instance limiting events
    /// </summary>
    public class ActiveEvent : IDisposable
    {
        private AudioSource primarySource = null;
        public AudioSource PrimarySource
        {
            get
            {
                return primarySource;
            }
            private set
            {
                primarySource = value;
                if (primarySource != null)
                {
                    primarySource.enabled = true;
                }
            }
        }

        private AudioSource secondarySource = null;
        public AudioSource SecondarySource
        {
            get
            {
                return secondarySource;
            }
            private set
            {
                secondarySource = value;
                if (secondarySource != null)
                {
                    secondarySource.enabled = true;
                }
            }
        }

        public bool IsPlaying
        {
            get
            {
                return
                    (primarySource != null && primarySource.isPlaying) ||
                    (secondarySource != null && secondarySource.isPlaying);
            }
        }

        public GameObject AudioEmitter
        {
            get;
            private set;
        }

        public string MessageOnAudioEnd
        {
            get;
            private set;
        }

<<<<<<< Updated upstream
        public AudioEvent AudioEvent = null;
        public bool IsStoppable = true;
        public float VolDest = 1;
        public float AltVolDest = 1;
        public float CurrentFade = 0;
        public bool PlayingAlt = false;
        public bool IsActiveTimeComplete = false;
        public float ActiveTime = 0;
        public bool CancelEvent = false;

        public ActiveEvent(AudioEvent audioEvent, GameObject emitter, AudioSource primarySource, AudioSource secondarySource, string messageOnAudioEnd = null)
        {
            this.AudioEvent = audioEvent;
=======
        public AudioEvent audioEvent = null;
        public bool isStoppable = true;
        public float volDest = 1;
        public float altVolDest = 1;
        public float currentFade = 0;
        public bool playingAlt = false;
        public bool isActiveTimeComplete = false;
        public float activeTime = 0;
        public bool cancelEvent = false;

        public ActiveEvent(AudioEvent audioEvent, GameObject emitter, AudioSource primarySource, AudioSource secondarySource, string messageOnAudioEnd = null)
        {
            this.audioEvent = audioEvent;
>>>>>>> Stashed changes
            AudioEmitter = emitter;
            PrimarySource = primarySource;
            SecondarySource = secondarySource;
            MessageOnAudioEnd = messageOnAudioEnd;
            SetSourceProperties();
        }

        public static AnimationCurve SpatialRolloff;

        /// <summary>
        /// Set the volume, spatialization, etc., on our AudioSources to match the settings on the event to play.
        /// </summary>
        private void SetSourceProperties()
        {
            Action<Action<AudioSource>> forEachSource = (action) =>
            {
                action(PrimarySource);
                if (SecondarySource != null)
                {
                    action(SecondarySource);
                }
            };

<<<<<<< Updated upstream
            AudioEvent audioEvent = this.AudioEvent;
            switch (audioEvent.Spatialization)
=======
            AudioEvent audioEvent = this.audioEvent;
            switch (audioEvent.spatialization)
>>>>>>> Stashed changes
            {
                case SpatialPositioningType.TwoD:
                    forEachSource((source) =>
                    {
                        source.spatialBlend = 0f;
                        source.spatialize = false;
                    });
                    break;
                case SpatialPositioningType.ThreeD:
                    forEachSource((source) =>
                    {
                        source.spatialBlend = 1f;
                        source.spatialize = false;
                    });
                    break;
                case SpatialPositioningType.SpatialSound:
                    forEachSource((source) =>
                    {
                        source.spatialBlend = 1f;
                        source.spatialize = true;
                    });
                    break;
                default:
<<<<<<< Updated upstream
                    Debug.LogErrorFormat("Unexpected spatialization type: {0}", audioEvent.Spatialization.ToString());
                    break;
            }

            if (audioEvent.Spatialization == SpatialPositioningType.SpatialSound)
=======
                    Debug.LogErrorFormat("Unexpected spatialization type: {0}", audioEvent.spatialization.ToString());
                    break;
            }

            if (audioEvent.spatialization == SpatialPositioningType.SpatialSound)
>>>>>>> Stashed changes
            {
                CreateFlatSpatialRolloffCurve();
                forEachSource((source) =>
                {
                    source.rolloffMode = AudioRolloffMode.Custom;
                    source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, SpatialRolloff);
<<<<<<< Updated upstream
                    SpatialSoundSettings.SetRoomSize(source, audioEvent.RoomSize);
                    SpatialSoundSettings.SetMinGain(source, audioEvent.MinGain);
                    SpatialSoundSettings.SetMaxGain(source, audioEvent.MaxGain);
                    SpatialSoundSettings.SetUnityGainDistance(source, audioEvent.UnityGainDistance);
=======
                    SpatialSoundSettings.SetRoomSize(source, audioEvent.roomSize);
                    SpatialSoundSettings.SetMinGain(source, audioEvent.minGain);
                    SpatialSoundSettings.SetMaxGain(source, audioEvent.maxGain);
                    SpatialSoundSettings.SetUnityGainDistance(source, audioEvent.unityGainDistance);
>>>>>>> Stashed changes
                });
            }
            else
            {
                forEachSource((source) =>
                {
<<<<<<< Updated upstream
                    if (audioEvent.Spatialization == SpatialPositioningType.ThreeD)
                    {
                        source.rolloffMode = AudioRolloffMode.Custom;
                        source.maxDistance = audioEvent.MaxDistanceAttenuation3D;
                        source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, audioEvent.AttenuationCurve);
                        source.SetCustomCurve(AudioSourceCurveType.SpatialBlend, audioEvent.SpatialCurve);
                        source.SetCustomCurve(AudioSourceCurveType.Spread, audioEvent.SpreadCurve);
                        source.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, audioEvent.ReverbCurve);
=======
                    if (audioEvent.spatialization == SpatialPositioningType.ThreeD)
                    {
                        source.rolloffMode = AudioRolloffMode.Custom;
                        source.maxDistance = audioEvent.maxDistanceAttenuation3D;
                        source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, audioEvent.attenuationCurve);
                        source.SetCustomCurve(AudioSourceCurveType.SpatialBlend, audioEvent.spatialCurve);
                        source.SetCustomCurve(AudioSourceCurveType.Spread, audioEvent.spreadCurve);
                        source.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, audioEvent.reverbCurve);
>>>>>>> Stashed changes
                    }
                    else
                    {
                        source.rolloffMode = AudioRolloffMode.Logarithmic;
                    }
                });
            }

<<<<<<< Updated upstream
            if (audioEvent.AudioBus != null)
            {
                forEachSource((source) => source.outputAudioMixerGroup = audioEvent.AudioBus);
=======
            if (audioEvent.bus != null)
            {
                forEachSource((source) => source.outputAudioMixerGroup = audioEvent.bus);
>>>>>>> Stashed changes
            }

            float pitch = 1f;

<<<<<<< Updated upstream
            if (audioEvent.PitchRandomization != 0)
            {
                pitch = UnityEngine.Random.Range(audioEvent.PitchCenter - audioEvent.PitchRandomization, audioEvent.PitchCenter + audioEvent.PitchRandomization);
            }
            else
            {
                pitch = audioEvent.PitchCenter;
=======
            if (audioEvent.pitchRandomization != 0)
            {
                pitch = UnityEngine.Random.Range(audioEvent.pitchCenter - audioEvent.pitchRandomization, audioEvent.pitchCenter + audioEvent.pitchRandomization);
            }
            else
            {
                pitch = audioEvent.pitchCenter;
>>>>>>> Stashed changes
            }
            forEachSource((source) => source.pitch = pitch);

            float vol = 1f;
<<<<<<< Updated upstream
            if (audioEvent.FadeInTime > 0)
            {
                forEachSource((source) => source.volume = 0f);
                this.CurrentFade = audioEvent.FadeInTime;
                if (audioEvent.VolumeRandomization != 0)
                {
                    vol = UnityEngine.Random.Range(audioEvent.VolumeCenter - audioEvent.VolumeRandomization, audioEvent.VolumeCenter + audioEvent.VolumeRandomization);
                }
                else
                {
                    vol = audioEvent.VolumeCenter;
                }
                this.VolDest = vol;
            }
            else
            {
                if (audioEvent.VolumeRandomization != 0)
                {
                    vol = UnityEngine.Random.Range(audioEvent.VolumeCenter - audioEvent.VolumeRandomization, audioEvent.VolumeCenter + audioEvent.VolumeRandomization);
                }
                else
                {
                    vol = audioEvent.VolumeCenter;
=======
            if (audioEvent.fadeInTime > 0)
            {
                forEachSource((source) => source.volume = 0f);
                this.currentFade = audioEvent.fadeInTime;
                if (audioEvent.volumeRandomization != 0)
                {
                    vol = UnityEngine.Random.Range(audioEvent.volumeCenter - audioEvent.volumeRandomization, audioEvent.volumeCenter + audioEvent.volumeRandomization);
                }
                else
                {
                    vol = audioEvent.volumeCenter;
                }
                this.volDest = vol;
            }
            else
            {
                if (audioEvent.volumeRandomization != 0)
                {
                    vol = UnityEngine.Random.Range(audioEvent.volumeCenter - audioEvent.volumeRandomization, audioEvent.volumeCenter + audioEvent.volumeRandomization);
                }
                else
                {
                    vol = audioEvent.volumeCenter;
>>>>>>> Stashed changes
                }
                forEachSource((source) => source.volume = vol);
            }

<<<<<<< Updated upstream
            float pan = audioEvent.PanCenter;
            if (audioEvent.PanRandomization != 0)
            {
                pan = UnityEngine.Random.Range(audioEvent.PanCenter - audioEvent.PanRandomization, audioEvent.PanCenter + audioEvent.PanRandomization);
=======
            float pan = audioEvent.panCenter;
            if (audioEvent.panRandomization != 0)
            {
                pan = UnityEngine.Random.Range(audioEvent.panCenter - audioEvent.panRandomization, audioEvent.panCenter + audioEvent.panRandomization);
>>>>>>> Stashed changes
            }
            forEachSource((source) => source.panStereo = pan);
        }

        /// <summary>
        /// Sets the pitch value for the primary source.
        /// </summary>
        /// <param name="newPitch">The value to set the pitch, between 0 (exclusive) and 3 (inclusive).</param>
        public void SetPitch(float newPitch)
        {
            if (newPitch <= 0 || newPitch > 3)
            {
                Debug.LogErrorFormat("Invalid pitch {0} set for event", newPitch);
                return;
            }

            this.PrimarySource.pitch = newPitch;
        }

        public void Dispose()
        {
            if (this.primarySource != null)
            {
                this.primarySource.enabled = false;
                this.primarySource = null;
            }

            if (this.secondarySource != null)
            {
                this.secondarySource.enabled = false;
                this.secondarySource = null;
            }
        }

        /// <summary>
        /// Creates a flat animation curve to negate Unity's distance attenuation when using Spatial Sound
        /// </summary>
        public static void CreateFlatSpatialRolloffCurve()
        {
            if (SpatialRolloff != null)
            {
                return;
            }
            SpatialRolloff = new AnimationCurve();
            SpatialRolloff.AddKey(0, 1);
            SpatialRolloff.AddKey(1, 1);
        }
    }
}