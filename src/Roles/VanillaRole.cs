using AmongUs.GameOptions;

namespace SkeldApi.Roles;

public class VanillaRole : CustomRole
{
    private RoleTypes _roleType;
    public override RoleTypes InitialVanillaRoleType => _roleType;
    public void SetRoleType(RoleTypes roleType)
    {
        _roleType = roleType;
    }
}
