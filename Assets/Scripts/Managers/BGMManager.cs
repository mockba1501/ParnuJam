using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] backgroundMusic;
    
    #region Singleton
    public static BGMManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SetupAudio();
    }
    
    #endregion
    
    private void SetupAudio()
    {
        // Check for existing AudioSource (manual assignment)
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Create if still null
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("BGMManager: Audio Source auto created");
        }
                
        // Configure
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.volume = 0.147f;

        // Load clips if not manually assigned
        if (backgroundMusic == null || backgroundMusic.Length == 0)
        {
            LoadBGMusicFromResources();
        }
    }
    
    private void LoadBGMusicFromResources()
    {
        backgroundMusic = Resources.LoadAll<AudioClip>("Audio/BGMs/");
    }
    
    public void ManageBGM(int num) // num is index of backGroundMusics: 0=BgmStart, 1=BgmPlay, 2=BgmEnd
    {
        // All safety checks
        if (audioSource == null)
        {
            Debug.LogError($"BGMManager: AudioSource is null! Sound {num} not played.");
            return;
        }
        
        if (backgroundMusic == null || backgroundMusic.Length == 0)
        {
            Debug.LogError($"BGMManager: No audio clips loaded! Sound {num} not played.");
            return;
        }
        
        if (num < 0 || num >= backgroundMusic.Length)
        {
            Debug.LogError($"BGMManager: Sound index {num} out of range.");
            return;
        }
        
        if (backgroundMusic[num] == null)
        {
            Debug.LogError($"BGMManager: Audio clip at index {num} is null!");
            return;
        }
        
        if (audioSource.clip == backgroundMusic[num] && audioSource.isPlaying)
        {
            return;
        }
        
        audioSource.clip = this.backgroundMusic[num];
        audioSource.Play();
    }
}
