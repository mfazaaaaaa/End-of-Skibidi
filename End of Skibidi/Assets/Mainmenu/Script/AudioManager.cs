using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }  // Singleton instance

    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource mainMenuMusicSource;
    [SerializeField] private AudioSource gameplayMusicSource;
    [SerializeField] private AudioSource sfxSource;  // AudioSource untuk SFX

    [Header("---------- Audio Mixer ----------")]
    [SerializeField] private AudioMixer audioMixer;  // Referensi ke AudioMixer

    private float sfxVolume = 1f;  // Volume default SFX

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // Event listener untuk scene change
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Hapus event listener saat script dihancurkan
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicBasedOnScene();  // Atur musik sesuai dengan scene
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
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);  // Putar SFX dengan volume dari AudioMixer
        }
        else
        {
            Debug.LogWarning("SFXSource or AudioClip is not set!");
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;  // Simpan volume SFX dari slider
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);  // Atur volume melalui AudioMixer
    }

    public bool IsMainMenuMusicPlaying()
    {
        return mainMenuMusicSource.isPlaying;
    }
}
