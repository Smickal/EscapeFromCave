using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAndLives : MonoBehaviour
{
    public void EnableScoreboard()
    {
        gameObject.SetActive(true);
    }

    public void DisableScoreboard()
    {
        gameObject.SetActive(false);
    }
}
