using UnityEngine;

public class BacksoundManager : MonoBehaviour
{
    private static BacksoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Tetap hidup di semua scene
        }
        else
        {
            Destroy(gameObject); // Hapus duplikat saat scene dimuat ulang
        }
    }
}
