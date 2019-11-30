using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class movement : MonoBehaviour
{
    bool alreadyFlashed;
    bool startCountdown = false;


    float flipVal = 1;
    [Header("Movement Stuff")]

    float fade = 100;
    Rigidbody2D rb2d;
    Vector3 refVel = Vector3.zero;
    float smoothMovementModifier = .05f;
    float horizVelocity;
    public float Speed = 6;
    public float jumpModifier = 5.5f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool grounded = false;
    public int doubleJumps = 1;

    [HideInInspector]public int curDoubleJumps;

    public bool canFlip = true;

    [Header("FlashlightStuff")]
    public float flashTime = 2.0f;
    private float flashTimer = 0;

    public int maxFlashes = 2;
    private int curFlashes;

    public float flashCoolDownTime;
    private float curFlashCooldown;

    Animator anim;
    public AudioClip snap;
    public AudioClip Jump;
    public AudioClip unSnap;

    public AudioClip flipGravity;
    public AudioClip reverseFlip;
    private audioController myAudio;
    private AudioSource footsteps;
    float resetTime = .1f;
    // Start is called before the first frame update
    void Start()
    {
        curFlashCooldown = flashCoolDownTime;
        curFlashes = maxFlashes;
        rb2d = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<audioController>();

        curDoubleJumps = doubleJumps;
        anim = GetComponent<Animator>();
        footsteps = GetComponents<AudioSource>()[1];

        GameManager.canFlash = false;
        GameManager.canFlipGravity = false;
        GameManager.canMove = false;
        GameManager.escaped = false;
        GameManager.hasDied = false;
        GameManager.reset = false;
        GameManager.lastCheckpointPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && GameManager.canFlipGravity && canFlip)
        {
            if (transform.localScale.y < 0)
            {
                myAudio.playSound(reverseFlip);
            }
            else
            {
                myAudio.playSound(flipGravity);
            }
            rb2d.gravityScale *= -1;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            flipVal *= -1;
            canFlip = false;
        }

        if (GameManager.canMove)
        {
            if (horizVelocity > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (grounded)
                {
                    anim.SetTrigger("Jump");
                    rb2d.velocity = new Vector2(rb2d.velocity.x, jumpModifier * flipVal);
                    myAudio.playSound(Jump);
                }
                else
                {
                    if (curDoubleJumps > 0)
                    {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpModifier * flipVal);
                        curDoubleJumps -= 1;
                        anim.SetTrigger("Jump");
                        myAudio.playSound(Jump);
                    }
                }
            }
        }
        if (GameManager.canFlash)
            flash();
        
        horizVelocity = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);


        
    }
    private void FixedUpdate()
    {
        if (resetTime > 0 && GameManager.reset)
        {
            resetTime -= Time.fixedDeltaTime;
        }
        if (resetTime <= 0)
        {
            resetTime = .1f;
            GameManager.reset = false;
        }
        if (startCountdown)
            fade -= Time.fixedDeltaTime / 20;

        SpriteRenderer[] color = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < color.Length; i++)
        {
            color[i].color = new Color(color[i].color.r, color[i].color.g, color[i].color.b, fade / 100);
            if (color[i].color.a <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        if (GameManager.canMove)
        {
            applyHorizontalMovement();

            if (horizVelocity != 0 && grounded)
            {
                if (!footsteps.isPlaying)
                    footsteps.Play();
            }
            else
            {
                if (footsteps.isPlaying)
                {
                    footsteps.Stop();
                }
            }
        }
        if (flipVal > 0)
        {
            if (rb2d.velocity.y < 0)
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * flipVal * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * flipVal * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
        else
        {
            if (rb2d.velocity.y > 0)
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * flipVal * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb2d.velocity.y < 0 && !Input.GetButton("Jump"))
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * flipVal * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
        
    }
    void applyHorizontalMovement()
    {
        Vector3 newVelocity = new Vector2(horizVelocity * Speed, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, newVelocity, ref refVel, smoothMovementModifier);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.x * horizVelocity);
    }
    void flash()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (curFlashes > 0 && flashTimer == 0)
            {
                if (!alreadyFlashed)
                {
                    alreadyFlashed = true;
                    myAudio.playNarration(5);
                }

                myAudio.playSound(snap);
                flashTimer = flashTime;
                Camera.main.backgroundColor = Color.black;
                curFlashes -= 1;
            }
        }
        if (flashTimer > 0)
        {
            flashTimer -= Time.fixedDeltaTime;
        }
        else if (Camera.main.backgroundColor == Color.black)
        {
            flashTimer = 0;
            Camera.main.backgroundColor = Color.white;
            myAudio.playSound(unSnap);
        }
        if (curFlashes < maxFlashes)
        {
            curFlashCooldown -= Time.fixedDeltaTime;
            if (curFlashCooldown <= 0)
            {
                curFlashes += 1;
                curFlashCooldown = flashCoolDownTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            rb2d.velocity = Vector2.zero;
            GameManager.reset = true;
            transform.position = GameManager.lastCheckpointPosition;
            fade -= 3;
            flipVal = 1;
            if (rb2d.gravityScale < 0)
            {
                rb2d.gravityScale *= -1;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            }
            if (!GameManager.hasDied)
            {
                GameManager.hasDied = true;
                myAudio.playNarration(7);
            }
        }
    }
}
