using System;

public static class EventManager
{
    public static Action StartGameEvent;
    public static Action ReplayGameEvent;
    public static Action PauseGameEvent;
    //public static Action TouchRingEvent;
    public static Action MissRingleEvent;

    public static Action<int> UpdateScoreEvent;

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

    public static void UpdateScore(int value)
    {
        UpdateScoreEvent?.Invoke(value);
    }
}
