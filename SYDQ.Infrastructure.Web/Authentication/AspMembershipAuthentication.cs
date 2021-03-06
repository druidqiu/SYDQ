﻿using System;
using System.Web.Security;

namespace SYDQ.Infrastructure.Web.Authentication
{
    public class AspMembershipAuthentication : ILocalAuthenticationService
    {
        public User Login(string email, string password)
        {
            var user = new User {IsAuthenticated = false};

            if (!Membership.ValidateUser(email, password)) return user;
            
            var validatedUser = Membership.GetUser(email);

            if (validatedUser == null || validatedUser.ProviderUserKey == null) return user;

            user.AuthenticationToken = validatedUser.ProviderUserKey.ToString();
            user.Email = email;
            user.IsAuthenticated = true;

            return user;
        }

        public User RegisterUser(string email, string password)
        {
            MembershipCreateStatus status;
            var user = new User {IsAuthenticated = false};

            Membership.CreateUser(email, password, email,
                Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
                true, out status);

            if (status == MembershipCreateStatus.Success)
            {
                var newlyCreatedUser = Membership.GetUser(email);
                if (newlyCreatedUser != null && newlyCreatedUser.ProviderUserKey != null)
                {
                    user.AuthenticationToken = newlyCreatedUser.ProviderUserKey.ToString();
                }
                user.Email = email;
                user.IsAuthenticated = true;
            }
            else
            {
                switch (status)
                {
                    case MembershipCreateStatus.DuplicateEmail:
                        throw new InvalidOperationException(
                            "There is already a user with this email address.");
                    case MembershipCreateStatus.DuplicateUserName:
                        throw new InvalidOperationException(
                            "There is already a user with this email address.");
                    case MembershipCreateStatus.InvalidEmail:
                        throw new InvalidOperationException(
                            "Your email address is invalid");
                    default:
                        throw new InvalidOperationException(
                            "There was a problem creating your account. Please try again.");
                }
            }

            return user;
        }
    }
}
