using SkeldApi.Enums;
using SkeldApi.Interfaces;
using System.Collections;

namespace SkeldApi.Modules.GameModes;

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

    public IEnumerator CoFixBlackScreenAfterMeeting(EndGameCondition endGameCondition)
    {
        throw new NotImplementedException();
    }

    public EndGameCondition CheckEndGame()
    {
        throw new NotImplementedException();
    }

    public EndGameCondition CheckEndGameOnExiled(NetworkedPlayerInfo? exiled)
    {
        throw new NotImplementedException();
    }

    public void OnEnd()
    {
        throw new NotImplementedException();
    }
}
