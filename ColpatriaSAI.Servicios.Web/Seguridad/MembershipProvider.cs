using System.Web.Services;
using System.Collections.Generic;

namespace ColpatriaSAI.Servicios.Web.Seguridad
{
    /// <summary>
    /// Summary description for MembershipProvider
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class MembershipProvider : System.Web.Services.WebService
    {
        protected System.Web.Security.MembershipProvider GetProvider(string providerName, string applicationName)
        {
            System.Web.Security.MembershipProvider provider;
            if ((providerName != null) && (System.Web.Security.Membership.Providers[providerName] != null))
            {
                provider = System.Web.Security.Membership.Providers[providerName];
            }
            else
            {
                provider = System.Web.Security.Membership.Provider;
            }

            if (applicationName != null)
            {
                provider.ApplicationName = applicationName;
            }

            return provider;
        }

        protected MembershipUser ConvertUser(System.Web.Security.MembershipUser user)
        {
            if (user == null) return null;
            MembershipUser membershipUser = new MembershipUser();
            membershipUser.Comment = user.Comment;
            membershipUser.CreationDate = user.CreationDate;
            membershipUser.Email = user.Email;
            membershipUser.IsApproved = user.IsApproved;
            membershipUser.IsLockedOut = user.IsLockedOut;
            membershipUser.IsOnline = user.IsOnline;
            membershipUser.LastActivityDate = user.LastActivityDate;
            membershipUser.LastLockoutDate = user.LastLockoutDate;
            membershipUser.LastLoginDate = user.LastLoginDate;
            membershipUser.LastPasswordChangedDate = user.LastPasswordChangedDate;
            membershipUser.PasswordQuestion = user.PasswordQuestion;
            membershipUser.ProviderName = user.ProviderName;
            membershipUser.ProviderUserKey = user.ProviderUserKey;
            membershipUser.UserName = user.UserName;
            return membershipUser;
        }

        protected System.Web.Security.MembershipUser ConvertUser(System.Web.Security.MembershipProvider provider, MembershipUser user)
        {
            if (user == null) return null;
            System.Web.Security.MembershipUser membershipUser =
                new System.Web.Security.MembershipUser(provider.Name, 
                                                       user.UserName,
                                                       user.ProviderUserKey,
                                                       user.Email,
                                                       user.PasswordQuestion,
                                                       user.Comment,
                                                       user.IsApproved,
                                                       user.IsLockedOut,
                                                       user.CreationDate,
                                                       user.LastLoginDate,
                                                       user.LastActivityDate,
                                                       user.LastPasswordChangedDate,
                                                       user.LastLockoutDate);
            return membershipUser;
        }

        protected List<MembershipUser> BuildUserList(System.Web.Security.MembershipUserCollection collection)
        {
            if (collection == null) return null;
            List<MembershipUser> list = new List<MembershipUser>();
            foreach (System.Web.Security.MembershipUser user in collection)
            {
                list.Add(ConvertUser(user));
            }
            return list;
        }

        public MembershipProvider()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod(Description = "Change the password of a user")]
        public bool ChangePassword(string providerName, string applicationName, string username, 
                                   string oldPassword, string newPassword)
        {
            return GetProvider(providerName, applicationName).ChangePassword(username, oldPassword, newPassword);
        }

        [WebMethod(Description = "Change the password of a user")]
        public bool ChangePasswordQuestionAndAnswer(string providerName, string applicationName, string username, 
                                                    string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return GetProvider(providerName, applicationName).ChangePasswordQuestionAndAnswer(username, password, 
                                                                                              newPasswordQuestion, newPasswordAnswer);
        }

        [WebMethod(Description = "Create a user")]
        public MembershipUser CreateUser(string providerName, string applicationName, string username, 
                                         string password, string email, string passwordQuestion, string passwordAnswer, 
                                         bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            return ConvertUser(GetProvider(providerName, applicationName).CreateUser(username, password, email,
                                                                                     passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status));
        }

        [WebMethod(Description = "Delete a user")]
        public bool DeleteUser(string providerName, string applicationName, string username, bool deleteAllRelatedData)
        {
            return GetProvider(providerName, applicationName).DeleteUser(username, deleteAllRelatedData);
        }

        [WebMethod(Description = "Find user by email address")]
        public List<MembershipUser> FindUsersByEmail(string providerName, string applicationName, string emailToMatch, 
                                                     int pageIndex, int pageSize, out int totalRecords)
        {
            return BuildUserList(GetProvider(providerName, applicationName).FindUsersByEmail(emailToMatch, pageIndex, 
                                                                                             pageSize, out totalRecords));
        }

        [WebMethod(Description = "Find users by username")]
        public List<MembershipUser> FindUsersByName(string providerName, string applicationName, string usernameToMatch, 
                                                    int pageIndex, int pageSize, out int totalRecords)
        {
            return BuildUserList(GetProvider(providerName, applicationName).FindUsersByName(usernameToMatch, pageIndex,
                                                                                            pageSize, out totalRecords));
        }

        [WebMethod(Description = "Get all users")]
        public List<MembershipUser> GetAllUsers(string providerName, string applicationName, int pageIndex, 
                                                int pageSize, out int totalRecords)
        {
            return BuildUserList(GetProvider(providerName, applicationName).GetAllUsers(pageIndex, pageSize, out totalRecords));
        }

        [WebMethod(Description = "Get number of users online")]
        public int GetNumberOfUsersOnline(string providerName, string applicationName)
        {
            return GetProvider(providerName, applicationName).GetNumberOfUsersOnline();
        }

        [WebMethod(Description = "Get the password of a user")]
        public string GetPassword(string providerName, string applicationName, string username, string answer)
        {
            return GetProvider(providerName, applicationName).GetPassword(username, answer);
        }

        [WebMethod(Description = "Get a user by username")]
        public MembershipUser GetUserByUserName(string providerName, string applicationName, string username, 
                                                bool userIsOnline)
        {
            return ConvertUser(GetProvider(providerName, applicationName).GetUser(username, userIsOnline));
        }

        [WebMethod(Description = "Get a user by provider key")]
        public MembershipUser GetUserByKey(string providerName, string applicationName, object providerUserKey, 
                                           bool userIsOnline)
        {
            return ConvertUser(GetProvider(providerName, applicationName).GetUser(providerUserKey, userIsOnline));
        }

        [WebMethod(Description = "Get a user by provider key")]
        public string GetUserNameByEmail(string providerName, string applicationName, string email)
        {
            return GetProvider(providerName, applicationName).GetUserNameByEmail(email);
        }

        [WebMethod(Description = "Reset a users password")]
        public string ResetPassword(string providerName, string applicationName, string username, string answer)
        {
            return GetProvider(providerName, applicationName).ResetPassword(username, answer);
        }

        [WebMethod(Description = "Unlock a user")]
        public bool UnlockUser(string providerName, string applicationName, string userName)
        {
            return GetProvider(providerName, applicationName).UnlockUser(userName);
        }

        [WebMethod(Description = "Update a user's details")]
        public void UpdateUser(string providerName, string applicationName, MembershipUser user)
        {
            System.Web.Security.MembershipProvider provider = GetProvider(providerName, applicationName);
            provider.UpdateUser(ConvertUser(provider, user));
        }

        [WebMethod(Description = "Validate a user by their password")]
        public bool ValidateUser(string providerName, string applicationName, string username, string password)
        {
            return GetProvider(providerName, applicationName).ValidateUser(username, password);
        }
    }
}

