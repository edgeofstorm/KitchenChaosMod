using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KithcenObjectSO kithcenObjectSO;

    public KithcenObjectSO GetKithcenObjectSO()
    {
        return kithcenObjectSO;
    }
}
