using UnityEngine;
using UnityEngine.UI;
public class Counter : MonoBehaviour
{
    [SerializeField] private Text _texCurrenttScore;
    [SerializeField] private int _numberPossibleMiss;

    private int _numberMiss;
    private float _durationFlying;

    public static int Score;
    private void Awake()
    {
        EventManager.MissRingleEvent += MissRing;
        EventManager.ReplayGameEvent += Replay;

        if (_texCurrenttScore == null)
            _texCurrenttScore = GetComponent<Text>();

        _texCurrenttScore.text = Score.ToString();
    }

    private void MissRing()
    {
        _numberMiss++;

        if (_numberMiss >= _numberPossibleMiss)
            EventManager.FailGame();
    }
    private void Replay()
    {
        Score = 0;
        _numberMiss = 0;
        _durationFlying = 0;
        _texCurrenttScore.text = Score.ToString();
    }
    private void Update()
    {
        if (!Plane.StopMoving)
        {
            _durationFlying += Time.deltaTime;

            float score = Plane.Speed * _durationFlying;
            Score = (int)(float)score;
            _texCurrenttScore.text = Score.ToString();
        }
    }
    private void OnDestroy()
    {
        EventManager.MissRingleEvent -= MissRing;
        EventManager.ReplayGameEvent -= Replay;
    }
}