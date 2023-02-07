using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectScriptableObject _kitchenObjectScriptableObject;

    public KitchenObjectScriptableObject GetKitchenOjectScriptableObject()
    {
        return _kitchenObjectScriptableObject;
    }
}
