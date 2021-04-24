using SO.Base;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SOExtensions/Data/Persistent/Concurrent Scenes")]
    public class ConcurrentScenesCollection : ListCollection<string>
    {
        public IEnumerable<string> GetScenesNames()
        {
            return m_GenericList;
        }
    }
}
