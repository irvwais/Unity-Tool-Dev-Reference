using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MultiObjectManager : MonoBehaviour
{
    public static List<ObjectEditorVisualShowcase> allObjects = new List<ObjectEditorVisualShowcase>();
    
    [Tooltip("If TRUE the editor scene will show straight lines from the manager to the objects. If FALSE the editor scene will show bezier curves instead.")]
    [SerializeField] private bool isUsingDrawLines = false;

#if UNITY_EDITOR // this makes sure that this function will only be in Unity Editor and not the Build
    private void OnDrawGizmos()
    {
        foreach (var objects in allObjects)
        {
            if (objects.objectTypeSo == null) continue;

            if (isUsingDrawLines) { Handles.DrawAAPolyLine(transform.position, objects.transform.position); }
            
            else
            {
                Vector3 managerPos = transform.position;
                Vector3 objectsPos = objects.transform.position;
                float halfHeightOfBezierCurve = (managerPos.y - objectsPos.y) * 0.5f;
                Vector3 tangentOffset = Vector3.up * halfHeightOfBezierCurve;
                
                Handles.DrawBezier(
                    managerPos,
                    objectsPos, 
                    managerPos - tangentOffset,
                    objectsPos + tangentOffset,
                    objects.objectTypeSo.color,
                    EditorGUIUtility.whiteTexture,
                    1f
                );   
            }
        }
    }
#endif
}
