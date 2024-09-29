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

    private bool isWalking = false;

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
            // isWalking = true;
        }
        // else
        // {
        //     isWalking = false;
        // }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;

        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;

        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;

        }



        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveDir != new Vector3(0, 0, 0);
        moveDir = moveDir * Time.deltaTime * moveSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveDir.y = 3;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveDir *= runMultiplier;

        }
        transform.position += moveDir;

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
