using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.U2D;
using UnityEngine.TerrainUtils;

public class ColorTransition : MonoBehaviour
{
    [Header("Target Objects")]
    [SerializeField]
    private List<TransitionObject> targetObjects = new List<TransitionObject>();

    [Header("Transition Speed")]
    [SerializeField]
    private float transitionSpeed = 5;

    [Header("Enables/Disables")]
    [SerializeField]
    private bool startOnEnable;
    [SerializeField]
    private bool destroyOnTransitionComplete;
    [SerializeField]
    private bool snapToStart;
    [SerializeField]
    private bool snapToEnd;

    public enum ObjectType
    {
        image,
        text,
        sprite,
        line
    }

    public enum TransitionDirection
    {
        startToEnd,
        endToStart
    }

    [Header("Transition Direction")]
    public TransitionDirection direction = TransitionDirection.startToEnd;

    [HideInInspector]
    public bool transitionDone;

    public void OnEnable()
    {
        if (startOnEnable)
        {
            StartTransition(direction);
        }
    }

    public void SetDestroyOnTransitionComplete(bool boolean)
    {
        destroyOnTransitionComplete = boolean;
    }

    public void SetToStart()
    {
        for (int i = targetObjects.Count - 1; i >= 0; i--)
        {
            if (targetObjects[i].type == ObjectType.sprite)
            {
                SpriteRenderer sprite = targetObjects[i].gameObject.GetComponent<SpriteRenderer>();

                sprite.color = targetObjects[i].startingColor;
            }
            else if (targetObjects[i].type == ObjectType.image)
            {
                Image image = targetObjects[i].gameObject.GetComponent<Image>();

                image.color = targetObjects[i].startingColor;
            }
            else if (targetObjects[i].type == ObjectType.text)
            {
                TextMeshProUGUI text = targetObjects[i].gameObject.GetComponent<TextMeshProUGUI>();

                text.color = targetObjects[i].startingColor;
            }
            else if (targetObjects[i].type == ObjectType.line)
            {
                LineRenderer lineRenderer = targetObjects[i].gameObject.GetComponent<LineRenderer>();

                lineRenderer.startColor = targetObjects[i].startingColor;
                lineRenderer.endColor = targetObjects[i].startingColor;
            }
        }
    }

    public void SetToEnd()
    {
        for (int i = targetObjects.Count - 1; i >= 0; i--)
        {
            if (targetObjects[i].type == ObjectType.sprite)
            {
                SpriteRenderer sprite = targetObjects[i].gameObject.GetComponent<SpriteRenderer>();

                sprite.color = targetObjects[i].endingColor;
            }
            else if (targetObjects[i].type == ObjectType.image)
            {
                Image image = targetObjects[i].gameObject.GetComponent<Image>();

                image.color = targetObjects[i].endingColor;
            }
            else if (targetObjects[i].type == ObjectType.text)
            {
                TextMeshProUGUI text = targetObjects[i].gameObject.GetComponent<TextMeshProUGUI>();

                text.color = targetObjects[i].endingColor;
            }
            else if (targetObjects[i].type == ObjectType.line)
            {
                LineRenderer lineRenderer = targetObjects[i].gameObject.GetComponent<LineRenderer>();

                lineRenderer.startColor = targetObjects[i].endingColor;
                lineRenderer.endColor = targetObjects[i].endingColor;
            }
        }
    }

    public void SetSnapToEnd(bool boolean)
    {
        this.snapToEnd = boolean;
    }

    public void SetSnapToStart(bool boolean)
    {
        this.snapToStart = boolean;
    }

    public void SetStartingColor(Color color, int objectIndex)
    {
        targetObjects[objectIndex].startingColor = color;
    }

    public void SetEndingColor(Color color, int objectIndex)
    {
        targetObjects[objectIndex].endingColor = color;
    }

    public void SetTransitionSpeed(float transitionSpeed)
    {
        this.transitionSpeed = transitionSpeed;
    }

    public void SetTransitionDirection(TransitionDirection transitionDirection)
    {
        this.direction = transitionDirection;
    }

