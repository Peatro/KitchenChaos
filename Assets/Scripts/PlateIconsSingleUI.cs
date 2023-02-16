using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetKitchenObjectSO(KitchenObjectScriptableObject kitchenObjectScriptableObject)
    {
        _image.sprite = kitchenObjectScriptableObject.sprite;
    }
}
