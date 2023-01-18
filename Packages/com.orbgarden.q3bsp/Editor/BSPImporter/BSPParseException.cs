using System;

namespace OrbGarden.Q3BSP.BSPImporter
{
    public class BSPParseException : Exception
    {
        public BSPParseException(string message) : base(message)
        { }
    }
}