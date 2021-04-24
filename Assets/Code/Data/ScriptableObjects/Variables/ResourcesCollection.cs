using SO.Base;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SOExtensions/Data/Instances/Resources Collection")]
    public class ResourcesCollection : ListCollection<AResource>
    {
        public IEnumerable<AResource> GetResources()
        {
            return m_GenericList;
        }

        public AResource GetRandom()
        {
            if (m_GenericList.Count > 0)
            {
                return m_GenericList[Random.Range(0, m_GenericList.Count)];
            }

            return null;
        }
    }
}
