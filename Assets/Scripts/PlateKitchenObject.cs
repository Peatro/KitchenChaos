using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectScriptableObject kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectScriptableObject> _validKitchenObjectSOList;

    private List<KitchenObjectScriptableObject> _kitchenObjectScriptableObjectsList;

    private void Awake()
    {
        _kitchenObjectScriptableObjectsList = new List<KitchenObjectScriptableObject>();
    }

    private void Start()
    {
        
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
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectScriptableObject
            });
            return true;
        }        
    }

    public List<KitchenObjectScriptableObject> GetKitchenObjectScriptableObjectList()
    {
        return _kitchenObjectScriptableObjectsList;
    }
}
