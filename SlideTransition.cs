using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTransition : MonoBehaviour
{
    [Header("Positioning")]
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private Vector3 endPosition;

    [Header("Transition Speed")]
    [SerializeField]
    private float transitionSpeed = 5;

    [Header("Enables/Disables")]
    [SerializeField]
    private bool startOnEnable;
    [SerializeField]
    private bool snapToStartingPosition;

    [Header("Keep Axis Positions")]
    [SerializeField]
    private bool retainYCoordinate;
    [SerializeField]
    private bool retainXCoordinate;
    [SerializeField]
    private bool retainZCoordinate;

    public enum TransitionDirection
    {
        startToEnd,
        endToStart
    }

    public enum TransitionBehavior
    {
        normal,
        slowToFast,
        fastToSlow
    }

    [Header("Slide Direction")]
    public TransitionDirection direction = TransitionDirection.startToEnd;

    [Header("Slide Behavior")]
    public TransitionBehavior behavior = TransitionBehavior.normal;

    [HideInInspector]
    public bool transitionDone = true;

    private RectTransform rect;


    public Vector3 StartPosition
    {
        get
        {
            return startPosition;
        }
        set
        {
            startPosition = value;
        }
    }

    public Vector3 EndPosition
    {
        get
        {
            return endPosition;
        }
        set
        {
            endPosition = value;
        }
    }

    private void Awake()
    {
        rect = gameObject.GetComponent<RectTransform>();
    }

    public void OnEnable()
    {
        if (startOnEnable)
        {
            StartTransition();
        }
    }

    public void SetSnapToStart(bool snapToStart)
    {
        this.snapToStartingPosition = snapToStart;
    }

    public void SetBehavior(TransitionBehavior behavior)
    {
        this.behavior = behavior;
    }

    public void SetTransitionSpeed(float transitionSpeed)
    {
        this.transitionSpeed = transitionSpeed;
    }

    public void SetToStart()
    {
        if (rect != null)
        {
            rect.anchoredPosition = startPosition;
        }
    }

    public void StartTransition()
    {
        // Avoid repeated transitions
        StopAllCoroutines();

        Vector3 targetPos;
        Vector3 startPos;

        // Determine target position based of boolean
        if (direction == TransitionDirection.startToEnd)
        {
            targetPos = endPosition;
            startPos = startPosition;
        }
        else
        {
            targetPos = startPosition;
            startPos = endPosition;
        }

        // Determine whether or not targetPos retains an axis
        if (retainXCoordinate)
        {
            targetPos = new Vector3(rect.anchoredPosition.x, targetPos.y, targetPos.z);
        }
        if (retainYCoordinate)
        {
            targetPos = new Vector3(targetPos.x, rect.anchoredPosition.y, targetPos.z);
        }
        if (retainZCoordinate)
        {
            targetPos = new Vector3(targetPos.x, targetPos.y, rect.anchoredPosition3D.z);
        }

        // Snap to starting position
        if (snapToStartingPosition)
        {
            if (direction == TransitionDirection.startToEnd)
            {
                if (retainYCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(startPosition.x, rect.anchoredPosition.y, startPosition.z);
                }
                else if (retainXCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(rect.anchoredPosition.x, startPosition.y, startPosition.z);
                }
                else if (retainZCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(startPosition.x, startPosition.y, rect.anchoredPosition3D.z);
                }
                else
                {
                    rect.anchoredPosition = startPosition;
                }
            }
            else
            {
                if (retainYCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(endPosition.x, rect.anchoredPosition.y, endPosition.z);
                }
                else if (retainXCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(rect.anchoredPosition.x, endPosition.y, endPosition.z);
                }
                else if (retainZCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(endPosition.x, endPosition.y, rect.anchoredPosition3D.z);
                }
                else
                {
                    rect.anchoredPosition3D = endPosition;
                }
            }
        }

        if (behavior == TransitionBehavior.normal)
        {
            StartCoroutine(SmoothMove(0, 0, transitionSpeed, targetPos));
        }
        else if (behavior == TransitionBehavior.fastToSlow)
        {
            StartCoroutine(SmoothMoveFastToSlow(0, 0, transitionSpeed, startPos, targetPos));
        }
        else if (behavior == TransitionBehavior.slowToFast)
        {
            StartCoroutine(SmoothMoveSlowToFast(0, 0, transitionSpeed, startPos, targetPos));
        }
    }

    public void StartTransition(TransitionDirection transitionDirection)
    {
        // Avoid repeated transitions
        StopAllCoroutines();

        Vector3 targetPos;
        Vector3 startPos;

        direction = transitionDirection;

        // Determine target position based of boolean
        if (direction == TransitionDirection.startToEnd)
        {
            targetPos = endPosition;
            startPos = startPosition;
        }
        else
        {
            targetPos = startPosition;
            startPos = endPosition;
        }

        // Determine whether or not targetPos retains an axis
        if (retainXCoordinate)
        {
            targetPos = new Vector3(rect.anchoredPosition.x, targetPos.y, targetPos.z);
        }
        if (retainYCoordinate)
        {
            targetPos = new Vector3(targetPos.x, rect.anchoredPosition.y, targetPos.z);
        }
        if (retainZCoordinate)
        {
            targetPos = new Vector3(targetPos.x, targetPos.y, rect.anchoredPosition3D.z);
        }

        // Snap to starting position
        if (snapToStartingPosition)
        {
            if (direction == TransitionDirection.startToEnd)
            {
                if (retainYCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(startPosition.x, rect.anchoredPosition.y, startPosition.z);
                }
                else if (retainXCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(rect.anchoredPosition.x, startPosition.y, startPosition.z);
                }
                else if (retainZCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(startPosition.x, startPosition.y, rect.anchoredPosition3D.z);
                }
                else
                {
                    rect.anchoredPosition = startPosition;
                }
            }
            else
            {
                if (retainYCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(endPosition.x, rect.anchoredPosition.y, endPosition.z);
                }
                else if (retainXCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(rect.anchoredPosition.x, endPosition.y, endPosition.z);
                }
                else if (retainZCoordinate)
                {
                    rect.anchoredPosition3D = new Vector3(endPosition.x, endPosition.y, rect.anchoredPosition3D.z);
                }
                else
                {
                    rect.anchoredPosition3D = endPosition;
                }
            }
        }

        if (behavior == TransitionBehavior.normal)
        {
            StartCoroutine(SmoothMove(0, 0, transitionSpeed, targetPos));
        }
        else if (behavior == TransitionBehavior.fastToSlow)
        {
            StartCoroutine(SmoothMoveFastToSlow(0, 0, transitionSpeed, startPos, targetPos));
        }
        else if (behavior == TransitionBehavior.slowToFast)
        {
            StartCoroutine(SmoothMoveSlowToFast(0, 0, transitionSpeed, startPos, targetPos));
        }
    }

    private IEnumerator SmoothMove(float time, float timeDecrement, float timeFactor, Vector3 targetPos)
    {
        transitionDone = false;

        // Wait for frame
        yield return new WaitForSeconds(Time.deltaTime);

        // Lerp towards target position by time
        rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, targetPos, time);

        // If lerp has not completed, continue lerp
        // Otherwise, set transitionDone to true
        if (time < 1)
        {
            // Float to modify time by
            float timeModifier;

            // Time decrement to decrease the time modifier with each loop
            timeDecrement++;

            // Set time modifier to account for time factor and time decrement
            timeModifier = ((Time.deltaTime * timeFactor) / timeDecrement);

            // Set a lower limit for time modifier, so that the time variable still increments enough for visible movement
            if (timeModifier < Time.deltaTime / 2)
            {
                timeModifier = Time.deltaTime / 2;
            }

            // Recursive call
            StartCoroutine(SmoothMove(time + timeModifier, timeDecrement, timeFactor, targetPos));
        }
        else
        {
            rect.anchoredPosition = targetPos;

            transitionDone = true;
        }
    }

    private IEnumerator SmoothMoveFastToSlow(float time, float counter, float timeFactor, Vector3 startingPos, Vector3 targetPos)
    {
        transitionDone = false;

        // Wait for frame
        yield return new WaitForSeconds(Time.deltaTime);

        // If lerp has not completed, continue lerp
        // Otherwise, set transitionDone to true
        if (time < 1)
        {
            counter += Time.deltaTime * timeFactor;

            // Lerp towards target position by time
            rect.anchoredPosition = Vector2.Lerp(startingPos, targetPos, time);

            StartCoroutine(SmoothMoveFastToSlow(Mathf.Log10(counter + 0.5f) + 0.3f, counter, timeFactor, startingPos, targetPos));

        }
        else
        {
            rect.anchoredPosition = targetPos;

            transitionDone = true;
        }
    }

    private IEnumerator SmoothMoveSlowToFast(float time, float counter, float timeFactor, Vector3 startingPos, Vector3 targetPos)
    {
        transitionDone = false;

        // Wait for frame
        yield return new WaitForSeconds(Time.deltaTime);

        // If lerp has not completed, continue lerp
        // Otherwise, set transitionDone to true
        if (time < 1)
        {
            counter += Time.deltaTime * timeFactor;

            // Lerp towards target position by time
            rect.anchoredPosition = Vector2.Lerp(startingPos, targetPos, time);

            StartCoroutine(SmoothMoveSlowToFast(0.5f * Mathf.Pow(3, (0.5f * counter) - 4), counter, timeFactor, startingPos, targetPos));
        }
        else
        {
            rect.anchoredPosition = targetPos;

            transitionDone = true;
        }
    }
}
