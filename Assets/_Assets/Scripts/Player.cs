using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Timeline;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float runMultiplier = 1f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerSize = 0.7f;
        bool canMove = !Physics.Raycast(origin: transform.position, direction: moveDir, maxDistance: playerSize);

        isWalking = moveDir != new Vector3(0, 0, 0);

        moveDir = moveDir * Time.deltaTime * moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveDir *= runMultiplier;
        }

        if (canMove)
        {
            transform.position += moveDir;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveDir.y = 3;
        }


        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        // Debug.Log(inputVector);
        // Debug.Log(Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
