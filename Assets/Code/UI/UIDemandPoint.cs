using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIDemandPoint : UIBehaviour
    {
        [SerializeField]
        private DemandsPointsCollection m_DemandsPointsCollection;

        [SerializeField]
        private UIDemand m_DemandPrefab;
        [SerializeField]
        private RectTransform m_DemandsRoot;

        [SerializeField]
        private UIDroppable m_DroppableComponent;

        private ADemandPoint m_DemandPoint;
        private Dictionary<Demand, UIDemand> m_Demands = new Dictionary<Demand, UIDemand>();

        protected override void Awake()
        {
            base.Awake();
            m_DroppableComponent.OnDropped.AddListener(HandleDropped);
        }

        // we want to load LogicScene first (so the subscription is in Start, not Awake), in a proper way we should load the scene in async way and wait for that to subscribe
        protected override void Start()
        {
            base.Start();
            Subscribe();
        }

        protected override void OnDestroy()
        {
            UnSubscribe();
            m_DroppableComponent.OnDropped.RemoveListener(HandleDropped);
            base.OnDestroy();
        }

        private void Subscribe()
        {
            m_DemandPoint = m_DemandsPointsCollection.Subscribe();
            m_DemandPoint.OnDemandGenerated.AddListener(HandleDemandGenerated);
            m_DemandPoint.OnDemandSatisfied.AddListener(HandleDemandRemoved);
        }

        private void UnSubscribe()
        {
            if (m_DemandPoint != null)
            {
                m_DemandPoint.OnDemandGenerated.RemoveListener(HandleDemandGenerated);
                m_DemandPoint.OnDemandSatisfied.RemoveListener(HandleDemandRemoved);
            }
        }

        private void HandleDemandGenerated(Demand demand)
        {
            // it definitely should be poolable but I want to finish this project finally
            UIDemand uiDemand = Instantiate(m_DemandPrefab, m_DemandsRoot);

            uiDemand.Initialize(demand.Resource.ResourceBlueprint.Sprite, demand.DemandValue);
            m_Demands.Add(demand, uiDemand);
        }

        private void HandleDemandRemoved(Demand demand)
        {
            if (m_Demands.ContainsKey(demand))
            {
                UIDemand uiDemand = m_Demands[demand];

                if (uiDemand != null)
                {
                    Destroy(uiDemand.gameObject);
                }

                m_Demands.Remove(demand);
            }
            else
            {
                Debug.LogWarningFormat("Trying to remove Demand from DemandPoint that's not here: {0}", demand.Resource.ResourceBlueprint.Name);
            }
        }

        private void HandleDropped(GameObject droppedGO)
        {
            UIResource uiResource = droppedGO.GetComponent<UIResource>();

            if (uiResource == null)
            {
                Debug.Log("UIDemandPoint has been dropped with something else than UIResource, returning.");
                return;
            }

            m_DemandPoint.OnResourceGiven.Invoke(uiResource.Resource);
        }
    }
}
