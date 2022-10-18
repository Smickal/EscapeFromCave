using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRolling : MonoBehaviour
{
   Vector2 startingPos;
   [SerializeField]float backgroundSpeedRoll = 5f;

   private void Start() 
   {
       startingPos = transform.position;
   }
    void FixedUpdate()
    {
        transform.position = new Vector2 (transform.position.x - backgroundSpeedRoll * Time.deltaTime, transform.position.y);
        if(transform.position.x < -44.4)
        {
            transform.position = startingPos;
        }
    }
}
