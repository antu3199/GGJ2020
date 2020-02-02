using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AudioClipDict {
  public string key;
  public AudioClip value;
};

public class MusicManager : Singleton<MusicManager>
{
  public AudioSource musicSource;
  public AudioSource soundEffect;
  public List<AudioClipDict> clipDict;

  float defaultVolume;

  void Awake() {
    if (FindObjectsOfType(typeof(MusicManager)).Length > 1)
    {
      Destroy(this.gameObject);
    }

    DontDestroyOnLoad(this.gameObject);
    defaultVolume = soundEffect.volume;
  }

  public void PlaySound(string name, float volume = -1) {
    if (name == "") {
      return;
    }
    
    if (volume == -1) {
      volume = defaultVolume;
    }

    foreach (AudioClipDict clip in clipDict) {
      if (clip.key == name) {
        soundEffect.PlayOneShot(clip.value, volume);
        return;
      }
    }
    Debug.LogError("CLIP WAS NOT FOUND");

  }


  
}
