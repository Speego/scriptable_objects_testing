using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIResources : UIBehaviour
    {
        [SerializeField]
        private ResourcesCollection m_ResourcesData;

        [SerializeField]
        private UIResource m_UIResourcePrefab;

        private List<UIResource> m_Resources = new List<UIResource>();

        protected override void Awake()
        {
            base.Awake();

            InitializeResources();
        }

        protected override void OnDestroy()
        {
            UninitializeResources();

            base.OnDestroy();
        }

        private void InitializeResources()
        {
            IEnumerable<AResource> resources = m_ResourcesData.GetResources();
            Transform thisTransform = transform;

            foreach (AResource resource in resources)
            {
                UIResource uiResource = Instantiate(m_UIResourcePrefab, thisTransform);
                uiResource.Initialize(resource);
            }
        }

        private void UninitializeResources()
        {
            for (int i = 0; i < m_Resources.Count; i++)
            {
                m_Resources[i].Uninitialize();
            }
        }
    }
}
