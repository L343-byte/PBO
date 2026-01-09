using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton: Memungkinkan skrip lain (seperti QuestionChest) mengakses AudioManager dengan mudah
    public static AudioManager instance;

    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource musicSource; // Untuk musik latar (Loop)
    [SerializeField] private AudioSource SFXSource;   // Untuk efek suara pendek (PlayOneShot)

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;   // Musik utama game
    public AudioClip correct;      // Dipanggil saat jawaban benar
    public AudioClip wrong;        // Dipanggil saat jawaban salah
    public AudioClip death;        // Suara saat karakter mati
    public AudioClip Win;          // Suara saat menang
    public AudioClip buttonClick;  // Suara klik tombol umum

    private void Awake()
    {
        // Logika Singleton: Memastikan hanya ada satu AudioManager di dalam game
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject); // Aktifkan jika ingin musik tidak terputus saat pindah level
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Memulai musik latar secara otomatis saat game dimulai
        if (background != null && musicSource != null)
        {
            musicSource.clip = background;
            musicSource.loop = true; // Musik akan terus berulang
            musicSource.Play();
        }
    }

    // Fungsi universal untuk memutar SFX tanpa memotong musik latar
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}