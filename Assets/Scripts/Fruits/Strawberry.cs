using UnityEngine;
using TMPro;

public class Strawberry : MonoBehaviour
{
    [SerializeField] private AudioClip strawberryClip;
    
    // Biarkan kosong jika Anda ingin menggunakan satu teks HUD saja
    [SerializeField] private TextMeshProUGUI hudStrawberryText; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerScript = collision.GetComponent<PlayerMovement>();

            if (playerScript != null)
            {
                // 1. Tambah jumlah di Player
                playerScript.Strawberry += 1;

                // 2. Mainkan Suara
                playerScript.PlaySFX(strawberryClip, 1.0f);

                // 3. Update HUD (Teks di pojok layar saat main)
                // Cari teks Strawberry secara otomatis jika belum diisi di Inspector
                if (hudStrawberryText == null) {
                    GameObject go = GameObject.Find("HUD_StrawberryText"); 
                    if(go != null) hudStrawberryText = go.GetComponent<TextMeshProUGUI>();
                }

                if (hudStrawberryText != null) {
                    hudStrawberryText.text = playerScript.Strawberry.ToString();
                }

                // 4. Hancurkan item
                Destroy(gameObject);
            }
        }
    }
}