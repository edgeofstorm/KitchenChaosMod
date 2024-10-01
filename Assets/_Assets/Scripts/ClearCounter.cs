using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KithcenObjectSO kithcenObjectSO;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() && !this.HasKitchenObject())
        {
            // KitchenObject kitchenObject = player.GetKitchenObject();
            // player.ClearKitchenObject();
            // this.SetKitchenObject(kitchenObject);
            // Debug.Log(this.GetKitchenObject());

            // Transform kitchenObjectTransform = Instantiate(kithcenObjectSO.prefab);
            // kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

            player.GetKitchenObject().SetKitchenObjectParent(this);
        }
        else
        {
            if (this.HasKitchenObject() && !player.HasKitchenObject())
            {
                // KitchenObject kitchenObject = this.GetKitchenObject();
                // this.ClearKitchenObject();
                // player.SetKitchenObject(kitchenObject);
                // Debug.Log(player.GetKitchenObject());

                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }
}
