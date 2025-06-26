using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;


namespace SDSFinder.Models;

public static class AuthorizationPolicies
{ 
    #region PolicyNames
    public const string USER = "User";
    public const string ADMIN_OPEN = "SDS Finder Manager";
    public const string ADMIN_INTERCOMPANY = "SDS Finder Intercompany";
    public const string DEVELOPER = "Developer";
    #endregion
    #region Methods
    /// <summary>
    /// Gets Active Directory roles from Config and creates policies.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public static void AddToolMasterAuthorizationOptions(this AuthorizationOptions options, ConfigurationManager configuration)
    {
        IConfigurationSection policySection = configuration.GetSection("AuthorizationPolicyGroups");

        string[] developerRoles = policySection.ParseConfigIntoList(DEVELOPER);
        options.AddBasicPolicy(DEVELOPER, developerRoles);

        string[] adminOpenRoles = policySection.ParseConfigIntoList(ADMIN_OPEN, developerRoles);
        options.AddBasicPolicy(ADMIN_OPEN, adminOpenRoles);

        string[] adminIntercompanyRoles = policySection.ParseConfigIntoList(ADMIN_INTERCOMPANY, developerRoles);
        options.AddBasicPolicy(ADMIN_INTERCOMPANY, adminIntercompanyRoles);


        string[] elevatedRoles = adminOpenRoles.Concat(developerRoles).Concat(adminIntercompanyRoles).ToArray();
        string[] userRoles = policySection.ParseConfigIntoList(USER, elevatedRoles);
        options.AddBasicPolicy(USER, userRoles);


    }
    #endregion
    private static void AddBasicPolicy(this AuthorizationOptions options, string policyName, string[] allowedRoles)
    {
        options.AddPolicy(policyName, policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.Requirements.Add(new RolesAuthorizationRequirement(allowedRoles));
        });
    }
    private static string[] ParseConfigIntoList(this IConfigurationSection configSection, string valueName, string[]? additionalRoles = null)
    {
        string[] result = configSection.GetValue<string>(valueName).Split(",").Select(x => x.Trim()).ToArray();

        if (additionalRoles is null) { return result; }
        return result.Concat(additionalRoles).ToArray();
    }
}

