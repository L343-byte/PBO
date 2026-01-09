using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ada untuk memuat ulang level

public class RestartLevel : MonoBehaviour
{
    public void Restarting() // Saya ganti nama fungsinya agar tidak tabrakan dengan nama Class
    {
        // Sangat penting: Mengembalikan waktu game ke normal (1)
        Time.timeScale = 1f;

        // Memuat ulang scene yang sedang aktif saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}