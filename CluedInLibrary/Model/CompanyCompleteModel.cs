using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedInLibrary.Model
{
    public class CompanyCompleteModel
    {
        public CompanyModel Company { get; set; }
        public List<CompanyEmployeeModel> CompanyEmployees { get; set; }
        public List<CompanyCustomerModel> CompanyCustomers { get; set; }
    }
}
