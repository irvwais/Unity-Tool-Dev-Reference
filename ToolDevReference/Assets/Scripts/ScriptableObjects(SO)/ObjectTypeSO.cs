using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectTypeSO : ScriptableObject // are essentially MonoBehaviours as assets instead of components you add to an object
{
    [Range(1f, 8f)] public float radius = 1;
    public float damage = 10;
    public Color color = Color.red;
}
