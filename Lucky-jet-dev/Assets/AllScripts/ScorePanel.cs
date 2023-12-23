using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private Text _textCurrentScore;
    [SerializeField] private Text _textMaxScore;

    private int _maxScore;

    private void OnEnable()
    {
        if (_textCurrentScore == null)
            _textCurrentScore = transform.Find("Text Current Score").GetComponent<Text>();
    }
    private void Start()
    {
        _maxScore = PlayerPrefs.GetInt("MaxScore");

        UpdateText();
    }
    private void UpdateText()
    {
        _textCurrentScore.text = Counter.Score.ToString();

        if (_maxScore > 0)
            _textMaxScore.text = $"Record {_maxScore}";
        else
            _textMaxScore.text = null;
    }
    private void OnApplicationQuit()
    {
        if (Counter.Score > _maxScore)
            PlayerPrefs.SetInt("MaxScore", _maxScore);
    }
}
