using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    [CreateAssetMenu(menuName = "SOExtensions/Data/Instances/Demands Points Collection")]
    public class DemandsPointsCollection : ScriptableObject
    {
        [SerializeField]
        private ADemandPoint m_DemandPointBlueprint;

        [NonSerialized]
        public UnityEvent<ADemandPoint> OnSubscribed = new UnityEvent<ADemandPoint>();

        protected List<ADemandPoint> m_Demands = new List<ADemandPoint>();

        private void OnEnable()
        {
            ClearDemandsPoints();
        }

        private void OnDisable()
        {
            Uninitialize();
        }

        private void ClearDemandsPoints()
        {
            for (int i = m_Demands.Count - 1; i >= 0; i--)
            {
#if UNITY_EDITOR
                DestroyImmediate(m_Demands[i]);
#else
                Destroy(m_GenericList[i]);
#endif
            }

            m_Demands.Clear();
        }

        private void Uninitialize()
        {
            OnSubscribed.RemoveAllListeners();
            ClearDemandsPoints();
        }    

        public ADemandPoint Subscribe()
        {
            ADemandPoint demandPoint = m_DemandPointBlueprint.Clone<ADemandPoint>();

            m_Demands.Add(demandPoint);
            OnSubscribed.Invoke(demandPoint);

            return demandPoint;
        }
    }
}
