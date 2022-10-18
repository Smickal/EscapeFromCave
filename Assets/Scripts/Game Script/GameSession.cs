using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
    public int playerLives = 3;
    public int currScore = 0;
    [SerializeField]TextMeshProUGUI livesText;
    [SerializeField]TextMeshProUGUI ScoreText;
    [SerializeField]TextMeshProUGUI PopUpTitleText;
    [SerializeField]TextMeshProUGUI stopWatchTimer;

    ScoreAndLives scoreAndLive;
    PauseAndReset pauseMenu;

    int startingLives;
    PausePopUp pausePopUpScreen;
    TimeStopwatch timeStopwatch;

    public bool isPause;
    float currTimeScale;
    void Awake() 
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        pausePopUpScreen = FindObjectOfType<PausePopUp>();
        scoreAndLive = FindObjectOfType<ScoreAndLives>();
        pauseMenu = FindObjectOfType<PauseAndReset>();
        timeStopwatch = FindObjectOfType<TimeStopwatch>();

        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }



    public void ProcessPlayerDeath()
    {
        if(playerLives >1)
        {
            DecreaseLiveCount();
        }
        else
        {
            FindObjectOfType<ScenePersist>().ResetScenePresist();
            ResetGameSessions();
            FindObjectOfType<TimeStopwatch>().StartStopWatch();
        }
    }


    public void ResetGameSessions()
    {
        playerLives = 0;
        FindObjectOfType<SceneManagement>().GoToGameOverScene();
    }

    public void DecreaseLiveCount()
    {
        playerLives -= 1;
        livesText.text = "Lives: " + playerLives.ToString();
        ResetCurrentGameScene();
    }


    private void Start() 
    {
        livesText.text = "Lives: " + playerLives.ToString();
        ScoreText.text = "Score: " + currScore.ToString();
        currTimeScale = Time.timeScale;
        pausePopUpScreen.DisablePausePopUp();
        startingLives = playerLives;
        timeStopwatch.StartStopWatch();
    }

    public void IncreaseScore(int incrementScore)
    {
        currScore += incrementScore;
        ScoreText.text = "Score: " + currScore.ToString();
    }

    //pause Mechanics
    public void PauseGameScene()
    {
        if(!isPause)
        {
            isPause = !isPause;
            Time.timeScale = 0f;
            pausePopUpScreen.EnablePausePopUp();
            if(playerLives >= 1)
            {
                PopUpTitleText.text = pausePopUpScreen.GetPausePopUpTitles();
            }
            else
            {
                PopUpTitleText.text = pausePopUpScreen.GetGameOverPopUpTitles();
            }
            pauseMenu.DisablePauseMenu();
            scoreAndLive.DisableScoreboard();
            timeStopwatch.PauseStopWatch();
        }
        else
        {
            isPause = !isPause;
            Time.timeScale = currTimeScale;
            pausePopUpScreen.DisablePausePopUp();
            pauseMenu.EnablePauseMenu();
            scoreAndLive.EnableScoreboard();
            timeStopwatch.StartStopWatch();
        }
    }

    public void ResetCurrentGameScene()
    {
        FindObjectOfType<SceneManagement>().ResetCurrentScene();
    }

    public void ReturnToMenuFromPause()
    {
        isPause = !isPause;
        Time.timeScale = currTimeScale;
        FindObjectOfType<ScenePersist>().ResetScenePresist();
        ResetGameSessions();
    }

    public void DestroyGameSession()
    {
        Destroy(gameObject);
    }
}
