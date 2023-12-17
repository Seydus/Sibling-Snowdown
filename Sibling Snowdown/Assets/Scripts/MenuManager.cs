using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Transition")]
    public GameObject disablePanel;
    public Animator animTransition;

    [Header("Main Menu")]
    public GameObject mainMenuObj;

    [Header("HowToPlay")]
    public GameObject howToPlayObj;

   public void StartGameBtn()
    {
        StartCoroutine(StartBackToMenu());
    }

    IEnumerator StartBackToMenu()
    {
        disablePanel.SetActive(true);
        animTransition.SetTrigger("isTransition");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(1);
    }

    public void HowToPlayBtn()
    {
        mainMenuObj.SetActive(false);
        howToPlayObj.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenuObj.SetActive(true);
        howToPlayObj.SetActive(false);
    }

    public void QuitBtn()
    {
        StartCoroutine(StartQuit());
    }

    IEnumerator StartQuit()
    {
        disablePanel.SetActive(true);
        animTransition.SetTrigger("isTransition");
        yield return new WaitForSecondsRealtime(1f);
        Application.Quit();
    }
}
