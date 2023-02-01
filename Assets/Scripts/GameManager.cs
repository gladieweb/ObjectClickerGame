using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string HomeScreenID = "Home";
    [SerializeField] private string HudScerenID = "Hud";
    [SerializeField] private string DebriefingID = "Debriefing";
    [SerializeField] private MMF_Player countdownFeedback;
    
    private int PlayerPoints = 0;
    private Coroutine _gameLoopCoroutine;
    private const int winCondition = 200;
    public Rect SpawnSpace;
    public GameDifficultSetting currentSettings;
    
    private bool isMatchLost = false;
    private bool isMatchWon = false;

    public static event Action<int> OnPointsChanged;
    public int PreferredSpawn = 0;
    public static bool canClick = true;
    IEnumerator Start()
    {
        var prepareTask = PrepareScene();
        yield return prepareTask;
        ShowMainMenu();
    }
    
     private void ShowMainMenu()
    {
        ScreenManager.Instance.OpenScreen(HomeScreenID);
    }
    IEnumerator PrepareScene()
    {
        yield break;
    }

    public void StartGame()
    {
        countdownFeedback.PlayFeedbacks();
        _gameLoopCoroutine = StartCoroutine(GameMatch());
    }

    public void EndGame()
    {
        isMatchLost = true;
        StopGame();
    }
    

    private IEnumerator GameMatch()
    {
        yield return new WaitUntil(() => !countdownFeedback.IsPlaying);
        Debug.Log("Play!");
        ScreenManager.Instance.OpenScreen(HudScerenID);

        while (!isMatchLost && !isMatchWon)
        {
            ManageSpawn();
            var spawnTimeInterval = Random.Range(currentSettings.minSpawnTime, currentSettings.maxSpawnTime);
            yield return new WaitForSeconds(spawnTimeInterval);
        }
    }

    private void ManageSpawn()
    {
        if (ClickObjectFactory.Instance.GetObjectsAmount() < currentSettings.maxScreenObjects)
        {
            if (ClickObjectFactory.Instance.GetObjectsAmount() < currentSettings.minScreenObjects)
            {
                do
                {
                    Spawn();
                    Debug.Log(ClickObjectFactory.Instance.GetObjectsAmount() +" /  "+ currentSettings.minScreenObjects);
                } while (ClickObjectFactory.Instance.GetObjectsAmount() < currentSettings.minScreenObjects);
            }
            else
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        if (PreferredSpawn > 0)
        {
            var clickObject = ClickObjectFactory.Instance.CreatePriorityClickObject(SpawnSpace);
            PreferredSpawn--;
        }
        else
        {
            var clickObject = ClickObjectFactory.Instance.CreateRandomClickObject(SpawnSpace);
        }
    }
    public void AddPlayerPoints(int val)
    {
        PlayerPoints += val;

        if (OnPointsChanged != null)
        {
            OnPointsChanged(PlayerPoints);
        }
        
        if (PlayerPoints >= winCondition)
        {
            isMatchWon = true;
            StopGame();
        }
    }

    private void StopGame()
    {
        StopCoroutine(_gameLoopCoroutine);
        ClickObjectFactory.Instance.RemoveAllObjects();
        ScreenManager.Instance.CloseScreen(HudScerenID);
        ScreenManager.Instance.OpenScreen(DebriefingID);
    }
    public void DecreasePlayerPoints(int val)
    {
        if(isMatchWon) return;
        PlayerPoints -= val;
        
        if (OnPointsChanged != null)
        {
            OnPointsChanged(PlayerPoints);
        }
    }

    public int GetPlayerPoints()
    {
        return PlayerPoints;
    }
    
}
