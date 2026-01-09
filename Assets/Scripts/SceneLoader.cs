using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject winPanel; // Tarik WinUI ke sini di Inspector

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        // Memastikan kursor muncul sebelum pindah scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        // Memastikan kursor tidak terkunci saat level diulang
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenWinPanel()
    {
        // Panggil ini saat pemain menang (misal dari GameManager)
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        // Memaksa kursor muncul agar bisa klik tombol Restart/Menu
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Menghentikan waktu game (opsional, jika ingin game pause saat menang)
        Time.timeScale = 0f; 
    }
}