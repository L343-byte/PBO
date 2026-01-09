using UnityEngine;
using TMPro;

public class Apple : MonoBehaviour
{
    [SerializeField] private AudioClip appleClip;
    private TextMeshProUGUI appleText; 

    private void Start()
    {
        // Mencari teks HUD (tampilan apel saat sedang lari)
        GameObject appleUI = GameObject.FindWithTag("AppleText");
        if (appleUI != null) {
            appleText = appleUI.GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerScript = collision.gameObject.GetComponent<PlayerMovement>();

            if (playerScript != null)
            {
                // 1. Tambah data di Player (untuk HUD)
                playerScript.Apple += 1;
                
                // 2. LAPOR KE GAMEMANAGER (PENTING untuk WinUI)
                if (GameManager.instance != null) {
                    GameManager.instance.AddApple();
                }

                // 3. Putar suara
                playerScript.PlaySFX(appleClip, 1.0f); 

                // 4. Update teks HUD jika ada
                if (appleText != null) {
                    appleText.text = playerScript.Apple.ToString();
                }

                Destroy(gameObject);
            }
        }
    }
}