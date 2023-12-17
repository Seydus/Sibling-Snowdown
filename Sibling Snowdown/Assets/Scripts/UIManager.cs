using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Chris UI")]
    public Slider chrisSlider;
    public TextMeshProUGUI chrisTxtSnowball;
    public TextMeshProUGUI chrisWinMessage;
    public GameObject chrisWinObj;

    [Header("Jess UI")]
    public Slider jesSlider;
    public TextMeshProUGUI jessTxtSnowball;
    public TextMeshProUGUI jessWinMessage;
    public GameObject jessWinObj;

    [Header("Pause Menu")]
    public GameObject pauseMenuObj;
    public bool isPause;

    [Header("Transition")]
    public Animator animTransition;
    public GameObject disablePanel;

    [Header("Music")]
    public GameMusic gameMusic;

    [Header("Stop")]
    public GameObject stopObj;

    private string[] chrisWinMessageList = new string[]
        {
            "Ha, I'm too fast for ya, Jess!",
            "Softball champ has nothing on the Baseball champ!",
            "Aw, is someone cold?",
            "Better go inside and warm up by the fire!",
            "Cocoa is for winners!",
            "Better luck next year, sis.",
            "How's that ice taste?",
            "You want a rematch or not?",
            "Lets go one more round, come on!",
            "On the first day of Christmas, I won this snowball fight!",
            "More cocoa for me!",
            "Ha, you look like Frosty the Snowman!",
            "Who's da best! Who's da best!",
            "Ma, I won!"
        };

    private string[] jessWinMessageList = new string[]
        {
            "Ha, too slow Chris!",
            "Can't beat the softball champ!",
            "I learned that from Elf!",
            "Mmm, tastes like...victory.",
            "Cocoa always hits the spot!",
            "You wanna go another round?",
            "Lets go again!",
            "Jack Frost is on my side!",
            "On the first day of Christmas, I won this snowball fight!",
            "I'm having a holly jolly Christmas!",
            "How's that powder taste?",
            "I love throwing slushees around!",
            "Cocoa, with marshmallows, not cinnamon.",
            "Ma, I won, I won!",
            "Dad, I beat Chris!"
        };

    public void Update()
    {
        PauseMenu();
    }

    public void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            pauseMenuObj.SetActive(isPause);
        }

        if(isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void PlayAgainBtn()
    {
        StartCoroutine(StartPlayAgain());
    }

    public void BackToMenu()
    {
        StartCoroutine(StartBackToMenu());
    }

    IEnumerator StartBackToMenu()
    {
        disablePanel.SetActive(true);
        animTransition.SetTrigger("isTransition");
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    IEnumerator StartPlayAgain()
    {
        disablePanel.SetActive(true);
        animTransition.SetTrigger("isTransition");
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void LoserStatus(string player)
    {
        switch (player)
        {
            case "Chris":
                jessWinObj.SetActive(true);
                jessWinMessage.text = jessWinMessageList[Random.Range(0, jessWinMessageList.Length)];
                break;
            case "Jess":
                chrisWinObj.SetActive(true);
                chrisWinMessage.text = chrisWinMessageList[Random.Range(0, chrisWinMessageList.Length)];
                break;
        }

        gameMusic.PlayWinningMusic();
    }
}
