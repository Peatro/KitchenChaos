using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectScriptableObject _kitchenObjectScriptableObject;

    private ClearCounter _clearCounter;

    public KitchenObjectScriptableObject GetKitchenOjectScriptableObject()
    {
        return _kitchenObjectScriptableObject;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        _clearCounter?.ClearKitchenObject();

        _clearCounter = clearCounter;

        if(clearCounter.HasKithcenObject())
        {
            Debug.LogError("Counter already has a KitchenObject!");
        }

        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() 
    {
        return _clearCounter;
    }
}
