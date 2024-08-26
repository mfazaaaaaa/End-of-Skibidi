using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource mainMenuMusicSource;
    [SerializeField] AudioSource gameplayMusicSource;

    [Header("---------- Audio Mixer ----------")]
    [SerializeField] AudioMixer audioMixer;  // Referensi ke AudioMixer

    private static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Mencegah AudioManager dihancurkan saat berpindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // Tambahkan event listener saat scene berganti
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Hapus event listener saat script dihancurkan
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicBasedOnScene();  // Panggil fungsi ini setiap kali scene berubah
    }

    private void PlayMusicBasedOnScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MainMenu")
        {
            StopAllMusic();  // Stop semua musik sebelum memutar yang baru
            if (!mainMenuMusicSource.isPlaying)
            {
                mainMenuMusicSource.Play();
            }
        }
        else
        {
            StopAllMusic();  // Stop semua musik sebelum memutar yang baru
            if (!gameplayMusicSource.isPlaying)
            {
                gameplayMusicSource.Play();
            }
        }
    }

    private void StopAllMusic()
    {
        if (mainMenuMusicSource.isPlaying)
        {
            mainMenuMusicSource.Stop();
        }
        if (gameplayMusicSource.isPlaying)
        {
            gameplayMusicSource.Stop();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }
}
