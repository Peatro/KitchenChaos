using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _hasProgressGameObject;
    [SerializeField] private Image _barImage;

    private IHasProgress _hasProgress;

    private void Start()
    {
        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
        if (_hasProgress == null )
        {
            Debug.LogError($"Game Object {_hasProgressGameObject} does not have a component that implements IHasProgress!" );
        }

        _hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        _barImage.fillAmount = 0;

        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArg e)
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
