using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="KitchenOnjectData", menuName ="KitchenObjectData/ ObjectData")]
public class SO_KitchenObject : ScriptableObject
{
    public Transform prepab;
    public Sprite sprite;
    public string objectName;
}
