using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter _cuttingCounter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _cuttingCounter.OnCut += _cuttingCounter_OnCut;
    }

    private void _cuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(CUT);
    }
}
