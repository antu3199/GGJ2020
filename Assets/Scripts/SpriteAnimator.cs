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

    float repeatDelayCounter = 0;

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
          if ( frameIndex + 1 == sprites.Count) {
            if (!repeat) {
              Pause();
              return;
            }

            repeatDelayCounter += Time.deltaTime;
            if (repeatDelayCounter >= repeatDelay) {
              repeatDelayCounter = 0;
              frameIndex = -1;
              nextFrame();
            }

          } else {
            nextFrame();
          }
         
        }
      }
    }

    private void nextFrame() {
        animTime = 0;
        frameIndex = (frameIndex + 1) % sprites.Count;
        spriteRend.sprite = sprites[frameIndex];
    }


    public void Play() {
      isPlaying = true;
    }

    public void Pause() {
      isPlaying = false;
    }

    public void ResetAnim() {
      frameIndex = 0;
      animTime = 0;
      spriteRend.sprite = sprites[frameIndex];
      repeatDelayCounter = 0;
    }

}
