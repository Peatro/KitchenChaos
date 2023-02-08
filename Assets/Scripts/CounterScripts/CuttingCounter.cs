using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeScriptableObject[] _cuttingRecipeScriptsArray;
    [SerializeField] private KitchenObjectScriptableObject _cutKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenOjectScriptableObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }                
            }
        }
        else
        {
            //Объект на столе
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenOjectScriptableObject()))
        {
            KitchenObjectScriptableObject outputKitchenSO = GetOutputForInput(GetKitchenObject().GetKitchenOjectScriptableObject());
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        foreach (CuttingRecipeScriptableObject cuttingRecipeScriptableObject in _cuttingRecipeScriptsArray)
        {
            if (cuttingRecipeScriptableObject.input == inputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    private KitchenObjectScriptableObject GetOutputForInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        foreach(CuttingRecipeScriptableObject cuttingRecipeScriptableObject in _cuttingRecipeScriptsArray)
        {
            if(cuttingRecipeScriptableObject.input == inputKitchenObjectSO)
            {
                return cuttingRecipeScriptableObject.output;
            }
        }
        return null;
    }
}
