using System.Collections.Generic;
using UnityEngine;

namespace SO.Base
{
    public class ListCollection<T> : ScriptableObject
    {
        [SerializeField]
        protected List<T> m_GenericList = new List<T>();
    }
}
