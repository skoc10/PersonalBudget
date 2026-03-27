using PersonalBudget.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace PersonalBudget.Permissions;

public class PersonalBudgetPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PersonalBudgetPermissions.GroupName);


        
        //Define your own permissions here. Example:
        //myGroup.AddPermission(PersonalBudgetPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PersonalBudgetResource>(name);
    }
}
