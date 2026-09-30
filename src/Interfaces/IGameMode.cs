using SkeldApi.Enums;
using System.Collections;

namespace SkeldApi.Interfaces;

public interface IGameMode
{
    void OnStart();

    void Update();

    EndGameCondition CheckEndGame();

    EndGameCondition CheckEndGameOnExiled(NetworkedPlayerInfo? exiled);

    IEnumerator CoFixBlackScreenAfterMeeting(EndGameCondition endGameCondition);

    void OnEnd();
}
