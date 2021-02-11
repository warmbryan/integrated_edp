using DBService.Models;
using System;
using System.Data;
using System.Collections.Generic;

namespace DBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        // business
        public bool CreateBusiness(string name, string regNum, string url, string type, string acra, string logoId, string userId)
        {
            Business business = new Business();
            return business.Register(name, regNum, url, type, acra, logoId, userId);
        }

        public List<Business> GetAllBusinesses()
        {
            Business business = new Business();
            return business.SelectAll();
        }

        public List<Business> GetAllBusinessByUserId(string userId)
        {
            Business b = new Business();
            return b.SelectAllByUserId(userId);
        }

        public Business GetSingleBusinessByBusinessId(string businessId)
        {
            Business b = new Business();
            return b.SelectOne(businessId);
        }

        public bool UpdateBusinessDetails(string businessId, string name, string registrationNumber, string url, string type, string acra, string logoId)
        {
            Business b = new Business();
            return b.Update(businessId, name, registrationNumber, url, type, acra, logoId);
        }

        public bool DeleteBusiness(string businessId)
        {
            Business b = new Business();
            return b.Delete(businessId);
        }

        // business employee access
        public List<BusinessEmployeeAccess> GetAllEmployeeByBusinessId(string businessId)
        {
            BusinessEmployeeAccess bea = new BusinessEmployeeAccess();
            return bea.SelectAllByBusinessId(businessId);
        }

        public List<BusinessEmployeeAccess> GetAllInvitationsByUserId(string userId)
        {
            BusinessEmployeeAccess bea = new BusinessEmployeeAccess();
            return bea.SelectAllByUserId(userId);
        }

        public bool UpdateEmployeeAccess(string userId, string businessId, bool rApp, bool wApp, bool rCC, bool wCC, string role)
        {
            BusinessEmployeeAccess businessEmployeeAccess = new BusinessEmployeeAccess();
            return businessEmployeeAccess.Update(userId, businessId, rApp, wApp, rCC, wCC, role);
        }

        public bool DeleteEmployeeAccess(string userId, string businessId)
        {
            BusinessEmployeeAccess businessEmployeeAccess = new BusinessEmployeeAccess();
            return businessEmployeeAccess.Delete(userId, businessId);
        }

        public bool AddEmployeeToBusinessByEmail(string userId, string businessId, bool rApp, bool wApp, bool rCC, bool wCC, string role)
        {
            BusinessEmployeeAccess bea = new BusinessEmployeeAccess();
            return bea.Create(userId, businessId, rApp, wApp, rCC, wCC, role);
        }

        // BusinessUser
        public bool CreateBusinessUser(string name, string email, string password, string phone)
        {
            return BusinessUser.Create(name, email, password, phone);
        }

        public bool BusinessUserExists(string email)
        {
            BusinessUser bu = new BusinessUser();
            return bu.Exists(email);
        }

        public BusinessUser GetBusinessUserByUserId(string userId)
        {
            BusinessUser bu = new BusinessUser();
            return bu.SelectOneByUserId(userId);
        }

        public BusinessUser GetBusinessUserByEmail(string email)
        {
            BusinessUser bu = new BusinessUser();
            return bu.SelectOneByEmail(email);
        }

        // Customer
        public Int16 InsertCustomer(String firstName, String lastName, String email, String password, String phoneNumber, DateTime dateOfBirth)
        {
            CustomerClass cust = new CustomerClass(firstName, lastName, email, password, phoneNumber, dateOfBirth);
            return cust.InsertIntoDatabase();
        }

        public CustomerClass SelectOneCustomer(Guid ID, String Email)
        {
            CustomerClass cust = new CustomerClass();
            return cust.SelectOneCustomer(ID, Email);
        }

        public List<CustomerClass> SelectAllCustomer()
        {
            CustomerClass cust = new CustomerClass();
            return cust.SelectAllCustomers();
        }

        public CustomerClass VerifyCustomer(String Email)
        {
            CustomerClass cust = new CustomerClass();
            return cust.VerifyUser(Email);
        }

        public Boolean VerifyPassword(String Email, String Password)
        {
            CustomerClass cust = new CustomerClass();
            cust = cust.VerifyUser(Email);
            if (cust != null)
            {
                return cust.decryptHashPassword(Password);
            }
            else
            {
                return false;
            }
        }

        public Int16 UpdateCustomer(Guid ID, String PastEmail, String purpose, Object valueOne, Object valueTwo)
        {
            CustomerClass cust = new CustomerClass();
            return (Int16)cust.UpdateCustomer(ID, PastEmail, purpose, valueOne, valueTwo);
        }

        public Int16 DeleteCustomer(Guid ID, String Email, DateTime deleteDate)
        {
            CustomerClass cust = new CustomerClass();
            return cust.FullDeleteCustomer(ID, Email, deleteDate);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        //----------------------Branch--------------------
        public List<String> SelectDistinctShopNameFromBranch()
        {
            Branch branch = new Branch();
            return branch.SelectDistinctShopName();
        }

        public DataSet SelectDistinctLocationFromBranch()
        {
            Branch branch = new Branch();
            return branch.SelectDistinctLocation();
        }

        public DataSet SearchFromBranch(string search, string location)
        {
            Branch branch = new Branch();
            return branch.Search(search, location);
        }

        //----------------------Search--------------------
        public int CreateSearch(string searchString, Guid customerId)
        {
            Search search = new Search(searchString, customerId);
            return search.Insert();
        }

        public DataSet SelectByCustomerIdFromSearch(Guid customerId)
        {
            Search search = new Search();
            return search.SelectByCustomerId(customerId);
        }

        public int HaveDateFromSearch(string searchString, Guid customerId)
        {
            Search search = new Search();
            return search.HaveDate(searchString, customerId);
        }

        public int UpdateSearch(int id)
        {
            Search search = new Search();
            return search.Update(id);
        }

        public int DeleteFromSearch(Guid customerId)
        {
            Search search = new Search();
            return search.Delete(customerId);
        }

        //----------------------View--------------------
        public int InsertView(Guid branchId, Guid customerId)
        {
            View view = new View(branchId, customerId);
            return view.Insert();
        }
        public DataSet SelectByCustomerIdFromView(Guid customerId)
        {
            View view = new View();
            return view.SelectByCustomerId(customerId);
        }

        public int HaveDateFromView(Guid branchId, Guid customerId)
        {
            View view = new View();
            return view.HaveDate(branchId, customerId);
        }

        public int UpdateView(int id)
        {
            View view = new View();
            return view.Update(id);
        }
        public int DeleteFromView(Guid customerId)
        {
            View view = new View();
            return view.Delete(customerId);
        }

        // appointments
        public int CreateAppointment(string aptdate, string apttime, string bookdate, string booktime, string partysize)
        {
            Appointment apt = new Appointment(aptdate, apttime, bookdate, booktime, partysize);
            return apt.Insert();
        }
    }
}
