using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="RecipeListData", menuName ="RecipeListData/ ObjectData")]
public class SO_RecipeList : ScriptableObject
{
    public List<SO_Recipe> recipeSOList;
}
