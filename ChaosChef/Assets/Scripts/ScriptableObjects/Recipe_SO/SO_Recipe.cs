using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="RecipeData", menuName ="RecipetData/ ObjectData")]
public class SO_Recipe : ScriptableObject
{
    public List<SO_KitchenObject> kitchenObjectSOList;
    public string recepieName;
}
