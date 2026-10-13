using SkeldApi.Attributes;
using SkeldApi.Enums;
using SkeldApi.Interfaces;
using System.Collections;

namespace SkeldApi.Modules.GameModes;

[RegisterGameMode(SkeldGameModes.Normal)]
internal class NormalGameMode : IGameMode
{
    public void OnStart()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public IEnumerator CoFixBlackScreenAfterMeeting(EndGameConditions endGameCondition)
    {
        throw new NotImplementedException();
    }

    public EndGameConditions CheckEndGame()
    {
        throw new NotImplementedException();
    }

    public EndGameConditions CheckEndGameOnExiled(NetworkedPlayerInfo? exiled)
    {
        throw new NotImplementedException();
    }

    public void OnEnd()
    {
        throw new NotImplementedException();
    }
}
