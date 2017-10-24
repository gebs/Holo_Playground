// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

<<<<<<< Updated upstream
using UnityEngine;

#if UNITY_WSA
using System;
using System.Collections.Generic;
#if UNITY_2017_2_OR_NEWER
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;
#else
using UnityEngine.VR.WSA;
using UnityEngine.VR.WSA.Persistence;
#endif
#if !UNITY_EDITOR
using HoloToolkit.Unity.SpatialMapping;
#endif
#endif

=======
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Persistence;
using UnityEngine.VR.WSA;
using HoloToolkit.Unity.SpatialMapping;
>>>>>>> Stashed changes

namespace HoloToolkit.Unity
{
    /// <summary>
<<<<<<< Updated upstream
    /// Wrapper around world anchor store to streamline some of the persistence API busy work.
=======
    /// Wrapper around world anchor store to streamline some of the persistence api busy work.
>>>>>>> Stashed changes
    /// </summary>
    public class WorldAnchorManager : Singleton<WorldAnchorManager>
    {
        /// <summary>
<<<<<<< Updated upstream
        /// Debug text for displaying information.
        /// </summary>
        public TextMesh AnchorDebugText;

        /// <summary>
        /// Enables detailed logs in console window.
        /// </summary>
        /// <remarks>If the Sharing Service is used, it will inherit the log settings.</remarks>
        [Tooltip("Enables detailed logs in console window.  If the Sharing Service is used, it will inherit the log settings.")]
        public bool ShowDetailedLogs;

        /// <summary>
        /// Enables anchors to be stored from subsequent game sessions.
        /// </summary>
        [Tooltip("Enables anchors to be stored from subsequent game sessions.")]
        public bool PersistentAnchors;

#if UNITY_WSA
        /// <summary>
        /// To prevent initializing too many anchors at once
        /// and to allow for the WorldAnchorStore to load asynchronously
=======
        /// To prevent initializing too many anchors at once
        /// and to allow for the WorldAnchorStore to load asyncronously
>>>>>>> Stashed changes
        /// without callers handling the case where the store isn't loaded yet
        /// we'll setup a queue of anchor attachment operations.  
        /// The AnchorAttachmentInfo struct has the data needed to do this.
        /// </summary>
<<<<<<< Updated upstream
        protected struct AnchorAttachmentInfo
        {
            public GameObject AnchoredGameObject { get; set; }
=======
        private struct AnchorAttachmentInfo
        {
            public GameObject GameObjectToAnchor { get; set; }
>>>>>>> Stashed changes
            public string AnchorName { get; set; }
            public AnchorOperation Operation { get; set; }
        }

<<<<<<< Updated upstream
        /// <summary>
        /// The data structure for anchor operations.
        /// </summary>
        protected enum AnchorOperation
        {
            /// <summary>
            /// Save anchor to anchor store.  Creates anchor if none exists.
            /// </summary>
            Save,
            /// <summary>
            /// Deletes anchor from anchor store.
            /// </summary>
=======
        private enum AnchorOperation
        {
            Create,
>>>>>>> Stashed changes
            Delete
        }

        /// <summary>
<<<<<<< Updated upstream
        /// The queue for local device anchor operations.
        /// </summary>
        protected Queue<AnchorAttachmentInfo> LocalAnchorOperations = new Queue<AnchorAttachmentInfo>();
=======
        /// The queue mentioned above.
        /// </summary>
        private Queue<AnchorAttachmentInfo> anchorOperations = new Queue<AnchorAttachmentInfo>();
>>>>>>> Stashed changes

        /// <summary>
        /// The WorldAnchorStore for the current application.
        /// Can be null when the application starts.
        /// </summary>
<<<<<<< Updated upstream
        public WorldAnchorStore AnchorStore { get; protected set; }

        /// <summary>
        /// Internal list of anchors and their GameObject references.
        /// </summary>
        protected Dictionary<string, GameObject> AnchorGameObjectReferenceList = new Dictionary<string, GameObject>(0);

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            AnchorStore = null;
        }

