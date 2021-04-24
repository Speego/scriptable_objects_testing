using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIDraggable : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // this should be totally pooled
        [SerializeField]
        private Image m_DraggablePrefab;

        [NonSerialized]
        public UnityEvent OnDragStarted = new UnityEvent();
        [NonSerialized]
        public UnityEvent OnDragEnded = new UnityEvent();

        private Sprite m_Icon;
        private Image m_Draggable;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            OnDragStarted.RemoveAllListeners();
            OnDragEnded.RemoveAllListeners();
        }

        public void Initialize(Sprite icon)
        {
            m_Icon = icon;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            m_Draggable = Instantiate(m_DraggablePrefab, Input.mousePosition, Quaternion.identity, transform);
            m_Draggable.sprite = m_Icon;
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_Draggable.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (m_Draggable != null)
            {
                Destroy(m_Draggable.gameObject);
            }
        }
    }
}
