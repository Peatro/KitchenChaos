using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    #region singleton
    private static Player instance;
    public static Player Instance { get; set; }
    #endregion

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float _playerMoveSpeed = 7.0f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _counterLayerMask;
    [SerializeField] private Transform _kitchenObjectHoldPoint;

    private bool _isWalking = false;
    private Vector3 _lastInteractDirection;
    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one Player Instance!");
        }
        Instance = this;
    }

    private void Start()
    {
        _gameInput.OnInteractAction += _gameInput_OnInteractAction;
        _gameInput.OnInteractAlternateAction += _gameInput_OnInteractAlternateAction;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    private void _gameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        _selectedCounter?.Interact(this);
    }
    private void _gameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        _selectedCounter?.InteractAlternate(this);
    }

    public bool IsWalking() => _isWalking;

    private void HandleInteractions()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            _lastInteractDirection = moveDirection;
        }

        float interactDistance = 2.0f;
        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, interactDistance, _counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != _selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0.0f, inputVector.y);

        float moveDistance = _playerMoveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2.0f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0.0f, 0.0f).normalized;
            canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0.0f, 0.0f, moveDirection.z).normalized;
                canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }
        _isWalking = moveDirection != Vector3.zero;

        float rotationSpeed = 10.0f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this._selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = _selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform() => _kitchenObjectHoldPoint;
    public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;
    public KitchenObject GetKitchenObject() => _kitchenObject;
    public void ClearKitchenObject() => _kitchenObject = null;
    public bool HasKitchenObject() => _kitchenObject != null;
}