        protected virtual void Start()
        {
            WorldAnchorStore.GetAsync(AnchorStoreReady);
        }

        protected virtual void Update()
        {
            if (AnchorStore == null) { return; }

            if (LocalAnchorOperations.Count > 0)
            {
                DoAnchorOperation(LocalAnchorOperations.Dequeue());
            }
        }

        #endregion // Unity Methods

        #region Event Callbacks
=======
        public WorldAnchorStore AnchorStore { get; private set; }
>>>>>>> Stashed changes

        /// <summary>
        /// Callback function that contains the WorldAnchorStore object.
        /// </summary>
        /// <param name="anchorStore">The WorldAnchorStore to cache.</param>
<<<<<<< Updated upstream
        protected virtual void AnchorStoreReady(WorldAnchorStore anchorStore)
        {
            AnchorStore = anchorStore;

            if (!PersistentAnchors)
            {
                AnchorStore.Clear();
            }
        }

        /// <summary>
        /// Called when tracking changes for a 'cached' anchor.  
        /// When an anchor isn't located immediately we subscribe to this event so
        /// we can save the anchor when it is finally located or downloaded.
        /// </summary>
        /// <param name="anchor">The anchor that is reporting a tracking changed event.</param>
        /// <param name="located">Indicates if the anchor is located or not located.</param>
        private void Anchor_OnTrackingChanged(WorldAnchor anchor, bool located)
        {
            if (located && SaveAnchor(anchor))
            {
                if (ShowDetailedLogs)
                {
                    Debug.LogFormat("[WorldAnchorManager] Successfully updated cached anchor \"{0}\".", anchor.name);
                }

                if (AnchorDebugText != null)
                {
                    AnchorDebugText.text += string.Format("\nSuccessfully updated cached anchor \"{0}\".", anchor.name);
                }
            }
            else
            {
                if (ShowDetailedLogs)
                {
                    Debug.LogFormat("[WorldAnchorManager] Failed to locate cached anchor \"{0}\", attempting to acquire anchor again.", anchor.name);
                }

                if (AnchorDebugText != null)
                {
                    AnchorDebugText.text += string.Format("\nFailed to locate cached anchor \"{0}\", attempting to acquire anchor again.", anchor.name);
                }

                GameObject anchoredObject;
                AnchorGameObjectReferenceList.TryGetValue(anchor.name, out anchoredObject);
                AnchorGameObjectReferenceList.Remove(anchor.name);
                AttachAnchor(anchoredObject, anchor.name);
            }

            anchor.OnTrackingChanged -= Anchor_OnTrackingChanged;
        }

        #endregion // Event Callbacks
#endif
        /// <summary>
        /// Generates the name for the anchor.
        /// If no anchor name was specified, the name of the anchor will be the same as the GameObject's name.
        /// </summary>
        /// <param name="gameObjectToAnchor">The GameObject to attach the anchor to.</param>
        /// <param name="proposedAnchorname">Name of the anchor. If none provided, the name of the GameObject will be used.</param>
        /// <returns>The name of the newly attached anchor.</returns>
        public static string GenerateAnchorName(GameObject gameObjectToAnchor, string proposedAnchorname = null)
        {
            return string.IsNullOrEmpty(proposedAnchorname) ? gameObjectToAnchor.name : proposedAnchorname;
        }

