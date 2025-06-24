using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public bool isMute = false;
    public AudioSource SFXSource;
    [Header("Audio Clips")]
    public AudioClip Win, Fail, Catch, Bomb;

    private Dictionary<SFXType, AudioClip> sfxMap;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        sfxMap = new Dictionary<SFXType, AudioClip>
        {
            { SFXType.Win, Win },
            { SFXType.Fail, Fail },
            { SFXType.Catch, Catch },
            { SFXType.Bomb, Bomb }
        };
    }

    public void Play(SFXType type)
    {
        if (isMute || !sfxMap.ContainsKey(type)) return;

        SFXSource.PlayOneShot(sfxMap[type]);
    }

    public void ToggleMute(out bool isMuted)
    {
        isMute = !isMute;
        SFXSource.mute = isMute;
        isMuted = isMute;
    }
}
public enum SFXType
{
    Win,
    Fail,
    Catch,
    Bomb
}
