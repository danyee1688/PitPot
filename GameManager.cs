using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject marketPanel;
    [SerializeField]
    private MarketController marketController;
    [SerializeField]
    private SeedPanelController seedPanelController;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private RoundResultsController roundResultsController;
    [SerializeField]
    private GameObject roundResultsPanel;
    [SerializeField]
    private GameObject tutorialPanel;
    [SerializeField]
    private TextMeshProUGUI tutorialText;
    [SerializeField]
    private SlideTransition seedPanelSlideTransition;
    [SerializeField]
    private SlideTransition infoPanelSlideTransition;
    [SerializeField]
    private GameObject optionsPanel;

    [Header("Debug")]
    [SerializeField]
    private bool debugEnabled;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTutorial());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel.activeInHierarchy)
            {
                SetOptionsPanel(false);
            }
            else
            {
                SetOptionsPanel(true);
            }
        }
    }

    public void ResetGameVariables()
    {
        CurrencyManager.Instance.MarketTokens = 3;
    }

    public void ResetDayVariables()
    {

    }

    public void ResetRoundVariables()
    {
        CurrencyManager.Instance.Coins = 0;
        PlotPanelController.Instance.UpdateCoinDisplay();
        PlotPanelController.Instance.ClearPlots();
        MarketController.Instance.UpdateMarketTokenDisplay();
        InventoryController.Instance.ResetInventory();
        SeedPanelController.Instance.DestroyAll();

        InfoPanelManager.Instance.SetNextDayButtonText("Next Day");
    }

    public void StartNextRound()
    {
        SetRoundResultsPanel(false);

        StartCoroutine(StartRound());
    }

    public IEnumerator StartRound()
    {
        SetMarketPanel(true);

        marketController.UpdateMarketTokenDisplay();
        marketController.RefreshMarket();

        yield return new WaitUntil(() => !marketPanel.activeInHierarchy);

        ResetRoundVariables();

        RoundManager.Instance.Round++;
        RoundManager.Instance.Day = 1;
        RoundManager.Instance.RoundActive = true;

        StartCoroutine(StartDay());

        yield return null;
    }

    public void StartNextDay()
    {
        if (RoundManager.Instance.Day == 7)
        {
            InfoPanelManager.Instance.HideAllInfo();

            Debug.Log("round concluded");

            RoundManager.Instance.RoundActive = false;

            SetRoundResultsPanel(true);

            StartCoroutine(roundResultsController.ShowResults());
        }
        else if (RoundManager.Instance.Day == 6)
        {
            RoundManager.Instance.Day++;

            InfoPanelManager.Instance.SetNextDayButtonText("Finish");

            StartCoroutine(StartDay());
        }
        else
        {
            RoundManager.Instance.Day++;

            StartCoroutine(StartDay());
        }       
    }

    public IEnumerator StartDay()
    {
        Debug.Log("New Day");

        ResetDayVariables();

        InfoPanelManager.Instance.HideAllInfo();
        InfoPanelManager.Instance.ShowRoundInfo();
        PlotPanelController.Instance.UpdatePlots();

        seedPanelController.Populate();

        yield return null;
    }

    public void SetMarketPanel(bool active)
    {
        marketPanel.SetActive(active);
    }

    public void SetRoundResultsPanel(bool active)
    {
        roundResultsPanel.SetActive(active);

        if (active)
        {
            roundResultsController.Reset();
        }
    }

    public void SetOptionsPanel(bool active)
    {
        optionsPanel.SetActive(active);
    }

    public Vector2 GetMouseWorldPosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetTutorialPanel(bool active)
    {
        tutorialPanel.SetActive(active);
    }

    public IEnumerator StartTutorial()
    {
        SetTutorialPanel(true);

        tutorialText.text = "Hey there, farmer!";

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0.1f);

        tutorialText.text = "It's time for you to make a living for yourself.";

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0.1f);

        tutorialText.text = "This farm is all yours for taking, but you'll have to buy the SEEDS yourself.";

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0.1f);

        infoPanelSlideTransition.StartTransition(SlideTransition.TransitionDirection.startToEnd);
        seedPanelSlideTransition.StartTransition(SlideTransition.TransitionDirection.startToEnd);

        yield return new WaitForSeconds(0.5f);

        tutorialText.text = "Your seeds will appear here on the left after you buy em, and information about them will be over on the right.";

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0.1f);

        tutorialText.text = "Make sure to harvest your plants when they're fully bloomed, all plants wilt in 2 days.";

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0.1f);

        tutorialText.text = "When your plants wilt, they'll lose all their value which is a bummer, but just click em to get rid of em.";

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0.1f);

        tutorialText.text = "Make sure you hit the weekly quota, or we'll have to kick you outta here!";

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0.1f);

        tutorialText.text = "Good luck buckaroo, I'll give you a few tokens for the market to start off.";

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return new WaitForSeconds(0.1f);

        SetTutorialPanel(false);

        ResetGameVariables();
        StartCoroutine(StartRound());
    }
}
