using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int scoreValue = 100;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().IncreaseScore(scoreValue);
            Destroy(gameObject);

        }
    }
}
