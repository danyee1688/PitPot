using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeTransition : MonoBehaviour
{
    [Header("Object")]
    [SerializeField]
    private GameObject targetObject;

    [Header("Size")]
    [SerializeField]
    private Vector3 _startingSize;
    [SerializeField]
    private Vector3 _endingSize;

    [Header("Booleans")]
    [SerializeField]
    private bool startOnEnable;
    [SerializeField]
    private bool snapToStart;
    [SerializeField]
    private bool snapToTarget;

    [Header("Transition Speed")]
    [SerializeField]
    private float transitionSpeed = 5;

    [Header("Misc")]
    public bool transitionDone;

    public enum TransitionDirection
    {
        startToEnd,
        endToStart
    }

    [Header("TransitionDirection")]
    public TransitionDirection direction = TransitionDirection.startToEnd;

    public Vector3 StartingSize
    {
        get
        {
            return _startingSize;
        }
        set
        {
            _startingSize = value;
        }
    }

    public Vector3 EndingSize
    {
        get
        {
            return _endingSize;
        }
        set
        {
            _endingSize = value;
        }
    }

    public void OnEnable()
    {
        if (startOnEnable)
        {
            StartTransition(direction);
        }
    }

    public void SetTransitionSpeed(float transitionSpeed)
    {
        this.transitionSpeed = transitionSpeed;
    }

    public void StartTransition(TransitionDirection direction)
    {
        StopAllCoroutines();

        if (snapToStart)
        {
            targetObject.transform.localScale = StartingSize;
        }
        else if (snapToTarget)
        {
            targetObject.transform.localScale = EndingSize;
        }

        StartCoroutine(SmoothModifySize(StartingSize, EndingSize, direction, 0, transitionSpeed));
    }

    public void StopTransition()
    {
        StopAllCoroutines();
    }

    private IEnumerator SmoothModifySize(Vector3 startingSize, Vector3 targetSize, TransitionDirection direction, float time, float timeFactor)
    {
        transitionDone = false;

        yield return new WaitForSeconds(Time.deltaTime);

        if (direction == TransitionDirection.startToEnd)
        {
            targetObject.transform.localScale = Vector3.Lerp(startingSize, targetSize, time);

            if (time < 1)
            {
                StartCoroutine(SmoothModifySize(startingSize, targetSize, direction, time + (Time.deltaTime * timeFactor), timeFactor));
            }
            else
            {
                transitionDone = true;
            }
        }
        else
        {
            targetObject.transform.localScale = Vector3.Lerp(targetSize, startingSize, time);

            if (time < 1)
            {
                StartCoroutine(SmoothModifySize(startingSize, targetSize, direction, time + (Time.deltaTime * timeFactor), timeFactor));
            }
            else
            {
                transitionDone = true;
            }
        }
    }
}
