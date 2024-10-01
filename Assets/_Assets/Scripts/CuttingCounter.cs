using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    // Start is called before the first frame update
    public override void Interact(Player player)
    {
        // if (!player.HasKitchenObject() && this.HasKitchenObject())
        // {
        //     // if kitchen object is slicable
        //     // slice
        //     // this.SetKitchenObject(sliced);
        //     // else
        //     // this.GetKitchenObject().SetKitchenObjectParent(player);
        // }
        // if (player.HasKitchenObject())
        // {
        //     player.GetKitchenObject().SetKitchenObjectParent(this);
        // }

        if (!this.HasKitchenObject())
        {
            // no kitchen obj on this cutting counter
            if (player.HasKitchenObject())
            {
                // player is carrying sth
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player not carrying anything
            }
        }
        else
        {
            //there is a kitchen object on this cutting counter
            if (player.HasKitchenObject())
            {
                // player is carrying sth
            }
            else
            {
                // plaer is not carryinh anything
                if (this.GetKitchenObject().IsSliced())
                {
                    this.GetKitchenObject().SetKitchenObjectParent(player);
                }
                else
                {
                    this.GetKitchenObject().DestroySelf();
                    // Debug.Log(this.GetKitchenObject().GetComponent<KitchenObject>());
                    // Transform kitchenObjectTransform = Instantiate(kithcenObjectSO.prefab);
                    // kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);


                    // this.GetKitchenObject().SliceKitchenObject();
                    // Debug.Log("sliced");
                }
            }
        }

    }
}
