using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KithcenObjectSO kithcenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    public void interact()
    {
        Transform kitchenObjectTransform = Instantiate(kithcenObjectSO.prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
    }
}
