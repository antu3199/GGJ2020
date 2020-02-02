using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{

    public Transform finishedBubbleContainer;
    
    public Color wheatColor;
    public Color woodColor;
    public Color oreColor;

    public Sprite wheatSprite;
    public Sprite woodSprite;
    public Sprite oreSprite;

    public SpriteRenderer circleSprite;
    public SpriteRenderer resourceSprite;

    public void MakeWheatPopup() {
      circleSprite.color = wheatColor;
      resourceSprite.sprite = wheatSprite;
    }

    public void MakeWoodPopup() {
      circleSprite.color = woodColor;
      resourceSprite.sprite = woodSprite;
    }

    public void MakeOrePopup() {
      circleSprite.color = oreColor;
      resourceSprite.sprite = oreSprite;
    }

    public void ShowBubble() {
      finishedBubbleContainer.gameObject.SetActive(true);
    }

    public void HideBubble() {
      finishedBubbleContainer.gameObject.SetActive(false);
    }
}
