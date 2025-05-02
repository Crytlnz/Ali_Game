using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundSlider : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("UI Slider")]
    public Slider volumeSlider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Set nilai awal dari slider
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("BackgroundVolume", 1f);
            audioSource.volume = volumeSlider.value;

            // Tambahkan listener untuk mengatur volume saat slider berubah
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    private void SetVolume(float volume)
    {
        audioSource.volume = volume;

        // Simpan pengaturan volume
        PlayerPrefs.SetFloat("BackgroundVolume", volume);
        PlayerPrefs.Save();
    }

    // Berhenti saat pause
    public void PauseSound()
    {
        audioSource.Pause();
    }

    // Lanjutkan saat resume
    public void ResumeSound()
    {
        audioSource.UnPause();
    }
}