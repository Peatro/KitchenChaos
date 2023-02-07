using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private KitchenObjectScriptableObject _kitchenObjectSriptableObject;

    public void Interact()
    {
        Debug.Log("Interact!");
        Transform kitchenObjectTransform =  Instantiate(_kitchenObjectSriptableObject.prefab, _counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
    }
}
