using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private UnityEngine.UI.Image barImage;


    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProggressChanged;
        barImage.fillAmount = 0;
    }


    private void CuttingCounter_OnProggressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
    }
}
