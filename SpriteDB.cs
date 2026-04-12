using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDB : MonoBehaviour
{
    public static SpriteDB Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    public List<Sprite> seedSprites;

    [SerializeField]
    public List<Sprite> plantSprites;
}
