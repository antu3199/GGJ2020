using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    public List<Sprite> sprites;

    public float transitionTime = 1;
    public bool repeat = false;
    public float repeatDelay = 0;

    public bool playOnStart = false;
    
    public bool isPlaying {get; set;}
    public int frameIndex{get; set;}

    float animTime;

    void Start() {
      if (playOnStart) {
        Play();
      }
    }

    void Update() {
      if (isPlaying) {
        animTime += Time.deltaTime;
        if (animTime >= transitionTime) {
          if (!repeat && frameIndex + 1 == sprites.Count) {
            Pause();
            return;
          }
          animTime = 0;
          frameIndex = (frameIndex + 1) % sprites.Count;
          spriteRend.sprite = sprites[frameIndex];
        }
      }
    }


    public void Play() {
      isPlaying = true;
    }

    public void Pause() {
      isPlaying = false;
    }

    public void Reset() {
      frameIndex = 0;
      animTime = 0;
      spriteRend.sprite = sprites[frameIndex];
    }

}
