using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Penting: Tambahkan ini agar Button bisa terbaca

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons; // Array untuk menampung semua tombol level

    private void Awake()
    {
        // Mengambil data level mana saja yang sudah terbuka, default adalah level 1
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Mematikan semua interaksi tombol terlebih dahulu
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        // Menyalakan tombol hanya untuk level yang sudah terbuka
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }
}