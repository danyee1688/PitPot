using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedDB
{
    public static List<Seed> seedList = new List<Seed>()
    {
        new Seed(0, "Daybloom", 3, 2, false,
            new List<string>() {"Flower", "Day"}),
        new Seed(1, "Moonglow", 5, 4, true,
            new List<string>() {"Flower", "Night"}),
        new Seed(2, "Blinkroot", 1, 2, true,
            new List<string>() {"Weed", "Night"}),
        new Seed(3, "Deathweed", 0, 1, true,
            new List<string>() {"Weed", "Night"}),
        new Seed(4, "Waterleaf", 3, 2, false,
            new List<string>() {"Weed", "Day"}),
        new Seed(5, "Fireblossom", 10, 5, false,
            new List<string>() {"Flower", "Day"}, true),
        new Seed(6, "Shiverthorn", 2, 1, false,
            new List<string>() {"Flower", "Day"}, true),
        new Seed(7, "Silverrod", 0, 3, false,
            new List<string>() {"Mushroom", "Night"}),
        new Seed(8, "Enigmanium", 1, 3, false,
            new List<string>() {"Mushroom", "Night"}),
        new Seed(9, "Sunlily", 2, 4, false,
            new List<string>() {"Flower", "Day"}),
    };

    public static Dictionary<int, string> seedDescriptions = new Dictionary<int, string>()
    {
        {0, "On harvest, gain an additional coin for every Daybloom in a 1 plot radius."},
        {1, "On bloom, all Night plants in a 1 plot radius gain 2 coin value." },
        {2, "When planted, reduce the grow time for a random plant on your plot by 1 day."},
        {3, "When planted, wilt all other plants in this column. Gain 2 coin value per plant wilted."},
        {4, "On harvest, give all bloomed Weeds 2 additional coin value."},
        {5, "Cannot be planted next to another plant. When planted, wilts all Waterleafs in the same column."},
        {6, "Can only be planted in the first column. On bloom, all other Flowers in the same row double their coin value." },
        {7, "On wilt, the plant above triggers it's 'On bloom' ability"},
        {8, "On bloom, gain coin value equal to the number of non-empty plots" },
        {9, "On harvest, other Day plants in the same row gain coin value equivalent to this plant's coin value" }
    };

    public static Seed GetRandomSeed()
    {
        int randomIndex = Random.Range(0, seedList.Count);

        return seedList[randomIndex];
    }
}
