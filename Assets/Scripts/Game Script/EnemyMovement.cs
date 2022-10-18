using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRigidBody;
    BoxCollider2D enemyBoxCollider;
    [SerializeField]float enemySpeed = 1f;
    [SerializeField]int scoreValue = 150;
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        enemyRigidBody.velocity = new Vector2(enemySpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(!other.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            enemySpeed  = -enemySpeed;
            transform.localScale = new Vector2(transform.localScale.x*-1,1f);          
        }
               
    }

    public int GetScoreValue()
    {
        return scoreValue;
    }
}
