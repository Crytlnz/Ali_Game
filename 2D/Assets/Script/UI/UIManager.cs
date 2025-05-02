using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // Referensi ke Panel Game Over

    // Fungsi ini dipanggil ketika karakter mati
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);  // Menampilkan panel Game Over
        Time.timeScale = 0f;  // Menjeda waktu permainan
    }

    // Fungsi untuk me-restart game
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Menjalankan kembali permainan
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Memuat ulang scene yang sama
    }

    // Fungsi untuk kembali ke main menu
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;  // Menjalankan kembali permainan
        SceneManager.LoadScene("MainMenu");  // Ganti dengan nama scene Main Menu kamu
    }
}
