using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource titleMusic;

    public AudioSource[] bgMusic;

    private int _currencyTrack;

    private bool _IsPaused;

    public AudioSource[] sfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _currencyTrack = 0;
    }

    private void Update()
    {
        if (_IsPaused == false)
        {
            if (bgMusic[_currencyTrack].isPlaying == false)
            {
                PlayNextMusic();
            }
        }
    }

    public void PlayMusic()
    {
        StopMusic();

        titleMusic.Play();
    }

    public void PlayNextMusic()
    {
        StopMusic();

        _currencyTrack++;

        if (_currencyTrack >= bgMusic.Length)
        {
            _currencyTrack = 0;
        }

        bgMusic[_currencyTrack].Play();
    }

    public void StopMusic()
    {
        foreach (AudioSource track in bgMusic)
        {
            track.Stop();
        }

        titleMusic.Stop();
    }

    public void PausedMusic()
    {
        _IsPaused = true;

        bgMusic[_currencyTrack].Pause();
    }

    public void ResumeMusic()
    {
        _IsPaused = false;

        bgMusic[_currencyTrack].Play();
    }    

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();

        sfx[sfxToPlay].Play();
    }

    public void PlaySFXPitchAdjusted(int sfxToPlay)
    {
        sfx[sfxToPlay].pitch = Random.Range(.8f, 1.2f);

        PlaySFX(sfxToPlay);
    }
}
