using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Image _barImage;

    private void Start()
    {
        _cuttingCounter.OnProgressChanged += _cuttingCounter_OnProgressChanged;

        _barImage.fillAmount = 0;

        Hide();
    }

    private void _cuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArg e)
    {
        _barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0.0f || e.progressNormalized == 1.0f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