        /// <summary>
        /// Attaches an anchor to the GameObject.  
        /// If the anchor store has an anchor with the specified name it will load the anchor, 
        /// otherwise a new anchor will be saved under the specified name.  
        /// If no anchor name is provided, the name of the anchor will be the same as the GameObject.
        /// </summary>
        /// <param name="gameObjectToAnchor">The GameObject to attach the anchor to.</param>
        /// <param name="anchorName">Name of the anchor.  If none provided, the name of the GameObject will be used.</param>
        /// <returns>The name of the newly attached anchor.</returns>
        public string AttachAnchor(GameObject gameObjectToAnchor, string anchorName = null)
        {
#if !UNITY_WSA || UNITY_EDITOR
            Debug.LogWarning("World Anchor Manager does not work for this build. AttachAnchor will not be called.");
            return null;
#else
            if (gameObjectToAnchor == null)
            {
                Debug.LogError("[WorldAnchorManager] Must pass in a valid gameObject");
                return null;
            }

            // This case is unexpected, but just in case.
            if (AnchorStore == null)
            {
                Debug.LogWarning("[WorldAnchorManager] AttachAnchor called before anchor store is ready.");
            }

            anchorName = GenerateAnchorName(gameObjectToAnchor, anchorName);

            LocalAnchorOperations.Enqueue(
                new AnchorAttachmentInfo
                {
                    AnchoredGameObject = gameObjectToAnchor,
                    AnchorName = anchorName,
                    Operation = AnchorOperation.Save
                }
            );

            return anchorName;
#endif
        }

        /// <summary>
        /// Removes the anchor component from the GameObject and deletes the anchor from the anchor store.
        /// </summary>
        /// <param name="gameObjectToUnanchor">The GameObject reference with valid anchor to remove from the anchor store.</param>
        public void RemoveAnchor(GameObject gameObjectToUnanchor)
        {
            if (gameObjectToUnanchor == null)
            {
                Debug.LogError("[WorldAnchorManager] Invalid GameObject! Try removing anchor by name.");
                if (AnchorDebugText != null)
                {
                    AnchorDebugText.text += "\nInvalid GameObject! Try removing anchor by name.";
                }
                return;
            }

            RemoveAnchor(string.Empty, gameObjectToUnanchor);
        }

        /// <summary>
        /// Removes the anchor from the anchor store, without a GameObject reference.  
        /// If a GameObject reference can be found, the anchor component will be removed.
        /// </summary>
        /// <param name="anchorName">The name of the anchor to remove from the anchor store.</param>
        public void RemoveAnchor(string anchorName)
        {
            if (string.IsNullOrEmpty(anchorName))
            {
                Debug.LogErrorFormat("[WorldAnchorManager] Invalid anchor \"{0}\"! Try removing anchor by GameObject.", anchorName);
                if (AnchorDebugText != null)
                {
                    AnchorDebugText.text += string.Format("\nInvalid anchor \"{0}\"! Try removing anchor by GameObject.", anchorName);
                }
                return;
            }

            RemoveAnchor(anchorName, null);
=======
        private void AnchorStoreReady(WorldAnchorStore anchorStore)
        {
            AnchorStore = anchorStore;
        }

        /// <summary>
        /// When the app starts grab the anchor store immediately.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            AnchorStore = null;
            WorldAnchorStore.GetAsync(AnchorStoreReady);
        }

        /// <summary>
        /// Each frame see if there is work to do and if we can do a unit, do it.
        /// </summary>
        private void Update()
        {
            if (AnchorStore != null && anchorOperations.Count > 0)
            {
                DoAnchorOperation(anchorOperations.Dequeue());
            }
        }

        /// <summary>
        /// Attaches an anchor to the game object.  If the anchor store has
        /// an anchor with the specified name it will load the acnhor, otherwise
        /// a new anchor will be saved under the specified name.
        /// </summary>
        /// <param name="gameObjectToAnchor">The Gameobject to attach the anchor to.</param>
        /// <param name="anchorName">Name of the anchor.</param>
        public void AttachAnchor(GameObject gameObjectToAnchor, string anchorName)
        {
            if (gameObjectToAnchor == null)
            {
                Debug.LogError("Must pass in a valid gameObject");
                return;
            }

            if (string.IsNullOrEmpty(anchorName))
            {
                Debug.LogError("Must supply an AnchorName.");
                return;
            }

            anchorOperations.Enqueue(
                new AnchorAttachmentInfo
                {
                    GameObjectToAnchor = gameObjectToAnchor,
                    AnchorName = anchorName,
                    Operation = AnchorOperation.Create
                }
            );
>>>>>>> Stashed changes
        }

