using SkeldApi.Enums;
using System.Collections;

namespace SkeldApi.Interfaces;

public interface IGameMode
{
    void OnStart();

    void Update();

    EndGameConditions CheckEndGame();

    EndGameConditions CheckEndGameOnExiled(NetworkedPlayerInfo? exiled);

    IEnumerator CoFixBlackScreenAfterMeeting(EndGameConditions endGameCondition);

    void OnEnd();
}
