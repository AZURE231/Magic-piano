using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour
{
    public AssetsSound[] sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        if (AudioManager.Instance == null)
        {
            Instance = this;
        }
        else if (AudioManager.Instance != this)
        {
            Destroy(this.gameObject);
        }
        // Add AudioSource for each sound
        foreach (AssetsSound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        AssetsSound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + s.name + " not found!");
            return;
        }

        s.source.Play();

    }


    public void StopPlaying(string sound)
    {
        AssetsSound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        StartCoroutine(FadeOut(0, s));
    }

    private IEnumerator FadeOut(float delay, AssetsSound sound)
    {
        yield return new WaitForSeconds(delay);

        float fadeTime = 2f; // Time over which the sound will fade out
        float startVolume = sound.source.volume;
        float timeElapsed = 0f;

        // Gradually reduce the volume over time
        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            sound.source.volume = Mathf.Lerp(startVolume, 0, timeElapsed / fadeTime);
            yield return null; // Wait until the next frame
        }

        // Make sure the sound is fully stopped
        sound.source.Stop();
        sound.source.volume = startVolume; // Reset volume for next play
    }

    public bool IsPlayingSong(string sound)
    {
        AssetsSound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return false;
        }
        return s.source.isPlaying;
    }
}
