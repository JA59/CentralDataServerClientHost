using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace iCDataCenterClientHost.CustomIdentity
{
    public class MyUserStore :  IUserStore<MyUser>,
                                IUserPasswordStore<MyUser>,
                                IUserRoleStore<MyUser>,
                                IPasswordHasher<MyUser>
    {
        public MyUserStore()
        {

        }

        public Task AddToRoleAsync(MyUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(MyUser user, CancellationToken cancellationToken)
        {
            if (XmlStore.Instance.UserDb.ContainsKey(user.Id))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "User already exists" }));
            }

            // find the first free id
            int newId = 1;
            while (XmlStore.Instance.UserDb.ContainsKey(newId)) newId++;

            XmlStore.Instance.UserDb.Add(newId, user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(MyUser user, CancellationToken cancellationToken)
        {
            if (XmlStore.Instance.UserDb.ContainsKey(user.Id))
            {
                XmlStore.Instance.UserDb.Remove(user.Id);
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "User did not exist" }));

        }

        public void Dispose()
        {
            
        }

        public Task<MyUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            MyUser user = null;
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            user = users.SingleOrDefault(f => f.Id.ToString() == userId);
            return Task.FromResult(user);
        }

        public Task<MyUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            MyUser user = null;
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            user = users.SingleOrDefault(f => f.UserName.ToLower() == normalizedUserName.ToLower());
            return Task.FromResult(user);
        }

        public Task<string> GetNormalizedUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            user = users.SingleOrDefault(f => f.Id == user.Id);
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(MyUser user, CancellationToken cancellationToken)
        {
            var hasher = new PasswordHasher<MyUser>();
            return Task.FromResult(hasher.HashPassword(user, user.PasswordHash));
        }

        public Task<IList<string>> GetRolesAsync(MyUser user, CancellationToken cancellationToken)
        {
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            user = users.SingleOrDefault(f => f.Id == user.Id);
            return Task.FromResult(user.Roles);
        }

        public Task<string> GetUserIdAsync(MyUser user, CancellationToken cancellationToken)
        {
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            if (users == null || users.Count == 0)
            {
                return Task.FromResult(String.Empty);
            }

            user = users.SingleOrDefault(f => f.Id == user.Id);

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            if (users == null || users.Count == 0)
            {
                return Task.FromResult(String.Empty);
            }

            user = users.SingleOrDefault(f => f.Id == user.Id);

            return Task.FromResult(user.UserName);
        }

        public Task<IList<MyUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public string HashPassword(MyUser user, string password)
        {
            return user.PasswordHash;
        }

        public Task<bool> HasPasswordAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsInRoleAsync(MyUser user, string roleName, CancellationToken cancellationToken)
        {
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            user = users.SingleOrDefault(f => f.Id == user.Id);
            return Task.FromResult(user.Roles.Contains(roleName));
        }

        public Task RemoveFromRoleAsync(MyUser user, string roleName, CancellationToken cancellationToken)
        {
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            user = users.SingleOrDefault(f => f.Id == user.Id);
            return Task.FromResult(user.Roles.Remove(roleName));
        }

        public Task SetNormalizedUserNameAsync(MyUser user, string normalizedName, CancellationToken cancellationToken)
        {
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            if (users == null || users.Count == 0)
            {
                return Task.FromResult(String.Empty);
            }

            user = users.SingleOrDefault(f => f.Id == user.Id);
            user.NormalizedUserName = normalizedName;

            return Task.FromResult(user.UserName);
        }

        public Task SetPasswordHashAsync(MyUser user, string passwordHash, CancellationToken cancellationToken)
        {
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            user = users.SingleOrDefault(f => f.Id == user.Id);
            user.PasswordHash = passwordHash;

            return Task.FromResult(user.PasswordHash);
        }

        public Task SetUserNameAsync(MyUser user, string userName, CancellationToken cancellationToken)
        {
            IList<MyUser> users = XmlStore.Instance.UserDb.Values.ToList<MyUser>();
            if (users == null || users.Count == 0)
            {
                return Task.FromResult(String.Empty);
            }

            user = users.SingleOrDefault(f => f.Id == user.Id);
            user.UserName = userName;

            return Task.FromResult(user.UserName);
        }

        public Task<IdentityResult> UpdateAsync(MyUser user, CancellationToken cancellationToken)
        {
            if (XmlStore.Instance.UserDb.ContainsKey(user.Id))
            {
                XmlStore.Instance.UserDb[user.Id] = user;
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "User did not exist to update" }));
        }

        public PasswordVerificationResult VerifyHashedPassword(MyUser user, string hashedPassword, string providedPassword)
        {
            return PasswordVerificationResult.Success;
        }
    }
}
