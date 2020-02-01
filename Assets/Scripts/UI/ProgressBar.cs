using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Tooltip("Health bar fill content.")]
    public Image m_content;

    private void Start() {
        SetFillAmount(0);
        gameObject.SetActive(false);
    }

    public void SetFillAmount(float value) {
        m_content.fillAmount = value;
    }
}
