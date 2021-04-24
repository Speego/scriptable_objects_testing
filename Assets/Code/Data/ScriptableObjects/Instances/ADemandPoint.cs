using Random = UnityEngine.Random;
using SO.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public abstract class ADemandPoint : ScriptableObject
    {
        [SerializeField]
        private RangeVariable m_DemandInterval;

        [SerializeField]
        private ResourcesCollection m_ResourcesCollection;

        private LinkedList<Demand> m_Demands = new LinkedList<Demand>();

        [NonSerialized]
        public UnityEvent<Demand> OnDemandGenerated = new UnityEvent<Demand>();
        [NonSerialized]
        public UnityEvent<Demand> OnDemandSatisfied = new UnityEvent<Demand>();

        [NonSerialized]
        public UnityEvent<AResource> OnResourceGiven = new UnityEvent<AResource>();

        private void OnEnable()
        {
            OnResourceGiven.AddListener(TrySatisfyDemand);
        }

        private void OnDisable()
        {
            Reset();
        }

        private void Reset()
        {
            ResetDemands();
            ResetEvents();
        }

        private void ResetDemands()
        {
            m_Demands.Clear();
        }

        private void ResetEvents()
        {
            OnDemandGenerated.RemoveAllListeners();
            OnDemandSatisfied.RemoveAllListeners();
            OnResourceGiven.RemoveAllListeners();
        }

        public IEnumerator StartDemandsGenerationCOR()
        {
            while (true)
            {
                Vector2 timeRange = m_DemandInterval.Value;
                float timeToNextDemand = Random.Range(timeRange.x, timeRange.y);

                // it would be good to have all possible WaitForSeconds cached but with random float generation it's not doable
                yield return new WaitForSeconds(timeToNextDemand);

                GenerateDemand();
            }
        }

        // it is something we could override for a different behaviour of a demand point
        protected virtual void GenerateDemand()
        {
            AResource resource = m_ResourcesCollection.GetRandom();

            if (resource == null)
            {
                Debug.LogError("No resources are set up! Look at resources collection.");
                return;
            }

            Vector2 demandRange = resource.ResourceBlueprint.DemandRange;
            // I just wanted int for better UI visibility, no better reason for that
            int demandValue = Random.Range((int)demandRange.x, (int)demandRange.y + 1);

            Demand demand = new Demand(resource, demandValue);
            m_Demands.AddLast(demand);

            OnDemandGenerated.Invoke(demand);
        }

        // I don't like that a demand point has to know about resource amount here
        private void TrySatisfyDemand(AResource resource)
        {
            // we assume that satisfying demands only in a consecutive way is possible
            if (m_Demands.Count <= 0)
            {
                Debug.Log("No demands to satisfy");
                return;
            }

            Demand demandToSatisfy = m_Demands.First.Value;

            if (demandToSatisfy.Resource != resource)
            {
                Debug.LogFormat("Demand {0}: {1} is not possible to satisfy, wrong resource: {2}", 
                    demandToSatisfy.Resource.ResourceBlueprint.Name, demandToSatisfy.DemandValue, resource.ResourceBlueprint.Name);
                return;
            }

            if (demandToSatisfy.DemandValue > resource.Amount)
            {
                Debug.LogFormat("Demand {0}: {1} is not possible to satisfy, no resources available: {2}", 
                    demandToSatisfy.Resource.ResourceBlueprint.Name, demandToSatisfy.DemandValue, resource.Amount);
                return;
            }

            SatisfyDemand(demandToSatisfy);
        }

        private void SatisfyDemand(Demand demand)
        {
            demand.Satisfy();
            m_Demands.Remove(demand);

            OnDemandSatisfied.Invoke(demand);
        }
    }
}
