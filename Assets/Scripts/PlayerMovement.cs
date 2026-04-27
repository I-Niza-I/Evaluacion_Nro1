using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float hSpeed = 7;
    [SerializeField] private float vSpeed = 7;


    [SerializeField] private AudioClip audioRun = null;
    [SerializeField] private AudioClip audioDeath = null;
    [SerializeField] private AudioClip audioJump = null;

    [SerializeField] private Animator animator;

    private SpriteRenderer spriteRenderer;
    private GameController gc;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private float rightBound;
    private float leftBound;
    private Vector2 movement;
    private float xPosLastFrame;

    private bool vivo = true;
    private bool isGround = false;


    private Vector2 jumpSpeed;


    private Vector3 myPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gc = GameController.GetInstance();

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        jumpSpeed = new Vector2(0, vSpeed);
        myPosition = transform.position;

        rightBound = gc.GetRightBound();
        leftBound = gc.GetLeftBound();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!vivo) return;


    }
    // Update is called once per frame
    void Update()
    {
        if (!vivo) return;


        if (Input.GetButtonDown("Jump") && isGround)
        {
            isGround = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Resetea solo la Y
            rb.AddForce(Vector2.up * vSpeed, ForceMode2D.Impulse);
            audioSource.clip = audioJump;
            audioSource.Play();
        }

        float input = Input.GetAxis("Horizontal");

        MovementHandler(input);
        ClampMovement();
        FlipPlayerX(input);

        xPosLastFrame = transform.position.x;

    }

    private void FlipPlayerX(float input)
    {
        if (input > 0 && (transform.position.x >= xPosLastFrame))
        {
            spriteRenderer.flipX = false;
        }
        else if (input < 0 && (transform.position.x <= xPosLastFrame))
        {
            spriteRenderer.flipX = true;
        }
    }

    private void ClampMovement()
    {
        float clampedX = Mathf.Clamp(transform.position.x, leftBound, rightBound);
        Vector2 pos = transform.position;
        pos.x = clampedX;
        transform.position = pos;
    }

    private void MovementHandler(float input)
    {

        movement.x = input * hSpeed * Time.deltaTime;
        transform.Translate(movement);
        if (input != 0)
        {
            animator.SetBool("isRunning", true);

            
            if (isGround && !audioSource.isPlaying)
            {
                audioSource.clip = audioRun;
                audioSource.Play();
            }
            
        }
        else
        {
            animator.SetBool("isRunning", false);
            
            if (isGround && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            
        }
      
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (!vivo) return;


        GameObject obj = col.gameObject;

        if (obj.tag == "Ground")
        {
            isGround = true;
        }
        if (obj.tag == "Spikes")
        {
            vivo = false;
            audioSource.Stop();
            animator.SetBool("isRunning", false);
            animator.SetTrigger("Death");

            audioSource.clip = audioDeath;
            audioSource.Play();

            gc.RestLives();
            StartCoroutine(SiguienteVida());
        }
    }

    public IEnumerator SiguienteVida()
    {
        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(1.0f);
        }

        animator.ResetTrigger("Death");
        animator.SetBool("isRunning", false);

        animator.Play("Idle");

        transform.position = myPosition;
        vivo = true;
    }

}
