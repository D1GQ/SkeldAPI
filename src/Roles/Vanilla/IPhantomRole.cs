namespace SkeldApi.Roles.Vanilla;

public interface IPhantomRole : IImpostorRole
{
    bool Vanish();

    bool Appear(ref bool shouldAnimate);
}
