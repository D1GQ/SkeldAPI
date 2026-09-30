namespace SkeldApi.Roles.Vanilla;

public interface IGuardianAngelRole : ICrewmateRole
{
    bool Protect(PlayerControl playerControl);
}
