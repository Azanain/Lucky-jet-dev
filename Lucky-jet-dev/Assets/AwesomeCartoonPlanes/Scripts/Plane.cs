using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour
{
	[SerializeField] private Transform _body;
	[SerializeField] private Transform _propeller1;
	[SerializeField] private Transform _propeller2;

	[Header("Parametrs")]
	[SerializeField] private float _moveSpeed;
	[Range(0, 10)] [SerializeField] private float _speedRotate;
	[SerializeField] private int _speedIncreaseInterval;
	[SerializeField] private int _percentIncreaseSpeed;

	[Header("KeyCodes")]
	[SerializeField] private KeyCode _turnLeft;
	[SerializeField] private KeyCode _turnRight;
	[SerializeField] private KeyCode _rise;
	[SerializeField] private KeyCode _lower;

	[Header("Points")]
	[SerializeField] private Transform[] _points;

	private int _numberPointsPassed;
	public static bool StopMoving{ get; private set; }
	private Vector3 _direction1;
	private Vector3 _direction2;

	private void Start()
	{
		StopMoving = true;
		EventManager.StartGameEvent += StartMoving;
		EventManager.ReplayGameEvent += Replay;
		EventManager.PauseGameEvent += Pause;

		StartCoroutine(Countdown());
	}
	void Update()
	{
		RotatePropeller();

		if (!StopMoving)
			Move();

		if (Input.GetKey(_turnLeft))
		{
			//RotateToDirection(-_body.right);
			RotateSlant(-_body.forward);
			_direction1 = _body.forward;

		}
		else if (Input.GetKey(_turnRight))
		{
			//RotateToDirection(_body.right);
			RotateSlant(_body.forward);
			_direction1 = _body.forward;
		}
		else
		{
			_direction1 = Vector3.zero;
			RotateToDirection(_points[GetNumberNextPoint()].position);
		}

		if (Input.GetKey(_rise))
		{
			RotateSlant(-_body.right);
			_direction2 = _body.forward;
		}
		else if (Input.GetKey(_lower))
		{
			RotateSlant(_body.right);
			_direction2 = _body.forward;
		}
		else
		{
			_direction2 = Vector3.zero;
			RotateToDirection(_points[GetNumberNextPoint()].position);
		}
	}
    private void Pause()
    {
		if (!StopMoving)
		{
			StopMoving = true;
			//StopCoroutine(Moving());
		}
        else
        {
            StopMoving = false;
            //StartCoroutine(Moving());
        }
    }
    private void StartMoving()
	{
		StopMoving = false;
		//StartCoroutine(Moving());
	}
	public void OnReplayGame()
	{
		_numberPointsPassed = 0;
		_body.transform.position = _points[0].position;
	}
	private int GetNumberNextPoint()
	{
		return _numberPointsPassed < _points.Length ? _numberPointsPassed + 1 : 0;
	}
	private void Move()
	{
		Vector3 direction = _direction1 != Vector3.zero || _direction2 != Vector3.zero ? _direction1 + _direction2 : _points[GetNumberNextPoint()].position;

		_body.position = Vector3.MoveTowards(_body.position, direction/*_points[GetNumberNextPoint()].position*/, _moveSpeed * Time.deltaTime);

		if (_body.position == _points[GetNumberNextPoint()].position)
			_numberPointsPassed = GetNumberNextPoint();
	}
	//public IEnumerator Moving()
	//{
  //      while (!StopMoving)
  //      {
		//	_body.position = Vector3.MoveTowards(_body.position, _points[GetNumberNextPoint()].position, _moveSpeed * Time.deltaTime);

		//	if (_body.position == _points[GetNumberNextPoint()].position)
		//		_numberPointsPassed = GetNumberNextPoint();

		//	yield return null;
  //      }
		//if (StopMoving)
		//	yield return null;
	//}
	private IEnumerator Countdown()
	{
        while (!StopMoving)
        {
			yield return new WaitForSeconds(_speedIncreaseInterval);
			int percent = _percentIncreaseSpeed / 100 + 1;
			_moveSpeed *= percent;
			StartCoroutine(Countdown());
        }
		if (StopMoving)
			yield return null;
	}
	private void RotatePropeller()
	{
		_propeller1.Rotate(1000 * Time.deltaTime, 0, 0);
		_propeller2.Rotate(50 * Time.deltaTime, 0, 0);
	}
    private void RotateToDirection(Vector3 direction)
    {
        Vector3 dir = direction.normalized;
        Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
        _body.rotation = Quaternion.RotateTowards(_body.rotation, toRotation, _speedRotate * Time.deltaTime);
    }
    private void RotateSlant(Vector3 slantDirection)
    {
        _body.Rotate(100 * Time.deltaTime * slantDirection);
    }
	private void Replay()
	{
		StopMoving = false;
		//StopCoroutine(Moving());
		_numberPointsPassed = 0;
		_body.position = _points[0].position;
		//StartCoroutine(Moving());
	}
    private void OnDestroy()
    {
		EventManager.StartGameEvent -= StartMoving;
		EventManager.ReplayGameEvent -= Replay;
		EventManager.PauseGameEvent -= Pause;
	}
#if UNITY_EDITOR
	private void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawLine(_points[0].position, _points[1].position);
		Gizmos.DrawLine(_points[1].position, _points[2].position);
		Gizmos.DrawLine(_points[2].position, _points[3].position);
		Gizmos.DrawLine(_points[3].position, _points[4].position);
		Gizmos.DrawLine(_points[4].position, _points[5].position);
		Gizmos.DrawLine(_points[5].position, _points[6].position);
		Gizmos.DrawLine(_points[6].position, _points[7].position);
        Gizmos.DrawLine(_points[7].position, _points[0].position);
    }
#endif
}
