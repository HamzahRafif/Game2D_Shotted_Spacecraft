using UnityEngine;

public class ExplosionDeactivator : MonoBehaviour
{
    // Fungsi ini akan dipanggil otomatis oleh Unity saat animasi persis di frame terakhir
    public void DeactivateExplosion()
    {
        gameObject.SetActive(false);
    }
}