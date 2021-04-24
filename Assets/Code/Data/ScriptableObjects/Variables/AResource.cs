using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public abstract class AResource : ScriptableObject, ISubject<float>
    {
        [SerializeField]
        protected ResourceData m_ResourceBlueprint;
        public ResourceData ResourceBlueprint { get { return m_ResourceBlueprint; } }

        private WaitForSeconds m_WaitForSeconds;

        protected float m_Amount;
        public float Amount { get { return m_Amount; } }

        private HashSet<IObserver<float>> m_Observers = new HashSet<IObserver<float>>();

        private void OnEnable()
        {
            m_WaitForSeconds = new WaitForSeconds(m_ResourceBlueprint.IncreaseInterval);
            ResetResource();
        }

        private void ResetResource()
        {
            m_Amount = 0f;
        }

        // I don't like it being public but I also don't want resources to know about demands... no better idea for now
        public void Reduce(float amount)
        {
            ModifyAmount(-amount);
        }

        public IEnumerator IncreaseAmountCOR()
        {
            while (true)
            {
                yield return m_WaitForSeconds;

                ModifyAmount(m_ResourceBlueprint.IncreaseRate);
            }
        }

        private void ModifyAmount(float modifier)
        {
            m_Amount += modifier;
            Notify(m_Amount);
        }

        #region ISubject
        public void Attach(IObserver<float> observer)
        {
            if (!m_Observers.Contains(observer))
            {
                m_Observers.Add(observer);
            }
        }

        public void Detach(IObserver<float> observer)
        {
            if (m_Observers.Contains(observer))
            {
                m_Observers.Remove(observer);
            }
        }

        public void Notify(float value)
        {
            foreach (IObserver<float> observer in m_Observers)
            {
                observer.OnNotify(value);
            }
        }
        #endregion
    }
}
