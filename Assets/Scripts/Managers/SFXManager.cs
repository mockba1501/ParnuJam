using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] sfxMusicClips;
    
    #region Singleton
    public static SFXManager Instance { get; private set; }
    
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
    
    private void OnEnable()
    {
        PlantManager.OnPlantSold += HandlePlantSold;
        PlantManager.OnFertilizerApplied += HandleFertilizerApplied;
    }
    private void OnDisable()
    {
        PlantManager.OnPlantSold -= HandlePlantSold;
        PlantManager.OnFertilizerApplied -= HandleFertilizerApplied;
    }

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
            Debug.Log("SFXManager: Audio Source auto created");
        }
                
        // Configure
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = 1f;

        // Load clips if not manually assigned
        if (sfxMusicClips == null || sfxMusicClips.Length == 0)
        {
            LoadAudioClipsFromResources();
        }
    }

    private void LoadAudioClipsFromResources()
    {
        sfxMusicClips = Resources.LoadAll<AudioClip>("Audio/SFXs/");
    }
    
        
    public void ManageSFX(int num) // num is index of sfxMusics: 0=, 1= , 2=
    {
        // All safety checks
        if (audioSource == null)
        {
            Debug.LogError($"SFXManager: AudioSource is null! Sound {num} not played.");
            return;
        }
        
        if (sfxMusicClips == null || sfxMusicClips.Length == 0)
        {
            Debug.LogError($"SFXManager: No audio clips loaded! Sound {num} not played.");
            return;
        }
        
        if (num < 0 || num >= sfxMusicClips.Length)
        {
            Debug.LogError($"SFXManager: Sound index {num} out of range.");
            return;
        }
        
        if (sfxMusicClips[num] == null)
        {
            Debug.LogError($"SFXManager: Audio clip at index {num} is null!");
            return;
        }
        
        audioSource.PlayOneShot(this.sfxMusicClips[num]);
    }

    private void HandlePlantSold(int value)
    {
        ManageSFX(0); // Harvest sound
    }

    private void HandleFertilizerApplied(bool isCorrect)
    {
        if(isCorrect)
        {
            ManageSFX(1); // Correct mix sound
        }
        else
        {
            ManageSFX(4); // Incorrect mix sound
        }
    }
}
