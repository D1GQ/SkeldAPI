using SkeldApi.Enums;
using System.Collections;
using UnityEngine;

namespace SkeldApi.GameModes;

public abstract class CustomGameMode : MonoBehaviour
{
    public abstract IEnumerator CoStart();

    public abstract void Update();

    public abstract EndGameConditions CheckEndGame();

    public abstract EndGameConditions CheckEndGameOnExiled(NetworkedPlayerInfo? exiled);

    public abstract IEnumerator CoFixBlackScreenAfterMeeting(EndGameConditions endGameCondition);

    public abstract IEnumerator CoEnd();
}
