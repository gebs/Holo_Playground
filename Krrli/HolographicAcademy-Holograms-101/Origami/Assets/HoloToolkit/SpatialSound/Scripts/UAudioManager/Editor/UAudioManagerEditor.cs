// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEditor;

namespace HoloToolkit.Unity
{
    [CustomEditor(typeof(UAudioManager))]
    public class UAudioManagerEditor : UAudioManagerBaseEditor<AudioEvent>
    {
        private void OnEnable()
        {
<<<<<<< Updated upstream
            this.MyTarget = (UAudioManager)target;
=======
            this.myTarget = (UAudioManager)target;
>>>>>>> Stashed changes
            SetUpEditor();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("globalEventInstanceLimit"));
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("globalInstanceBehavior"));
            DrawInspectorGUI(false);
        }
    }
}