using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections; 

public class PlayerMovement : MonoBehaviour
{ 
    // --- VARIABEL & PENGATURAN ---
    [Header("Koleksi Item")]
    public int Apple = 0;      
    public int Strawberry = 0; 

    [Header("Pengaturan Gerakan")]
    public float speed = 5f;        
    public float jumpSpeed = 12f;   
    private float direction = 0f;   
    private Rigidbody2D player;     
    private Animator animator;      
    private Vector3 initialScale;   

    [Header("Ground Check (Pencegah Spam Loncat)")]
    public Transform groundCheck;     // Objek kosong di kaki pemain
    public float checkRadius = 0.2f;  // Jarak deteksi tanah
    public LayerMask whatIsGround;    // Pilih Layer "Ground" di Inspector
    private bool isGrounded;          // Status apakah menyentuh tanah

    [Header("Sistem Hati (Health)")]
    public int maxHearts = 3;       
    private int currentHearts;      
    private bool isDead = false;    
    private bool isInvincible = false; 
    public float invincibilityDuration = 1f; 

    [Header("Efek Visual Hit")]
    private SpriteRenderer spriteRenderer; 
    public Color damageColor = Color.red;   
    public float flashDuration = 0.2f;      

    [Header("Audio Settings")]
    public AudioSource audioSource; 
    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip healSound; 

    [Header("UI Reference")]
    [SerializeField] private GameObject pauseMenu; 

    // Awake dipanggil saat objek pertama kali dibuat
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start dipanggil sebelum frame pertama
    void Start()
    {
        currentHearts = maxHearts;
        initialScale = transform.localScale; 
        Time.timeScale = 1f; 
        
        // Mematikan kursor saat bermain agar tidak mengganggu
        ShowCursor(false); 

        if (spriteRenderer != null) spriteRenderer.color = Color.white; 
    }

    // Update dipanggil setiap frame
    void Update()
    {
        if (isDead) return; 

        if (Time.timeScale == 0f) return;

        // --- LOGIKA GROUND CHECK ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        direction = Input.GetAxis("Horizontal");

        // --- LOGIKA LOMPAT ---
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            PlaySFX(jumpSound, 0.7f);
        }

        // Mengatur animasi
        animator.SetBool("isRunning", Mathf.Abs(direction) > 0.01f);
        animator.SetBool("isGrounded", isGrounded); 
        animator.SetFloat("YVelocity", player.velocity.y);

        // Membalik arah hadap
        if (direction > 0) transform.localScale = initialScale;
        else if (direction < 0) transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
    }

    private void FixedUpdate()
    {
        if (isDead || Time.timeScale == 0f) return;
        player.velocity = new Vector2(direction * speed, player.velocity.y);
    }

    // --- SISTEM VISUAL DEBUG ---
    // Fungsi ini akan menggambar lingkaran merah di Scene View (hanya saat editing)
    // Gunakan ini untuk memastikan posisi GroundCheck sudah benar di kaki pemain
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }

    // --- SISTEM UI & KURSOR ---

    public void TogglePause()
    {
        if (pauseMenu == null)
            pauseMenu = GameObject.Find("PauseMenu");

        if (pauseMenu != null)
        {
            bool isActive = !pauseMenu.activeSelf;
            pauseMenu.SetActive(isActive);
            Time.timeScale = isActive ? 0f : 1f; 
            ShowCursor(isActive); 
        }
    }

    public void ShowCursor(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }

    // --- SISTEM HEALTH & DAMAGE ---

    public void TakeDamage()
    {
        if (isDead || isInvincible) return;

        currentHearts--; 
        PlaySFX(hurtSound, 1.0f); 
        
        StartCoroutine(DamageFlash());    
        StartCoroutine(BecomeInvincible()); 

        if (currentHearts <= 0) StartCoroutine(DieAndRespawn()); 
    }

    IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    IEnumerator DamageFlash()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = damageColor; 
            yield return new WaitForSeconds(flashDuration); 
            spriteRenderer.color = Color.white; 
        }
    }

    IEnumerator DieAndRespawn()
    {
        isDead = true; 
        player.velocity = Vector2.zero; 
        player.simulated = false; 
        animator.SetTrigger("die"); 
        
        yield return new WaitForSeconds(1.5f); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Deteksi Tabrakan
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deteksi jika menabrak musuh atau jebakan
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy"))
            TakeDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallZone"))
            StartCoroutine(DieAndRespawn());
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip, volume);
    }

    public int GetCurrentHearts() => currentHearts;

    public void Heal(int amount)
    {
        currentHearts = Mathf.Min(currentHearts + amount, maxHearts);
        if (healSound != null) PlaySFX(healSound, 1.0f);
    }
}