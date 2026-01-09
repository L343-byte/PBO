using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    
    // Kita hapus void Update di sini karena sudah ditangani oleh PlayerMovement
    
    public void ResumeButton()
    {
        container.SetActive(false);
        Time.timeScale = 1f;

        // Pastikan kursor disembunyikan lagi jika game lanjut
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        
        // Munculkan kursor sebelum ke Main Menu agar bisa diklik
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("MainMenu");
    }
}