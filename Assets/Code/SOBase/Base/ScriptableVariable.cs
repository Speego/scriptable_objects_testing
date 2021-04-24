using UnityEngine;

namespace SO.Base
{
    public abstract class ScriptableVariable<T> : ScriptableObject
    {
        [SerializeField]
        private T m_Value;
        public T Value { get { return m_Value; } }
    }
}
