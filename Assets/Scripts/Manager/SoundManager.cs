using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    #region SINGLETON
    public static SoundManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion SINGLETON

    [SerializeField]
    private AudioClip menuClip;

    [SerializeField]
    private AudioClip gameClip;

    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
        audioSource.clip = menuClip;
        audioSource.Play();
    }

    public void PlayGameClip()
    {
        audioSource.Stop();
        audioSource.clip = gameClip;
        audioSource.Play();
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }

    public void VolumeUp()
    {
        audioSource.volume += 0.1f;
    }

    public void VolumeDown()
    {
        audioSource.volume -= 0.1f;
    }
}
