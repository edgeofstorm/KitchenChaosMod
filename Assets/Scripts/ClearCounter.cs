using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{


    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player not carrying anything
            }
        }
        else
        {
            // There is a KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // player is holding a plate
                    plateKitchenObject.AddIngredient(this.GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                }
                else
                {
                    // player is carrying sth other than plate
                    if (this.GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // counter is holding a plate
                        plateKitchenObject.AddIngredient(player.GetKitchenObject().GetKitchenObjectSO());
                        player.GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}