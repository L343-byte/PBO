using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ChestSystem : MonoBehaviour {
    [Header("UI Link")]
    public GameObject quizPanel;
    public TextMeshProUGUI questionTextUI;
    public Button[] answerButtons;

    [Header("Audio Settings")]
    public AudioSource audioSource; 
    public AudioClip correctSound;  
    public AudioClip wrongSound;    

    [Header("Data")]
    public QuestionData questionData;
    private bool playerInRange = false;
    private bool isDestroying = false; 

    void Update() {
        // Cek jika pemain di dekat peti, menekan E, dan panel sedang tidak terbuka
        if (!isDestroying && playerInRange && Input.GetKeyDown(KeyCode.E) && quizPanel != null && !quizPanel.activeSelf) {
            OpenChest();
        }
    }

    void OpenChest() {
        if (quizPanel == null || questionData == null) {
            Debug.LogError("Quiz Panel atau Question Data belum diisi di Inspector!");
            return;
        }

        // Isi teks pertanyaan
        questionTextUI.text = questionData.questionText;

        // Atur tombol jawaban
        for (int i = 0; i < answerButtons.Length; i++) {
            var buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if(buttonText != null) buttonText.text = questionData.answers[i];

            int index = i;
            answerButtons[i].GetComponent<Image>().color = Color.white; 
            
            // Sembunyikan ikon salah (jika ada)
            Transform xIcon = answerButtons[i].transform.Find("WrongIcon");
            if(xIcon != null) xIcon.gameObject.SetActive(false);

            // Pasang fungsi klik
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => StartCoroutine(CheckAnswer(index)));
        }

        // Tampilkan Panel dan Pause Game
        quizPanel.SetActive(true);
        Time.timeScale = 0f; 
        
        // Aktifkan kursor agar bisa memilih jawaban
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None; 
    }

    IEnumerator CheckAnswer(int id) {
        if (id == questionData.correctAnswerIndex) {
            // JAWABAN BENAR
            isDestroying = true; 
            
            if (correctSound != null && audioSource != null) audioSource.PlayOneShot(correctSound);
            answerButtons[id].GetComponent<Image>().color = Color.green;

            if (GameManager.instance != null) GameManager.instance.ChestOpened();
            
            // Tunggu sebentar agar pemain bisa melihat warna hijau
            yield return new WaitForSecondsRealtime(0.5f);
            
            // Tutup panel kuis
            if (quizPanel != null) quizPanel.SetActive(false);
            
            // KEMBALIKAN KONTROL GAME
            Time.timeScale = 1f;
            Cursor.visible = false; 
            Cursor.lockState = CursorLockMode.None; 
            
            // Matikan collider agar tidak bisa diinteraksi lagi saat animasi hancur
            if (GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = false;
            if (GetComponent<SpriteRenderer>() != null) GetComponent<SpriteRenderer>().enabled = false;
            
            Destroy(gameObject, 0.1f); 
        } else {
            // JAWABAN SALAH
            if (wrongSound != null && audioSource != null) audioSource.PlayOneShot(wrongSound);
            
            answerButtons[id].GetComponent<Image>().color = Color.red;
            Transform specificIcon = answerButtons[id].transform.Find("WrongIcon");
            
            if (specificIcon != null) specificIcon.gameObject.SetActive(true); 
            
            yield return new WaitForSecondsRealtime(1f);
            
            // Reset warna tombol agar bisa mencoba lagi
            if (specificIcon != null) specificIcon.gameObject.SetActive(false);
            answerButtons[id].GetComponent<Image>().color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isDestroying) return;
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (isDestroying || quizPanel == null) return;
        
        if (other.CompareTag("Player")) {
            playerInRange = false;
            
            // LOGIKA YANG DISATUKAN:
            // Hanya menutup panel dan mereset kursor jika panel kuis sedang terbuka
            if (quizPanel.activeSelf) {
                quizPanel.SetActive(false);
                Time.timeScale = 1f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}