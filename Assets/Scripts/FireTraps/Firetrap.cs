using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay = 1.5f; 
    [SerializeField] private float activeTime = 2f;      

    private Animator anim;
    private SpriteRenderer spriteRender;

    private bool triggered; 
    private bool active;    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 1. Jika pemain menginjak, mulai proses aktivasi (merah)
            if (!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }

            // 2. HANYA beri damage jika status 'active' TRUE
            // Kita tambahkan pengecekan tambahan: pastikan api benar-benar sedang aktif
            if (active)
            {
                PlayerMovement player = collision.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.TakeDamage(); 
                }
            }
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        // FASE PERINGATAN (Hanya warna, belum ada damage)
        spriteRender.color = Color.red; 
        active = false; // Memastikan damage mati di fase ini

        yield return new WaitForSeconds(activationDelay);

        // FASE AKTIF (Api Keluar)
        spriteRender.color = Color.white; 
        anim.SetBool("activated", true); 
        
        // JEDA KECIL: Menunggu animasi api muncul sedikit sebelum menyalakan damage
        yield return new WaitForSeconds(0.1f); 
        active = true; // SEKARANG BARU BISA MEMBERI DAMAGE

        yield return new WaitForSeconds(activeTime);

        // FASE RESET (Api Padam)
        active = false; // Matikan damage sebelum animasi hilang
        anim.SetBool("activated", false); 
        
        yield return new WaitForSeconds(0.5f); // Jeda sebelum trap bisa dipicu lagi
        triggered = false; 
    }
}