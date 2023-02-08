using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectScriptableObject _kitchenObjectScriptableObject;

    private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjectScriptableObject GetKitchenOjectScriptableObject() => _kitchenObjectScriptableObject;

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenOBjectParent)
    {
        _kitchenObjectParent?.ClearKitchenObject();

        _kitchenObjectParent = kitchenOBjectParent;

        if(kitchenOBjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject!");
        }

        kitchenOBjectParent.SetKitchenObject(this);

        transform.parent = kitchenOBjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() => _kitchenObjectParent;
}
