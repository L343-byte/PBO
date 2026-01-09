using UnityEngine;

public class UniversalPause : MonoBehaviour
{
    // Seret objek PauseMenu Anda ke sini di Inspector
    public GameObject pauseMenuUI;

    void Update()
    {
        // Pengecekan tombol ESC yang sangat sederhana
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI != null)
            {
                bool isPaused = !pauseMenuUI.activeSelf;
                pauseMenuUI.SetActive(isPaused);
                
                // Atur waktu dan kursor
                Time.timeScale = isPaused ? 0f : 1f;
                Cursor.visible = isPaused;
                Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
                
                Debug.Log("Status Pause: " + isPaused);
            }
            else
            {
                Debug.LogWarning("Objek PauseMenuUI belum dimasukkan ke Inspector!");
            }
        }
    }
}