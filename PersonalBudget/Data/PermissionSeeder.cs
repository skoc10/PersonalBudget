using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalBudget.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace PersonalBudget.Data;

public class PermissionSeeder : ITransientDependency
{
    private readonly IPermissionManager _permissionManager;
    private readonly IIdentityRoleRepository _roleRepository;

    public PermissionSeeder(
        IPermissionManager permissionManager,
        IIdentityRoleRepository roleRepository)
    {
        _permissionManager = permissionManager;
        _roleRepository = roleRepository;
    }

    public async Task SeedAsync()
    {
        // Get or create admin role
        var adminRole = await _roleRepository.FindByNameAsync("admin");
        if (adminRole == null)
        {
            return; // If admin role doesn't exist, skip seeding
        }

        // Define permissions to grant
        var permissionsToGrant = new List<string>
        {
            PersonalBudgetPermissions.Categories.Default,
            PersonalBudgetPermissions.Categories.Create,
            PersonalBudgetPermissions.Categories.Edit,
            PersonalBudgetPermissions.Categories.Delete,
            PersonalBudgetPermissions.Expenses.Default,
            PersonalBudgetPermissions.Expenses.Create,
            PersonalBudgetPermissions.Expenses.Edit,
            PersonalBudgetPermissions.Expenses.Delete,
        };

        // Grant permissions to admin role
        foreach (var permission in permissionsToGrant)
        {
            await _permissionManager.SetAsync(permission, "R:" + adminRole.Name, true);
        }
    }
}
