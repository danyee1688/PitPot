using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketController : MonoBehaviour
{
    [SerializeField]
    private GameObject marketItem;
    [SerializeField]
    private Transform seedLayoutPanel;
    [SerializeField]
    private Transform fertilizerLayoutPanel;
    [SerializeField]
    private TextMeshProUGUI marketTokenText;
    [Header("Debug")]
    [SerializeField]
    private bool debugEnabled;

    public static MarketController Instance;

    private List<GameObject> marketItems = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void PopulateMarket()
    {
        // Normal behavior
        if (!debugEnabled)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject marketItemTemp = Instantiate(marketItem, seedLayoutPanel);
                MarketItemController marketItemController = marketItemTemp.GetComponent<MarketItemController>();

                marketItemController.Instantiate(SeedDB.GetRandomSeed());

                marketItems.Add(marketItemTemp);
            }
        }
        // Debug market population
        else
        {
            // Put options here for testing
            List<Seed> options = new List<Seed>()
            {
                SeedDB.seedList[2],
            };

            for (int i = 0; i < 4; i++)
            {
                int randomIndex = Random.Range(0, options.Count);
                Seed randomSeed = options[randomIndex];

                GameObject marketItemTemp = Instantiate(marketItem, seedLayoutPanel);
                MarketItemController marketItemController = marketItemTemp.GetComponent<MarketItemController>();

                marketItemController.Instantiate(randomSeed);

                marketItems.Add(marketItemTemp);
            }
        }
    }

    public void ClearMarket()
    {
        for (int i = 0; i < marketItems.Count; i++)
        {
            Destroy(marketItems[0]);
            marketItems.RemoveAt(0);
        }

        marketItems.Clear();
    }

    public void RefreshMarket()
    {
        ClearMarket();
        PopulateMarket();
    }

    public void PurchaseSeed(MarketItemController marketItemController, GameObject marketItemGO)
    {
        if (CurrencyManager.Instance != null)
        {
            if (CurrencyManager.Instance.MarketTokens >= marketItemController.Cost)
            {
                CurrencyManager.Instance.MarketTokens -= marketItemController.Cost;
                UpdateMarketTokenDisplay();

                if (marketItemController.ItemType.Equals("seed"))
                {
                    InventoryController.Instance.AddSeed(marketItemController.Seed);
                }

                marketItems.Remove(marketItemGO);
                Destroy(marketItemGO);
            }
        }
    }

    public void UpdateMarketTokenDisplay()
    {
        marketTokenText.text = CurrencyManager.Instance.MarketTokens.ToString();
    }
}
