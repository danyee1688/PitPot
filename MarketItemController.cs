using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TextMeshProUGUI costText;

    private string itemType;
    private Seed seed = null;
    private int cost = 0;

    public Seed Seed
    {
        get
        {
            return seed;
        }
        set
        {
            seed = value;
        }
    }

    public int Cost
    {
        get
        {
            return cost;
        }
        set
        {
            cost = value;

            costText.text = cost.ToString();
        }
    }

    public string ItemType
    {
        get
        {
            return itemType;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        iconImage.transform.localScale = new Vector3(1.10f, 1.10f, 1);

        if (itemType.Equals("seed") && seed != null)
        {
            InfoPanelManager.Instance.ShowSeedInfo(seed);
        }

        AudioManager.Instance.Play(4);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        iconImage.transform.localScale = new Vector3(1, 1, 1);

        if (itemType.Equals("seed") && seed != null)
        {
            InfoPanelManager.Instance.HideSeedInfo();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MarketController.Instance.PurchaseSeed(this, gameObject);
    }

    public void Instantiate(Seed seed)
    {
        itemType = "seed";

        this.seed = seed;

        iconImage.sprite = SpriteDB.Instance.seedSprites[seed.SeedID];

        Cost = 1;
    }
}
