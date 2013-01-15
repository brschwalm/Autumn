using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;
using Autumn.Mvc.Infrastructure.Helpers;

namespace Autumn.Mvc.Infrastructure.Authentication
{
    public class AccountMembershipService : IMembershipService
    {
        private MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public string GetCanonicalUsername(string userName)
        {
            var user = _provider.GetUser(userName, true);
            if (user != null)
            {
                return user.UserName;
            }

            return null;
        }

		/// <summary>
		/// Creates a new user in the Membership Database
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="email"></param>
		/// <param name="id">The Id of the user (if it were openid or auth id)</param>
		/// <returns></returns>
        public MembershipUser CreateUser(string userName, string password, string email, string id, out MembershipCreateStatus status)
        {
            MembershipUser user = _provider.CreateUser(userName, password, email, null, null, true, StringHelper.StringToGuid(id), out status);

            return user;
        }

		/// <summary>
		/// Creates a new user in the Membership Database without the Open ID
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		public MembershipUser CreateUser(string userName, string password, string email, out MembershipCreateStatus status)
		{
			MembershipUser user = _provider.CreateUser(userName, password, email, null, null, true, null, out status);

			return user;
		}

		/// <summary>
		/// Gets a user by their ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public MembershipUser GetUserById(string id)
        {
			return _provider.GetUser(StringHelper.StringToGuid(id), true);
        }

		/// <summary>
		/// Gets a user by their UserName
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public MembershipUser GetUser(string userName)
		{
			return _provider.GetUser(userName, true);
		}

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }

        /// <summary>
        /// Converts a string to a GUID
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		//public Guid StringToGUID(string value)
		//{
		//    // Create a new instance of the MD5CryptoServiceProvider object.
		//    MD5 md5Hasher = MD5.Create();
		//    // Convert the input string to a byte array and compute the hash.
		//    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
		//    return new Guid(data);
		//}
    }
}
