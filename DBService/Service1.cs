using DBService.Models;
using System;
using System.Collections.Generic;
using System.Data;

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

        public List<BusinessUser> SelectAllBusiness()
        {
            BusinessUser business = new BusinessUser();
            return business.SelectAllBusiness();
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

        public bool UpdateEmployeeAccess(string userId, string businessId, string roleId, bool rApp, bool wApp, bool rCC, bool wCC)
        {
            BusinessEmployeeAccess businessEmployeeAccess = new BusinessEmployeeAccess();
            return businessEmployeeAccess.Update(userId, businessId, roleId, rApp, wApp, rCC, wCC);
        }

        public bool DeleteEmployeeAccess(string userId, string businessId)
        {
            BusinessEmployeeAccess businessEmployeeAccess = new BusinessEmployeeAccess();
            return businessEmployeeAccess.Delete(userId, businessId);
        }

        public bool AddEmployeeToBusinessByEmail(string userId, string businessId, string roleId, bool rApp, bool wApp, bool rCC, bool wCC)
        {
            BusinessEmployeeAccess bea = new BusinessEmployeeAccess();
            return bea.Create(userId, businessId, roleId, rApp, wApp, rCC, wCC);
        }

        public BusinessEmployeeAccess GetOneEmployeeAccess(string beaId)
        {
            BusinessEmployeeAccess bea = new BusinessEmployeeAccess();
            return bea.SelectOne(beaId);
        }

        public bool AcceptBusinessInvite(string beaId)
        {
            BusinessEmployeeAccess bea = new BusinessEmployeeAccess();
            return bea.AcceptInvitation(beaId);
        }

        public bool RejectBusinessInvite(string beaId)
        {
            BusinessEmployeeAccess bea = new BusinessEmployeeAccess();
            return bea.RejectInvitation(beaId);
        }

        // BusinessUser
        public bool CreateBusinessUser(string name, string email, string password, string phone)
        {
            BusinessUser tmp = new BusinessUser(name,email,password,phone);
            return tmp.Create();
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

        public CustomerClass SelectOneCustomer(String Email)
        {
            CustomerClass cust = new CustomerClass();
            return cust.SelectOneCustomer(Email);
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


        public Boolean VerifyPassword(String UserName, String Password, String Role)
        {
            if (Role == "Customer")
            {
                CustomerClass cust = new CustomerClass();
                cust = cust.VerifyUser(UserName);
                if (cust != null)
                {
                    return cust.decryptHashPassword(Password);
                }
                else
                {
                    return false;
                }
            }
            else if (Role == "Business")
            {
                BusinessUser business = new BusinessUser();
                business = business.SelectOneByEmail(UserName);
                if (business != null)
                {
                    return business.decryptHashPassword(Password);
                }
                else
                {
                    return false;
                }
            }
            else if (Role == "Admin")
            {
                AdminClass admin = new AdminClass();
                admin = admin.SelectOneAdmin(UserName);
                if (admin != null)
                {
                    return admin.decryptHashPassword(Password);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Int16 UpdateCustomer(Guid ID, String PastEmail, String firstName, String lastName, String email, String PhoneNumber, DateTime dateOfBirth)
        {
            CustomerClass cust = new CustomerClass();
            return (Int16)cust.UpdateCustomer(ID, PastEmail, firstName, lastName, email, PhoneNumber, dateOfBirth);
        }

        public Int16 UpdateCustomerPassword(Guid ID, String PastEmail, String Password)
        {
            CustomerClass cust = new CustomerClass();
            return (Int16)cust.UpdateCustomerPassword(ID, PastEmail, Password);
        }

        public Int16 UpdateCustomerStatus(Guid ID, String PastEmail, String purpose, Boolean status)
        {
            CustomerClass cust = new CustomerClass();
            return (Int16)cust.UpdateCustomerStatus(ID, PastEmail, purpose, status);
        }

        public Int16 DeleteCustomer(Guid ID, String Email, DateTime deleteDate)
        {
            CustomerClass cust = new CustomerClass();
            return cust.FullDeleteCustomer(ID, Email, deleteDate);
        }

        //Admin
        public Int16 InsertAdmin(String adminName, String userName, String password, String role)
        {
            AdminClass admin = new AdminClass(adminName, userName, password, role);
            return admin.InsertIntoDatabase();
        }

        public AdminClass SelectOneAdmin(String userName)
        {
            AdminClass admin = new AdminClass();
            return admin.SelectOneAdmin(userName);
        }

        public List<AdminClass> SelectAllAdmin()
        {
            AdminClass admin = new AdminClass();
            return admin.SelectAllAdmin();
        }

        public Int16 InsertOneBlacklist(Int32 duration, String reason, String customerId, String customerName)
        {
            BlackListClass tmp = new BlackListClass(duration, reason, customerId, customerName);
            return tmp.InsertIntoDatabase();
        }

        public BlackListClass SelectOneBlacklist(Guid ID, String customerId)
        {
            BlackListClass tmp = new BlackListClass();
            return tmp.SelectOneBlacklist(ID, customerId);
        }

        public List<BlackListClass> SelectAllBlacklist(String customerId)
        {
            BlackListClass tmp = new BlackListClass();
            return tmp.SelectAllBlacklist(customerId);
        }

        public Int16 UpdateBlacklistDeleted(Guid ID, String customerId, Boolean status)
        {
            BlackListClass tmpClass = new BlackListClass();
            return tmpClass.UpdateBlacklist(ID, customerId, status);
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
        public DataSet SelectDistinctCategoryFromBranch()
        {
            Branch branch = new Branch();
            return branch.SelectDistinctCategory();
        }
        public DataSet SearchFromBranch(string search, string location, string category)
        {
            Branch branch = new Branch();
            return branch.Search(search, location, category);
        }

        public Branch SelectByIdFromBranch(Guid id)
        {
            Branch branch = new Branch();
            return branch.SelectById(id);
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

        //----------------------Review--------------------
        public int InsertReview(double rating, string comment, string title, Guid customerId, Guid branchId)
        {
            Review review = new Review(rating, comment, title, customerId, branchId);
            return review.Insert();
        }

        public DataSet SelectByBranchIdFromReview(Guid id, Guid customerId, string sort)
        {
            Review review = new Review();
            return review.SelectByBranchId(id, customerId, sort);
        }
        public DataSet SelectAllByBranchIdFromReview(Guid id, string sort)
        {
            Review review = new Review();
            return review.SelectAllByBranchId(id, sort);
        }
        public Review HaveExistingReview(Guid branchId, Guid customerId)
        {
            Review review = new Review();
            return review.HaveExistingReview(branchId, customerId);
        }

        public int UpdateReview(int id, String title, String comment, Double rating)
        {
            Review review = new Review();
            return review.Update(id, title, comment, rating);
        }
        public int DeleteReview(int id)
        {
            Review review = new Review();
            return review.Delete(id);
        }

        public DataSet SelectByCustomerIdFromReview(Guid id)
        {
            Review review = new Review();
            return review.SelectByCustomerId(id);
        }

        public double SelectRatingByBranchIdFromReview(Guid branchId)
        {
            Review review = new Review();
            return review.SelectRatingByBranchId(branchId);
        }

        public DataSet SelectReportedReview()
        {
            Review review = new Review();
            return review.SelectReportedReview();
        }

        public int AddNumReportToReview(int id)
        {
            Review review = new Review();
            return review.AddNumReport(id);
        }

        public int ResetNumReportToReview(int id)
        {
            Review review = new Review();
            return review.ResetNumReport(id);
        }

        // appointments
        public int CreateAppointment(string aptdate, string apttime, string bookdate, string booktime, string partysize, DateTime aptDateTime, string customerid, string branchid, string appointmentsettingid)
        {
            Appointment apt = new Appointment(aptdate, apttime, bookdate, booktime, partysize, aptDateTime, customerid, branchid, appointmentsettingid);
            return apt.Insert();
        }

        public int ModifyAppointment(string userid, string aptdate, string apttime, string bookdate, string booktime, string partysize, DateTime aptDateTime, string customerid, string branchid, string appointmentsettingid)
        {
            Appointment apt = new Appointment(aptdate, apttime, bookdate, booktime, partysize, aptDateTime, customerid, branchid, appointmentsettingid);
            return apt.Modify(userid);
        }

        public List<Appointment> GetAllAppointment()
        {
            Appointment apt = new Appointment();
            return apt.SelectAll();
        }

        public List<Appointment> GetAllAppointmentByTodayDate()
        {
            Appointment apt = new Appointment();
            return apt.SelectByTodayDate();
        }

        public List<Appointment> GetAllAppointmentByTodayDateAscend(string aptDate)
        {
            Appointment apt = new Appointment();
            return apt.SelectByTodayDateAscend(aptDate);
        }

        public int setArrived(string aptTime, string aptDate)
        {
            Appointment apt = new Appointment();
            return apt.setArrived(aptTime, aptDate);
        }

        public int deleteAppointment(string aptTime, string aptDate)
        {
            Appointment apt = new Appointment();
            return apt.deleteAppointment(aptTime, aptDate);
        }

        public List<string> selectAllAppointmentDateAscend()
        {
            Appointment apt = new Appointment();
            return apt.selectAllAppointmentDateAscend();
        }

        public int selectCountByDate(string aptDate)
        {
            Appointment apt = new Appointment();
            return apt.selectCountByDate(aptDate);
        }

        // -------------------------------- Business Role --------------------------------
        public BusinessRole CreateBusinessRole(string name, string businessId)
        {
            BusinessRole br = new BusinessRole();
            return br.CreateBusinessRole(name, businessId);
        }

        public BusinessRole GetBusinessRole(string businessRoleId)
        {
            BusinessRole br = new BusinessRole();
            return br.GetBusinessRole(businessRoleId);
        }

        public bool UpdateBusinessRole(string businessRoleId, string name, string businessId)
        {
            BusinessRole br = new BusinessRole();
            if (br.CheckBusinessRoleExists(businessRoleId, businessId))
                return br.UpdateBusinessRole(businessRoleId, name);
            else
                return false;
        }

        public bool DeleteBusinessRole(string businessRoleId)
        {
            BusinessRole br = new BusinessRole();
            return br.DeleteBusinessRole(businessRoleId);
        }

        public List<BusinessRole> GetBusinessRoles(string businessId)
        {
            BusinessRole br = new BusinessRole();
            return br.GetBusinessRoles(businessId);
        }
    }
}
