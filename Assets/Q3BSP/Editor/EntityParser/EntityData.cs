namespace OrbGarden.TrenchbroomImport.EntityParser
{
    internal class EntityData
    {
        private string m_DataString;
        
        public EntityData(string entityDataString)
        {
            m_DataString = entityDataString;
        }

        public override string ToString()
        {
            return m_DataString;
        }
    }
}