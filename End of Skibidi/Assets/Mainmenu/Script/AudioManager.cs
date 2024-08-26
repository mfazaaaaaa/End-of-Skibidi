using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource mainMenuMusicSource;
    [SerializeField] private AudioSource gameplayMusicSource;

    private static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mencegah AudioManager dihancurkan saat berpindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusicBasedOnScene();
        SceneManager.sceneLoaded += OnSceneLoaded; // Tambahkan event listener saat scene berganti
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Hapus event listener saat script dihancurkan
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene Loaded: {scene.name}"); // Debugging
        PlayMusicBasedOnScene(); // Panggil fungsi ini setiap kali scene berubah
    }

    private void PlayMusicBasedOnScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log($"Current Scene Name: {sceneName}"); // Debugging

        if (sceneName == "MainMenu")
        {
            if (!mainMenuMusicSource.isPlaying)
            {
                Debug.Log("Playing Main Menu Music"); // Debugging
                gameplayMusicSource.Stop();
                mainMenuMusicSource.Play();
            }
        }
        else if (sceneName.StartsWith("Level1")) // Misalnya nama scene gameplay dimulai dengan "Level"
        {
            if (!gameplayMusicSource.isPlaying)
            {
                Debug.Log("Playing Gameplay Music"); // Debugging
                mainMenuMusicSource.Stop();
                gameplayMusicSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"Scene {sceneName} tidak dikenali untuk pemutaran musik"); // Debugging
        }
    }
}
