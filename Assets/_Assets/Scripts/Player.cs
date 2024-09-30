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

        float playerRadius = 0.7f;
        float playerHeight = 2f;
        float moveDistance = Time.deltaTime * moveSpeed;
        //point1 : top of the capsule
        //point2 : bottom of the capsule
        //radius : radius of the capsule
        bool canMove = !Physics.CapsuleCast(point1: transform.position, point2: transform.position + Vector3.up * playerHeight, radius: playerRadius, direction: moveDir, maxDistance: moveDistance);

        isWalking = moveDir != new Vector3(0, 0, 0);

        moveDir = moveDir * moveDistance;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveDir *= runMultiplier;
        }


        if (!canMove)
        {
            // attempt x axis movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(point1: transform.position, point2: transform.position + Vector3.up * playerHeight, radius: playerRadius, direction: moveDirX, maxDistance: moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // attempt z axis movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(point1: transform.position, point2: transform.position + Vector3.up * playerHeight, radius: playerRadius, direction: moveDirZ, maxDistance: moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;

                }
            }

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
