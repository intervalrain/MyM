using System.Reflection;

using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Security.Permissions;
using MyMoney.Domain.Enums;

namespace MyMoney.Infrastructure.Security;

public class PermissionProvider : IPermissionProvider
{
    public IEnumerable<string> GetPermissions(UserType userType)
    {
        return userType switch
        {
            UserType.Admin => GetAllPermissions(),
            _ => GetPermissionsByCategory(nameof(Permission))
        };
    }

    private IEnumerable<string> GetAllPermissions()
    {
        return GetPermissionsFromNestedTypes(typeof(Permission).GetNestedTypes(BindingFlags.Public | BindingFlags.Static));
    }

    private IEnumerable<string> GetPermissionsByCategory(string category)
    {
        var nestedType = typeof(Permission).GetNestedType(category, BindingFlags.Public | BindingFlags.Static);

        return nestedType == null
            ? throw new ArgumentException($"Category '{category}' not found.")
            : (IEnumerable<string>)GetPermissionsFromNestedTypes(nestedType);
    }

    private static List<string> GetPermissionsFromNestedTypes(params Type[] types)
    {
        var permissions = new List<string>();

        foreach (var type in types)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string));

            foreach (var field in fields)
            {
                if (field.GetValue(null) is string value)
                {
                    permissions.Add(value);
                }
            }
        }

        return permissions;
    }


}

