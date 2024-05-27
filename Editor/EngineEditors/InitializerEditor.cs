﻿using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OneJS.Editor {
    [CustomEditor(typeof(Initializer))]
    [CanEditMultipleObjects]
    public class InitializerEditor : UnityEditor.Editor {
        SerializedProperty _defaultFiles;
        SerializedProperty _outputsZip;

        SerializedProperty _version;
        SerializedProperty _forceExtract;
        SerializedProperty _ignoreList;

        bool showAssets;

        void OnEnable() {
            _defaultFiles = serializedObject.FindProperty("defaultFiles");
            _outputsZip = serializedObject.FindProperty("outputsZip");

            _version = serializedObject.FindProperty("version");
            _forceExtract = serializedObject.FindProperty("forceExtract");
            _ignoreList = serializedObject.FindProperty("ignoreList");
        }

        public override void OnInspectorGUI() {
            var initializer = target as Initializer;
            serializedObject.Update();
            EditorGUILayout.HelpBox("Sets up OneJS for first-time use by creating essential files in the WorkingDir if they are missing. Also takes care of @outputs folder extraction for Standalone Player.", MessageType.None);

            EditorGUILayout.PropertyField(_defaultFiles, new GUIContent("Default Files"));
            EditorGUILayout.PropertyField(_version, new GUIContent("Version"));
            EditorGUILayout.PropertyField(_forceExtract, new GUIContent("Force Extract"));
            EditorGUILayout.PropertyField(_outputsZip, new GUIContent("outputs.tgz"));
            EditorGUILayout.PropertyField(_ignoreList, new GUIContent("Ignore List"));

            // showAssets = EditorGUILayout.Foldout(showAssets, "Default Assets", true);
            // if (showAssets) {
            //     GUILayout.Space(10);
            //     EditorGUILayout.PropertyField(_onejsCoreZip, new GUIContent("    onejs-core.tgz"));
            //     EditorGUILayout.PropertyField(_outputsZip, new GUIContent("    outputs.tgz"));
            //     GUILayout.Space(10);
            // }

            GUILayout.BeginHorizontal();

            if (GUILayout.Button(new GUIContent("Package outputs.tgz", "Packages the @outputs folder into outputs.tgz. This is also automatically done when you make a player build."), GUILayout.Height(30))) {
                initializer.PackageOutputsZipWithPrompt();
            }

            GUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
}