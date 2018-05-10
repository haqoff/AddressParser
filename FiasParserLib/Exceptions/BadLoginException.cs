using System;

namespace FiasParserGUI.Exceptions
{
    public class BadLoginException : Exception
    {
        public BadLoginException(string msg) : base(msg)
        {
                
        }
                    
    }
}
