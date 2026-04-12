using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    private Inventory main;

    public Inventory MainInventory
    {
        get
        {
            return main;
        }
        set
        {
            main = value;
        }
    }

    public void Awake()
    {
        Instance = this;

        main = new Inventory();
    }

    public void AddSeed(Seed seed)
    {
        if (main == null)
        {
            Debug.LogError("no inventory");
        }

        main.AddSeed(seed);
    }

    public void DiscardSeed(Seed seed)
    {
        if (main == null)
        {
            Debug.LogError("no inventory");
        }

        main.DiscardSeed(seed);
    }

    public void ResetInventory()
    {
        main.MoveAllToDeck();
    }
}
