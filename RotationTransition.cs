using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTransition : MonoBehaviour
{
    [Header("Object")]
    [SerializeField]
    private GameObject targetObject;

    [Header("Rotation")]
    [SerializeField]
    private float _startingRotation;
    [SerializeField]
    private float _endingRotation;

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

    public float StartingRotation
    {
        get
        {
            return _startingRotation;
        }
        set
        {
            _startingRotation = value;
        }
    }

    public float EndingRotation
    {
        get
        {
            return _endingRotation;
        }
        set
        {
            _endingRotation = value;
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

    public void StartTransition()
    {
        StopAllCoroutines();

        if (snapToStart)
        {
            targetObject.transform.rotation = Quaternion.Euler(0, 0, StartingRotation);
        }
        else if (snapToTarget)
        {
            targetObject.transform.rotation = Quaternion.Euler(0, 0, EndingRotation);
        }

        StartCoroutine(SmoothModifyRotation(StartingRotation, EndingRotation, TransitionDirection.startToEnd, 0, transitionSpeed));
    }

    public void StartTransition(TransitionDirection direction)
    {
        StopAllCoroutines();

        if (snapToStart)
        {
            targetObject.transform.rotation = Quaternion.Euler(0, 0, StartingRotation);
        }
        else if (snapToTarget)
        {
            targetObject.transform.rotation = Quaternion.Euler(0, 0, EndingRotation);
        }

        StartCoroutine(SmoothModifyRotation(StartingRotation, EndingRotation, direction, 0, transitionSpeed));
    }

    public void StopTransition()
    {
        StopAllCoroutines();
    }

    private IEnumerator SmoothModifyRotation(float startingRotation, float endingRotation, TransitionDirection direction, float time, float timeFactor)
    {
        transitionDone = false;

        yield return new WaitForSeconds(Time.deltaTime);

        if (direction == TransitionDirection.startToEnd)
        {
            targetObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, startingRotation), Quaternion.Euler(0, 0, endingRotation), time);

            if (time < 1)
            {
                StartCoroutine(SmoothModifyRotation(startingRotation, endingRotation, direction, time + (Time.deltaTime * timeFactor), timeFactor));
            }
            else
            {
                transitionDone = true;
            }
        }
        else
        {
            targetObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, endingRotation), Quaternion.Euler(0, 0, startingRotation), time);

            if (time < 1)
            {
                StartCoroutine(SmoothModifyRotation(startingRotation, endingRotation, direction, time + (Time.deltaTime * timeFactor), timeFactor));
            }
            else
            {
                transitionDone = true;
            }
        }
    }
}
