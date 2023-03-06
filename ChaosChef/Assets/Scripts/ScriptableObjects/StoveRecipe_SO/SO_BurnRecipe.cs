using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="KitchenOnjectData", menuName ="BurnRecipeData/ BurnData")]
public class SO_BurnRecipe : ScriptableObject
{
    public SO_KitchenObject input;
    public SO_KitchenObject output;
    public float burnedTime;
}
