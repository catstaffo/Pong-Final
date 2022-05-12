using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rb;
    public int id;
    public float moveSpeed = 2f;
    public float aiDeadZone = 1f;

    private Vector3 startPosition;
    private int direction = 0;
    private float moveSpeedMultiplier = 1f;

    private void Start()
    {
        startPosition = transform.position;
        GameManager.instance.onReset += ResetPosition;
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
    }
    
    public void Update()
    {
       if(IsAi())
        {
          MoveAi();
        }
        else
        {
            float movement = ProcessInput();
            Move(movement);
        }
       

    }

    private bool IsAi()
    {
        
        bool isPlayer2Ai = id == 2 && GameManager.instance.IsPlayer2Ai();
        return isPlayer2Ai;
    }

    private void MoveAi()
    {
       Vector2 ballPos = GameManager.instance.ball.transform.position;

       if (Mathf.Abs(ballPos.y - transform.position.y) > aiDeadZone)
       {
            direction = ballPos.y > transform.position.y ? 1 : -1;
        }

       if (Random.value < 0.01f)
       {
            moveSpeedMultiplier = Random.Range(0.5f, 1.5f);
        }

       Move(direction);
    }

    private float ProcessInput()
    {
        float movement = 0f;
        switch (id)
        {
            case 1:
            movement = Input.GetAxis("MovePlayer1");
            break;
            case 2:
            movement = Input.GetAxis("MovePlayer2");
            break;
        }
        return movement;
    }

    private void Move(float movement)
    {
        Vector2 velo = rb.velocity;
        velo.y = moveSpeed * movement;
        rb.velocity = velo;

    }
}
