using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class Options : MonoBehaviour
{
    public static Options Instance{ get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private AudioSource _audioSourceSceneMusic;
    [SerializeField] private AudioSource _audioClickButton;

    [Header("Slider")]
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderEffects;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (_audioSourceSceneMusic == null)
            _audioSourceSceneMusic = GetComponent<AudioSource>();

        if (_sliderMusic == null)
            _sliderMusic = transform.Find("Parametrs/Music/Slider Volume Music").GetComponent<Slider>();

        if (_sliderEffects == null)
            _sliderEffects = transform.Find("Parametrs/Effetcs/Slider Volume Effetcs").GetComponent<Slider>();
    }
    private void Start()
    {
        StartMusicOnCurrentScene();
    }
    /// <summary>
    /// запуск музыки на текущей сцене
    /// </summary>
    private void StartMusicOnCurrentScene()
    {
        _sliderMusic.value = 1;
        _sliderEffects.value = 1;

        if (_audioSourceSceneMusic != null)
            _audioSourceSceneMusic.Play();
    }
    /// <summary>
    ///  слайдер настройки громкости музыки
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeVolumeMusic(float volume)
    {
        _mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
    }
    /// <summary>
    ///  слайдер настройки громкости уффектов
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeVolumeEffects(float volume)
    {
        _mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, volume));
    }
    /// <summary>
    /// Звук нажатия кгопки по фефолту
    /// </summary>
    public void ButtonPressAudio()
    {
        if (_audioClickButton != null && _audioClickButton.clip != null)
            _audioClickButton.PlayOneShot(_audioClickButton.clip);
    }
}
