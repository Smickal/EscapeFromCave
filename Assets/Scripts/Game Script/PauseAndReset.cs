using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAndReset : MonoBehaviour
{
    public void EnablePauseMenu()
    {
        gameObject.SetActive(true);
    }

    public void DisablePauseMenu()
    {
        gameObject.SetActive(false);
    }
}
