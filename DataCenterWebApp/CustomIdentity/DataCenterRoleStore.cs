using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iCDataCenterClientHost.CustomIdentity
{
    /// <summary>
    /// IRoleStore for iC Data Center roles
    /// </summary>
    public class DataCenterRoleStore : IRoleStore<DataCenterRole>
    {
        public Task<IdentityResult> CreateAsync(DataCenterRole role, CancellationToken cancellationToken)
        {
            if (DataCenterIdentities.Instance.RoleDictionary.ContainsKey(role.Id))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Role already exists" }));

            }

            // find the first free id
            DataCenterIdentities.Instance.RoleDictionary.Add(role.Id, role);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(DataCenterRole role, CancellationToken cancellationToken)
        {
            if (DataCenterIdentities.Instance.RoleDictionary.ContainsKey(role.Id))
            {
                DataCenterIdentities.Instance.RoleDictionary.Remove(role.Id);
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "role did not exist" }));
        }

        public void Dispose()
        {
        }

        public Task<DataCenterRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            DataCenterRole role = null;
            IList<DataCenterRole> roles = DataCenterIdentities.Instance.RoleDictionary.Values.ToList<DataCenterRole>();
            role = roles.SingleOrDefault(f => f.Id.ToString() == roleId);
            return Task.FromResult(role);
        }

        public Task<DataCenterRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            DataCenterRole role = null;
            IList<DataCenterRole> roles = DataCenterIdentities.Instance.RoleDictionary.Values.ToList<DataCenterRole>();
            role = roles.SingleOrDefault(f => f.Name == normalizedRoleName);
            return Task.FromResult(role);
        }

        public Task<string> GetNormalizedRoleNameAsync(DataCenterRole role, CancellationToken cancellationToken)
        {
            DataCenterRole r = null;
            IList<DataCenterRole> roles = DataCenterIdentities.Instance.RoleDictionary.Values.ToList<DataCenterRole>();
            r = roles.SingleOrDefault(f => f.Id == role.Id);
            return Task.FromResult(r.Name);
        }

        public Task<string> GetRoleIdAsync(DataCenterRole role, CancellationToken cancellationToken)
        {
            DataCenterRole r = null;
            IList<DataCenterRole> roles = DataCenterIdentities.Instance.RoleDictionary.Values.ToList<DataCenterRole>();
            r = roles.SingleOrDefault(f => f.Id == role.Id);
            return Task.FromResult(r.Id);
        }

        public Task<string> GetRoleNameAsync(DataCenterRole role, CancellationToken cancellationToken)
        {
            DataCenterRole r = null;
            IList<DataCenterRole> roles = DataCenterIdentities.Instance.RoleDictionary.Values.ToList<DataCenterRole>();
            r = roles.SingleOrDefault(f => f.Id == role.Id);
            return Task.FromResult(r.Name);
        }

        public Task SetNormalizedRoleNameAsync(DataCenterRole role, string normalizedName, CancellationToken cancellationToken)
        {
            DataCenterRole r = null;
            IList<DataCenterRole> roles = DataCenterIdentities.Instance.RoleDictionary.Values.ToList<DataCenterRole>();
            r = roles.SingleOrDefault(f => f.Id == role.Id);
            return Task.FromResult(r.Name);
        }

        public Task SetRoleNameAsync(DataCenterRole role, string roleName, CancellationToken cancellationToken)
        {
            IList<DataCenterRole> roles = DataCenterIdentities.Instance.RoleDictionary.Values.ToList<DataCenterRole>();

            role = roles.SingleOrDefault(f => f.Id == role.Id);
            role.Name = roleName;

            return Task.FromResult(role.Name);
        }

        public Task<IdentityResult> UpdateAsync(DataCenterRole role, CancellationToken cancellationToken)
        {
            if (DataCenterIdentities.Instance.RoleDictionary.ContainsKey(role.Id))
            {
                DataCenterIdentities.Instance.RoleDictionary[role.Id] = role;
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "role did not exist to update" }));
        }
    }
}
