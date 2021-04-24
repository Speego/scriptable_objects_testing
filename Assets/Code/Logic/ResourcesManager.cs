using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField]
        private ResourcesCollection m_Resources;

        private void Awake()
        {
            InitializeResources();
        }

        private void OnDestroy()
        {
            UninitializeResources();
        }

        private void InitializeResources()
        {
            IEnumerable<AResource> resources = m_Resources.GetResources();

            foreach (AResource resource in resources)
            {
                StartCoroutine(resource.IncreaseAmountCOR());
            }
        }

        private void UninitializeResources()
        {
            StopAllCoroutines();
        }
    }
}
