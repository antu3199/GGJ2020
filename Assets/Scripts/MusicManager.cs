using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
  public AudioSource musicSource;

  void Awake() {
    if (FindObjectsOfType(typeof(MusicManager)).Length > 1)
    {
      Destroy(this.gameObject);
    }

    DontDestroyOnLoad(this.gameObject);
  }
}
