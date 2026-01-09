using System.Collections; // Dibutuhkan untuk menggunakan IEnumerator (Coroutine)
using UnityEngine;
using UnityEngine.SceneManagement; // Dibutuhkan untuk berpindah antar Scene (level)

public class SceneController : MonoBehaviour
{
    // Variabel static agar script ini bisa diakses dari mana saja tanpa referensi manual
    public static SceneController instance;

    // Slot untuk memasukkan Animator transisi (Canvas) di Inspector
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        // SISTEM SINGLETON: Memastikan hanya ada satu SceneController di dalam game
        if (instance == null)
        {
            instance = this;
            // Menjaga agar objek ini tidak hancur saat berpindah ke scene baru
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Jika ada SceneController ganda, hancurkan yang baru agar tidak bentrok
            Destroy(gameObject);
        }
    }

    // Fungsi publik yang bisa dipanggil oleh objek lain (misal: pintu finish atau tombol)
    public void NextLevel()
    {
        // Menjalankan fungsi LoadLevel secara asynchronous (berjalan di latar belakang)
        StartCoroutine(LoadLevel());
    }

    // Fungsi utama untuk mengatur alur transisi dan perpindahan scene
    IEnumerator LoadLevel()
    {
        // 1. Jalankan animasi keluar (misal: layar menjadi hitam) dengan memicu trigger "End"
        transitionAnim.SetTrigger("End");

        // 2. Tunggu selama 1 detik agar animasi selesai diputar sebelum pindah scene
        yield return new WaitForSeconds(1);

        // 3. Muat scene berikutnya berdasarkan urutan angka di Build Settings (Index saat ini + 1)
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        // 4. Setelah scene baru dimuat, jalankan animasi masuk (misal: layar menjadi terang)
        transitionAnim.SetTrigger("Start");
    }
}