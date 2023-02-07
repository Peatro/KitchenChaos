using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 7.0f;

    private void Update()
    {  
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDirecrion = new Vector3(inputVector.x, 0.0f, inputVector.y);
        transform.position += moveDirecrion * _playerSpeed * Time.deltaTime;

        float rotationSpeed = 10.0f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirecrion, Time.deltaTime * rotationSpeed);
    }
}
