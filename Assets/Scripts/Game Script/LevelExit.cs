using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == ("Player"))
        {
            FindObjectOfType<SceneManagement>().LoadNextLevel();  
        }
    }

}
