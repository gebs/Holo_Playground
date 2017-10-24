// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using HoloToolkit.Unity.InputModule;
using Cursor = HoloToolkit.Unity.InputModule.Cursor;
=======
>>>>>>> Stashed changes

namespace HoloToolkit.Unity
{
    /// <summary>
<<<<<<< Updated upstream
    /// Renders the UI and handles update logic for MixedRealityToolkit/Configure/Apply Mixed Reality Scene Settings.
    /// </summary>
    public class SceneSettingsWindow : AutoConfigureWindow<SceneSettingsWindow.SceneSetting>
    {
        /// <summary>
        /// Can be found in the meta file of the camera prefab.  We use the GUID in case people move the toolkit folders &amp; assets around in their own projects.
        /// <remarks>Currently points to the MixedRealityCameraParent.prefab</remarks>
        /// </summary>
        private const string CameraPrefabGUID = "d29bc40b7f3df26479d6a0aac211c355";

        /// <summary>
        /// Can be found in the meta file of the camera prefab.  We use the GUID in case people move the toolkit folders &amp; assets around in their own projects.
        /// <remarks>Currently points to the InputManager.prefab</remarks>
        /// </summary>
        private const string InputSystemPrefabGUID = "3eddd1c29199313478dd3f912bfab2ab";

        /// <summary>
        /// Can be found in the meta file of the camera prefab.  We use the GUID in case people move the toolkit folders &amp; assets around in their own projects.
        /// <remarks>Currently points to the DefaultCursor.prefab</remarks>
        /// </summary>
        private const string DefaultCursorPrefabGUID = "a611e772ef8ddf64d8106a9cbb70f31c";

        #region Nested Types

        public enum SceneSetting
        {
            AddMixedRealityCamera,
            CameraToOrigin,
            AddInputSystem,
            AddDefaultCursor,
        }

        #endregion // Nested Types

        #region Overrides / Event Handlers

        protected override void ApplySettings()
        {
            if (Values[SceneSetting.AddMixedRealityCamera])
            {
                if (CameraCache.Main != null)
                {
                    DestroyImmediate(CameraCache.Main.gameObject.GetParentRoot());
                }

                PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(CameraPrefabGUID)));
            }

            var mainCamera = CameraCache.Refresh(Camera.main);

            if (mainCamera == null)
            {
                Debug.LogWarning("Could not find a valid \"MainCamera\"!  Unable to update camera position.");
            }
            else
            {
                if (Values[SceneSetting.CameraToOrigin])
                {
                    mainCamera.transform.position = Vector3.zero;
                }
            }

            if (Values[SceneSetting.AddInputSystem])
            {
                var inputManager = FindObjectOfType<InputManager>();
                if (inputManager != null)
                {
                    DestroyImmediate(inputManager.gameObject);
                }

                var eventSystems = FindObjectsOfType<EventSystem>();
                foreach (var eventSystem in eventSystems)
                {
                    DestroyImmediate(eventSystem.gameObject);
                }

                var inputModules = FindObjectsOfType<StandaloneInputModule>();
                foreach (var inputModule in inputModules)
                {
                    DestroyImmediate(inputModule.gameObject);
                }

                PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(InputSystemPrefabGUID)));
            }

            if (Values[SceneSetting.AddDefaultCursor])
            {
                var cursors = FindObjectsOfType<Cursor>();
                foreach (var cursor in cursors)
                {
                    DestroyImmediate(cursor.gameObject.GetParentRoot());
                }

                PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(DefaultCursorPrefabGUID)));

                FindObjectOfType<InputManager>().GetComponent<SimpleSinglePointerSelector>().Cursor = FindObjectOfType<Cursor>();
            }

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

            Close();
