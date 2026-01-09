using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    [Header("Attack Settings")]
    [SerializeField] private float damage;
    [SerializeField] private AudioClip hitSound; // Slot untuk file suara di Inspector

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    // Menggunakan OnCollisionEnter2D agar deteksi tabrakan lebih akurat untuk objek fisik
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ambil script PlayerMovement dari objek pemain
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            
            if (player != null)
            {
                // 1. Memutar suara hit di posisi player
                player.PlaySFX(hitSound, 1.0f);
                
                // 2. Mengurangi darah player menggunakan fungsi TakeDamage yang sudah ada
                player.TakeDamage(); 
            }
        }
    }
}