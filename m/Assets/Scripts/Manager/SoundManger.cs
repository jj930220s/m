using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger instance;

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    private AudioSource musicAudioSource;
    public AudioClip musicClip;

    public SoundSource soundSourcePrefab;

    private void Awake()
    {
        instance = this;
        musicAudioSource=GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    private void Start()
    {
        ChangeBackgroundMusic(musicClip);
    }

    public void ChangeBackgroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();    
        musicAudioSource.clip=clip;    
        musicAudioSource.Play();    
    }

    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }












}