        /// <summary>
        /// Removes the anchor from the game object and deletes the anchor
        /// from the anchor store.
        /// </summary>
<<<<<<< Updated upstream
        /// <param name="anchorName">Name of the anchor to remove from the anchor store.</param>
        /// <param name="gameObjectToUnanchor">GameObject to remove the anchor from.</param>
        private void RemoveAnchor(string anchorName, GameObject gameObjectToUnanchor)
        {
            if (string.IsNullOrEmpty(anchorName) && gameObjectToUnanchor == null)
            {
                Debug.LogWarning("Invalid Remove Anchor Request!");
                return;
            }

#if !UNITY_WSA || UNITY_EDITOR
            Debug.LogWarning("World Anchor Manager does not work for this build. RemoveAnchor will not be called.");
#else
            // This case is unexpected, but just in case.
            if (AnchorStore == null)
            {
                Debug.LogWarning("[WorldAnchorManager] RemoveAnchor called before anchor store is ready.");
            }

            LocalAnchorOperations.Enqueue(
                new AnchorAttachmentInfo
                {
                    AnchoredGameObject = gameObjectToUnanchor,
                    AnchorName = anchorName,
                    Operation = AnchorOperation.Delete
                });
#endif
=======
        /// <param name="gameObjectToUnanchor">gameObject to remove the anchor from.</param>
        public void RemoveAnchor(GameObject gameObjectToUnanchor)
        {
            if (gameObjectToUnanchor == null)
            {
                Debug.LogError("Invalid GameObject");
                return;
            }

            // This case is unexpected, but just in case.
            if (AnchorStore == null)
            {
                Debug.LogError("remove anchor called before anchor store is ready.");
                return;
            }

            anchorOperations.Enqueue(
                new AnchorAttachmentInfo
                {
                    GameObjectToAnchor = gameObjectToUnanchor,
                    AnchorName = string.Empty,
                    Operation = AnchorOperation.Delete
                });
>>>>>>> Stashed changes
        }

        /// <summary>
        /// Removes all anchors from the scene and deletes them from the anchor store.
        /// </summary>
        public void RemoveAllAnchors()
        {
<<<<<<< Updated upstream
#if !UNITY_WSA || UNITY_EDITOR
            Debug.LogWarning("World Anchor Manager does not work for this build. RemoveAnchor will not be called.");
#else
=======
>>>>>>> Stashed changes
            SpatialMappingManager spatialMappingManager = SpatialMappingManager.Instance;

            // This case is unexpected, but just in case.
            if (AnchorStore == null)
            {
<<<<<<< Updated upstream
                Debug.LogWarning("[WorldAnchorManager] RemoveAllAnchors called before anchor store is ready.");
            }

            var anchors = FindObjectsOfType<WorldAnchor>();

            if (anchors == null) { return; }

            for (var i = 0; i < anchors.Length; i++)
            {
                // Don't remove SpatialMapping anchors if exists
                if (spatialMappingManager != null && anchors[i].gameObject.transform.parent.gameObject == spatialMappingManager.gameObject)
                { continue; }

                // Let's check to see if there are anchors we weren't accounting for.
                // Maybe they were created without using the WorldAnchorManager.
                if (!AnchorGameObjectReferenceList.ContainsKey(anchors[i].name))
                {
                    Debug.LogWarning("[WorldAnchorManager] Removing an anchor that was created outside of the WorldAnchorManager.  Please use the WorldAnchorManager to create or delete anchors.");
                    if (AnchorDebugText != null)
                    {
                        AnchorDebugText.text += string.Format("\nRemoving an anchor that was created outside of the WorldAnchorManager.  Please use the WorldAnchorManager to create or delete anchors.");
                    }
                }

                LocalAnchorOperations.Enqueue(new AnchorAttachmentInfo
                {
                    AnchorName = anchors[i].name,
                    AnchoredGameObject = anchors[i].gameObject,
                    Operation = AnchorOperation.Delete
                });
            }
#endif
        }

#if UNITY_WSA
        /// <summary>
        /// Executes the anchor operations from the localAnchorOperations queue.
        /// </summary>
        /// <param name="anchorAttachmentInfo">Parameters for attaching the anchor.</param>
        protected void DoAnchorOperation(AnchorAttachmentInfo anchorAttachmentInfo)
        {
            string anchorId = anchorAttachmentInfo.AnchorName;
            GameObject anchoredGameObject = anchorAttachmentInfo.AnchoredGameObject;

            switch (anchorAttachmentInfo.Operation)
            {
                case AnchorOperation.Save:
                    if (anchoredGameObject == null)
                    {
                        Debug.LogError("[WorldAnchorManager] The GameObject referenced must have been destroyed before we got a chance to anchor it.");
                        if (AnchorDebugText != null)
                        {
                            AnchorDebugText.text += "\nThe GameObject referenced must have been destroyed before we got a chance to anchor it.";
                        }
                        break;
                    }

                    if (string.IsNullOrEmpty(anchorId))
                    {
                        anchorId = anchoredGameObject.name;
                    }

                    // Try to load a previously saved world anchor.
                    WorldAnchor savedAnchor = AnchorStore.Load(anchorId, anchoredGameObject);

                    if (savedAnchor == null)
                    {
                        // Check if we need to import the anchor.
                        if (ImportAnchor(anchorId, anchoredGameObject) == false)
                        {
                            if (ShowDetailedLogs)
                            {
                                Debug.LogFormat("[WorldAnchorManager] Anchor could not be loaded for \"{0}\". Creating a new anchor.", anchoredGameObject.name);
                            }

                            if (AnchorDebugText != null)
                            {
                                AnchorDebugText.text += string.Format("\nAnchor could not be loaded for \"{0}\". Creating a new anchor.", anchoredGameObject.name);
                            }

                            // Create anchor since one does not exist.
                            CreateAnchor(anchoredGameObject, anchorId);
                        }
                    }
                    else
                    {
                        savedAnchor.name = anchorId;
                        if (ShowDetailedLogs)
                        {
                            Debug.LogFormat("[WorldAnchorManager] Anchor loaded from anchor store and updated for \"{0}\".", anchoredGameObject.name);
                        }

                        if (AnchorDebugText != null)
                        {
                            AnchorDebugText.text += string.Format("\nAnchor loaded from anchor store and updated for \"{0}\".", anchoredGameObject.name);
                        }
                    }

                    AnchorGameObjectReferenceList.Add(anchorId, anchoredGameObject);
=======
                Debug.LogError("remove all anchors called before anchor store is ready.");
            }

            WorldAnchor[] anchors = FindObjectsOfType<WorldAnchor>();

            if (anchors != null)
            {
                foreach (WorldAnchor anchor in anchors)
                {
                    // Don't remove SpatialMapping anchors if exists
                    if (spatialMappingManager == null ||
                        anchor.gameObject.transform.parent.gameObject != spatialMappingManager.gameObject)
                    {
                        anchorOperations.Enqueue(new AnchorAttachmentInfo()
                        {
                            AnchorName = anchor.name,
                            GameObjectToAnchor = anchor.gameObject,
                            Operation = AnchorOperation.Delete
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Function that actually adds the anchor to the game object.
        /// </summary>
        /// <param name="anchorAttachmentInfo">Parameters for attaching the anchor.</param>
        private void DoAnchorOperation(AnchorAttachmentInfo anchorAttachmentInfo)
        {
            switch (anchorAttachmentInfo.Operation)
            {
                case AnchorOperation.Create:
                    string anchorName = anchorAttachmentInfo.AnchorName;
                    GameObject gameObjectToAnchor = anchorAttachmentInfo.GameObjectToAnchor;

                    if (gameObjectToAnchor == null)
                    {
                        Debug.LogError("GameObject must have been destroyed before we got a chance to anchor it.");
                        break;
                    }

                    // Try to load a previously saved world anchor.
                    WorldAnchor savedAnchor = AnchorStore.Load(anchorName, gameObjectToAnchor);
                    if (savedAnchor == null)
                    {
                        // Either world anchor was not saved / does not exist or has a different name.
                        Debug.LogWarning(gameObjectToAnchor.name + " : World anchor could not be loaded for this game object. Creating a new anchor.");

                        // Create anchor since one does not exist.
                        CreateAnchor(gameObjectToAnchor, anchorName);
                    }
                    else
                    {
                        savedAnchor.name = anchorName;
                        Debug.Log(gameObjectToAnchor.name + " : World anchor loaded from anchor store and updated for this game object.");
                    }

>>>>>>> Stashed changes
                    break;
                case AnchorOperation.Delete:
                    if (AnchorStore == null)
                    {
<<<<<<< Updated upstream
                        Debug.LogError("[WorldAnchorManager] Remove anchor called before anchor store is ready.");
                        break;
                    }

                    // If we don't have a GameObject reference, let's try to get the GameObject reference from our dictionary.
                    if (!string.IsNullOrEmpty(anchorId) && anchoredGameObject == null)
                    {
                        AnchorGameObjectReferenceList.TryGetValue(anchorId, out anchoredGameObject);
                    }

                    if (anchoredGameObject != null)
                    {
                        var anchor = anchoredGameObject.GetComponent<WorldAnchor>();

                        if (anchor != null)
                        {
                            anchorId = anchor.name;
                            DestroyImmediate(anchor);
                        }
                        else
                        {
                            Debug.LogErrorFormat("[WorldAnchorManager] Unable remove WorldAnchor from {0}!", anchoredGameObject.name);
                            if (AnchorDebugText != null)
                            {
                                AnchorDebugText.text += string.Format("\nUnable remove WorldAnchor from {0}!", anchoredGameObject.name);
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("[WorldAnchorManager] Unable find a GameObject to remove an anchor from!");
                        if (AnchorDebugText != null)
                        {
                            AnchorDebugText.text += "\nUnable find a GameObject to remove an anchor from!";
                        }
                    }

                    if (!string.IsNullOrEmpty(anchorId))
                    {
                        AnchorGameObjectReferenceList.Remove(anchorId);
                        DeleteAnchor(anchorId);
                    }
                    else
                    {
                        Debug.LogError("[WorldAnchorManager] Unable find an anchor to delete!");
                        if (AnchorDebugText != null)
                        {
                            AnchorDebugText.text += "\nUnable find an anchor to delete!";
                        }
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
=======
                        Debug.LogError("Remove anchor called before anchor store is ready.");
                        break;
                    }

                    GameObject gameObjectToUnanchor = anchorAttachmentInfo.GameObjectToAnchor;
                    var anchor = gameObjectToUnanchor.GetComponent<WorldAnchor>();

                    if (anchor != null)
                    {
                        AnchorStore.Delete(anchor.name);
                        DestroyImmediate(anchor);
                    }
                    else
                    {
                        Debug.LogError("Cannot get anchor while deleting");
                    }

                    break;
>>>>>>> Stashed changes
            }
        }

        /// <summary>
        /// Creates an anchor, attaches it to the gameObjectToAnchor, and saves the anchor to the anchor store.
        /// </summary>
        /// <param name="gameObjectToAnchor">The GameObject to attach the anchor to.</param>
        /// <param name="anchorName">The name to give to the anchor.</param>
        private void CreateAnchor(GameObject gameObjectToAnchor, string anchorName)
        {
<<<<<<< Updated upstream
            var anchor = gameObjectToAnchor.EnsureComponent<WorldAnchor>();
=======
            var anchor = gameObjectToAnchor.AddComponent<WorldAnchor>();
>>>>>>> Stashed changes
            anchor.name = anchorName;

            // Sometimes the anchor is located immediately. In that case it can be saved immediately.
            if (anchor.isLocated)
            {
                SaveAnchor(anchor);
            }
            else
            {
                // Other times we must wait for the tracking system to locate the world.
                anchor.OnTrackingChanged += Anchor_OnTrackingChanged;
            }
        }

        /// <summary>
<<<<<<< Updated upstream
        /// Saves the anchor to the anchor store.
        /// </summary>
        /// <param name="anchor">Anchor.</param>
        private bool SaveAnchor(WorldAnchor anchor)
        {
            // Save the anchor to persist holograms across sessions.
            if (AnchorStore.Save(anchor.name, anchor))
            {
                if (ShowDetailedLogs)
                {
                    Debug.LogFormat("[WorldAnchorManager] Successfully saved anchor \"{0}\".", anchor.name);
                }

                if (AnchorDebugText != null)
                {
                    AnchorDebugText.text += string.Format("\nSuccessfully saved anchor \"{0}\".", anchor.name);
                }

                ExportAnchor(anchor);

                return true;
            }

            Debug.LogErrorFormat("[WorldAnchorManager] Failed to save anchor \"{0}\"!", anchor.name);

            if (AnchorDebugText != null)
            {
                AnchorDebugText.text += string.Format("\nFailed to save anchor \"{0}\"!", anchor.name);
            }
            return false;
        }

        /// <summary>
        /// Deletes the anchor from the Anchor Store.
        /// </summary>
        /// <param name="anchorId">The anchor id.</param>
        private void DeleteAnchor(string anchorId)
        {
            if (AnchorStore.Delete(anchorId))
            {
                Debug.LogFormat("[WorldAnchorManager] Anchor {0} deleted successfully.", anchorId);
                if (AnchorDebugText != null)
                {
                    AnchorDebugText.text += string.Format("\nAnchor {0} deleted successfully.", anchorId);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(anchorId))
                {
                    anchorId = "NULL";
                }

                Debug.LogErrorFormat("[WorldAnchorManager] Failed to delete \"{0}\".", anchorId);
                if (AnchorDebugText != null)
                {
                    AnchorDebugText.text += string.Format("\nFailed to delete \"{0}\".", anchorId);
                }
            }
        }

        /// <summary>
        /// Called before creating anchor.  Used to check if import required.
        /// </summary>
        /// <param name="anchorId">Name of the anchor to import.</param>
        /// <param name="objectToAnchor">GameObject to anchor.</param>
        /// <returns>Success.</returns>
        protected virtual bool ImportAnchor(string anchorId, GameObject objectToAnchor)
        {
            return false;
        }

        /// <summary>
        /// Called after creating a new anchor.
        /// </summary>
        /// <param name="anchor">The anchor to export.</param>
        /// <returns>Success.</returns>
        protected virtual void ExportAnchor(WorldAnchor anchor) { }
#endif
=======
        /// When an anchor isn't located immediately we subscribe to this event so
        /// we can save the anchor when it is finally located.
        /// </summary>
        /// <param name="self">The anchor that is reporting a tracking changed event.</param>
        /// <param name="located">Indicates if the anchor is located or not located.</param>
        private void Anchor_OnTrackingChanged(WorldAnchor self, bool located)
        {
            if (located)
            {
                Debug.Log(gameObject.name + " : World anchor located successfully.");

                SaveAnchor(self);

                // Once the anchor is located we can unsubscribe from this event.
                self.OnTrackingChanged -= Anchor_OnTrackingChanged;
            }
            else
            {
                Debug.LogError(gameObject.name + " : World anchor failed to locate.");
            }
        }

        /// <summary>
        /// Saves the anchor to the anchor store.
        /// </summary>
        /// <param name="anchor"></param>
        private void SaveAnchor(WorldAnchor anchor)
        {
            // Save the anchor to persist holograms across sessions.
            if (AnchorStore.Save(anchor.name, anchor))
            {
                Debug.Log(gameObject.name + " : World anchor saved successfully.");
            }
            else
            {
                Debug.LogError(gameObject.name + " : World anchor save failed.");
            }
        }
>>>>>>> Stashed changes
    }
}
