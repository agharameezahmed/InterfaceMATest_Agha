using InterfaceMATest_Domain.Models;
using System.Collections.Generic;

namespace InterfaceMATest_Domain.Response
{
    public class BaseHouseServiceResponse
    {
        public IEnumerable<House> Houses { get; set; }
    }
}
