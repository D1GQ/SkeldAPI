using SkeldApi.Enums;
using System.Collections;
using UnityEngine;

namespace SkeldApi.GameModes;

public abstract class CustomGameMode : MonoBehaviour
{
    public abstract void OnStart();

    public abstract void Update();

    public abstract EndGameConditions CheckEndGame();

    public abstract EndGameConditions CheckEndGameOnExiled(NetworkedPlayerInfo? exiled);

    public abstract IEnumerator CoFixBlackScreenAfterMeeting(EndGameConditions endGameCondition);

    public abstract void OnEnd();
}
