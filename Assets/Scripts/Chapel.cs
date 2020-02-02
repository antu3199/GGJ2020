using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapel : DropArea {
    private int m_donatedGold;
    [Tooltip("The required quantity of gold donated to win the game.")]
    public int m_victoryGoldAmount;

    public void DonateGold(int amount) {
        m_donatedGold += amount;
        if (m_donatedGold >= m_victoryGoldAmount) {
            WinGame();
        }
    }

    private void WinGame() {
        Debug.Log("Game Won");
        // TODO
    }
}
