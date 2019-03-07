using System.Collections.Generic;

namespace CluedInLibrary.Model
{
    public class CompanyCompleteModel
    {
        public CompanyModel Company { get; set; }
        public List<CompanyEmployeeModel> CompanyEmployees { get; set; }
        public List<CompanyCustomerModel> CompanyCustomers { get; set; }
    }
}
