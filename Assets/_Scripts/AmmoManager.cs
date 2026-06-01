using UnityEngine;
using TMPro; // Wajib ditambahkan agar Unity mengenali TextMeshPro

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance;

    [Header("Ammo Settings")]
    public int maxAmmo = 20;
    public int currentAmmo;

    [Header("UI Settings")]
    public TMP_Text ammoText; // Ini yang akan memunculkan kolom di Inspector

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    public bool TryShoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateAmmoUI();
            return true;
        }
        else
        {
            if (ammoText != null)
            {
                ammoText.text = "AMMO: 0 (RELOAD!)";
                ammoText.color = Color.red;
            }
            return false;
        }
    }

    public void RefillAmmo()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();

        if (ammoText != null)
        {
            ammoText.color = Color.white;
        }
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "AMMO: " + currentAmmo;
        }
    }
}