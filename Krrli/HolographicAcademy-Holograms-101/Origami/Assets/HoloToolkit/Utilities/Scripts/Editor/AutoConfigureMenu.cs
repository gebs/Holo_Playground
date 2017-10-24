// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;
<<<<<<< Updated upstream
using UnityEditor.Build;
=======
>>>>>>> Stashed changes
using UnityEngine;

namespace HoloToolkit.Unity
{
    /// <summary>
    /// Configuration options derived from here: 
<<<<<<< Updated upstream
    /// https://developer.microsoft.com/en-us/windows/mixed-reality/unity_development_overview#Configuring_a_Unity_project_for_HoloLens
    /// </summary>
    public class AutoConfigureMenu
#if UNITY_2017_1_OR_NEWER
        : IActiveBuildTargetChanged
#endif
    {
#if UNITY_2017_1_OR_NEWER
        public delegate void BuildTargetArgs(BuildTarget newTarget);
        public static event BuildTargetArgs ActiveBuildTargetChanged;
#endif

        /// <summary>
        /// Displays a help page for the HoloToolkit.
        /// </summary>
        [MenuItem("Mixed Reality Toolkit/Configure/Show Help", false, 3)]
        public static void ShowHelp()
        {
            Application.OpenURL("https://github.com/Microsoft/MixedRealityToolkit-Unity/wiki");
=======
    /// https://developer.microsoft.com/en-us/windows/holographic/unity_development_overview#Configuring_a_Unity_project_for_HoloLens
    /// </summary>
    public class AutoConfigureMenu : MonoBehaviour
    {
        /// <summary>
        /// Displays a help page for the HoloToolkit.
        /// </summary>
        [MenuItem("HoloToolkit/Configure/Show Help", false, 3)]
        public static void ShowHelp()
        {
            Application.OpenURL("https://github.com/Microsoft/HoloToolkit-Unity/wiki");
>>>>>>> Stashed changes
        }

        /// <summary>
        /// Applies recommended scene settings to the current scenes
        /// </summary>
<<<<<<< Updated upstream
        [MenuItem("Mixed Reality Toolkit/Configure/Apply Mixed Reality Scene Settings #&s", false, 1)]
        public static void ShowSceneSettingsWindow()
        {
            var window = (SceneSettingsWindow)EditorWindow.GetWindow(typeof(SceneSettingsWindow), true, "Apply Mixed Reality Scene Settings");
=======
        [MenuItem("HoloToolkit/Configure/Apply HoloLens Scene Settings", false, 1)]
        public static void ApplySceneSettings()
        {
            SceneSettingsWindow window = (SceneSettingsWindow)EditorWindow.GetWindow(typeof(SceneSettingsWindow), true, "Apply HoloLens Scene Settings");
>>>>>>> Stashed changes
            window.Show();
        }

        /// <summary>
        /// Applies recommended project settings to the current project
        /// </summary>
<<<<<<< Updated upstream
        [MenuItem("Mixed Reality Toolkit/Configure/Apply Mixed Reality Project Settings #&p", false, 0)]
        public static void ShowProjectSettingsWindow()
        {
            var window = (ProjectSettingsWindow)EditorWindow.GetWindow(typeof(ProjectSettingsWindow), true, "Apply Mixed Reality Project Settings");
=======
        [MenuItem("HoloToolkit/Configure/Apply HoloLens Project Settings", false, 1)]
        public static void ApplyProjectSettings()
        {
            ProjectSettingsWindow window = (ProjectSettingsWindow)EditorWindow.GetWindow(typeof(ProjectSettingsWindow), true, "Apply HoloLens Project Settings");
>>>>>>> Stashed changes
            window.Show();
        }

        /// <summary>
        /// Applies recommended capability settings to the current project
        /// </summary>
<<<<<<< Updated upstream
        [MenuItem("Mixed Reality Toolkit/Configure/Apply UWP Capability Settings #&c", false, 2)]
        public static void ShowCapabilitySettingsWindow()
        {
            var window = (CapabilitySettingsWindow)EditorWindow.GetWindow(typeof(CapabilitySettingsWindow), true, "Apply UWP Capability Settings");
            window.Show();
        }

#if UNITY_2017_1_OR_NEWER
        public int callbackOrder { get; private set; }

        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
            if (ActiveBuildTargetChanged != null)
            {
                ActiveBuildTargetChanged.Invoke(newTarget);
            }
        }
#endif
    }
}
=======
        [MenuItem("HoloToolkit/Configure/Apply HoloLens Capability Settings", false, 2)]
        static void ApplyHoloLensCapabilitySettings()
        {
            CapabilitySettingsWindow window = (CapabilitySettingsWindow)EditorWindow.GetWindow(typeof(CapabilitySettingsWindow), true, "Apply HoloLens Capability Settings");
            window.Show();
        }
    }
}
>>>>>>> Stashed changes
