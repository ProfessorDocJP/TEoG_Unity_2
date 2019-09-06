﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPrefab))]
public class EnemyPrefabEditor : Editor
{
    private bool nameFold;
    private bool healthFold;
    private bool StartRace;
    private bool statsFold;

    public override void OnInspectorGUI()
    {
        //GUILayout.Label("test");
        EnemyPrefab myTarget = (EnemyPrefab)target;
        GUILayout.Label("Summary", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");
        GUILayout.Label(myTarget.FullName);
        GUILayout.BeginHorizontal();
        GUILayout.Label(myTarget.Gender.ToString());
        GUILayout.Label(myTarget.Race);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        nameFold = EditorGUILayout.Foldout(nameFold, "Name", true, EditorStyles.foldout);
        if (nameFold)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();
            GUILayout.Label("First name");
            GUILayout.Label("Last name");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            myTarget.firstName = EditorGUILayout.TextArea("");
            myTarget.lastName = EditorGUILayout.TextArea("");
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Random name giver", EditorStyles.boldLabel);
            myTarget.randomName = (RandomName)EditorGUILayout.ObjectField(myTarget.randomName, typeof(RandomName), true);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Give first name");
            myTarget.NeedFirstName = EditorGUILayout.Toggle(myTarget.NeedFirstName);
            GUILayout.Label("Give last name");
            myTarget.NeedLastName = EditorGUILayout.Toggle(myTarget.NeedLastName);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }
        StartRace = EditorGUILayout.Foldout(StartRace, "Assing race", true, EditorStyles.foldout);
        if (StartRace)
        {
            GUILayout.BeginVertical("Box");
            if (GUILayout.Button("Add options"))
            {
                myTarget.assingRace.AddOption();
            }
            serializedObject.Update();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Race", EditorStyles.boldLabel);
            GUILayout.Label("Probability", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();
            SerializedProperty RaceList = serializedObject.FindProperty("assingRace.Options");
            for (int i = 0; i < RaceList.arraySize; i++)
            {
                EditorGUILayout.PropertyField(RaceList.GetArrayElementAtIndex(i));
            }
            serializedObject.ApplyModifiedProperties();
            GUILayout.EndVertical();
        }
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Inventory"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Looks"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Vore"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Body"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("expSystem"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Perk"), true);
        serializedObject.ApplyModifiedProperties();

        healthFold = EditorGUILayout.Foldout(healthFold, "Health & will");
        if (healthFold)
        {
            GUILayout.BeginVertical("Box");
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("HP"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("WP"), true);
            serializedObject.ApplyModifiedProperties();
            GUILayout.EndVertical();
        }

        statsFold = EditorGUILayout.Foldout(statsFold, "Stats");
        if (statsFold)
        {
            GUILayout.BeginVertical("Box");
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("strength"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("charm"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("endurance"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dexterity"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("intelligence"), true);
            serializedObject.ApplyModifiedProperties();
            GUILayout.EndVertical();
        }

        /* EditorGUILayout.LabelField("Sciptable Objects",EditorStyles.boldLabel);
             GUILayout.BeginVertical("Box");
                 GUILayout.BeginHorizontal();
                     GUILayout.Label("Sprites");
                     myTarget.sprites = (CharSprites)EditorGUILayout.ObjectField(myTarget.sprites, typeof(CharSprites), true);
                 GUILayout.EndHorizontal();
                 GUILayout.BeginHorizontal();
                     GUILayout.Label("Settings");
                     myTarget.settings = (Settings)EditorGUILayout.ObjectField(myTarget.settings, typeof(Settings), true);
                 GUILayout.EndHorizontal();
                 GUILayout.BeginHorizontal();
                     GUILayout.Label("EventLog");
                     myTarget.eventLog = (EventLog)EditorGUILayout.ObjectField(myTarget.eventLog, typeof(EventLog), true);
                 GUILayout.EndHorizontal();
            GUILayout.EndVertical(); */
        GUILayout.Label("Standard editor and end of custom editor", EditorStyles.boldLabel);
        GUILayout.Space(20);
        base.OnInspectorGUI();
    }
}