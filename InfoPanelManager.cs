using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanelManager : MonoBehaviour
{
    public static InfoPanelManager Instance;

    [SerializeField]
    private GameObject seedInfoPanel;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private TextMeshProUGUI tagsText;
    [SerializeField]
    private TextMeshProUGUI seedStatText;
    [SerializeField]
    private TextMeshProUGUI optionalText;
    [SerializeField]
    private GameObject roundInfoPanel;
    [SerializeField]
    private TextMeshProUGUI roundText;
    [SerializeField]
    private TextMeshProUGUI dayText;
    [SerializeField]
    private TextMeshProUGUI goalText;
    [SerializeField]
    private TextMeshProUGUI nextDayButtonText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowSeedInfo(Seed seed)
    {
        seedInfoPanel.SetActive(true);
       
        titleText.text = seed.SeedName;
        descriptionText.text = SeedDB.seedDescriptions[seed.SeedID];

        string tags = "";

        for (int i = 0; i < seed.Tags.Count - 1; i++)
        {
            tags += seed.Tags[i] + ", ";
        }

        tags += seed.Tags[^1];

        tagsText.text = tags;
        seedStatText.text = "Time to grow: " + seed.SeedGrowTime.ToString() + " days\nSells for: " + seed.SeedValue.ToString() + " coins";
    }

    public void HideSeedInfo()
    {
        seedInfoPanel.SetActive(false);

        if (RoundManager.Instance.RoundActive)
        {
            ShowRoundInfo();
        }
    }

    public void ShowPlantInfo(PlantController plantController)
    {
        seedInfoPanel.SetActive(true);

        Seed seed = plantController.ParentSeed;

        titleText.text = seed.SeedName;
        descriptionText.text = SeedDB.seedDescriptions[seed.SeedID];

        string tags = "";

        for (int i = 0; i < seed.Tags.Count - 1; i++)
        {
            tags += seed.Tags[i] + ", ";
        }

        tags += seed.Tags[^1];

        tagsText.text = tags;

        if (plantController.Wilting)
        {
            seedStatText.text = "Wilts in: " + plantController.WiltTime.ToString() + " days\nCurrent value: " + plantController.PlantValue.ToString() + " coins";
        }
        else if (plantController.Wilted)
        {
            seedStatText.text = "Wilted\nNo value";
        }
        else
        {
            seedStatText.text = "Blooms in: " + plantController.GrowTime.ToString() + " days\nCurrent value: " + plantController.PlantValue.ToString() + " coins";
        }  
    }

    public void HidePlantInfo()
    {
        seedInfoPanel.SetActive(false);

        if (RoundManager.Instance.RoundActive)
        {
            ShowRoundInfo();
        }
    }

    public void ShowRoundInfo()
    {
        roundInfoPanel.SetActive(true);

        roundText.text = "Round " +  RoundManager.Instance.Round.ToString();
        dayText.text = "Day " + RoundManager.Instance.Day.ToString();
        goalText.text = "Goal: " + RoundManager.Instance.ScoreGoal.ToString() + " coins";
    }

    public void HideAllInfo()
    {
        seedInfoPanel.SetActive(false);
        roundInfoPanel.SetActive(false);
    }

    public void SetNextDayButtonText(string text)
    {
        nextDayButtonText.text = text;
    }
}
