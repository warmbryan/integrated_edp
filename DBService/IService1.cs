using DBService.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace DBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        // Business
        [OperationContract]
        bool CreateBusiness(string name, string regNum, string url, string type, string acra, string logoId, string userId);

        [OperationContract]
        List<Business> GetAllBusinesses();

        [OperationContract]
        List<Business> GetAllBusinessByUserId(string userId);

        [OperationContract]
        Business GetSingleBusinessByBusinessId(string businessId);

        [OperationContract]
        bool UpdateBusinessDetails(string businessId, string name, string registrationNumber, string url, string type, string acra, string logoId);

        [OperationContract]
        bool DeleteBusiness(string businessId);

        // Employee
        [OperationContract]
        List<BusinessEmployeeAccess> GetAllEmployeeByBusinessId(string businessId);

        [OperationContract]
        bool AddEmployeeToBusinessByEmail(string userId, string businessId, bool rApp, bool wApp, bool rCC, bool wCC, string role);

        [OperationContract]
        bool BusinessUserExists(string email);

        [OperationContract]
        List<BusinessEmployeeAccess> GetAllInvitationsByUserId(string userId);

        [OperationContract]
        bool UpdateEmployeeAccess(string userId, string businessId, bool rApp, bool wApp, bool rCC, bool wCC, string role);

        [OperationContract]
        bool DeleteEmployeeAccess(string userId, string businessId);

        // BusinessUser
        [OperationContract]
        bool CreateBusinessUser(string name, string email, string password, string phone);

        [OperationContract]
        BusinessUser GetBusinessUserByUserId(string userId);

        [OperationContract]
        BusinessUser GetBusinessUserByEmail(string email);

        // Customer
        [OperationContract]
        Int16 InsertCustomer(String firstName, String lastName, String email, String password, String phoneNumber, DateTime dateOfBirth);

        [OperationContract]
        CustomerClass SelectOneCustomer(Guid ID, String Email);

        [OperationContract]
        List<CustomerClass> SelectAllCustomer();

        [OperationContract]
        CustomerClass VerifyCustomer(String Email);

        [OperationContract]
        Boolean VerifyPassword(String Email, String Password);

        [OperationContract]
        Int16 UpdateCustomer(Guid ID, String PastEmail, String purpose, Object valueOne, Object valueTwo);

        [OperationContract]
        Int16 DeleteCustomer(Guid ID, String Email, DateTime deleteDate);

        //----------------------Branch--------------------
        [OperationContract]
        List<String> SelectDistinctShopNameFromBranch();

        [OperationContract]
        DataSet SelectDistinctLocationFromBranch();

        [OperationContract]
        DataSet SearchFromBranch(string search, string location);

        //----------------------Search--------------------
        [OperationContract]
        int CreateSearch(string searchString, Guid customerId);

        [OperationContract]
        DataSet SelectByCustomerIdFromSearch(Guid customerId);

        [OperationContract]
        int HaveDateFromSearch(string searchString, Guid customerId);

        [OperationContract]
        int UpdateSearch(int id);

        [OperationContract]
        int DeleteFromSearch(Guid customerId);

        //----------------------View--------------------
        [OperationContract]
        int InsertView(Guid branchId, Guid customerId);

        [OperationContract]
        DataSet SelectByCustomerIdFromView(Guid customerId);

        [OperationContract]
        int HaveDateFromView(Guid branchId, Guid customerId);

        [OperationContract]
        int UpdateView(int id);

        [OperationContract]
        int DeleteFromView(Guid customerId);

        // Business Appointments
        [OperationContract]
        int CreateAppointment(string aptdate, string apttime, string bookdate, string booktime, string partysize);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "DBService.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
