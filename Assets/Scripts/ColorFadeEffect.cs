using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFadeEffect : MonoBehaviour
{

  public SpriteRenderer spriteRend;
  public Color colorTo;
  public bool goBackToNormal;
  public bool destroyOnDone;

  public float fadeSpeed = 1;

  public bool isFading;

  public bool repeat;
  public bool playOnStart = false;

  float fadeCounter;
  bool isGoingTo;

  Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = spriteRend.color;
        if (playOnStart) {
          StartFade();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading) {
          if (isGoingTo) {
            fadeCounter += Time.deltaTime * fadeSpeed;
            spriteRend.color = Color.Lerp(initialColor, colorTo, fadeCounter);
            if (fadeCounter >= 1) {
              if (goBackToNormal) {
                isGoingTo = false;
              } else {
                if (repeat) {
                  StartFade();
                } else if (destroyOnDone) {
                  DestroyMe();
                }
              }
            } 
          } else {
            fadeCounter -= Time.deltaTime * fadeSpeed;
            spriteRend.color = Color.Lerp(initialColor, colorTo, fadeCounter);
            if (fadeCounter <= 0) {
              if (repeat) {
                StartFade();
              } else if (destroyOnDone) {
                DestroyMe();
              }
            }
          }
        }
    }


    public void StartFade() {
      fadeCounter = 0;
      isFading = true;
      isGoingTo = true;
    }

    public void DestroyMe() {
      Destroy(this);
    }
    
}
