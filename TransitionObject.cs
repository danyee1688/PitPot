using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorTransition;

[System.Serializable]
public class TransitionObject
{
    public GameObject gameObject;
    public ObjectType type;
    public Color startingColor;
    public Color endingColor;

    public TransitionObject(GameObject gameObject, ObjectType type, Color startingColor, Color endingColor)
    {
        this.gameObject = gameObject;
        this.type = type;
        this.startingColor = startingColor;
        this.endingColor = endingColor;
    }
}
