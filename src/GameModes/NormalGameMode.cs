using SkeldApi.Attributes.Register;
using SkeldApi.Enums;
using SkeldApi.Managers;
using SkeldApi.Roles;
using System.Collections;

namespace SkeldApi.GameModes;

[RegisterGameMode(SkeldGameModes.Normal)]
internal class NormalGameMode : CustomGameMode
{
    public override IEnumerator CoStart()
    {
        AssignRoles();
        yield return CoPlayIntro();
    }

    public virtual void AssignRoles()
    {
        int playerCount = PlayerControl.AllPlayerControls.Count;
        CustomRole[] roles = new CustomRole[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            roles[i] = CustomRoleManager.AllCustomRoles.First();

            PlayerControl player = PlayerControl.AllPlayerControls[i];
            var modifiers = GetModifiers(player, roles[i]);
        }
    }

    public virtual RoleModifier[] GetModifiers(PlayerControl player, CustomRole role)
    {
        List<RoleModifier> modifiers = [];
        return [.. modifiers];
    }

    public virtual IEnumerator CoPlayIntro()
    {
        throw new NotImplementedException();
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }

    public override IEnumerator CoFixBlackScreenAfterMeeting(EndGameConditions endGameCondition)
    {
        throw new NotImplementedException();
    }

    public override EndGameConditions CheckEndGame()
    {
        throw new NotImplementedException();
    }

    public override EndGameConditions CheckEndGameOnExiled(NetworkedPlayerInfo? exiled)
    {
        throw new NotImplementedException();
    }

    public override IEnumerator CoEnd()
    {
        throw new NotImplementedException();
    }
}