    public void StartTransition(TransitionDirection direction)
    {
        StopAllCoroutines();

        for (int i = targetObjects.Count - 1; i >= 0; i--)
        {
            if (targetObjects[i].type == ObjectType.sprite)
            {
                SpriteRenderer sprite = targetObjects[i].gameObject.GetComponent<SpriteRenderer>();

                if (snapToStart)
                {
                    sprite.color = targetObjects[i].startingColor;
                }

                if (snapToEnd)
                { 
                    sprite.color = targetObjects[i].endingColor;
                }

                if (direction == TransitionDirection.startToEnd)
                {
                    StartCoroutine(SmoothColorLerp(targetObjects[i], sprite.color, targetObjects[i].endingColor, 0, transitionSpeed));
                }
                else if (direction == TransitionDirection.endToStart)
                {
                    StartCoroutine(SmoothColorLerp(targetObjects[i], sprite.color, targetObjects[i].startingColor, 0, transitionSpeed));
                }
            }
            else if (targetObjects[i].type == ObjectType.image)
            {
                Image image = targetObjects[i].gameObject.GetComponent<Image>();

                if (snapToStart)
                {
                    image.color = targetObjects[i].startingColor;
                }

                if (snapToEnd)
                {
                    image.color = targetObjects[i].endingColor;
                }

                if (direction == TransitionDirection.startToEnd)
                {
                    StartCoroutine(SmoothColorLerp(targetObjects[i], image.color, targetObjects[i].endingColor, 0, transitionSpeed));
                }
                else if (direction == TransitionDirection.endToStart)
                {
                    StartCoroutine(SmoothColorLerp(targetObjects[i], image.color, targetObjects[i].startingColor, 0, transitionSpeed));
                }
            }
            else if (targetObjects[i].type == ObjectType.text)
            {
                TextMeshProUGUI text = targetObjects[i].gameObject.GetComponent<TextMeshProUGUI>();

                if (snapToStart)
                {
                    text.color = targetObjects[i].startingColor;
                }

                if (snapToEnd)
                {
                    text.color = targetObjects[i].endingColor;
                }

                if (direction == TransitionDirection.startToEnd)
                {
                    StartCoroutine(SmoothColorLerp(targetObjects[i], text.color, targetObjects[i].endingColor, 0, transitionSpeed));    
                }
                else if (direction == TransitionDirection.endToStart)
                {
                    StartCoroutine(SmoothColorLerp(targetObjects[i], text.color, targetObjects[i].startingColor, 0, transitionSpeed));
                }
            }
            else if (targetObjects[i].type == ObjectType.line)
            {
                LineRenderer lineRenderer = targetObjects[i].gameObject.GetComponent<LineRenderer>();

                if (snapToStart)
                {
                    lineRenderer.startColor = targetObjects[i].startingColor;
                    lineRenderer.endColor = targetObjects[i].startingColor;
                }

                if (snapToEnd)
                {
                    lineRenderer.startColor = targetObjects[i].endingColor;
                    lineRenderer.endColor = targetObjects[i].endingColor;
                }

                if (direction == TransitionDirection.startToEnd)
                {
                    StartCoroutine(SmoothColorLerp(targetObjects[i], lineRenderer.startColor, targetObjects[i].endingColor, 0, transitionSpeed));
                }
                else if (direction == TransitionDirection.endToStart)
                {
                    StartCoroutine(SmoothColorLerp(targetObjects[i], lineRenderer.startColor, targetObjects[i].startingColor, 0, transitionSpeed));
                }
            }
        }
    }

