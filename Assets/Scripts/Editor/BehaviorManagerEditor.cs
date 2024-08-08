using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BehaviorManager))]
public class BehaviorManagerEditor : Editor
{
    private string stateName;

    private EmpathyObjectSO empathyObject;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();

        stateName = EditorGUILayout.TextField("State Name", stateName);

        if (GUILayout.Button("Play State"))
        {
            PlayState();
        }

        empathyObject = (EmpathyObjectSO)EditorGUILayout.ObjectField("Empathy Object", empathyObject, typeof(EmpathyObjectSO), false);

        if (empathyObject != null)
        {
            if (GUILayout.Button("Check Empathy Object"))
            {
                CheckEmpathyObject();
            }
        }
    }

    private void CheckEmpathyObject()
    {
        BehaviorManager behaviorManager = (BehaviorManager)target;

        if (behaviorManager != null)
        {
            behaviorManager.CheckEmpathyObject(empathyObject);
        }
        else
        {
            Debug.LogWarning("BehaviorManager not found!");
        }
    }

    private void PlayState()
    {
        BehaviorManager behaviorManager = (BehaviorManager)target;

        if (behaviorManager != null)
        {
            behaviorManager.PlayAnimatorState(stateName);
        }
        else
        {
            Debug.LogWarning("BehaviorManager or Animator not found!");
        }
    }
}
