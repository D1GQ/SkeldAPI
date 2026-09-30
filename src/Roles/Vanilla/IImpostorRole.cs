namespace SkeldApi.Roles.Vanilla;

public interface IImpostorRole : IVentRole
{
    bool Kill(PlayerControl target);

    bool Sabotage(SystemTypes system, byte tag);
}
