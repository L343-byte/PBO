using UnityEngine;
using UnityEngine.SceneManagement; // Ini wajib ada agar SceneManager berfungsi

public class NextLevel : MonoBehaviour
{
    // Fungsi untuk pindah ke level selanjutnya secara otomatis
    public void LoadNextLevel()
    {
        // 1. Ambil index level sekarang dan tambah 1
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // 2. Cek apakah level selanjutnya ada di Build Settings
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            
            // 3. Kembalikan waktu ke normal (Penting!)
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("Sudah di level terakhir!");
        }
    }
}