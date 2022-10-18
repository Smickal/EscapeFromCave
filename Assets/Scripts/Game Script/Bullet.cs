using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRigid;
    [SerializeField]float bulletSpeed = 1f;
    PlayerMovement player;
    float xSpeed;
    void Start()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }


    void Update()
    {
        bulletRigid.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            
            Destroy(other.gameObject);
            int EnemiesScoreValue = FindObjectOfType<EnemyMovement>().GetScoreValue();
            FindObjectOfType<GameSession>().IncreaseScore(EnemiesScoreValue);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);    
    }
}
