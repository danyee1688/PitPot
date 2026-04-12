using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<Seed> seedsInHand = new List<Seed>();
    private List<Seed> seedsInDeck = new List<Seed>();
    private List<Seed> seedsInDiscard = new List<Seed>();

    public List<Seed> SeedsInHand
    {
        get
        {
            return seedsInHand;
        }
        set
        {
            seedsInHand = value;
        }
    }

    public List<Seed> SeedsInDeck
    {
        get
        {
            return seedsInDeck;
        }
        set
        {
            seedsInDeck = value;
        }
    }

    public List<Seed> SeedsInDiscard
    {
        get
        {
            return seedsInDiscard;
        }
        set
        {
            seedsInDiscard = value;
        }
    }

    public Inventory()
    {

    }

    public void AddSeed(Seed seed)
    {
        seedsInDeck.Add(seed);
    }

    public Seed DrawSeed(int handSize)
    {
        if (seedsInDeck.Count > 0)
        {
            int randomIndex = Random.Range(0, seedsInDeck.Count);

            Seed seedTemp = seedsInDeck[randomIndex];

            seedsInHand.Add(seedTemp);
            seedsInDeck.RemoveAt(randomIndex);

            return seedTemp;
        }
        else
        {
            if (seedsInDiscard.Count > 0)
            {
                MoveDiscardToDeck();

                return DrawSeed(handSize);
            }
            else
            {
                return null;
            }
        }
    }

    public void DiscardSeed(Seed seed)
    {
        seedsInHand.Remove(seed);
        seedsInDiscard.Add(seed);
    }

    public void MoveDiscardToDeck()
    {
        Debug.Log("Moving discard to deck");

        seedsInDeck.AddRange(seedsInDiscard);

        seedsInDiscard.Clear();
    }

    public void MoveHandToDeck()
    {
        Debug.Log("Moving hand to deck");

        seedsInDeck.AddRange(seedsInHand);

        seedsInHand.Clear();
    }

    public void MoveAllToDeck()
    {
        MoveHandToDeck();
        MoveDiscardToDeck();
    }
}
