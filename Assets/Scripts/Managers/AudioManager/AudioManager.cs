using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance { get; set; }

    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;

    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip dangerousMusic;

    [SerializeField] List<AudioClip> attackSounds;
    [SerializeField] List<AudioClip> equipSounds;
    [SerializeField] AudioClip pickUpSound;
    [SerializeField] AudioClip gameOverSound;
    
    private void Awake() {
        instance = this;
        SoundSaveManager.Init();
        if (musicSlider)
            musicSlider.onValueChanged.AddListener(ChangeAudioSourceVolume);

        if (soundSlider)
            soundSlider.onValueChanged.AddListener(ChangeSoundFXVolume);
    }
    void Start() {
        LoadMusicAndSoundVolume();
        musicSource.Play();
    }

    public void PlaySound(List<AudioClip> sounds) {
        soundSource.PlayOneShot(sounds[Random.Range(0, sounds.Count)], soundSource.volume);
    }
    public void PlayGameMusic() {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }
    public void PlayDangerousMusic() {
        musicSource.clip = dangerousMusic;
        musicSource.Play();
    }
    public void PlayEquipSound() {
        soundSource.PlayOneShot(equipSounds[Random.Range(0, equipSounds.Count)], soundSource.volume);
    }

    public void PlayPickupSound() { 
        soundSource.PlayOneShot(pickUpSound, soundSource.volume);
    }
    public void PlayGameOverSound() {
        musicSource.Stop();
        soundSource.PlayOneShot(gameOverSound, soundSource.volume);
    }

    public void SaveMusicSoundVolume() {
        SaveProps saveProps = new SaveProps();
        saveProps.musicVolume = musicSource.volume;
        saveProps.soundVolume = soundSource.volume;
        string newLoad = JsonUtility.ToJson(saveProps);
        SoundSaveManager.Save(newLoad);
    }
    public void LoadMusicAndSoundVolume() {
        string loadString = SoundSaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(loadString);

        if(musicSource)
        musicSource.volume = saveProps.musicVolume;

        if (musicSlider != null)
            musicSlider.value = saveProps.musicVolume;

        if(soundSource)
        soundSource.volume = saveProps.soundVolume;

        if (soundSlider != null)
            soundSlider.value = saveProps.soundVolume;
    }
    private void ChangeAudioSourceVolume(float volume) {
        musicSource.volume = volume;
    }
    private void ChangeSoundFXVolume(float volume) {
        soundSource.volume = volume;
    }
}
