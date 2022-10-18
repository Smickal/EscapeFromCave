using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float playerSpeed = 10f;
    [SerializeField]float jumpSpeed = 5f;
    [SerializeField]float climbSpeed = 5f;
    [SerializeField]float deathAnimationJump = 20f;
    [SerializeField]Collider2D playerFeetCollider;
    [SerializeField]GameObject bulletPrefabs;
    [SerializeField]Transform gun;
    Vector2 moveInput;
    Rigidbody2D playerRigid2D;
    Animator playerAnimator;
    Collider2D playerCollider;
    GameSession gameSession;
    float currentGravity;

    bool isAlive = true;
    
    [SerializeField]AudioClip deathSFX;
    [SerializeField]AudioClip shootSFX;
    void Start()
    {
        playerRigid2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        gameSession = FindObjectOfType<GameSession>();
        currentGravity = playerRigid2D.gravityScale;
    }

    void Update()
    {
        if(isAlive && !gameSession.isPause)
        {
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed && playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRigid2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void ClimbLadder()
    {
        if(playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Vector2 climbVelocity = new Vector2(playerRigid2D.velocity.x, moveInput.y * climbSpeed);
            playerRigid2D.velocity = climbVelocity;
            playerRigid2D.gravityScale = Mathf.Epsilon;
            bool isClimbingAndNotTouchingGround = Mathf.Abs(playerRigid2D.velocity.y) > Mathf.Epsilon;
            playerAnimator.SetBool("isClimbing", isClimbingAndNotTouchingGround);
        }
        else
        {   
            playerRigid2D.gravityScale = currentGravity;
            playerAnimator.SetBool("isClimbing", false);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, playerRigid2D.velocity.y);
        playerRigid2D.velocity = playerVelocity;

        bool isPlayerMoving = Mathf.Abs(playerRigid2D.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", isPlayerMoving);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigid2D.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigid2D.velocity.x), 1f);
        }
    }

    void Die()
    {
        if(playerRigid2D.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            FindObjectOfType<TimeStopwatch>().PauseStopWatch();
            AudioSource.PlayClipAtPoint(deathSFX,Camera.main.transform.position, 0.2f);
            StartCoroutine(AnimationDyingPlayer());
            
        }
    }

    IEnumerator AnimationDyingPlayer()
    {
        playerAnimator.SetTrigger("Dying");
        playerCollider.enabled = !playerCollider.enabled;
        playerRigid2D.velocity = new Vector2(1f,deathAnimationJump);
        

        yield return new WaitForSeconds(1f);
        
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    void OnFire()
    {
        if(isAlive && !gameSession.isPause)
        {
            playerAnimator.SetTrigger("Shooting");
            AudioSource.PlayClipAtPoint(shootSFX,Camera.main.transform.position, 0.5f);
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefabs, gun.transform.position,Quaternion.identity);
    }
}
