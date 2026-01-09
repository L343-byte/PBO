using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private float damage = 1f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mendeteksi jika yang masuk ke area trigger adalah Player
        if (collision.CompareTag("Player"))
        {
            // Mencari komponen Health di objek yang menabrak (Player)
            Health playerHealth = collision.GetComponent<Health>();

            if (playerHealth != null)
            {
                // Memanggil fungsi TakeDamage yang sudah kita buat tadi (termasuk efek merah & respawn)
                playerHealth.TakeDamage(damage);
                
                // Pesan di Console untuk memastikan sistem bekerja
                Debug.Log("Player menyentuh duri, memberikan damage: " + damage);
            }
        }
    }
}