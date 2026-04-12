using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDB : MonoBehaviour
{
    public static ParticleSystemDB Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    public GameObject bloomPS;
    [SerializeField]
    public GameObject harvestPS;
}
