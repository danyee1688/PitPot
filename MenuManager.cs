using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private SlideTransition titleSlideTransition;
    [SerializeField]
    private ColorTransition titleColorTransition;
    [SerializeField]
    private TextMeshProUGUI continueText;

    private bool inputAllowed = false;

    private IEnumerator Start()
    {
        titleColorTransition.StartTransition(ColorTransition.TransitionDirection.startToEnd);

        yield return new WaitUntil(() => titleColorTransition.transitionDone);

        titleSlideTransition.StartTransition(SlideTransition.TransitionDirection.startToEnd);

        yield return new WaitUntil(() => titleSlideTransition.transitionDone);

        continueText.gameObject.SetActive(true);

        inputAllowed = true;
    }

    private void Update()
    {
        if (inputAllowed)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