=======
    /// Renders the UI and handles update logic for HoloToolkit/Configure/Apply HoloLens Scene Settings.
    /// </summary>
    public class SceneSettingsWindow : AutoConfigureWindow<SceneSettingsWindow.SceneSetting>
    {
        #region Nested Types
        public enum SceneSetting
        {
            CameraToOrigin,
            CameraClearBlack,
            NearClipPlane,
            FieldOfView,
        }
        #endregion // Nested Types

        #region Overrides / Event Handlers
        protected override void ApplySettings()
        {
            // Ensure we have a camera
            if (Camera.main == null)
            {
                Debug.LogWarning(@"Could not apply settings - no camera tagged with ""MainCamera""");
                return;
            }

            // Apply individual settings
            if (Values[SceneSetting.CameraToOrigin])
            {
                Camera.main.transform.position = Vector3.zero;
            }
            if (Values[SceneSetting.CameraClearBlack])
            {
                Camera.main.clearFlags = CameraClearFlags.SolidColor;
                Camera.main.backgroundColor = Color.clear;
            }
            if (Values[SceneSetting.NearClipPlane])
            {
                Camera.main.nearClipPlane = 0.85f;
            }
            if (Values[SceneSetting.FieldOfView])
            {
                Camera.main.fieldOfView = 16.0f;
            }
>>>>>>> Stashed changes
        }

        protected override void LoadSettings()
        {
<<<<<<< Updated upstream
            for (int i = 0; i <= (int)SceneSetting.AddDefaultCursor; i++)
=======
            for (int i = (int)SceneSetting.CameraToOrigin; i <= (int)SceneSetting.FieldOfView; i++)
>>>>>>> Stashed changes
            {
                Values[(SceneSetting)i] = true;
            }
        }

<<<<<<< Updated upstream
        protected override void OnGuiChanged()
        {
        }

        protected override void LoadStrings()
        {
            Names[SceneSetting.AddMixedRealityCamera] = "Add the Mixed Reality Camera Prefab";
            Descriptions[SceneSetting.AddMixedRealityCamera] =
                "Recommended\n\n" +
                "Adds the Mixed Reality Camera Prefab to the scene.\n\n" +
                "The prefab comes preset with all the components and options for automatically handling Occluded and Transparent Mixed Reality Applications.";

            Names[SceneSetting.CameraToOrigin] = "Move Camera to Origin";
            Descriptions[SceneSetting.CameraToOrigin] =
                "Recommended\n\n" +
                "Moves the main camera to the world origin of the scene (0, 0, 0).\n\n" +
                "<color=#ffff00ff><b>Note:</b></color> When a Mixed Reality application starts, the users head is the center of the world. By not having your Main Camera centered at " +
                "the world origin (0, 0, 0) will result in GameObjects not appearing where they are expected. This option should remain checked unless you have alternative methods " +
                "that explicitly deal with any apparent offset.";

            Names[SceneSetting.AddInputSystem] = "Add the Input Manager Prefab";
            Descriptions[SceneSetting.AddInputSystem] =
                "Recommended\n\n" +
                "Adds the Input Manager Prefab to the scene.\n\n" +
                "The prefab comes preset with all the components and options for automatically handling input for Mixed Reality Applications.\n\n" +
                "<color=#ff0000ff><b>Warning!</b></color> This will remove and replace any currently existing Input Managers or Event Systems in your scene.";

            Names[SceneSetting.AddDefaultCursor] = "Add the Default Cursor Prefab";
            Descriptions[SceneSetting.AddDefaultCursor] =
                "Recommended\n\n" +
                "Adds the  Default Cursor Prefab to the scene.\n\n" +
                "The prefab comes preset with all the components and options for automatically handling cursor animations for Mixed Reality Applications.\n\n" +
                "<color=#ff0000ff><b>Warning!</b></color> This will remove and replace any currently existing Cursors in your scene.";
=======
        protected override void LoadStrings()
        {
            Names[SceneSetting.CameraToOrigin] = "Move Camera to Origin";
            Descriptions[SceneSetting.CameraToOrigin] = "Moves the main camera to the origin of the scene (0,0,0).\n\nWhen a HoloLens application starts, the users head is the center of the world. Not having the main camera at 0,0,0 will result in holograms not appearing where they are expeted. This option should remain checked unless you have code that explicitly deals with any offset.";

            Names[SceneSetting.CameraClearBlack] = "Camera Clears to Black";
            Descriptions[SceneSetting.CameraClearBlack] = "Causes the camera to render to a black background instead of the default skybox.\n\nIn HoloLens the color black is transparent. Rendering to a black background allows the user to see the real world wherever there are no holograms. This option should remain checked unless you are building a VR-like experience or are implementing advanced rendering techniques.";

            Names[SceneSetting.NearClipPlane] = "Update Near Clipping Plane";
            Descriptions[SceneSetting.NearClipPlane] = "Updates the near clipping plane of the main camera to the recommended setting.\n\nThe recommended near clipping plane is designed to reduce eye fatigue. This option should remain checked unless you have a specific need to allow closer inspection of holograms and understand the impact of closely focused objects. (e.g. vergence accommodation conflict)";

            Names[SceneSetting.FieldOfView] = "Update Field of View";
            Descriptions[SceneSetting.FieldOfView] = "Updates the main camera Field of View.\n\nAllows the Unity Editor to more closely reflect what will be seen on the device at runtime. This option should remain checked unless you design-time requirements for a specific FOV.";
>>>>>>> Stashed changes
        }

        protected override void OnEnable()
        {
<<<<<<< Updated upstream
            base.OnEnable();

            minSize = new Vector2(350, 250);
=======
            // Pass to base first
            base.OnEnable();

            // Set size
            minSize = new Vector2(350, 240);
>>>>>>> Stashed changes
            maxSize = minSize;
        }
        #endregion // Overrides / Event Handlers
    }
}