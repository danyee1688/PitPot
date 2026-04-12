using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed
{
    private int seedID;
    private string seedName;
    private string seedDescription;
    private string seedFlavorText;
    private int seedGrowTime;
    private bool seedNightBloom;
    private int seedValue;
    private Sprite seedSprite;
    private List<string> seedTags = new List<string>();
    private bool hasPlantRestriction = false;

    public int SeedID
    {
        get
        {
            return seedID;
        }
        set
        {
            seedID = value;
        }
    }

    public string SeedName
    {
        get
        {
            return seedName;
        }
        set
        {
            seedName = value;
        }
    }

    public int SeedValue
    {
        get
        {
            return seedValue;
        }
        set
        {
            seedValue = value;
        }
    }

    public int SeedGrowTime
    {
        get
        {
            return seedGrowTime;
        }
        set
        {
            seedGrowTime = value;
        }
    }

    public Sprite SeedSprite
    {
        get
        {
            return seedSprite;
        }
        set
        {
            seedSprite = value;
        }
    }

    public string SeedDescription
    {
        get
        {
            return seedDescription;
        }
        set
        {
            seedDescription = value;
        }
    }

    public List<string> Tags
    {
        get
        {
            return seedTags;
        }
        set
        {
            seedTags = value;
        }
    }

    public bool HasPlantRestriction
    {
        get
        {
            return hasPlantRestriction;
        }
        set
        {
            hasPlantRestriction = value;
        }
    }

    public Seed(int seedID, string seedName, int seedValue, int seedGrowTime, bool seedNightBloom, List<string> seedTags)
    {
        this.seedID = seedID;
        this.seedName = seedName;
        this.seedValue = seedValue;
        this.seedGrowTime = seedGrowTime;
        this.seedNightBloom = seedNightBloom;
        this.seedTags = seedTags;
    }

    public Seed(int seedID, string seedName, int seedValue, int seedGrowTime, bool seedNightBloom, List<string> seedTags, bool hasPlantRestriction)
    {
        this.seedID = seedID;
        this.seedName = seedName;
        this.seedValue = seedValue;
        this.seedGrowTime = seedGrowTime;
        this.seedNightBloom = seedNightBloom;
        this.seedTags = seedTags;
        this.hasPlantRestriction = hasPlantRestriction;
    }

    public override string ToString()
    {
        return "seed name: " + seedName;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        Seed seed = obj as Seed;

        if ((System.Object)seed == null) 
        {
            return false;
        }

        return SeedID == seed.SeedID;
    }

    public bool Equals(Seed other)
    {
        if ((object)other == null)
        {
            return false;
        }

        return SeedID == other.SeedID;
    }
}
