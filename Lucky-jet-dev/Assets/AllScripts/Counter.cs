using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Counter : MonoBehaviour
{
    [SerializeField] private Text _textScore;
    [SerializeField] private int _numberPossibleMiss;

    private int _score;
    private void Awake()
    {
        //EventManager.TouchRingEvent += AddCoin;
        EventManager.MissRingleEvent += MissRing;
        EventManager.StartGameEvent += StartCounter;
        EventManager.ReplayGameEvent += Replay;
        EventManager.PauseGameEvent += PauseCounter;

        if (_textScore == null)
            _textScore = GetComponentInChildren<Text>();

        _textScore.text = _score.ToString();
    }

    private void MissRing()
    {
        throw new NotImplementedException();
    }
    //private void AddCoin()
    //{
        
    //}
    private void Replay()
    {
        _score = 0;
        _textScore.text = _score.ToString();
    }
    private void StartCounter()
    { 
        StartCoroutine(ScoreIncrease());
    }
    private void PauseCounter()
    {
        if (Plane.StopMoving)
            StopCoroutine(ScoreIncrease());
        else
            StartCoroutine(ScoreIncrease());
    }
    private IEnumerator ScoreIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _score++;
            _textScore.text = _score.ToString();
        }
    }
    private void OnDestroy()
    {
        EventManager.MissRingleEvent -= MissRing;
        EventManager.StartGameEvent -= StartCounter;
        EventManager.ReplayGameEvent -= Replay;
        EventManager.PauseGameEvent -= PauseCounter;
    }
}