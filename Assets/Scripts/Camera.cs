using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variabel untuk menyimpan referensi posisi pemain yang akan diikuti
    Transform target;

    // Digunakan secara internal oleh fungsi SmoothDamp untuk melacak kecepatan saat ini
    Vector3 velocity = Vector3.zero;

    // Mengatur kelembutan gerakan kamera (0 = instan, 1 = sangat lambat)
    [Range(0, 1)]
    public float smoothTime = 0.15f;

    // Memberikan jarak tambahan antara posisi kamera dan posisi pemain (misal: agar kamera lebih tinggi)
    public Vector3 positionOffset;

    // Header untuk merapikan tampilan di Inspector Unity
    [Header("Axis Limitation")]
    // xLimit.x adalah batas kiri, xLimit.y adalah batas kanan
    public Vector2 xLimit; 
    // yLimit.x adalah batas bawah, yLimit.y adalah batas atas
    public Vector2 yLimit; 

    private void Awake()
    {
        // Mencari objek di dalam scene yang memiliki Tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        // Jika ditemukan, simpan komponen Transform-nya ke variabel target
        if (player != null) target = player.transform;
    }

    // LateUpdate dipanggil setelah semua fungsi Update selesai (sangat baik untuk kamera agar tidak bergetar)
    private void LateUpdate()
    {
        // Jika target (pemain) tidak ditemukan atau hancur, jangan jalankan kode di bawahnya
        if (target == null) return;

        // Tentukan posisi tujuan kamera berdasarkan posisi pemain ditambah jarak offset
        Vector3 targetPosition = target.position + positionOffset;

        // Membatasi (Clamp) posisi X agar kamera tidak melewati batas kiri dan kanan map
        float clampedX = Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y);
        
        // Membatasi (Clamp) posisi Y agar kamera tidak melewati batas bawah dan atas map
        float clampedY = Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y);

        // Gabungkan hasil batasan tersebut. Z harus tetap di -10 agar objek 2D terlihat oleh kamera.
        Vector3 finalPosition = new Vector3(clampedX, clampedY, -10f);

        // Pindahkan posisi kamera dari posisi saat ini ke posisi tujuan secara halus (smooth)
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, smoothTime);
    }
}