using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour, IKitchenObjectParent
{
    public PlayerInputHandler InputHandler {get; private set;}

    public static PlayerController Instance {get; private set;}

    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    public class OnSelectedCounterChangeEventArgs: EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    [Header("PLayer Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    private Vector3 playerDirection;
    private Vector2 moveDir;
    private bool isWalking;

    [Header("PLayer Interact Detetcion")]
    [SerializeField] private float maxDetectDistance =2f;
    [SerializeField] private LayerMask coutrerLayerMask;
    private Vector3 lastInteractDirection;
    private Vector3 collisionDetectDirection;
    private BaseCounter selectedCounter;
    
    private KitchenObject kitchenObject;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    

    private void Awake() {
        if(Instance != null)
        {
            Debug.LogError("here is more than one PlayerController");
        }
        Instance = this;
        InputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Start() {
        InputHandler.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update() {
        MovePlayer();
        HandleInteraction();
    }

    #region  PlayerMovement
    private void MovePlayer()
    {
        moveDir = new Vector2(InputHandler.NorInputX, InputHandler.NorInputY);
        moveDir = moveDir.normalized;

        playerDirection = new Vector3(moveDir.x, 0f, moveDir.y);

        transform.position += playerDirection * moveSpeed * Time.fixedDeltaTime;
        
        transform.forward = Vector3.Slerp(transform.forward, playerDirection, rotateSpeed * Time.deltaTime);
        
        isWalking = playerDirection != Vector3.zero;
    }
    #endregion


    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        selectedCounter?.Interact(this);
    }

    private void HandleInteraction()
    {
        collisionDetectDirection = new Vector3(InputHandler.NorInputX, 0f, InputHandler.NorInputY);

        if(collisionDetectDirection != Vector3.zero)
        {
            lastInteractDirection = collisionDetectDirection;
        }

        if(Physics.Raycast(transform.position, lastInteractDirection,out RaycastHit rayCatHit,maxDetectDistance, coutrerLayerMask))
        {
            if(rayCatHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }else
            {
                SetSelectedCounter(null);
            }
        }else
        {
            SetSelectedCounter(null);
        }

    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs{selectedCounter = selectedCounter});
    }

    public bool IsWalking 
    {
        get => isWalking;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitChenObject()
    {
        return kitchenObject != null;
    }
    
}
