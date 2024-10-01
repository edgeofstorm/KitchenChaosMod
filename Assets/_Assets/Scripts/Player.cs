using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Timeline;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    // private static Player instance;
    // public static Player Instance
    // {
    //     get
    //     {
    //         return instance;
    //     }
    //     set
    //     {
    //         instance = value;
    //     }
    // }

    // singleton
    public static Player Instance { get; private set; }


    [SerializeField] private float INTERACTION_DISTANCE = 2f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float runMultiplier = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    // [SerializeField] private KitchenObject kitchenObject;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
        public bool hasKitchenObject;
    }


    private bool isWalking = false;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("there is more than one player!");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // even if the player stops moving use the last interaction point
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        // if last interaction direction hits sth layered with counters
        // if the object thats hit is clearCounter type
        // then set the this.clearCounter to clear counter thats hit by raycast
        // only if there is a new counter(if it's previous counter dont update)
        // other cases set this.clearCounter to null
        // finally publish an event

        if (Physics.Raycast(origin: transform.position, direction: lastInteractDir, out RaycastHit raycastHit, maxDistance: INTERACTION_DISTANCE, layerMask: counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                SetSelectedCounter(baseCounter);
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
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(point1: transform.position, point2: transform.position + Vector3.up * playerHeight, radius: playerRadius, direction: moveDirX, maxDistance: moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // attempt z axis movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(point1: transform.position, point2: transform.position + Vector3.up * playerHeight, radius: playerRadius, direction: moveDirZ, maxDistance: moveDistance);
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

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        if (this.selectedCounter != selectedCounter)
        {
            this.selectedCounter = selectedCounter;


            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                selectedCounter = selectedCounter,
                hasKitchenObject = this.HasKitchenObject()
            });
        }
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        //item grab animation
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return this.kitchenObject;
    }

    public void ClearKitchenObject()
    {
        // animation + logging
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return this.kitchenObject != null;
    }
}
