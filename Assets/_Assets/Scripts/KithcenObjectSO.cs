using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// represents any kind of kitchen object the player can interact
[CreateAssetMenu()]
public class KithcenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;

}
