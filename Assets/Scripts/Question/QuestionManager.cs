using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public void JawabanBenar()
    {
        Debug.Log("Jawaban kamu BENAR!");
        // Tambahkan logika di sini (misal: tambah poin, lanjut soal)
    }

    public void JawabanSalah()
    {
        Debug.Log("Jawaban kamu SALAH!");
        // Tambahkan logika di sini (misal: kurangi nyawa)
    }
}