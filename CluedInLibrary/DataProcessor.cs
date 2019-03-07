using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CluedinLibrary;
using CluedInLibrary.Model;

namespace CluedInLibrary
{
    public class DataProcessor
    {
        public static async Task<List<CompanyModel>> GetCompanies(int value = 0)
        {
            string url = "";

            url = value > 0 ? $"https://cluedintestapi.herokuapp.com/api/companies/{value}" : "https://cluedintestapi.herokuapp.com/api/companies";

            using (var response = await APIHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var companies = await response.Content.ReadAsAsync<List<CompanyModel>>();

                    return companies;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<CompanyModel> GetCompanyById(int id)
        {
            string url = $"https://cluedintestapi.herokuapp.com/api/companies/{id}";

            using (var response = await APIHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<CompanyModel>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public static async Task<List<CompanyEmployeeModel>> GetEmployeesByCompanyId(int id)
        {
            string url = $"https://cluedintestapi.herokuapp.com/api/companies/{id}/employees";

            using (var response = await APIHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<CompanyEmployeeModel>>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<CompanyCustomerModel>> GetCustomersByCompanyId(int id)
        {
            string url = $"https://cluedintestapi.herokuapp.com/api/companies/{id}/customers";

            using (var response = await APIHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<CompanyCustomerModel>>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
