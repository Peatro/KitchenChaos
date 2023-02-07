using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private KitchenObjectScriptableObject _kitchenObjectSriptableObject;

    private KitchenObject _kitchenObject;

    public void Interact(Player player)
    {
        if (_kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectSriptableObject.prefab, _counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            _kitchenObject.SetKitchenObjectParent(player);
        }        
    }

    public Transform GetKitchenObjectFollowTransform() => _counterTopPoint;

    public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;

    public KitchenObject GetKitchenObject() => _kitchenObject;

    public void ClearKitchenObject() => _kitchenObject = null;

    public bool HasKithcenObject() => _kitchenObject != null;
}
