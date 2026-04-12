using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPanelController : MonoBehaviour
{
    public static SeedPanelController Instance;

    [SerializeField]
    private GameObject seedGO;
    [SerializeField]
    private Transform seedLayoutPanel;
    [SerializeField]
    private GameManager gameManager;

    private List<GameObject> seedGOList = new List<GameObject>();
    private GameObject selectedSeedGO;
    private Seed selectedSeed;

    public Seed SelectedSeed
    {
        get
        {
            return selectedSeed;
        }
        set
        {
            selectedSeed = value;
        }
    }

    public GameObject SelectedSeedGO
    {
        get
        {
            return selectedSeedGO;
        }
        set
        {
            selectedSeedGO = value;
        }
    }

    private const int HAND_SIZE = 5;
    private const float SEED_MIN_X = -35;
    private const float SEED_MAX_X = 35;
    private const float SEED_MIN_Y = -90;
    private const float SEED_MAX_Y = 90;

    private void Awake()
    {
        Instance = this;
    }

    public void DestroyAll()
    {
        for (int i = seedGOList.Count - 1; i >= 0; i--)
        {
            Destroy(seedGOList[i]);
            seedGOList.RemoveAt(i);
        }
    }

    public void Populate()
    {
        Inventory mainInventory = InventoryController.Instance.MainInventory;

        List<Seed> seedsToPopulate = new List<Seed>();

        for (int i = 0; i < HAND_SIZE; i++)
        {
            Seed seedTemp = mainInventory.DrawSeed(HAND_SIZE);

            if (seedTemp != null)
            {
                seedsToPopulate.Add(seedTemp);
            }
        }

        foreach (Seed seed in seedsToPopulate)
        {
            GameObject seedGOTemp = Instantiate(seedGO, seedLayoutPanel);

            seedGOTemp.transform.localPosition = GetRandomSeedPosition();

            SeedController seedController = seedGOTemp.GetComponent<SeedController>();

            seedController.Instantiate(seed, this, gameManager);

            seedGOList.Add(seedGOTemp);
        }
    }

    public void UnselectAllSeeds()
    {
        foreach (GameObject seedGOTemp in seedGOList)
        {
            SeedController seedController = seedGOTemp.GetComponent<SeedController>();

            seedController.Selected = false;
        }
    }

    public Vector2 GetRandomSeedPosition()
    {
        return new Vector2(Random.Range(SEED_MIN_X, SEED_MAX_X), Random.Range(SEED_MIN_Y, SEED_MAX_Y));
    }

    public void DeleteSelectedSeed()
    {
        seedGOList.Remove(SelectedSeedGO);
        Destroy(SelectedSeedGO);

        SelectedSeed = null;
        SelectedSeedGO = null;
    }
}
