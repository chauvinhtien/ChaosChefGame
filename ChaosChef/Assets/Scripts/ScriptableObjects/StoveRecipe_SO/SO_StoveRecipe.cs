using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="KitchenOnjectData", menuName ="StoveRecipeData/ StoveData")]
public class SO_StoveRecipe : ScriptableObject
{
    public SO_KitchenObject input;
    public SO_KitchenObject output;
    public float stoveTime;
}
