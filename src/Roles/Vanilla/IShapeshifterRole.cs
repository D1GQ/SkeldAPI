namespace SkeldApi.Roles.Vanilla;

public interface IShapeshifterRole : IImpostorRole
{
    bool CheckShapeshift(PlayerControl target, ref bool shouldAnimate);
}
