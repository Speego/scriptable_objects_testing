using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIDemand : UIBehaviour
    {
        [SerializeField]
        private Image m_ResourceIcon;
        [SerializeField]
        private TextMeshProUGUI m_ValueText;

        public void Initialize(Sprite icon, int value)
        {
            m_ResourceIcon.sprite = icon;
            m_ValueText.text = value.ToString();
        }
    }
}
