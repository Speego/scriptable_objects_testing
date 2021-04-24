using Data;
using UnityEngine;

namespace Logic
{
    public class DemandsManager : MonoBehaviour
    {
        [SerializeField]
        private DemandsPointsCollection m_DemandsPoints;

        private void Awake()
        {
            m_DemandsPoints.OnSubscribed.AddListener(HandleDemandPointSubscribed);

            //test
            //m_DemandsPoints.Subscribe();
            //m_DemandsPoints.Subscribe();
            //m_DemandsPoints.Subscribe();
        }

        private void OnDestroy()
        {
            m_DemandsPoints.OnSubscribed.RemoveListener(HandleDemandPointSubscribed);
            
            StopAllCoroutines();
        }

        private void HandleDemandPointSubscribed(ADemandPoint demandPoint)
        {
            StartCoroutine(demandPoint.StartDemandsGenerationCOR());
        }
    }
}
