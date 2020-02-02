using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public List<Resource> m_resourceIconsKeys;
    public List<Sprite> m_resourceIconsValues;
    private Dictionary<Resource, Sprite> m_resourceIconsDict;

    public Image m_icon;
    [Tooltip("Health bar fill content.")]
    public Image m_content;

    private void Awake() {
        if (m_resourceIconsKeys.Count != m_resourceIconsValues.Count) {
            Debug.LogWarning("Invalid initialization of resource icons.");
        } else {
            m_resourceIconsDict = new Dictionary<Resource, Sprite>();
            for (int i = 0; i < m_resourceIconsKeys.Count; i++) {
                m_resourceIconsDict[m_resourceIconsKeys[i]] = m_resourceIconsValues[i];
            }
        }
    }

    private void Start() {
        SetFillAmount(0);
        gameObject.SetActive(false);
    }
    
    public void SetResourceIcon(Resource resource) {
        if (m_resourceIconsDict.ContainsKey(resource)) {
            m_icon.sprite = m_resourceIconsDict[resource];
        }
    }

    public void SetFillAmount(float value) {
        m_content.fillAmount = value;
    }
}
