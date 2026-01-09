using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Winner Settings")]
    public GameObject winUI; 
    public GameObject questionUI; 
    public TextMeshProUGUI chestScoreText; 
    
    [Header("Win UI Score (Panel Menang)")]
    public TextMeshProUGUI winAppleText;      
    public TextMeshProUGUI winStrawberryText; 

    [Header("HUD Settings (Pojok Layar)")]
    public TextMeshProUGUI hudAppleText; 
    public TextMeshProUGUI hudStrawberryText; 

    [Header("Game Stats")]
    public int totalChestsInLevel = 3;
    private int chestsOpened = 0;
    private int applesCollected = 0;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1f; 
        
        if (winUI != null) winUI.SetActive(false);
        if (questionUI != null) questionUI.SetActive(false);

        // PERUBAHAN: Paksa kursor muncul saat awal permainan
        ShowCursorPermanently();
    }

    void Update()
    {
        // PERUBAHAN: Pengaman kursor agar tetap ada setiap frame
        // Ini akan melawan script lain (seperti PlayerController) yang mencoba menyembunyikan kursor
        if (Cursor.visible == false || Cursor.lockState != CursorLockMode.None)
        {
            ShowCursorPermanently();
        }
    }

    // Fungsi khusus untuk memaksa kursor tampil
    public void ShowCursorPermanently()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void AddApple()
    {
        applesCollected++;
        if (hudAppleText != null) 
        {
            hudAppleText.text = applesCollected.ToString();
        }
    }

    public void ChestOpened()
    {
        chestsOpened++;
        if (chestsOpened >= totalChestsInLevel)
        {
            ShowWinUI();
        }
    }

    public void ShowWinUI()
    {
        if (winUI != null) winUI.SetActive(true); 
        
        if (chestScoreText != null) {
            chestScoreText.text = "CHEST DIDAPAT: " + chestsOpened + " / " + totalChestsInLevel;
        }

        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null) 
        {
            if (winAppleText != null) winAppleText.text = "APPLE DIDAPAT: " + player.Apple.ToString();
            if (winStrawberryText != null) winStrawberryText.text = "STRAWBERRY DIDAPAT: " + player.Strawberry.ToString();
        }

        Time.timeScale = 0f; 
        ShowCursorPermanently();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            SceneManager.LoadScene("MainMenu"); 
        }
    }
}