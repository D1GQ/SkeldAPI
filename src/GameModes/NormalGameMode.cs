using SkeldApi.Attributes.Register;
using SkeldApi.Enums;
using System.Collections;

namespace SkeldApi.GameModes;

[RegisterGameMode(SkeldGameModes.Normal)]
internal class NormalGameMode : CustomGameMode
{
    public override void OnStart()
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

    public override void OnEnd()
    {
        throw new NotImplementedException();
    }
}
