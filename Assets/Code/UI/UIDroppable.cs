using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIDroppable : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        [SerializeField]
        private Image m_Image;

        private Color m_OriginalColor;
        private Color m_DroppedColor;

        [NonSerialized]
        public UnityEvent<GameObject> OnDropped = new UnityEvent<GameObject>();

        protected override void Awake()
        {
            base.Awake();

            m_OriginalColor = m_Image.color;
            m_DroppedColor = Color.white;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            OnDropped.RemoveAllListeners();
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject go = eventData.pointerDrag;
            OnDropped.Invoke(go);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_Image.color = m_DroppedColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_Image.color = m_OriginalColor;
        }
    }
}
