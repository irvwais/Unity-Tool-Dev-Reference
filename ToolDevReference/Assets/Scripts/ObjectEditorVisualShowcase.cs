using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways] // used to run code in editor and not only when play is clicked
public class ObjectEditorVisualShowcase : MonoBehaviour
{
    public ObjectTypeSO objectTypeSo;

    private static readonly int ShaderPropertyColor = Shader.PropertyToID("_Color"); // cache string ID into an int for optimizing 
    private MaterialPropertyBlock _mpb;
    
    
    private void Awake()
    {
        #region MATERIAL_NOTES
        // // This creates a new material as an asset
        // Shader shader = Shader.Find("Default/Diffuse"); // shader is a legacy shader
        // Material material = new Material(shader);
        // material.hideFlags = HideFlags.HideAndDontSave; // this does not save the asset or save to the scene in which it is called itself so it can avoid potential memory leaks from constantly creating a new asset
        //
        // // This Duplicates the material on the object
        // GetComponent<MeshRenderer>().material.color = color;
        //
        // // This modifies the asset that is attached to the object, so the asset in the project folder changes
        // GetComponent<MeshRenderer>().sharedMaterial.color = color;
        #endregion
    }
    
    private void OnEnable() => MultiObjectManager.allObjects.Add(this);
    
    private void OnDisable() => MultiObjectManager.allObjects.Remove(this);

    private void OnValidate() => ApplyColor(); // On Validate is called when anything is changed in the inspector on property change

    private void OnDrawGizmos() // OnDrawGizmosSelected() used for drawing gizmo only when selected in editor
    {
        if (objectTypeSo == null) return;
        
        Handles.color = objectTypeSo.color;
        Handles.DrawWireDisc(transform.position, transform.up, objectTypeSo.radius);
    }

    private MaterialPropertyBlock Mpb => _mpb ??= new MaterialPropertyBlock(); // ??= (Null-Coalescing Operator) is the same as if (_mpb == null) then _mpb = new Material...
    
    private void ApplyColor()
    {
        if (objectTypeSo == null) return;
        
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>(); // get the mesh renderer of the object
        Mpb.SetColor(ShaderPropertyColor, objectTypeSo.color); // set the color of the material property block
        meshRenderer.SetPropertyBlock(_mpb); // assign the MPB to the mesh renderer
    }
}
