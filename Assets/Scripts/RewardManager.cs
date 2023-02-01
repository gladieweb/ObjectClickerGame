using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class RewardManager : Singleton<RewardManager>
{
    private float coinMultiplier = 1;
    private const int preferredRewardAmount = 3;
    public static event Action<float> OnTimeAdded;
    public static event Action<float> OnTimeSpeedChanged;

    public void GrantReward(ClickObject clickObjectData)
    {
        GrantCoins(clickObjectData);
        GrantNextThreeCoinObjects(clickObjectData);
        GrantTime(clickObjectData);
        GrantScramble(clickObjectData);
        GrantBlockInput(clickObjectData);
        DuplicateCoinsAndTimer(clickObjectData);

    }

    private void DuplicateCoinsAndTimer(ClickObject clickObjectData)
    {
        if (clickObjectData.duplicateGivenPoints)
        {
            coinMultiplier = coinMultiplier * 2;
            OnTimeSpeedChanged?.Invoke(clickObjectData.clockSpeed);
        }
    }

    private void GrantBlockInput(ClickObject clickObjectData)
    {
        if (clickObjectData.blockCapabilitiesTime > 0)
        {
            StartCoroutine(FreezeInput(clickObjectData.blockCapabilitiesTime));
        }
    }

    private  void GrantScramble(ClickObject clickObjectData)
    {
        if (clickObjectData.scrambleAll)
        {
            ClickObjectFactory.Instance.Scramble(GameManager.Instance.SpawnSpace);
        }
    }

    private  void GrantTime(ClickObject clickObjectData)
    {
        var timeToAdd = 0.0f;
        if (clickObjectData.fixedExtraTime > 0)
        {
            timeToAdd = clickObjectData.fixedExtraTime;
        }

        if (clickObjectData.per20PointExtraTime > 0)
        {
            timeToAdd += (GameManager.Instance.GetPlayerPoints() / 20.0f) * clickObjectData.per20PointExtraTime;
        }

        OnTimeAdded?.Invoke(timeToAdd);
    }

    private  void GrantNextThreeCoinObjects(ClickObject clickObjectData)
    {
        if (clickObjectData.threeCoin)
        {
            GameManager.Instance.PreferredSpawn = preferredRewardAmount;
        }
    }

    private void GrantCoins(ClickObject clickObjectData)
    {
        var coins = clickObjectData.givePoints;
        if (clickObjectData.from1RandomPoints)
        {
            coins = Random.Range(1, coins+1);
        }

        coins = (int)(coins * coinMultiplier);
        
        GameManager.Instance.AddPlayerPoints(coins);
    }

    public void LoseReward(ClickObject clickObjectData)
    {
        var coins = clickObjectData.losePoints;
        GameManager.Instance.DecreasePlayerPoints(coins);
    }

    IEnumerator FreezeInput(float seconds)
    {
        GameManager.canClick = false;
        yield return new WaitForSeconds(seconds);
        GameManager.canClick = true;
    }
}
