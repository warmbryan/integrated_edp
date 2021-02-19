using DBService.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
        bool AddEmployeeToBusinessByEmail(string userId, string businessId, string roleId, bool rApp, bool wApp, bool rCC, bool wCC);

        [OperationContract]
        bool BusinessUserExists(string email);

        [OperationContract]
        List<BusinessEmployeeAccess> GetAllInvitationsByUserId(string userId);

        [OperationContract]
        bool UpdateEmployeeAccess(string userId, string businessId, string roleId, bool rApp, bool wApp, bool rCC, bool wCC);

        [OperationContract]
        bool DeleteEmployeeAccess(string userId, string businessId);

        [OperationContract]
        BusinessEmployeeAccess GetOneEmployeeAccess(string beaId);

        [OperationContract]
        bool AcceptBusinessInvite(string beaId);

        [OperationContract]
        bool RejectBusinessInvite(string beaId);

        // BusinessUser
        [OperationContract]
        bool CreateBusinessUser(string name, string email, string password, string phone);

        [OperationContract]
        BusinessUser GetBusinessUserByUserId(string userId);

        [OperationContract]
        BusinessUser GetBusinessUserByEmail(string email);

        [OperationContract]
        Int16 UpdateBusinessStatus(String PastEmail, String purpose, Boolean status);

        [OperationContract]
        Int16 DeleteBusinessUser(String Email, DateTime deleteDate);

        // START WEI RONG
        // Customer

        [OperationContract]
        Int16 UpdateBusinessVerification(string businessId, bool value);

        [OperationContract]
        Int16 InsertCustomer(String firstName, String lastName, String email, String password, String phoneNumber, DateTime dateOfBirth);

        [OperationContract]
        CustomerClass SelectOneCustomer(String Email);

        [OperationContract]
        List<CustomerClass> SelectAllCustomer();

        [OperationContract]
        CustomerClass VerifyCustomer(String Email);

        [OperationContract]
        Boolean VerifyPassword(String Email, String Password, String Role);

        [OperationContract]
        Int16 UpdateCustomer(Guid ID, String PastEmail, String firstName, String lastName, String email, String PhoneNumber, DateTime dateOfBirth);

        [OperationContract]
        Int16 UpdateCustomerPassword(Guid ID, String PastEmail, String Password);

        [OperationContract]
        Int16 UpdateCustomerStatus(Guid ID, String PastEmail, String purpose, Boolean status);

        [OperationContract]
        Int16 DeleteCustomer(Guid ID, String Email, DateTime deleteDate);
        // END WEI RONG

        //----------------------Branch--------------------
        [OperationContract]
        List<String> SelectDistinctShopNameFromBranch();

        [OperationContract]
        DataSet SelectDistinctLocationFromBranch();

        [OperationContract]
        DataSet SelectDistinctCategoryFromBranch();

        [OperationContract]
        DataSet SearchFromBranch(string search, string location, string category);

        [OperationContract]
        Branch SelectByIdFromBranch(Guid id);

        [OperationContract]
        bool CreateBranch(Guid businessId, string name, string description, string address, string address2, string city, string state, string zip, string country, string phone, string email, bool isMainBranch);

        [OperationContract]
        bool UpdateBranch(Guid id, string name, string description, string address, string address2, string city, string state, string zip, string country, string phone, string email, bool isMainBranch);

        [OperationContract]
        bool DeleteBranch(Guid id);

        [OperationContract]
        List<Branch> SelectBranchesByBusinessId(Guid businessId);

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

        //----------------------Review--------------------
        [OperationContract]
        int InsertReview(double rating, string comment, string title, Guid customerId, Guid branchId);

        [OperationContract]
        DataSet SelectByBranchIdFromReview(Guid id, Guid customerId, string sort);

        [OperationContract]
        DataSet SelectAllByBranchIdFromReview(Guid id, string sort);

        [OperationContract]
        Review HaveExistingReview(Guid branchId, Guid customerId);

        [OperationContract]
        int UpdateReview(int id, String title, String comment, Double rating);

        [OperationContract]
        int DeleteReview(int id);

        [OperationContract]
        DataSet SelectByCustomerIdFromReview(Guid id);

        [OperationContract]
        double SelectRatingByBranchIdFromReview(Guid branchId);

        [OperationContract]
        DataSet SelectReportedReview();

        [OperationContract]
        int AddNumReportToReview(int id);

        [OperationContract]
        int ResetNumReportToReview(int id);

        // Business Appointments
        [OperationContract]
        int CreateAppointment(string aptdate, string apttime, string bookdate, string booktime, string partysize, DateTime aptDateTime, string customerid, string branchid, string appointmentsettingid);

        [OperationContract]
        int ModifyAppointment(string userid, string aptdate, string apttime, string bookdate, string booktime, string partysize, DateTime aptDateTime, string customerid, string branchid, string appointmentsettingid);

        [OperationContract]
        List<Appointment> GetAllAppointment();

        [OperationContract]
        List<Appointment> GetAllAppointmentByTodayDate();

        [OperationContract]
        List<Appointment> GetAllAppointmentByTodayDateAscend(string aptdate);

        [OperationContract]
        int setArrived(string aptTime, string aptDate);

        [OperationContract]
        int deleteAppointment(string aptTime, string aptDate);

        [OperationContract]
        List<string> selectAllAppointmentDateAscend();

        [OperationContract]
        int selectCountByDate(string aptDate);

        // Business Role
        [OperationContract]
        BusinessRole CreateBusinessRole(string name, string businessId);

        [OperationContract]
        BusinessRole GetBusinessRole(string businessRoleId);

        [OperationContract]
        bool UpdateBusinessRole(string businessRoleId, string name, string businessId);

        [OperationContract]
        bool DeleteBusinessRole(string businessRoleId);

        [OperationContract]
        List<BusinessRole> GetBusinessRoles(string businessId);

        // Admin
        [OperationContract]
        Int16 InsertAdmin(String adminName, String userName, String password, String role);

        [OperationContract]
        AdminClass SelectOneAdmin(String userName);

        [OperationContract]
        Int16 InsertOneBlacklist(Int32 duration, String reason, String customerId, String customerName);

        [OperationContract]
        Int16 UpdateBlacklistDeleted(Guid ID, String customerId, Boolean status);

        [OperationContract]
        BlackListClass SelectOneBlacklist(Guid ID, String customerId);

        [OperationContract]
        List<BlackListClass> SelectAllBlacklist(String customerId);

        [OperationContract]
        List<AdminClass> SelectAllAdmin();
        [OperationContract]
        List<BusinessUser> SelectAllBusiness();
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
