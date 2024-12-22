using UnityEngine;

public class Player : MonoBehaviour
{
    private int lives = 3;
    private bool isInvincible = false;



    [Header("Settings")]
    public float jumpForce = 10f;

    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    private bool isGrounded = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetInteger("State", 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            if (!isGrounded)
            {
                animator.SetInteger("State", 2);
            }
            isGrounded = true;
            
        }
    }

    void Hit()
    {
        GameManager.Instance.lives -= 1;
        
    }

    void Heal()
    {
        GameManager.Instance.lives = Mathf.Min(3, lives + 1);
    }

    public void KillPlayer()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
        rb.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    void StopInvincible()
    {
        isInvincible = false;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
            if(!isInvincible)
            {
                Destroy(collider.gameObject);
                Hit();

            }
        }
        else if(collider.gameObject.CompareTag("Food"))
        {
            Destroy(collider.gameObject);
            Heal();
        }
        else if (collider.gameObject.CompareTag("GoldBaechu"))
        {
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}
