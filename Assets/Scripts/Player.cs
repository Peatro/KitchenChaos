using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 7.0f;
    [SerializeField] private GameInput _gameInput;

    private bool _isWalking = false;

    private void Update()
    {  
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDirecrion = new Vector3(inputVector.x, 0.0f, inputVector.y);
        transform.position += moveDirecrion * _playerSpeed * Time.deltaTime;

        _isWalking = moveDirecrion != Vector3.zero;

        float rotationSpeed = 10.0f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirecrion, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
}
