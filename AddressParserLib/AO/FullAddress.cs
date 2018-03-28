using System.Collections.Generic;
using System.Text;

namespace AddressParserLib
{
    public class FullAddress
    {
        /// <summary>
        /// Индекс адреса.
        /// </summary>
        public string PostalCode { get; set; }

        private List<AddressObject> objects;

        public FullAddress(List<AddressObject> objects)
        {
            this.objects = objects;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(PostalCode);
            foreach (var item in objects)
            {
                if(sb.ToString()!="")
                    sb.Append(", ");
                sb.Append(item?.Name);
            }
            return sb.ToString();
        }
    }
}
