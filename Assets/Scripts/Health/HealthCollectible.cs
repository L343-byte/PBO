using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private int healthValue = 1; // Jumlah hati yang ditambah
    [SerializeField] private AudioClip collectSound; // Suara saat mengambil hati

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah yang menabrak adalah Player
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            // Pastikan player tidak dalam kondisi darah penuh sebelum mengambil
            if (player != null && player.GetCurrentHearts() < player.maxHearts)
            {
                // 1. Tambah darah player
                player.Heal(healthValue);

                // 2. Putar suara (opsional)
                if (player.audioSource != null && collectSound != null)
                {
                    player.PlaySFX(collectSound, 1.0f);
                }

                // 3. Hapus objek hati dari map
                Destroy(gameObject);
            }
        }
    }
}