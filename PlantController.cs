using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    private TextMeshProUGUI valueText;
    [SerializeField]
    private TextMeshProUGUI growTimeText;
    [SerializeField]
    private Image mainImage;

    private PlotController parentPlot;
    private Seed parentSeed;
    private int growTime = 0;
    private int wiltTime = 0;
    private int plantValue = 0;
    private bool wilting = false;
    private bool wilted = false;
    private Vector2 arrayPos;

    public int GrowTime
    {
        get
        {
            return growTime;
        }
        set
        {
            if (value == 0)
            {
                growTime = 0;

                mainImage.sprite = SpriteDB.Instance.plantSprites[parentSeed.SeedID];

                Instantiate(ParticleSystemDB.Instance.bloomPS, transform.position, Quaternion.identity);
                
                PlotPanelController.Instance.OnPlantBloomAction(this);

                wilting = true;

                growTimeText.color = Color.red;

                WiltTime = 2;
            }
            else
            {
                growTime = value;

                growTimeText.text = growTime.ToString();
            }
        }
    }

    public int PlantValue
    {
        get
        {
            return plantValue;
        }
        set
        {
            if (!wilted)
            {
                plantValue = value;

                valueText.text = plantValue.ToString();
            }
        }
    }

    public int WiltTime
    {
        get
        {
            return wiltTime;
        }
        set
        {
            if (value == 0)
            {
                wilting = false;
                wilted = true;

                mainImage.color = Color.black;

                plantValue = 0;
                valueText.text = plantValue.ToString();

                growTimeText.gameObject.SetActive(false);

                PlotPanelController.Instance.OnPlantWilt(this);
            }
            else
            {
                wiltTime = value;

                growTimeText.text = wiltTime.ToString();
            }
        }
    }

    public bool Wilting
    {
        get
        {
            return wilting;
        }
    }

    public bool Wilted
    {
        get
        {
            return wilted;
        }
    }

    public Seed ParentSeed
    {
        get
        {
            return parentSeed;
        }
        set
        {
            parentSeed = value;
        }
    }

    public PlotController ParentPlot
    {
        get
        {
            return parentPlot;
        }
    }

    public Vector2 ArrayPos
    {
        get
        {
            return arrayPos;
        }
        set
        {
            arrayPos = value;
        }
    }

    public void Instantiate(Seed parentSeed, PlotController parentPlot, Vector2 arrayPos)
    {
        this.parentSeed = parentSeed;
        this.parentPlot = parentPlot;
        this.arrayPos = arrayPos;

        GrowTime = this.parentSeed.SeedGrowTime;
        PlantValue = this.parentSeed.SeedValue;

        mainImage.sprite = SpriteDB.Instance.seedSprites[parentSeed.SeedID];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GrowTime == 0 || Wilted)
        {
            AudioManager.Instance.Play(5);

            InfoPanelManager.Instance.HideSeedInfo();

            PlotPanelController.Instance.OnPlantHarvest(this);

            Instantiate(ParticleSystemDB.Instance.harvestPS, transform.position, Quaternion.identity);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InfoPanelManager.Instance.HideAllInfo();
        InfoPanelManager.Instance.ShowPlantInfo(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoPanelManager.Instance.HidePlantInfo();
    }

    public void Wilt()
    {
        WiltTime = 0;
    }
}
