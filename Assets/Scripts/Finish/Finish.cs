using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject winUI;
    public AudioManager audioManager; // Kita gunakan cara drag-and-drop agar lebih aman

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Pastikan audioManager sudah diisi di Inspector
            if (audioManager != null)
            {
                // Panggil variabel 'Win' sesuai yang ada di AudioManager
                audioManager.PlaySFX(audioManager.Win); 
            }

            UnlockNewLevel();
            winUI.SetActive(true);
            Time.timeScale = 0; 
        }
    }

    void UnlockNewLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex >= PlayerPrefs.GetInt("ReachedIndex", 1))
        {
            PlayerPrefs.SetInt("ReachedIndex", currentSceneIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}