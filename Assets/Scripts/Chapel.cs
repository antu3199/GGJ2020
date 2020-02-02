using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chapel : DropArea {
    [Serializable]
    public class Material {
        public Resource m_resource;
        public int m_cost;
    }

    private int m_donatedGold;
    //
    public List<Material> m_costForGold = new List<Material>();
    private Dictionary<Resource, int> m_goldMaterials = new Dictionary<Resource, int>();
    //
    [Tooltip("The required quantity of gold donated to win the game.")]
    public int m_victoryGoldAmount = 1000;
    public SpriteRenderer spriteRend;
    public List<Sprite> sprites;

    void Awake() {
        foreach(Material material in m_costForGold) {
            m_goldMaterials.Add(material.m_resource, material.m_cost);
        }
    }

    public void DonateGold(int amount) {
        m_donatedGold += amount;
        int spriteNum = Mathf.Clamp(sprites.Count * m_donatedGold / m_victoryGoldAmount, 0, sprites.Count - 1);
        spriteRend.sprite = sprites[spriteNum];
        if (m_donatedGold >= m_victoryGoldAmount) {
            WinGame();
        }
    }

    public void tryMakeGold() {
        if(SummonerInventory.Instance.tryConsumeResources(m_goldMaterials)) {
            DonateGold(1);
            SummonerInventory.Instance.addResource(Resource.GOLD, 1);
        }

    }

    private void WinGame() {
        Debug.Log("Game Won");
        // TODO
    }
}
