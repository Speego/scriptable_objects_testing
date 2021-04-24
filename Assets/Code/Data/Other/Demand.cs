
namespace Data
{
    public class Demand
    {
        private AResource m_Resource;
        public AResource Resource { get { return m_Resource; } }

        private int m_DemandValue;
        public int DemandValue { get { return m_DemandValue; } }

        private bool m_Satisfied = false;

        public Demand(AResource resource, int value)
        {
            m_Resource = resource;
            m_DemandValue = value;
        }


        public void Satisfy()
        {
            if (m_Satisfied)
            {
                return;
            }

            m_Resource.Reduce(m_DemandValue);
        }
    }
}
