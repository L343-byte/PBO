using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/Question")]
public class QuestionData : ScriptableObject 
{
    [Header("Konten Pertanyaan")]
    [TextArea(4, 10)]
    public string questionText;

    [Header("Opsi Jawaban")]
    public string[] answers;

    [Header("Pengaturan Jawaban")]
    [Tooltip("Index dimulai dari 0 (0 = pilihan pertama, 1 = kedua, dst)")]
    public int correctAnswerIndex;
}