    public void StartTransition(TransitionDirection direction, int objectIndex)
    {
        StopAllCoroutines();

        if (targetObjects[objectIndex].type == ObjectType.sprite)
        {
            SpriteRenderer sprite = targetObjects[objectIndex].gameObject.GetComponent<SpriteRenderer>();

            if (snapToStart)
            {
                sprite.color = targetObjects[objectIndex].startingColor;
            }

            if (snapToEnd)
            {
                sprite.color = targetObjects[objectIndex].endingColor;
            }

            if (direction == TransitionDirection.startToEnd)
            {
                StartCoroutine(SmoothColorLerp(targetObjects[objectIndex], sprite.color, targetObjects[objectIndex].endingColor, 0, transitionSpeed));
            }
            else if (direction == TransitionDirection.endToStart)
            {
                StartCoroutine(SmoothColorLerp(targetObjects[objectIndex], sprite.color, targetObjects[objectIndex].startingColor, 0, transitionSpeed));
            }
        }
        else if (targetObjects[objectIndex].type == ObjectType.image)
        {
            Image image = targetObjects[objectIndex].gameObject.GetComponent<Image>();

            if (snapToStart)
            {
                image.color = targetObjects[objectIndex].startingColor;
            }

            if (snapToEnd)
            {
                image.color = targetObjects[objectIndex].endingColor;
            }

            if (direction == TransitionDirection.startToEnd)
            {
                StartCoroutine(SmoothColorLerp(targetObjects[objectIndex], image.color, targetObjects[objectIndex].endingColor, 0, transitionSpeed));
            }
            else if (direction == TransitionDirection.endToStart)
            {
                StartCoroutine(SmoothColorLerp(targetObjects[objectIndex], image.color, targetObjects[objectIndex].startingColor, 0, transitionSpeed));
            }
        }
        else if (targetObjects[objectIndex].type == ObjectType.text)
        {
            TextMeshProUGUI text = targetObjects[objectIndex].gameObject.GetComponent<TextMeshProUGUI>();

            if (snapToStart)
            {
                text.color = targetObjects[objectIndex].startingColor;
            }

            if (snapToEnd)
            {
                text.color = targetObjects[objectIndex].endingColor;
            }

            if (direction == TransitionDirection.startToEnd)
            {
                StartCoroutine(SmoothColorLerp(targetObjects[objectIndex], text.color, targetObjects[objectIndex].endingColor, 0, transitionSpeed));
            }
            else if (direction == TransitionDirection.endToStart)
            {
                StartCoroutine(SmoothColorLerp(targetObjects[objectIndex], text.color, targetObjects[objectIndex].startingColor, 0, transitionSpeed));
            }
        }
        else if (targetObjects[objectIndex].type == ObjectType.line)
        {
            LineRenderer lineRenderer = targetObjects[objectIndex].gameObject.GetComponent<LineRenderer>();

            if (snapToStart)
            {
                lineRenderer.startColor = targetObjects[objectIndex].startingColor;
                lineRenderer.endColor = targetObjects[objectIndex].startingColor;
            }

            if (snapToEnd)
            {
                lineRenderer.startColor = targetObjects[objectIndex].endingColor;
                lineRenderer.endColor = targetObjects[objectIndex].endingColor;
            }

            if (direction == TransitionDirection.startToEnd)
            {
                StartCoroutine(SmoothColorLerp(targetObjects[objectIndex], lineRenderer.startColor, targetObjects[objectIndex].endingColor, 0, transitionSpeed));
            }
            else if (direction == TransitionDirection.endToStart)
            {
                StartCoroutine(SmoothColorLerp(targetObjects[objectIndex], lineRenderer.startColor, targetObjects[objectIndex].startingColor, 0, transitionSpeed));
            }
        }
    }

    private IEnumerator SmoothColorLerp(TransitionObject targetObject, Color currentColor, Color targetColor, float time, float timeFactor)
    {
        transitionDone = false;

        yield return new WaitForSeconds(Time.deltaTime);

        if (targetObject.type == ObjectType.sprite)
        {
            SpriteRenderer sprite = targetObject.gameObject.GetComponent<SpriteRenderer>();

            sprite.color = Color.Lerp(currentColor, targetColor, time);
        }
        else if (targetObject.type == ObjectType.image)
        {
            Image image = targetObject.gameObject.GetComponent<Image>();

            image.color = Color.Lerp(currentColor, targetColor, time);
        }
        else if (targetObject.type == ObjectType.text)
        {
            TextMeshProUGUI text = targetObject.gameObject.GetComponent<TextMeshProUGUI>();

            text.color = Color.Lerp(currentColor, targetColor, time);
        }
        else if (targetObject.type == ObjectType.line)
        {
            LineRenderer lineRenderer = targetObject.gameObject.GetComponent<LineRenderer>();

            lineRenderer.startColor = Color.Lerp(currentColor, targetColor, time);
            lineRenderer.endColor = Color.Lerp(currentColor, targetColor, time);
        }

        if (time < 1)
        {
            StartCoroutine(SmoothColorLerp(targetObject, currentColor, targetColor, time + (Time.deltaTime * timeFactor), timeFactor));
        }
        else
        {
            transitionDone = true;

            if (destroyOnTransitionComplete)
            {
                Destroy(gameObject);
            }
        }
    }
}
