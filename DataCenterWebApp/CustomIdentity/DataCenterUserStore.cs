using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace iCDataCenterClientHost.CustomIdentity
{
    /// <summary>
    /// IUserStore for iC Data Center users.
    /// </summary>
    public class DataCenterUserStore :  IUserStore<DataCenterUser>,
                                IUserPasswordStore<DataCenterUser>,
                                IUserRoleStore<DataCenterUser>,
                                IPasswordHasher<DataCenterUser>
    {
        public DataCenterUserStore()
        {

        }

        public Task AddToRoleAsync(DataCenterUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(DataCenterUser user, CancellationToken cancellationToken)
        {
            if (DataCenterIdentities.Instance.UserDictionary.ContainsKey(user.Id))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "User already exists" }));
            }

            // find the first free id
            int newId = 1;
            while (DataCenterIdentities.Instance.UserDictionary.ContainsKey(newId)) newId++;

            DataCenterIdentities.Instance.UserDictionary.Add(newId, user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(DataCenterUser user, CancellationToken cancellationToken)
        {
            if (DataCenterIdentities.Instance.UserDictionary.ContainsKey(user.Id))
            {
                DataCenterIdentities.Instance.UserDictionary.Remove(user.Id);
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "User did not exist" }));

        }

        public void Dispose()
        {
            
        }

        public Task<DataCenterUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            DataCenterUser user = null;
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            user = users.SingleOrDefault(f => f.Id.ToString() == userId);
            return Task.FromResult(user);
        }

        public Task<DataCenterUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            DataCenterUser user = null;
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            user = users.SingleOrDefault(f => f.UserName.ToLower() == normalizedUserName.ToLower());
            return Task.FromResult(user);
        }

        public Task<string> GetNormalizedUserNameAsync(DataCenterUser user, CancellationToken cancellationToken)
        {
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            user = users.SingleOrDefault(f => f.Id == user.Id);
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(DataCenterUser user, CancellationToken cancellationToken)
        {
            var hasher = new PasswordHasher<DataCenterUser>();
            return Task.FromResult(hasher.HashPassword(user, user.PasswordHash));
        }

        public Task<IList<string>> GetRolesAsync(DataCenterUser user, CancellationToken cancellationToken)
        {
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            user = users.SingleOrDefault(f => f.Id == user.Id);
            return Task.FromResult(user.Roles);
        }

        public Task<string> GetUserIdAsync(DataCenterUser user, CancellationToken cancellationToken)
        {
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            if (users == null || users.Count == 0)
            {
                return Task.FromResult(String.Empty);
            }

            user = users.SingleOrDefault(f => f.Id == user.Id);

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(DataCenterUser user, CancellationToken cancellationToken)
        {
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            if (users == null || users.Count == 0)
            {
                return Task.FromResult(String.Empty);
            }

            user = users.SingleOrDefault(f => f.Id == user.Id);

            return Task.FromResult(user.UserName);
        }

        public Task<IList<DataCenterUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public string HashPassword(DataCenterUser user, string password)
        {
            return user.PasswordHash;
        }

        public Task<bool> HasPasswordAsync(DataCenterUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsInRoleAsync(DataCenterUser user, string roleName, CancellationToken cancellationToken)
        {
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            user = users.SingleOrDefault(f => f.Id == user.Id);
            return Task.FromResult(user.Roles.Contains(roleName));
        }

        public Task RemoveFromRoleAsync(DataCenterUser user, string roleName, CancellationToken cancellationToken)
        {
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            user = users.SingleOrDefault(f => f.Id == user.Id);
            return Task.FromResult(user.Roles.Remove(roleName));
        }

        public Task SetNormalizedUserNameAsync(DataCenterUser user, string normalizedName, CancellationToken cancellationToken)
        {
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            if (users == null || users.Count == 0)
            {
                return Task.FromResult(String.Empty);
            }

            user = users.SingleOrDefault(f => f.Id == user.Id);
            user.NormalizedUserName = normalizedName;

            return Task.FromResult(user.UserName);
        }

        public Task SetPasswordHashAsync(DataCenterUser user, string passwordHash, CancellationToken cancellationToken)
        {
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            user = users.SingleOrDefault(f => f.Id == user.Id);
            user.PasswordHash = passwordHash;

            return Task.FromResult(user.PasswordHash);
        }

        public Task SetUserNameAsync(DataCenterUser user, string userName, CancellationToken cancellationToken)
        {
            IList<DataCenterUser> users = DataCenterIdentities.Instance.UserDictionary.Values.ToList<DataCenterUser>();
            if (users == null || users.Count == 0)
            {
                return Task.FromResult(String.Empty);
            }

            user = users.SingleOrDefault(f => f.Id == user.Id);
            user.UserName = userName;

            return Task.FromResult(user.UserName);
        }

        public Task<IdentityResult> UpdateAsync(DataCenterUser user, CancellationToken cancellationToken)
        {
            if (DataCenterIdentities.Instance.UserDictionary.ContainsKey(user.Id))
            {
                DataCenterIdentities.Instance.UserDictionary[user.Id] = user;
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "User did not exist to update" }));
        }

        public PasswordVerificationResult VerifyHashedPassword(DataCenterUser user, string hashedPassword, string providedPassword)
        {
            return PasswordVerificationResult.Success;
        }
    }
}
