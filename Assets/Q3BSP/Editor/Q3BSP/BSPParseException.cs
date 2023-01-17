using System;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    public class BSPParseException : Exception
    {
        public BSPParseException(string message) : base(message)
        { }
    }
}