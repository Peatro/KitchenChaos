using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArg> OnProgressChanged;
    public class OnProgressChangedEventArg : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeScriptableObject[] _cuttingRecipeScriptsArray;

    private int _cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenOjectScriptableObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    _cuttingProgress = 0;

                    CuttingRecipeScriptableObject cuttingRecipeSO = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenOjectScriptableObject());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArg
                    {
                        progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }                
            }
        }
        else
        {
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
            _cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeScriptableObject cuttingRecipeSO = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenOjectScriptableObject());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArg
            {
                progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (_cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectScriptableObject outputKitchenSO = GetOutputForInput(GetKitchenObject().GetKitchenOjectScriptableObject());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenSO, this);
            }            
        }
    }

    private bool HasRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        CuttingRecipeScriptableObject cuttingRecipeSO = GetCuttingRecipeWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectScriptableObject GetOutputForInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        CuttingRecipeScriptableObject cuttingRecipeSO = GetCuttingRecipeWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeScriptableObject GetCuttingRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        foreach (CuttingRecipeScriptableObject cuttingRecipeScriptableObject in _cuttingRecipeScriptsArray)
        {
            if (cuttingRecipeScriptableObject.input == inputKitchenObjectSO)
            {
                return cuttingRecipeScriptableObject;
            }
        }
        return null;
    }
}
