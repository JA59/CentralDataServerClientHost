using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iCDataCenterClientHost.CustomIdentity
{
    public class MyRoleStore : IRoleStore<MyRole>
    {
        public Task<IdentityResult> CreateAsync(MyRole role, CancellationToken cancellationToken)
        {
            if (XmlStore.Instance.RoleDb.ContainsKey(role.Id))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Role already exists" }));

            }

            // find the first free id
            XmlStore.Instance.RoleDb.Add(role.Id, role);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(MyRole role, CancellationToken cancellationToken)
        {
            if (XmlStore.Instance.RoleDb.ContainsKey(role.Id))
            {
                XmlStore.Instance.RoleDb.Remove(role.Id);
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "role did not exist" }));
        }

        public void Dispose()
        {
        }

        public Task<MyRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            MyRole role = null;
            IList<MyRole> roles = XmlStore.Instance.RoleDb.Values.ToList<MyRole>();
            role = roles.SingleOrDefault(f => f.Id.ToString() == roleId);
            return Task.FromResult(role);
        }

        public Task<MyRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            MyRole role = null;
            IList<MyRole> roles = XmlStore.Instance.RoleDb.Values.ToList<MyRole>();
            role = roles.SingleOrDefault(f => f.Name == normalizedRoleName);
            return Task.FromResult(role);
        }

        public Task<string> GetNormalizedRoleNameAsync(MyRole role, CancellationToken cancellationToken)
        {
            MyRole r = null;
            IList<MyRole> roles = XmlStore.Instance.RoleDb.Values.ToList<MyRole>();
            r = roles.SingleOrDefault(f => f.Id == role.Id);
            return Task.FromResult(r.Name);
        }

        public Task<string> GetRoleIdAsync(MyRole role, CancellationToken cancellationToken)
        {
            MyRole r = null;
            IList<MyRole> roles = XmlStore.Instance.RoleDb.Values.ToList<MyRole>();
            r = roles.SingleOrDefault(f => f.Id == role.Id);
            return Task.FromResult(r.Id);
        }

        public Task<string> GetRoleNameAsync(MyRole role, CancellationToken cancellationToken)
        {
            MyRole r = null;
            IList<MyRole> roles = XmlStore.Instance.RoleDb.Values.ToList<MyRole>();
            r = roles.SingleOrDefault(f => f.Id == role.Id);
            return Task.FromResult(r.Name);
        }

        public Task SetNormalizedRoleNameAsync(MyRole role, string normalizedName, CancellationToken cancellationToken)
        {
            MyRole r = null;
            IList<MyRole> roles = XmlStore.Instance.RoleDb.Values.ToList<MyRole>();
            r = roles.SingleOrDefault(f => f.Id == role.Id);
            return Task.FromResult(r.Name);
        }

        public Task SetRoleNameAsync(MyRole role, string roleName, CancellationToken cancellationToken)
        {
            IList<MyRole> roles = XmlStore.Instance.RoleDb.Values.ToList<MyRole>();

            role = roles.SingleOrDefault(f => f.Id == role.Id);
            role.Name = roleName;

            return Task.FromResult(role.Name);
        }

        public Task<IdentityResult> UpdateAsync(MyRole role, CancellationToken cancellationToken)
        {
            if (XmlStore.Instance.RoleDb.ContainsKey(role.Id))
            {
                XmlStore.Instance.RoleDb[role.Id] = role;
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "role did not exist to update" }));
        }
    }
}
