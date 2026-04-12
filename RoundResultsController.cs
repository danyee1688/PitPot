using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundResultsController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI goalText;
    [SerializeField]
    private TextMeshProUGUI verdictText;
    [SerializeField]
    private TextMeshProUGUI marketText;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject restartButton;

    private bool lerpingGoal = false;

    public IEnumerator ShowResults()
    {
        StartCoroutine(LerpGoalValue(0, CurrencyManager.Instance.Coins, 0, 10));

        yield return new WaitUntil(() => !lerpingGoal);
        yield return new WaitForSeconds(0.25f);

        verdictText.gameObject.SetActive(true);

        bool verdict;

        if (CurrencyManager.Instance.Coins >= RoundManager.Instance.ScoreGoal)
        {
            verdictText.color = Color.green;
            verdictText.text = "Success!";

            verdict = true;
        }
        else
        {
            verdictText.color = Color.red;
            verdictText.text = "Failure...";

            verdict = false;
        }

        if (verdict)
        {
            yield return new WaitForSeconds(0.25f);

            CurrencyManager.Instance.MarketTokens += 2;

            marketText.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.25f);

            nextButton.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(0.25f);

            restartButton.SetActive(true);

            yield return null;
        }
    }

    public IEnumerator LerpGoalValue(int currentValue, int targetValue, float count, float timeFactor)
    {
        lerpingGoal = true;

        if (!goalText.gameObject.activeInHierarchy)
        {
            goalText.gameObject.SetActive(true);
        }

        goalText.text = "Goal: " + Mathf.RoundToInt(Mathf.Lerp(currentValue, targetValue, count)) + "/" + RoundManager.Instance.ScoreGoal + " coins";

        yield return new WaitForSeconds(Time.deltaTime);

        if (count < 1)
        {
            StartCoroutine(LerpGoalValue(currentValue, targetValue, count + (Time.deltaTime * timeFactor), timeFactor));
        }
        else
        {
            goalText.text = "Goal: " + targetValue + "/" + RoundManager.Instance.ScoreGoal + " coins";

            lerpingGoal = false;
        }
    }

    public void Reset()
    {
        goalText.text = "";
        goalText.gameObject.SetActive(false);

        verdictText.text = "";
        verdictText.gameObject.SetActive(false);

        marketText.gameObject.SetActive(false);

        nextButton.SetActive(false);
        restartButton.SetActive(false);

        lerpingGoal = false;
    }
}
