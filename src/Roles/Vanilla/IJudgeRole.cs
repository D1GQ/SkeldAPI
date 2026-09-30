namespace SkeldApi.Roles.Vanilla;

public interface IJudgeRole : ICrewmateRole
{
    bool Overrule(PlayerControl target);
}
