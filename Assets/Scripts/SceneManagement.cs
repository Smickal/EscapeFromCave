using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currSceneIndex + 1;

        if(currSceneIndex > 0)
        {
            FindObjectOfType<ScenePersist>().ResetScenePresist();
        }

        SceneManager.LoadScene(nextSceneIndex);

    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(AnimationTime());
        SceneManager.LoadScene(0);
    }

    public void ResetCurrentScene()
    {
        StartCoroutine(AnimationTime());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator AnimationTime()
    {   
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }

    public void GoToGameOverScene()
    {
        SceneManager.LoadScene(4);
    }
}
