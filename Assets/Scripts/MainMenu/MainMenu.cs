using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Dibutuhkan untuk memproses perpindahan antar Scene (level)

public class MainMenu : MonoBehaviour
{
    // Fungsi ini dipanggil saat tombol "Play" atau "Start" ditekan
    public void PlayGame()
    {
        // Memuat scene berikutnya berdasarkan index angka di Build Settings
        // Index 1 biasanya adalah level pertama setelah scene Main Menu (index 0)
        // LoadSceneAsync digunakan agar proses pemuatan berjalan di latar belakang (lebih lancar)
        SceneManager.LoadSceneAsync(1);
    }

    // Fungsi ini dipanggil saat tombol "Quit" atau "Keluar" ditekan
    public void QuitGame()
    {
        // Menutup aplikasi/game secara keseluruhan
        // Catatan: Perintah ini hanya bekerja setelah game di-build (tidak berfungsi di dalam Editor Unity)
        Application.Quit();
    }
}