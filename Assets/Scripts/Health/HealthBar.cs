using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Referensi")]
    public PlayerMovement playerMovement; 
    public Image[] hearts; // Array untuk menampung 3 objek gambar hati
    
    [Header("Sprites")]
    public Sprite fullHeart;  // Sprite hati merah utuh
    public Sprite emptyHeart; // Sprite hati hitam/kosong

    void Update()
    {
        // Pastikan referensi ke player ada
        if (playerMovement == null) return;

        // Mendapatkan jumlah hati saat ini dari player
        int currentHearts = playerMovement.GetCurrentHearts();

        for (int i = 0; i < hearts.Length; i++)
        {
            // Jika index hati lebih kecil dari nyawa sekarang, tampilkan hati penuh
            if (i < currentHearts)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true; // Pastikan gambar muncul
            }
            // Jika lebih besar, tampilkan hati kosong
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}