using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectPulse : MonoBehaviour
{
    [SerializeField]
    private bool startOnEnable;
    [SerializeField]
    private float pulseSpeed;

    private bool startToEnd = true;
    private ColorTransition colorTransition;
    private Coroutine _mainCoroutine;

    public Coroutine MainCoroutine
    {
        get
        {
            return _mainCoroutine;
        }
    }

    private void Awake()
    {
        colorTransition = GetComponent<ColorTransition>();
    }

    private void Start()
    {
        colorTransition?.SetTransitionSpeed(pulseSpeed);
    }

    public void OnEnable()
    {
        if (startOnEnable)
        {
            StartPulse();
        }
    }

    public void StartPulse()
    {
        if (_mainCoroutine != null)
        {
            StopCoroutine(_mainCoroutine);
        }

        _mainCoroutine = StartCoroutine(PulseCoroutine());
    }

    public void StopPulse()
    {
        if (_mainCoroutine != null)
        {
            StopCoroutine(_mainCoroutine);
        }
    }

    public IEnumerator PulseCoroutine()
    {
        if (startToEnd)
        {
            colorTransition.StartTransition(ColorTransition.TransitionDirection.startToEnd);
            startToEnd = false;
        }
        else
        {
            colorTransition.StartTransition(ColorTransition.TransitionDirection.endToStart);
            startToEnd = true;
        }

        yield return new WaitUntil(() => colorTransition.transitionDone);

        _mainCoroutine = StartCoroutine(PulseCoroutine());
    }
}
