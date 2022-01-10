using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Music")] [SerializeField] private AudioClip musicClip;

    [Header("Sounds")] [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip dieClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip pointClip;
    [SerializeField] private AudioClip revivePointClip;
    [SerializeField] private AudioClip winClip;

    public AudioClip WalkClip => walkClip;
    public AudioClip ShootClip => shootClip;
    public AudioClip JumpClip => jumpClip;
    public AudioClip DieClip => dieClip;
    public AudioClip CoinClip => coinClip;
    public AudioClip PointClip => pointClip;
    public AudioClip DashClip => dashClip;
    public AudioClip RevivePointClip => revivePointClip;

    public AudioClip WinClip => winClip;
    public AudioSource musicAudioSource;
    private ObjectPooler soundObjectPooler;

    protected override void Awake()
    {
        soundObjectPooler = GetComponent<ObjectPooler>();
        musicAudioSource = GetComponent<AudioSource>();

        PlayMusic();
    }

    private void PlayMusic()
    {
        musicAudioSource.loop = true;
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }

    public void PlaySound(AudioClip clipToPlay, float volume)
    {
        GameObject audioPooled = soundObjectPooler.GetObjectFromPool();
        AudioSource audioSource = null;

        if (audioPooled != null)
        {
            audioPooled.SetActive(true);
            audioSource = audioPooled.GetComponent<AudioSource>();
        }

        audioSource.clip = clipToPlay;
        audioSource.volume = volume;
        audioSource.Play();

        StartCoroutine(ReturnToPool(audioPooled, clipToPlay.length + 1));
    }

    private IEnumerator ReturnToPool(GameObject objectPool, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.SetActive(false);
    }
}



