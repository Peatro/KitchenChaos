using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectScriptableObject _plateScriptableObject;

    [SerializeField] private float _spawnPlateTimerMax = 4.0f;
    private float _spawnPlateTimer;

    [SerializeField] private int _platesSpawnedAmountMax = 4;
    private int _platesSpawnedAmount;
    

    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;
        if(_spawnPlateTimer > _spawnPlateTimerMax)
        {
            _spawnPlateTimer = 0.0f;

            if(_platesSpawnedAmount < _platesSpawnedAmountMax)
            {
                _platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, new EventArgs());
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(_platesSpawnedAmount > 0)
            {
                _platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(_plateScriptableObject, player);

                OnPlateRemoved?.Invoke(this, new EventArgs());
            }
        }
    }
}
