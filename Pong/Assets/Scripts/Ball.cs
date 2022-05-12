using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 1f;
    public float maxStartY = 4f;
    public float speedMultiplier = 1.1f;

    private float startX = 0f;

    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.instance.onReset += ResetBall;
        GameManager.instance.gameUI.onStartGame += ResetBall;
    }

    private void ResetBall(){
        ResetBallPosition();
        InitialPush();
    }

   

    public void InitialPush()
    {
        Vector2 dir = Vector2.left;
        if (Random.value < 0.5f)
            {
                dir = Vector2.right;
            }
        else
            {
                dir = Vector2.left;
            }
        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb.velocity = dir * moveSpeed;

    }

  

    private void ResetBallPosition()
    {
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if(scoreZone != null)
        {   GameManager.instance.OnScoreZoneReached(scoreZone.id);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle paddle = collision.collider.GetComponent<Paddle>();
        if(paddle)
        {
            GameManager.instance.gameAudio.PlayPaddleSound();
            rb.velocity *= speedMultiplier;
        
        }

        Wall wall = collision.collider.GetComponent<Wall>();
        if(paddle)
        {
            GameManager.instance.gameAudio.PlayWallSound();
        
        }
    }
}
