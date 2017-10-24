// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
<<<<<<< Updated upstream
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
=======
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
>>>>>>> Stashed changes

namespace HoloToolkit.Unity
{
    /// <summary>
<<<<<<< Updated upstream
    /// Renders the UI and handles update logic for HoloToolkit/Configure/Apply Mixed Reality Project Settings.
    /// </summary>
    public class ProjectSettingsWindow : AutoConfigureWindow<ProjectSettingsWindow.ProjectSetting>
    {
        private const string SharingServiceURL = "https://raw.githubusercontent.com/Microsoft/MixedRealityToolkit-Unity/master/External/HoloToolkit/Sharing/Server/SharingService.exe";
        private const string InputManagerAssetURL = "https://raw.githubusercontent.com/Microsoft/MixedRealityToolkit-Unity/master/ProjectSettings/InputManager.asset";

        #region Nested Types

        public enum ProjectSetting
        {
            BuildWsaUwp,
            WsaEnableXR,
            WsaUwpBuildToD3D,
            TargetOccludedDevices,
            SharingServices,
            XboxControllerSupport,
            DotNetScriptingBackend,
        }

        #endregion // Nested Types

        #region Overrides / Event Handlers

        protected override void ApplySettings()
        {
            // Apply individual settings
            if (Values[ProjectSetting.BuildWsaUwp])
            {
                if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WSAPlayer)
                {
#if UNITY_2017_1_OR_NEWER
                    EditorUserBuildSettings.SwitchActiveBuildTargetAsync(BuildTargetGroup.WSA, BuildTarget.WSAPlayer);
#else
                    EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WSA, BuildTarget.WSAPlayer);
#endif
                }
                else
                {
                    UpdateSettings(EditorUserBuildSettings.activeBuildTarget);
                }
            }
            else
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
            }
        }

        protected override void LoadSettings()
        {
            for (int i = (int)ProjectSetting.BuildWsaUwp; i <= (int)ProjectSetting.DotNetScriptingBackend; i++)
            {
                switch ((ProjectSetting)i)
                {
                    case ProjectSetting.BuildWsaUwp:
                    case ProjectSetting.WsaEnableXR:
                    case ProjectSetting.WsaUwpBuildToD3D:
                    case ProjectSetting.DotNetScriptingBackend:
                        Values[(ProjectSetting)i] = true;
                        break;
                    case ProjectSetting.TargetOccludedDevices:
                        Values[(ProjectSetting)i] = EditorPrefsUtility.GetEditorPref(Names[(ProjectSetting)i], false);
                        break;
                    case ProjectSetting.SharingServices:
                        Values[(ProjectSetting)i] = EditorPrefsUtility.GetEditorPref(Names[(ProjectSetting)i], false);
                        break;
                    case ProjectSetting.XboxControllerSupport:
                        Values[(ProjectSetting)i] = EditorPrefsUtility.GetEditorPref(Names[(ProjectSetting)i], false);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void UpdateSettings(BuildTarget currentBuildTarget)
        {
            EditorPrefsUtility.SetEditorPref(Names[ProjectSetting.SharingServices], Values[ProjectSetting.SharingServices]);
            if (Values[ProjectSetting.SharingServices])
            {
                string sharingServiceDirectory = Directory.GetParent(Path.GetFullPath(Application.dataPath)).FullName + "\\External\\HoloToolkit\\Sharing\\Server";
                string sharingServicePath = sharingServiceDirectory + "\\SharingService.exe";
                if (!File.Exists(sharingServicePath) &&
                    EditorUtility.DisplayDialog("Attention!",
                        "You're missing the Sharing Service Executable in your project.\n\n" +
                        "Would you like to download the missing files from GitHub?\n\n" +
                        "Alternatively, you can download it yourself or specify a target IP to connect to at runtime on the Sharing Stage.",
                        "Yes", "Cancel"))
                {
                    using (var webRequest = UnityWebRequest.Get(SharingServiceURL))
                    {
#if UNITY_2017_2_OR_NEWER
                        webRequest.SendWebRequest();
#else
                        webRequest.Send();
#endif
                        while (!webRequest.isDone)
                        {
                            if (webRequest.downloadProgress != -1)
                            {
                                EditorUtility.DisplayProgressBar(
                                    "Downloading the SharingService executable from GitHub",
                                    "Progress...", webRequest.downloadProgress);
                            }
                        }

                        EditorUtility.ClearProgressBar();

#if UNITY_2017_1_OR_NEWER
                        if (webRequest.isNetworkError || webRequest.isHttpError)
#else
                            if (webRequest.isError)
#endif
                        {
                            Debug.LogError("Network Error: " + webRequest.error);
                        }
                        else
                        {
                            byte[] sharingServiceData = webRequest.downloadHandler.data;
                            Directory.CreateDirectory(sharingServiceDirectory);
                            File.WriteAllBytes(sharingServicePath, sharingServiceData);
                        }
                    }
                }
                else
                {
                    Debug.LogFormat("Alternatively, you can download from this link: {0}", SharingServiceURL);
                }

                PlayerSettings.WSA.SetCapability(PlayerSettings.WSACapability.InternetClientServer, true);
                PlayerSettings.WSA.SetCapability(PlayerSettings.WSACapability.PrivateNetworkClientServer, true);
            }
            else
            {
                PlayerSettings.WSA.SetCapability(PlayerSettings.WSACapability.InternetClient, false);
                PlayerSettings.WSA.SetCapability(PlayerSettings.WSACapability.InternetClientServer, false);
                PlayerSettings.WSA.SetCapability(PlayerSettings.WSACapability.PrivateNetworkClientServer, false);
            }

            var inputManagerPath = Directory.GetParent(Path.GetFullPath(Application.dataPath)).FullName + "\\ProjectSettings\\InputManager.asset";
            bool userPermission = Values[ProjectSetting.XboxControllerSupport];

            if (userPermission)
            {
                userPermission = EditorUtility.DisplayDialog("Attention!",
                    "Hi there, we noticed that you've enabled the Xbox Controller support.\n\n" +
                    "Do you give us permission to download the latest input mapping definitions from " +
                    "the Mixed Reality Toolkit's GitHub page and replace your project's InputManager.asset?\n\n",
                    "OK", "Cancel");

                if (userPermission)
                {
                    using (var webRequest = UnityWebRequest.Get(InputManagerAssetURL))
                    {
#if UNITY_2017_2_OR_NEWER
                        webRequest.SendWebRequest();
#else
                        webRequest.Send();
#endif

                        while (!webRequest.isDone)
                        {
                            if (webRequest.downloadProgress != -1)
                            {
                                EditorUtility.DisplayProgressBar("Downloading InputManager.asset from GitHub", "Progress...", webRequest.downloadProgress);
                            }
                        }

                        EditorUtility.ClearProgressBar();

#if UNITY_2017_1_OR_NEWER
                        if (webRequest.isNetworkError || webRequest.isHttpError)
#else
                            if (webRequest.isError)
#endif
                        {
                            Debug.LogError("Network Error: " + webRequest.error);
                            userPermission = false;
                        }
                        else
                        {
                            File.Copy(inputManagerPath, inputManagerPath + ".old", true);
                            File.WriteAllText(inputManagerPath, webRequest.downloadHandler.text);
                        }
                    }
                }
            }

            if (!userPermission)
            {
                Values[ProjectSetting.XboxControllerSupport] = false;
                if (File.Exists(inputManagerPath + ".old"))
                {
                    File.Copy(inputManagerPath + ".old", inputManagerPath, true);
                    File.Delete(inputManagerPath + ".old");
                    Debug.Log("Previous Input Mapping Restored.");
                }
                else
                {
                    Debug.LogWarning("No old Input Mapping found!");
                }
            }

            EditorPrefsUtility.SetEditorPref(Names[ProjectSetting.XboxControllerSupport], Values[ProjectSetting.XboxControllerSupport]);

            if (currentBuildTarget != BuildTarget.WSAPlayer)
            {
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                Close();
                return;
            }

            EditorUserBuildSettings.wsaUWPBuildType = Values[ProjectSetting.WsaUwpBuildToD3D]
                ? WSAUWPBuildType.D3D
                : WSAUWPBuildType.XAML;

            UnityEditorInternal.VR.VREditor.SetVREnabledOnTargetGroup(BuildTargetGroup.WSA, Values[ProjectSetting.WsaEnableXR]);

            if (!Values[ProjectSetting.WsaEnableXR])
            {
                EditorUserBuildSettings.wsaSubtarget = WSASubtarget.AnyDevice;
                UnityEditorInternal.VR.VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.WSA, new[] { "None" });
                PlayerSettings.WSA.SetCapability(PlayerSettings.WSACapability.HumanInterfaceDevice, false);
                BuildDeployPrefs.BuildPlatform = "Any CPU";
            }
            else
            {
#if !UNITY_2017_2_OR_NEWER
                Values[ProjectSetting.TargetOccludedDevices] = false;
#endif
                if (!Values[ProjectSetting.TargetOccludedDevices])
                {
                    EditorUserBuildSettings.wsaSubtarget = WSASubtarget.HoloLens;
                    UnityEditorInternal.VR.VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.WSA, new[] { "WindowsMR" });
                    PlayerSettings.WSA.SetCapability(PlayerSettings.WSACapability.HumanInterfaceDevice, Values[ProjectSetting.XboxControllerSupport]);
                    BuildDeployPrefs.BuildPlatform = "x86";

                    for (var i = 0; i < QualitySettings.names.Length; i++)
                    {
                        QualitySettings.DecreaseLevel(true);
                    }
                }
                else
                {
                    EditorUserBuildSettings.wsaSubtarget = WSASubtarget.PC;
                    UnityEditorInternal.VR.VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.WSA, new[] { "WindowsMR" });
                    PlayerSettings.WSA.SetCapability(PlayerSettings.WSACapability.HumanInterfaceDevice, false);
                    BuildDeployPrefs.BuildPlatform = "x64";

                    for (var i = 0; i < QualitySettings.names.Length; i++)
                    {
                        QualitySettings.IncreaseLevel(true);
                    }
                }

                int currentQualityLevel = QualitySettings.GetQualityLevel();

                // HACK: Edits QualitySettings.asset Directly
                // TODO: replace with friendlier version that uses built in APIs when Unity fixes or makes available.
                // See: http://answers.unity3d.com/questions/886160/how-do-i-change-qualitysetting-for-my-platform-fro.html
                try
                {
                    // Find the WSA element under the platform quality list and replace it's value with the current level.
                    string settingsPath = "ProjectSettings/QualitySettings.asset";
                    string matchPattern = @"(m_PerPlatformDefaultQuality.*Windows Store Apps:) (\d+)";
                    string replacePattern = @"$1 " + currentQualityLevel;

                    string settings = File.ReadAllText(settingsPath);
                    settings = Regex.Replace(settings, matchPattern, replacePattern, RegexOptions.Singleline);

                    File.WriteAllText(settingsPath, settings);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

            EditorPrefsUtility.SetEditorPref(Names[ProjectSetting.TargetOccludedDevices], Values[ProjectSetting.TargetOccludedDevices]);

            PlayerSettings.SetScriptingBackend(BuildTargetGroup.WSA,
                Values[ProjectSetting.DotNetScriptingBackend]
                    ? ScriptingImplementation.WinRTDotNET
                    : ScriptingImplementation.IL2CPP);

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            Close();
        }

        protected override void OnGuiChanged()
        {
=======
    /// Renders the UI and handles update logic for HoloToolkit/Configure/Apply HoloLens Project Settings.
    /// </summary>
    public class ProjectSettingsWindow : AutoConfigureWindow<ProjectSettingsWindow.ProjectSetting>
    {

        #region Nested Types
        public enum ProjectSetting
        {
            BuildWsaUwp,
            WsaUwpBuildToD3D,
            WsaFastestQuality,
            WsaEnableVR
        }
        #endregion // Nested Types

        #region Internal Methods
        /// <summary>
        /// Enables virtual reality for WSA and ensures HoloLens is in the supported SDKs.
        /// </summary>
        private void EnableVirtualReality()
        {
            try
            {
                // Grab the text from the project settings asset file
                string settingsPath = "ProjectSettings/ProjectSettings.asset";
                string settings = File.ReadAllText(settingsPath);

                // We're looking for the list of VR devices for the current build target, then
                // ensuring that the HoloLens is in that list
                bool foundBuildTargetVRSettings = false;
                bool foundBuildTargetMetro = false;
                bool foundBuildTargetEnabled = false;
                bool foundDevices = false;
                bool foundHoloLens = false;

                StringBuilder builder = new StringBuilder(); // Used to build the final output
                string[] lines = settings.Split(new char[] { '\n' });
                for (int i = 0; i < lines.Length; ++i)
                {
                    string line = lines[i];

                    // Look for the build target VR settings
                    if (!foundBuildTargetVRSettings)
                    {
                        if (line.Contains("m_BuildTargetVRSettings:"))
                        {
                            // If no targets are enabled at all, just create the known entries and skip the rest of the tests
                            if (line.Contains("[]"))
                            {
                                // Remove the empty array symbols
                                line = line.Replace(" []", "\n");

                                // Generate the new lines
                                line += "  - m_BuildTarget: Metro\n";
                                line += "    m_Enabled: 1\n";
                                line += "    m_Devices:\n";
                                line += "    - HoloLens";

                                // Mark all fields as found so we don't search anymore
                                foundBuildTargetVRSettings = true;
                                foundBuildTargetMetro = true;
                                foundBuildTargetEnabled = true;
                                foundDevices = true;
                                foundHoloLens = true;
                            }
                            else
                            {
                                // The target VR settngs were found but the others
                                // still need to be searched for.
                                foundBuildTargetVRSettings = true;
                            }
                        }
                    }

                    // Look for the build target for Metro
                    else if (!foundBuildTargetMetro)
                    {
                        if (line.Contains("m_BuildTarget: Metro"))
                        {
                            foundBuildTargetMetro = true;
                        }
                    }

                    else if (!foundBuildTargetEnabled)
                    {
                        if (line.Contains("m_Enabled"))
                        {
                            line = "    m_Enabled: 1";
                            foundBuildTargetEnabled = true;
                        }
                    }

                    // Look for the enabled Devices list
                    else if (!foundDevices)
                    {
                        if (line.Contains("m_Devices:"))
                        {
                            // Clear the empty array symbols if any
                            line = line.Replace(" []", "");
                            foundDevices = true;
                        }
                    }

                    // Once we've found the list look for HoloLens or the next non element
                    else if (!foundHoloLens)
                    {
                        // If this isn't an element in the device list
                        if (!line.Contains("-"))
                        {
                            // add the hololens element, and mark it found
                            builder.Append("    - HoloLens\n");
                            foundHoloLens = true;
                        }

                        // Otherwise test if this is the hololens device
                        else if (line.Contains("HoloLens"))
                        {
                            foundHoloLens = true;
                        }
                    }

                    builder.Append(line);

                    // Write out a \n for all but the last line
                    // NOTE: Specifically preserving unix line endings by avoiding StringBuilder.AppendLine
                    if (i != lines.Length - 1)
                    {
                        builder.Append('\n');
                    }
                }

                // Capture the final string
                settings = builder.ToString();

                File.WriteAllText(settingsPath, settings);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// Modifies the WSA default quality setting to the fastest
        /// </summary>
        private void SetFastestDefaultQuality()
        {
            try
            {
                // Find the WSA element under the platform quality list and replace it's value with 0
                string settingsPath = "ProjectSettings/QualitySettings.asset";
                string matchPattern = @"(m_PerPlatformDefaultQuality.*Windows Store Apps:) (\d+)";
                string replacePattern = @"$1 0";

                string settings = File.ReadAllText(settingsPath);
                settings = Regex.Replace(settings, matchPattern, replacePattern, RegexOptions.Singleline);

                File.WriteAllText(settingsPath, settings);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        #endregion // Internal Methods

        #region Overrides / Event Handlers
        protected override void ApplySettings()
        {
            // See the blow notes for why text asset serialization is required
            if (EditorSettings.serializationMode != SerializationMode.ForceText)
            {
                // NOTE: PlayerSettings.virtualRealitySupported would be ideal, except that it only reports/affects whatever platform tab
                // is currently selected in the Player settings window. As we don't have code control over what view is selected there
                // this property is fairly useless from script.

                // NOTE: There is no current way to change the default quality setting from script

                string dialogTitle = "Updates require text serialization of assets";
                string message = "Unity doesn't provide apis for updating the default quality or enabling VR support.\n\n" +
                    "Is it ok if we force text serialization of assets so that we can modify the properties directly?";

                bool forceText = EditorUtility.DisplayDialog(dialogTitle, message, "Yes", "No");
                if (!forceText)
                {
                    return;
                }

                EditorSettings.serializationMode = SerializationMode.ForceText;
            }

            // Apply individual settings
            if (Values[ProjectSetting.BuildWsaUwp])
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.WSAPlayer);
                EditorUserBuildSettings.wsaSDK = WSASDK.UWP;
            }
            if (Values[ProjectSetting.WsaUwpBuildToD3D])
            {
                EditorUserBuildSettings.wsaUWPBuildType = WSAUWPBuildType.D3D;
            }
            if (Values[ProjectSetting.WsaFastestQuality])
            {
                SetFastestDefaultQuality();
            }
            if (Values[ProjectSetting.WsaEnableVR])
            {
                EnableVirtualReality();
            }

            // Since we went behind Unity's back to tweak some settings we 
            // need to reload the project to have them take effect
            bool canReload = EditorUtility.DisplayDialog(
                "Project reload required!",
                "Some changes require a project reload to take effect.\n\nReload now?",
                "Yes", "No");

            if (canReload)
            {
                string projectPath = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
                EditorApplication.OpenProject(projectPath);
            }
        }

        protected override void LoadSettings()
        {
            for (int i = (int)ProjectSetting.BuildWsaUwp; i <= (int)ProjectSetting.WsaEnableVR; i++)
            {
                Values[(ProjectSetting)i] = true;
            }
>>>>>>> Stashed changes
        }

        protected override void LoadStrings()
        {
<<<<<<< Updated upstream
            Names[ProjectSetting.BuildWsaUwp] = "Target Windows Universal UWP";
            Descriptions[ProjectSetting.BuildWsaUwp] =
                "<b>Required</b>\n\n" +
                "Switches the currently active target to produce a Store app targeting the Universal Windows Platform.\n\n" +
                "<color=#ffff00ff><b>Note:</b></color> Cross platform development can be done with this toolkit, but many features and" +
                "tools will not work if the build target is not Windows Universal.";

            Names[ProjectSetting.WsaEnableXR] = "Enable XR";
            Descriptions[ProjectSetting.WsaEnableXR] =
                "<b>Required</b>\n\n" +
                "Enables 'Windows Holographic' for Windows Store apps.\n\n" +
                "If disabled, your application will run as a normal UWP app on PC, and will launch as a 2D app on HoloLens.\n\n" +
                "<color=#ff0000ff><b>Warning!</b></color> HoloLens and tools like 'Holographic Remoting' will not function without this enabled.";

            Names[ProjectSetting.WsaUwpBuildToD3D] = "Build for Direct3D";
            Descriptions[ProjectSetting.WsaUwpBuildToD3D] =
                "Recommended\n\n" +
                "Produces an app that targets Direct3D instead of Xaml.\n\n" +
                "Pure Direct3D apps run faster than applications that include Xaml. This option should remain checked unless you plan to " +
                "overlay Unity content with Xaml content or you plan to switch between Unity views and Xaml views at runtime.";

            Names[ProjectSetting.TargetOccludedDevices] = "Target Occluded Devices";
            Descriptions[ProjectSetting.TargetOccludedDevices] =
                "Changes the target Device and updates the default quality settings, if needed. Occluded devices are generally VR hardware (like the Acer HMD) " +
                "that do not have a 'see through' display, while transparent devices (like the HoloLens) are generally AR hardware where users can see " +
                "and interact with digital elements in the physical world around them.\n\n" +
#if !UNITY_2017_2_OR_NEWER
                "<color=#ff0000ff><b>Warning!</b></color> Occluded Devices are only supported in Unity 2017.2 and newer and cannot be enabled.\n\n" +
#endif
                "<color=#ffff00ff><b>Note:</b></color> If you're not targeting Occluded devices, It's generally recommended that Transparent devices use " +
                "the lowest default quality setting, and is set automatically for you. This can be manually changed in your the Project's Quality Settings.";

            Names[ProjectSetting.SharingServices] = "Enable Sharing Services";
            Descriptions[ProjectSetting.SharingServices] =
                "Enables the use of the Sharing Services in your project for all apps on any platform.\n\n" +
                "<color=#ffff00ff><b>Note:</b></color> Start the Sharing Server via 'HoloToolkit/Sharing Service/Launch Sharing Service'.\n\n" +
                "<color=#ffff00ff><b>Note:</b></color> The InternetClientServer and PrivateNetworkClientServer capabilities will be enabled in the " +
                "appx manifest for you.";

            Names[ProjectSetting.XboxControllerSupport] = "Enable Xbox Controller Support";
            Descriptions[ProjectSetting.XboxControllerSupport] =
                "Enables the use of Xbox Controller support for all apps on any platform.\n\n" +
                "<color=#ff0000ff><b>Warning!</b></color> Enabling this feature will copy your old InputManager.asset and append it with \".old\".  " +
                "To revert simply disable Xbox Controller Support.\n\n" +
                "<color=#ffff00ff><b>Note:</b></color> ONLY the HoloLens platform target requires the HID capabilities be defined in the appx manifest.  " +
                "This capability is automatically enabled for you if you enable Xbox Controller Support and enable VR and target the HoloLens device.";

            Names[ProjectSetting.DotNetScriptingBackend] = "Enable .NET scripting backend";
            Descriptions[ProjectSetting.DotNetScriptingBackend] =
                "Recommended\n\n" +
                "If you have the .NET unity module installed this will update the backend scripting profile, otherwise the scripting backend will be IL2CPP.";
=======
            Names[ProjectSetting.BuildWsaUwp] = "Target Windows Store and UWP";
            Descriptions[ProjectSetting.BuildWsaUwp] = "Required\n\nSwitches the currently active target to produce a Store app targeting the Universal Windows Platform.\n\nSince HoloLens only supports Windows Store apps, this option should remain checked unless you plan to manually switch the target later before you build.";

            Names[ProjectSetting.WsaUwpBuildToD3D] = "Build for Direct3D";
            Descriptions[ProjectSetting.WsaUwpBuildToD3D] = "Recommended\n\nProduces an app that targets Direct3D instead of Xaml.\n\nPure Direct3D apps run faster than applications that include Xaml. This option should remain checked unless you plan to overlay Unity content with Xaml content or you plan to switch between Unity views and Xaml views at runtime.";

            Names[ProjectSetting.WsaFastestQuality] = "Set Quality to Fastest";
            Descriptions[ProjectSetting.WsaFastestQuality] = "Recommended\n\nChanges the quality settings for Windows Store apps to the 'Fastest' setting.\n\n'Fastest' is the recommended quality setting for HoloLens apps, but this option can be unchecked if you have already optimized your project for the HoloLens.";

            Names[ProjectSetting.WsaEnableVR] = "Enable VR";
            Descriptions[ProjectSetting.WsaEnableVR] = "Required\n\nEnables VR for Windows Store apps and adds the HoloLens as a target VR device.\n\nThe application will not compile for HoloLens and tools like Holographic Remoting will not function without this enabled. Therefore this option should remain checked unless you plan to manually perform these steps later.";
>>>>>>> Stashed changes
        }

        protected override void OnEnable()
        {
<<<<<<< Updated upstream
            base.OnEnable();

#if UNITY_2017_1_OR_NEWER
            AutoConfigureMenu.ActiveBuildTargetChanged += UpdateSettings;
#endif

            minSize = new Vector2(350, 350);
            maxSize = minSize;
        }

        #endregion // Overrides / Event Handlers
    }
}
=======
            // Pass to base first
            base.OnEnable();

            // Set size
            minSize = new Vector2(350, 260);
            maxSize = minSize;
        }
        #endregion // Overrides / Event Handlers
    }
}
>>>>>>> Stashed changes
