using SO.Base;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "SOExtensions/Data/Persistent/Resource Blueprint")]
    public class ResourceData : ScriptableObject
    {
        [SerializeField]
        private string m_ResourceName;
        public string Name { get { return m_ResourceName; } }

        [SerializeField]
        private Sprite m_Sprite;
        public Sprite Sprite { get { return m_Sprite; } }

        [SerializeField]
        private FloatVariable m_IncreaseRate;
        public float IncreaseRate { get { return m_IncreaseRate.Value; } }

        [SerializeField]
        private FloatVariable m_IncreaseInterval;
        public float IncreaseInterval { get { return m_IncreaseInterval.Value; } }

        [SerializeField]
        private RangeVariable m_DemandRange;
        public Vector2 DemandRange { get { return m_DemandRange.Value; } }
    }
}
