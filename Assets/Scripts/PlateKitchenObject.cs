using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectScriptableObject> _validKitchenObjectSOList;

    private List<KitchenObjectScriptableObject> _kitchenObjectScriptableObjectsList;

    private void Awake()
    {
        _kitchenObjectScriptableObjectsList = new List<KitchenObjectScriptableObject>();
    }

    public bool TryAddIngredient(KitchenObjectScriptableObject kitchenObjectScriptableObject)
    {
        if(!_validKitchenObjectSOList.Contains(kitchenObjectScriptableObject))
        {
            return false;
        }

        if(_kitchenObjectScriptableObjectsList.Contains(kitchenObjectScriptableObject))
        {
            return false;
        }
        else
        {
            _kitchenObjectScriptableObjectsList.Add(kitchenObjectScriptableObject);
            return true;
        }
        
    }
}
