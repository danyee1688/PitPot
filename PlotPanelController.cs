using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlotPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject plant;
    [SerializeField]
    private List<PlotController> plotControllers = new List<PlotController>();
    [SerializeField]
    private TextMeshProUGUI coinText;
    [SerializeField]
    private GameObject coinPanel;

    private PlantController[,] plantArray = new PlantController[4, 4];

    public static PlotPanelController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Plant(PlotController plotController, Seed seed, Vector2 pos)
    {
        Debug.Log("planted seed: " + seed);

        bool success = true;

        if (seed.HasPlantRestriction)
        {
            List<PlantController> plantList = new List<PlantController>();

            switch (seed.SeedID)
            {
                case 5:
                    plantList = GetPlantsInRadius(pos, 1, false);

                    if (plantList.Count > 0)
                    {
                        success = false;
                    }

                    break;
                case 6:
                    if (pos.x != 0)
                    {
                        success = false;
                    }

                    break;
                default:
                    break;
            }
        }

        if (success)
        {
            GameObject plantTemp = Instantiate(plant, plotController.gameObject.transform);
            PlantController plantController = plantTemp.GetComponent<PlantController>();

            plotController.Planted = true;

            plantArray[(int)pos.x, (int)pos.y] = plantController;

            plantController.Instantiate(seed, plotController, new Vector2((int)pos.x, (int)pos.y));

            OnPlantPlantAction(plantController);

            SeedPanelController.Instance.DeleteSelectedSeed();

            AudioManager.Instance.Play(0);
        }
    }

    public void ClearPlots()
    {
        List<PlantController> plantList = GetPlantArrayAsList();
 
        for (int i = plantList.Count - 1; i >= 0; i--)
        {
            Destroy(plantList[i]);
            plantList.RemoveAt(i);
        }
    }

    public void UpdatePlots()
    {
        ShuffleList(plotControllers);

        foreach (PlotController plotController in plotControllers)
        {
            if (plotController.Planted)
            {
                PlantController plantController = plantArray[(int)plotController.Pos.x, (int)plotController.Pos.y].GetComponent<PlantController>();

                if (plantController.Wilting)
                {
                    plantController.WiltTime--;
                }
                else
                {
                    plantController.GrowTime--;
                }
            }
        }
    }

    // Fisher-Yates shuffle
    // https://discussions.unity.com/t/shuffle-list/360687/2
    public void ShuffleList(List<PlotController> list)
    {
        System.Random rand = new System.Random();

        int n = list.Count;

        while (n > 1)
        {
            n--;

            int k = rand.Next(n + 1);

            PlotController plotControllerTemp = list[k];
            list[k] = list[n];
            list[n] = plotControllerTemp;
        }
    }

    public void UpdateCoinDisplay()
    {
        if (!coinPanel.activeInHierarchy)
        {
            coinPanel.SetActive(true);
        }

        coinText.text = CurrencyManager.Instance.Coins.ToString();
    }

    public List<PlantController> GetPlantArrayAsList()
    {
        List<PlantController> plantList = new List<PlantController>();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                plantList.Add(plantArray[i, j]);
            }
        }

        return plantList;
    }

    public List<PlantController> GetPlantArrayAsList(List<PlantController> blacklist)
    {
        List<PlantController> plantList = new List<PlantController>();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (!blacklist.Contains(plantArray[i, j]))
                {
                    plantList.Add(plantArray[i, j]);
                }
            }
        }

        return plantList;
    }

    public List<PlantController> GetPlantArrayAsList(List<PlantController> blacklist, bool filterOutNull)
    {
        List<PlantController> plantList = new List<PlantController>();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (!blacklist.Contains(plantArray[i, j]) && plantArray[i, j] != null)
                {
                    plantList.Add(plantArray[i, j]);
                }
            }
        }

        return plantList;
    }

    public List<PlantController> GetPlantsInRadius(PlantController centerPlant, int radius, bool includeCenter)
    {
        Debug.Log("Plant in radius debug");

        List<PlantController> plantList = new List<PlantController>();
        int centerPosX = (int)centerPlant.ArrayPos.x;
        int centerPosY = (int)centerPlant.ArrayPos.y;
        int minimumX = 0;
        int minimumY = 0;
        int maximumX = 0;
        int maximumY = 0;

        Debug.Log("center plant pos: " + centerPosX + ", " + centerPosY);

        if (centerPosX - radius < 0)
        {
            minimumX = 0;
        }
        else
        {
            minimumX = centerPosX - radius;
        }

        if (centerPosY - radius < 0)
        {
            minimumY = 0;
        }
        else
        {
            minimumY = centerPosY - radius;
        }

        if (centerPosX + radius > 3)
        {
            maximumX = 3;
        }
        else
        {
            maximumX = centerPosX + radius;
        }

        if (centerPosY + radius > 3)
        {
            maximumY = 3;
        }
        else
        {
            maximumY = centerPosY + radius;
        }

        if (centerPlant != null && radius >= 1)
        {
            for (int i = minimumX; i <= maximumX; i++)
            {
                for (int j = minimumY; j <= maximumY; j++)
                {
                    if (plantArray[i, j] != null)
                    {
                        Debug.Log("Plant at position " + i + ", " + j);

                        if (includeCenter && plantArray[i, j] == centerPlant)
                        {
                            plantList.Add(plantArray[i, j]);
                        }

                        if (plantArray[i, j] != centerPlant)
                        {
                            plantList.Add(plantArray[i, j]);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Null at position " + i + ", " + j);
                    }
                }
            }
        }

        Debug.Log("found " + plantList.Count + " plant(s) in a " + radius + " plot radius");

        return plantList;
    }

    public List<PlantController> GetPlantsInRadius(Vector2 center, int radius, bool includeCenter)
    {
        Debug.Log("Plant in radius debug");

        List<PlantController> plantList = new List<PlantController>();
        int centerPosX = (int)center.x;
        int centerPosY = (int)center.y;
        int minimumX = 0;
        int minimumY = 0;
        int maximumX = 0;
        int maximumY = 0;

        Debug.Log("center plant pos: " + centerPosX + ", " + centerPosY);

        if (centerPosX - radius < 0)
        {
            minimumX = 0;
        }
        else
        {
            minimumX = centerPosX - radius;
        }

        if (centerPosY - radius < 0)
        {
            minimumY = 0;
        }
        else
        {
            minimumY = centerPosY - radius;
        }

        if (centerPosX + radius > 3)
        {
            maximumX = 3;
        }
        else
        {
            maximumX = centerPosX + radius;
        }

        if (centerPosY + radius > 3)
        {
            maximumY = 3;
        }
        else
        {
            maximumY = centerPosY + radius;
        }

        if (radius >= 1)
        {
            for (int i = minimumX; i <= maximumX; i++)
            {
                for (int j = minimumY; j <= maximumY; j++)
                {
                    if (plantArray[i, j] != null)
                    {
                        Debug.Log("Plant at position " + i + ", " + j);

                        if (includeCenter && new Vector2(i, j) == center)
                        {
                            plantList.Add(plantArray[i, j]);
                        }

                        if (new Vector2(i, j) != center)
                        {
                            plantList.Add(plantArray[i, j]);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Null at position " + i + ", " + j);
                    }
                }
            }
        }

        Debug.Log("found " + plantList.Count + " plant(s) in a " + radius + " plot radius");

        return plantList;
    }

    public List<PlantController> GetPlantsInRow(int row, List<PlantController> blacklist)
    {
        List<PlantController> plantListTemp = new List<PlantController>();

        for (int i = 0; i < 4; i++)
        {
            if (!blacklist.Contains(plantArray[i, row]) && plantArray[i, row] != null)
            {
                plantListTemp.Add(plantArray[i, row]);
            }
        }

        Debug.Log("found " + plantListTemp.Count + " plant(s) in row " + row);

        return plantListTemp;
    }

    public List<PlantController> GetPlantsInColumn(int column, List<PlantController> blacklist)
    {
        List<PlantController> plantListTemp = new List<PlantController>();

        for (int i = 0; i < 4; i++)
        {
            if (!blacklist.Contains(plantArray[column, i]) && plantArray[column, i] != null)
            {
                plantListTemp.Add(plantArray[column, i]);
            }
        }

        Debug.Log("found " + plantListTemp.Count + " plant(s) in column " + column);

        return plantListTemp;
    }

    public List<PlantController> FilterPlantList(List<PlantController> plantList, KeyValuePair<string, string> criteria)
    {
        List<PlantController> filteredList = new List<PlantController>();

        foreach (PlantController plantController in plantList)
        {
            if (plantController != null)
            {
                switch (criteria.Key)
                {
                    case "name":
                        if (plantController.ParentSeed.SeedName == criteria.Value)
                        {
                            filteredList.Add(plantController);
                        }

                        break;
                    case "tag":
                        Debug.Log("plant tags: " + plantController.ParentSeed.Tags);

                        if (plantController.ParentSeed.Tags.Contains(criteria.Value))
                        {
                            filteredList.Add(plantController);
                        }

                        break;
                    case "state":
                        if (criteria.Value == "Bloomed")
                        {
                            if (plantController.GrowTime == 0)
                            {
                                filteredList.Add(plantController);
                            }
                        }

                        break;
                    default:
                        break;
                }
            }
        }

        Debug.Log("found " + filteredList.Count + " plant(s) that match the given criteria: " + criteria.Key + " - " + criteria.Value);

        return filteredList;
    }

    public void OnPlantWilt(PlantController plantController)
    {
        Seed parentSeed = plantController.ParentSeed;
        KeyValuePair<string, string> criteria;
        List<PlantController> plantListTemp;
        List<PlantController> blacklist = new List<PlantController>();

        switch (parentSeed.SeedID)
        {
            case 7:
                if (plantController.ArrayPos.y < 3)
                {
                    PlantController plantControllerTemp = plantArray[(int)plantController.ArrayPos.x, (int)plantController.ArrayPos.y + 1];

                    if (plantControllerTemp != null)
                    {
                        Debug.Log("plant above is not null");

                        OnPlantBloomAction(plantControllerTemp);
                    }
                }

                break;

            default:
                break;
        }
    }

    public void OnPlantBloomAction(PlantController plantController)
    {
        Debug.Log(plantController.ParentSeed.SeedName + " has triggered on bloom");

        Seed parentSeed = plantController.ParentSeed;
        KeyValuePair<string, string> criteria;
        List<PlantController> plantListTemp;
        List<PlantController> blacklist = new List<PlantController>();

        switch (parentSeed.SeedID)
        {
            case 1:
                criteria = new KeyValuePair<string, string>("tag", "Night");

                plantListTemp = FilterPlantList(GetPlantsInRadius(plantController, 1, false), criteria);

                foreach (PlantController plantControllerTemp in plantListTemp)
                {
                    plantControllerTemp.PlantValue += 2;
                }

                break;
            case 6:
                blacklist.Add(plantController);

                plantListTemp = GetPlantsInRow((int)plantController.ArrayPos.y, blacklist);

                foreach (PlantController plantControllerTemp in plantListTemp)
                {
                    plantControllerTemp.PlantValue *= 2;
                }

                break;
            case 8:
                plantListTemp = GetPlantArrayAsList();

                int count = 0;

                foreach (PlantController plantControllerTemp in plantListTemp)
                {
                    if (plantControllerTemp != null)
                    {
                        count++;
                    }
                }

                plantController.PlantValue += count;

                break;
            default:
                break;
        }
    }

    public void OnPlantHarvest(PlantController plantController)
    {
        PlotController parentPlot = plantController.ParentPlot;
        parentPlot.Planted = false;

        OnPlantHarvestAction(plantController);

        plantArray[(int)parentPlot.Pos.x, (int)parentPlot.Pos.y] = null;

        CurrencyManager.Instance.Coins += plantController.PlantValue;
        UpdateCoinDisplay();

        InventoryController.Instance.DiscardSeed(plantController.ParentSeed);

        Destroy(plantController.gameObject);
    }

    public void OnPlantHarvestAction(PlantController plantController)
    {
        Seed parentSeed = plantController.ParentSeed;
        KeyValuePair<string, string> criteria;
        List<PlantController> plantListTemp;
        List<PlantController> blacklist = new List<PlantController>();

        switch (parentSeed.SeedID)
        {
            case 0:
                criteria = new KeyValuePair<string, string>("name", "Daybloom");

                plantListTemp = FilterPlantList(GetPlantsInRadius(plantController, 1, false), criteria);

                if (plantListTemp.Count > 0)
                {
                    CurrencyManager.Instance.Coins += plantListTemp.Count;
                }

                break;
            case 4:
                criteria = new KeyValuePair<string, string>("tag", "Weed");

                plantListTemp = FilterPlantList(GetPlantArrayAsList(), criteria);

                criteria = new KeyValuePair<string, string>("state", "Bloomed");

                plantListTemp = FilterPlantList(plantListTemp, criteria);

                foreach (PlantController plantControllerTemp in plantListTemp)
                {
                    if (plantControllerTemp != plantController)
                    {
                        plantControllerTemp.PlantValue += 2;
                    }
                }

                break;
            case 9:
                blacklist.Add(plantController);

                criteria = new KeyValuePair<string, string>("tag", "Day");

                plantListTemp = FilterPlantList(GetPlantsInRow((int)plantController.ArrayPos.y, blacklist), criteria);

                if (plantListTemp.Count > 0)
                {
                    foreach (PlantController plantControllerTemp in plantListTemp)
                    {
                        plantControllerTemp.PlantValue += plantController.PlantValue;
                    }
                }

                break;
            default:
                break;
        }
    }

    public void OnPlantPlantAction(PlantController plantController)
    {
        Seed parentSeed = plantController.ParentSeed;
        KeyValuePair<string, string> criteria;
        List<PlantController> plantListTemp;
        List<PlantController> blacklist = new List<PlantController>();

        switch (parentSeed.SeedID)
        {
            case 2:
                blacklist.Add(plantController);

                plantListTemp = GetPlantArrayAsList(blacklist, true);

                if (plantListTemp.Count > 0)
                {
                    int randomIndex = Random.Range(0, plantListTemp.Count);

                    if (plantListTemp[randomIndex].GrowTime > 0)
                    {
                        plantListTemp[randomIndex].GrowTime--;
                    }
                }

                break;
            case 3:
                blacklist.Add(plantController);

                plantListTemp = GetPlantsInColumn((int)plantController.ArrayPos.x, blacklist);

                if (plantListTemp.Count > 0)
                {
                    foreach (PlantController plantControllerTemp in plantListTemp)
                    {
                        if (!plantControllerTemp.Wilted)
                        {
                            plantControllerTemp.Wilt();
                        }
                    }

                    plantController.PlantValue += (2 * plantListTemp.Count);
                }

                break;
            case 5:
                criteria = new KeyValuePair<string, string>("name", "Waterleaf");

                plantListTemp = GetPlantsInColumn((int)plantController.ArrayPos.x, blacklist);

                plantListTemp = FilterPlantList(plantListTemp, criteria);

                foreach (PlantController plantControllerTemp in plantListTemp)
                {
                    plantControllerTemp.Wilt();
                }

                break;
            default:
                break;
        }
    }
}
