using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KithcenObjectSO kithcenObjectSO;

    // public ClearCounter ClearCounter { get; set; }

    private IKitchenObjectParent kitchenObjectParent;

    private bool isSliced = false;

    public KithcenObjectSO GetKithcenObjectSO()
    {
        return kithcenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        if (this.kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("kitchenObjectParent already has a Kitchen Object");
        }
        this.kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetClearKitchenObjectParent()
    {
        return this.kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }

    public void SliceKitchenObject()
    {
        isSliced = true;
    }

    public bool IsSliced()
    {
        return isSliced;
    }


}
