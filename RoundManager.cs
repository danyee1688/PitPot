using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    private int scoreGoal = 0;
    private int round = 0;
    private int day = 0;
    private bool roundActive = false;

    public int Round
    {
        get
        {
            return round;
        }
        set
        {
            round = value;

            SetGoal(round);
        }
    }

    public int Day
    {
        get
        {
            return day;
        }
        set
        {
            day = value;
        }
    }

    public int ScoreGoal
    {
        get
        {
            return scoreGoal;
        }
        set
        {
            scoreGoal = value;
        }
    }

    public bool RoundActive
    {
        get
        {
            return roundActive;
        }
        set
        {
            roundActive = value;
        }
    }

    public static RoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetGoal(int round)
    {
        ScoreGoal = Mathf.RoundToInt(Mathf.Pow(round, 3) + 10);
    }
}
