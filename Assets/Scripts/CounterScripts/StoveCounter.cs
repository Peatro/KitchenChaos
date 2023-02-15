using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress 
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArg> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeScriptableObject[] _fryingRecipeScriptableObjectsArray;
    [SerializeField] private BurningRecipeScriptableObject[] _burningRecipeScriptableObjectsArray;

    private State _state;
    private float _fryingTimer;
    private float _burningTimer;
    private FryingRecipeScriptableObject _fryingRecipeScriptableObject;
    private BurningRecipeScriptableObject _burningRecipeScriptableObject;

    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {    
        if(HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    _fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArg
                    {
                        progressNormalized = _fryingTimer / _fryingRecipeScriptableObject.fryingTimeMax
                    });

                    if (_fryingTimer > _fryingRecipeScriptableObject.fryingTimeMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(_fryingRecipeScriptableObject.output, this);
                                                
                        _state = State.Fried;
                        _burningTimer = 0.0f;
                        _burningRecipeScriptableObject = GetBurningRecipeWithInput(GetKitchenObject().GetKitchenOjectScriptableObject());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = _state
                        });
                        
                    }
                    break;
                case State.Fried:
                    _burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArg
                    {
                        progressNormalized = _burningTimer / _burningRecipeScriptableObject.burningTimeMax
                    });
                     
                    if (_burningTimer > _burningRecipeScriptableObject.burningTimeMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(_burningRecipeScriptableObject.output, this);

                        _state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = _state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArg
                        {
                            progressNormalized = 0.0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }

        Debug.Log(_state);
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenOjectScriptableObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    _fryingRecipeScriptableObject = GetFryingRecipeWithInput(GetKitchenObject().GetKitchenOjectScriptableObject());

                    _state = State.Frying;
                    _fryingTimer = 0.0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = _state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArg
                    {
                        progressNormalized = _fryingTimer / _fryingRecipeScriptableObject.fryingTimeMax
                    });
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                _state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = _state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArg
                {
                    progressNormalized = 0.0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        FryingRecipeScriptableObject fryingRecipeScriptableObject = GetFryingRecipeWithInput(inputKitchenObjectSO);
        return fryingRecipeScriptableObject != null;
    }

    private KitchenObjectScriptableObject GetOutputForInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        FryingRecipeScriptableObject fryingRecipeScriptableObject = GetFryingRecipeWithInput(inputKitchenObjectSO);
        if (fryingRecipeScriptableObject != null)
        {
            return fryingRecipeScriptableObject.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeScriptableObject GetFryingRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        foreach (FryingRecipeScriptableObject fryingRecipeScriptableObject in _fryingRecipeScriptableObjectsArray)
        {
            if (fryingRecipeScriptableObject.input == inputKitchenObjectSO)
            {
                return fryingRecipeScriptableObject;
            }
        }
        return null;
    }

    private BurningRecipeScriptableObject GetBurningRecipeWithInput(KitchenObjectScriptableObject inputKitchenObjectSO)
    {
        foreach (BurningRecipeScriptableObject burningRecipeScriptableObject in _burningRecipeScriptableObjectsArray)
        {
            if (burningRecipeScriptableObject.input == inputKitchenObjectSO)
            {
                return burningRecipeScriptableObject;
            }
        }
        return null;
    }
}
