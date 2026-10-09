namespace SkeldApi.Roles.Vanilla;

public interface IShapeshifterRole : IImpostorRole
{
    bool Shapeshift(PlayerControl target, ref bool shouldAnimate);
}
