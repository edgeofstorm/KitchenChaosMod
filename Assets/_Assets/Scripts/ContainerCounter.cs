using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KithcenObjectSO kithcenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && !player.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kithcenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}