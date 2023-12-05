using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EDI.Crud.Utilities.Base
{
    public class DomainLayerException : Exception
    {
        public DomainLayerException()
        {
        }

        public DomainLayerException(string message) : base(message)
        {
        }

        public DomainLayerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DomainLayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
