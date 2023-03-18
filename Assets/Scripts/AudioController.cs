using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    [Header("====AudioSources====")]
    [SerializeField] AudioSource[] _soundSources;
    [SerializeField] AudioSource[] _musicSources;


    [Space(20)]
    [Header("====AudioClips====")]
    [SerializeField] AudioClip _startMusic;
    [SerializeField] AudioClip _mainMusic;


    [Space(20)]
    [Header("====Sliders====")]
    [SerializeField] Slider _soundSlider;
    [SerializeField] Slider _musicSlider;

    [Space(20)]
    [Header("====Keys====")]
    [SerializeField] string _soundKey;
    [SerializeField] string _musicKey;


    private void Awake()
    {
        Instance = this;
        StartCoroutine(SwitchMusic());
    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey(_soundKey)) PlayerPrefs.SetFloat(_soundKey, 0);
        _soundSlider.value = PlayerPrefs.GetFloat(_soundKey);
        foreach (AudioSource soundSource in _soundSources) soundSource.volume = _soundSlider.value;

        if (!PlayerPrefs.HasKey(_musicKey)) PlayerPrefs.SetFloat(_musicKey, 0);
        _musicSlider.value = PlayerPrefs.GetFloat(_musicKey);
        foreach (AudioSource musicSource in _musicSources) musicSource.volume = _musicSlider.value;
    }

    public void PlaySound(int index)
    {
        _soundSources[index].Play();
    }


    public void ChangeSoundVolume()
    {
        PlayerPrefs.SetFloat(_soundKey, _soundSlider.value);

        foreach (AudioSource soundSource in _soundSources) soundSource.volume = _soundSlider.value;
    }
    public void ChangeMusicVolume()
    {
        PlayerPrefs.SetFloat(_musicKey, _musicSlider.value);

        foreach (AudioSource musicSource in _musicSources) musicSource.volume = _musicSlider.value;
    }


    IEnumerator SwitchMusic()
    {
        yield return new WaitForSeconds(_startMusic.length - 2.7f);
        _musicSources[0].clip = _mainMusic;
        _musicSources[0].Play();
    }
}
