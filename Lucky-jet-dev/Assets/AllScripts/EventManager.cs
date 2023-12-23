using System;

public static class EventManager
{
    public static Action StartGameEvent;
    public static Action ReplayGameEvent;
    public static Action PauseGameEvent;
    public static Action MissRingleEvent;
    public static Action UpdateScoreEvent;
    public static Action FailGameEvent;
    public static Action CirclePassedEvent;

    public static void StartGame()
    {
        StartGameEvent?.Invoke();
    }
    public static void ReplayGame()
    {
        ReplayGameEvent?.Invoke();
    }
    public static void PauseGame()
    {
        PauseGameEvent?.Invoke();
    }
    public static void UpdateScore()
    {
        UpdateScoreEvent?.Invoke();
    }
    public static void MissRingle()
    {
        MissRingleEvent?.Invoke();
    }
    public static void FailGame()
    {
        PauseGame();
        FailGameEvent?.Invoke();
    }
    public static void CirclePassed()
    {
        CirclePassedEvent?.Invoke();
    }
}
