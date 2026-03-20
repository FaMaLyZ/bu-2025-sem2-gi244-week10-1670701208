using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;

    private Rigidbody rb;
    private InputAction jumpAction;
    private int jumpCount = 0;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;
    public bool isDash = false;

    public int playerHP;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity *= gravityModifier;

        jumpAction = InputSystem.actions.FindAction("Jump");

        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpAction.triggered && !gameOver && jumpCount <2)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            jumpCount++;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSfx);
        }   
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDash)
        {
            isDash = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {   
 
            isDash = false;
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (playerHP <= 0)
            {
                Debug.Log("Game Over!");
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                Instantiate(explosionParticle);
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSfx);
            }
            else 
            {
                Debug.Log("Player hit");
                playerHP--;
                Instantiate(explosionParticle);
                playerAudio.PlayOneShot(crashSfx);
                Destroy(collision.gameObject);

            }
        }
    }
    
}