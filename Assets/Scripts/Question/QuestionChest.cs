using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class QuestionChest : MonoBehaviour 
{
    [Header("UI References")]
    public GameObject questionPanel;
    public TMP_Text questionText;
    [TextArea(3,10)]
    public string isiPertanyaan;

    [Header("Audio Settings")]
    public AudioSource audioSource; 
    public AudioClip soundBenar;
    public AudioClip soundSalah;

    [Header("Efek Salah (Warna)")]
    public Image[] wrongButtonsImages; 
    public Color errorColor = Color.red;
    private Color originalColor;

    private bool isPlayerNearby = false;
    private bool isProcessing = false;

    void Start() {
        if (questionPanel != null) questionPanel.SetActive(false);
        if (wrongButtonsImages != null && wrongButtonsImages.Length > 0)
            originalColor = wrongButtonsImages[0].color;
        if (audioSource != null) audioSource.playOnAwake = false;
    }

    void Update() {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !questionPanel.activeSelf && !isProcessing) {
            questionPanel.SetActive(true);
            questionText.text = isiPertanyaan;
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void KlikBenar() {
        if (isProcessing) return;
        isProcessing = true;
        if (audioSource != null && soundBenar != null) audioSource.PlayOneShot(soundBenar);
        Invoke("TutupDanHancurkan", 0.5f);
    }

    public void KlikSalah() {
        if (isProcessing) return;
        if (audioSource != null && soundSalah != null) audioSource.PlayOneShot(soundSalah);
        StartCoroutine(EfekSalah());
    }

    IEnumerator EfekSalah() {
        isProcessing = true;
        foreach (Image img in wrongButtonsImages) { if (img != null) img.color = errorColor; }
        yield return new WaitForSecondsRealtime(0.8f);
        foreach (Image img in wrongButtonsImages) { if (img != null) img.color = originalColor; }
        isProcessing = false;
    }

    void TutupDanHancurkan() {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Player")) isPlayerNearby = true; }
    private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("Player")) isPlayerNearby = false; }
}