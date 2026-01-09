using UnityEngine;

public class UICursorFixer : MonoBehaviour
{
    void OnEnable()
    {
        // Munculkan kursor saat panel aktif
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // Terus paksa kursor muncul agar tidak "dicuri" script lain
        if (Cursor.visible == false || Cursor.lockState != CursorLockMode.None)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void OnDisable()
    {
        // Opsional: Sembunyikan kembali saat panel ditutup (jika ingin kembali main)
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }
}