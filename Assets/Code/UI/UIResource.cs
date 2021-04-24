using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIResource : MonoBehaviour, IObserver<float>
    {
        [SerializeField]
        private Image m_Icon;
        [SerializeField]
        private TextMeshProUGUI m_NameText;
        [SerializeField]
        private TextMeshProUGUI m_ValueText;

        [SerializeField]
        private UIDraggable m_DraggableComponent;

        private AResource m_Resource;
        public AResource Resource { get { return m_Resource; } }

        public void Initialize(AResource resource)
        {
            m_Resource = resource;

            UpdateValueText(resource.Amount);

            m_Resource.Attach(this);

            ResourceData resourceData = resource.ResourceBlueprint;
            m_Icon.sprite = resourceData.Sprite;
            m_NameText.text = resourceData.Name;

            m_DraggableComponent.Initialize(resourceData.Sprite);
        }

        public void Uninitialize()
        {
            if (m_Resource != null)
            {
                m_Resource.Detach(this);
            }
        }

        public void OnNotify(float value)
        {
            UpdateValueText(value);
        }

        private void UpdateValueText(float value)
        {
            m_ValueText.text = value.ToString();
        }
    }
}
