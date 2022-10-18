using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePopUp : MonoBehaviour
{
    public string PausePopUpTitle = "PAUSED !";
    public string GameOverPopUpTitle = "GAME OVER !";

    public void EnablePausePopUp()
    {
        gameObject.SetActive(true);
    }

    public void DisablePausePopUp()
    {
        gameObject.SetActive(false);
    }

    public string GetPausePopUpTitles()
    {
        return PausePopUpTitle;
    }
    public string GetGameOverPopUpTitles()
    {
        return GameOverPopUpTitle;
    }
}
