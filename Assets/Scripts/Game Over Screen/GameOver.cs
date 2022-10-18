using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField]TextMeshProUGUI liveText;
    [SerializeField]TextMeshProUGUI timeText;


    TimeStopwatch time;

    private void Start() {
        scoreText.text = "Score : "+ FindObjectOfType<GameSession>().currScore.ToString();
        liveText.text = "Lives left :" + FindObjectOfType<GameSession>().playerLives.ToString();

        time = FindObjectOfType<TimeStopwatch>();
        timeText.text = string.Format("Time : {0:00}:{1:00}:{2:00}", time.minutes, time.seconds, time.miliSeconds);

        FindObjectOfType<GameSession>().DestroyGameSession();
        time.ResetStopWatch();
    }
}
