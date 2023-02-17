using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public LevelController levelController;
    public float playerSpeed = 7f;
    Rigidbody2D rb_player;
    float horizontalInput = 0f;
    float verticalInput = 0f;
    bool isDash = false;
    float dashTime = 0f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 5f;
    float currentDashCooldown = 0f;
    public float dashPower = 15f;
    public Image dashCooldownImage;
    bool isStun = false;
    float stunTime = 0f;
    public float stunDuration = 0.5f;
    public float stunCooldown = 5f;
    float currentStunCooldown = 0f;
    public Image stunCooldownImage;
    public GameObject stunAreaObject;
    Animator playerAnim;
    public AudioSource dashSound;
    public AudioSource stunSound;

    // Start is called before the first frame update
    void Start()
    {
        rb_player = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //turn off action when paused
        if (Time.timeScale <= 0f)
        {
            return;
        }

        //movement input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0f)
        {
            animUp();
        }
        else if (verticalInput < 0f)
        {
            animDown();
        }
        else if (horizontalInput > 0f)
        {
            animRight();
        }
        else if (horizontalInput < 0f)
        {
            animLeft();
        }

        //stun
        if (Input.GetButtonDown("Jump") && currentStunCooldown <= 0f)
        {
            stunTime = stunDuration;
            currentStunCooldown = stunCooldown;
            stunCooldownImage.fillAmount = 0;
            isStun = true;
            stunSound.Play();
        }
        if (stunTime > 0 && isStun)
        {
            stunTime -= Time.deltaTime;
            horizontalInput = 0f;
            verticalInput = 0f;
        }
        else if (stunTime <= 0 && isStun)
        {
            Destroy(Instantiate(stunAreaObject, new Vector2(transform.position.x, transform.position.y - 0.75f), Quaternion.Euler(0, 0, 0)), 0.5f);
            isStun = false;
        }
        if (currentStunCooldown > 0 && !isStun)
        {
            currentStunCooldown -= Time.deltaTime;
            stunCooldownImage.fillAmount += Time.deltaTime / stunCooldown;
        }

        //dash
        if (Input.GetButtonDown("Fire3") && currentDashCooldown <= 0f && !isStun)
        {
            dashTime = dashDuration;
            currentDashCooldown = dashCooldown;
            dashCooldownImage.fillAmount = 0;
            isDash = true;
            dashSound.Play();
        }
        if (dashTime > 0 && isDash)
        {
            dashTime -= Time.deltaTime;
            rb_player.velocity = new Vector2(horizontalInput * dashPower, verticalInput * dashPower);
        }
        else if (dashTime <= 0 && isDash)
        {
            isDash = false;
            rb_player.velocity = new Vector2(0, 0f);
        }
        if (currentDashCooldown > 0 && !isDash)
        {
            currentDashCooldown -= Time.deltaTime;
            dashCooldownImage.fillAmount += Time.deltaTime / dashCooldown;
        }

        //apply movement
        transform.position = transform.position + new Vector3(horizontalInput * playerSpeed * Time.deltaTime, verticalInput * playerSpeed * Time.deltaTime, 0);
    }

    void animLeft()
    {
        playerAnim.SetBool("left", true);
        playerAnim.SetBool("right", false);
        playerAnim.SetBool("up", false);
        playerAnim.SetBool("down", false);
    }
    void animRight()
    {
        playerAnim.SetBool("left", false);
        playerAnim.SetBool("right", true);
        playerAnim.SetBool("up", false);
        playerAnim.SetBool("down", false);
    }
    void animUp()
    {
        playerAnim.SetBool("left", false);
        playerAnim.SetBool("right", false);
        playerAnim.SetBool("up", true);
        playerAnim.SetBool("down", false);
    }
    void animDown()
    {
        playerAnim.SetBool("left", false);
        playerAnim.SetBool("right", false);
        playerAnim.SetBool("up", false);
        playerAnim.SetBool("down", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            levelController.DeadgeScreen();
        }
        if (collision.collider.name == "NextStageTrigger")
        {
            levelController.NextStage();
        }
        if(collision.collider.CompareTag("Bullet"))
        {
            Destroy(collision.collider.gameObject);
            levelController.DeadgeScreen();
        }
    }
}
