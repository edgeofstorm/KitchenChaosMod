using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{

    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private GameObject[] visualGameObjectArray;
    [SerializeField] private List<KitchenObjectSO_GameObject> KitchenObjectSOGameObjectList;



    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        plateKitchenObject.OnPlateCleared += PlateKitchenObject_PlateCleared;
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in KitchenObjectSOGameObjectList)
        {
            if (e.kitchenObjectSO == kitchenObjectSOGameObject.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);

            }
        }
    }

    private void PlateKitchenObject_PlateCleared(object sender, EventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in KitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }
}
