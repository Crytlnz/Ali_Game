using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Finishhh : MonoBehaviour
{
    [Header("Event saat pemain mencapai finish")]
    public UnityEvent onFinishTrigger;

    private bool hasFinished = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasFinished && collision.CompareTag("Player"))
        {
            hasFinished = true;
            onFinishTrigger?.Invoke();
        }
    }

    // Fungsi untuk pindah ke scene selanjutnya
    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Ini adalah level terakhir.");
        }
    }

    // Fungsi untuk restart level saat ini
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Fungsi untuk kembali ke main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Pastikan nama scene main menu adalah "MainMenu"
    }
}
