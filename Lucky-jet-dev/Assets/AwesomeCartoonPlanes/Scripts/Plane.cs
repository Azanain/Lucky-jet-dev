using UnityEngine;

public class Plane : MonoBehaviour
{
	[SerializeField] private Transform _moveBody;
	[SerializeField] private Transform _rotateBody;
	[SerializeField] private Transform _propeller1;
	[SerializeField] private Transform _propeller2;

	[Header("Parametrs")]
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _speedRotate;
	[SerializeField] private int _speedIncreaseInterval;
	[SerializeField] private float _percentIncreaseSpeed;
	[SerializeField] private float _speedManeuver;
	[Range(0, 2), SerializeField] private float _dureationOutPath;

	[Header("KeyCodes")]
	[SerializeField] private KeyCode _turnLeft;
	[SerializeField] private KeyCode _turnRight;
	[SerializeField] private KeyCode _up;
	[SerializeField] private KeyCode _lower;

	[Header("Points")]
	[SerializeField] private Transform[] _points;
	public static bool StopMoving { get; private set; }
	public static float Speed;

	private int _numberPointsPassed;
	private float _timeReturnToPathLeft, _timeReturnToPathRight, _timeReturnToPathUp, _timeReturnToPathDown;
	private Vector3 _startPosition;
	private Quaternion _startRotation;
	private float _startMoveSpeed;
	private float _durationFlyingToIncrease = 0;

	private enum TypeDirection { up, down, left, right }

	private void Start()
	{
		Speed = _moveSpeed;
		StopMoving = true;
		EventManager.StartGameEvent += StartMoving;
		EventManager.ReplayGameEvent += Replay;
		EventManager.PauseGameEvent += Pause;

		_startPosition = _moveBody.position;
		_startRotation = _moveBody.rotation;
		_startMoveSpeed = _moveSpeed;
	}
	void Update()
	{
		RotatePropeller();

		if (!StopMoving)
		{
			Move();

			if (Input.GetKey(_turnLeft))
			{
				if (_timeReturnToPathLeft < _dureationOutPath)
				{
					_timeReturnToPathLeft += Time.deltaTime;

					Maneuver(TypeDirection.right);
					_moveBody.Translate(Vector3.left * _moveSpeed / _speedManeuver);
				}
			}
			else if (Input.GetKey(_turnRight))
			{
				if (_timeReturnToPathRight < _dureationOutPath)
				{
					_timeReturnToPathRight += Time.deltaTime;

					Maneuver(TypeDirection.left);
					_moveBody.Translate(Vector3.right * _moveSpeed / _speedManeuver);
				}
			}
			else
			{
				if (_timeReturnToPathLeft > 0)
					ReturnToPath(TypeDirection.left);

				if (_timeReturnToPathRight > 0)
					ReturnToPath(TypeDirection.right);

				LookAtNextPoint();
				//_moveBody.LookAt(_points[GetNumberNextPoint()]);
			}


			if (Input.GetKey(_up))
			{
				if (_timeReturnToPathUp < _dureationOutPath)
				{
					_timeReturnToPathUp += Time.deltaTime;

					Maneuver(TypeDirection.up);
					_moveBody.Translate(Vector3.up * _moveSpeed / _speedManeuver);
				}
			}
			else if (Input.GetKey(_lower))
			{
				if (_timeReturnToPathDown < _dureationOutPath)
				{
					_timeReturnToPathDown += Time.deltaTime;

					Maneuver(TypeDirection.down);
					_moveBody.Translate(Vector3.down * _moveSpeed / _speedManeuver);
				}
			}
			else
			{
				if (_timeReturnToPathUp > 0)
					ReturnToPath(TypeDirection.up);

				if (_timeReturnToPathDown > 0)
					ReturnToPath(TypeDirection.down);
			}
		}

		if (!StopMoving)
		{
			_durationFlyingToIncrease += Time.deltaTime;

			if (_durationFlyingToIncrease >= _speedIncreaseInterval)
			{
				float percent = _percentIncreaseSpeed / 100 + 1;
				Debug.Log(percent);
				_durationFlyingToIncrease = 0;
				_moveSpeed *= percent;
				Speed = _moveSpeed;
			}
		}
	}
	private void LookAtNextPoint()
	{
		Vector3 direction = _points[GetNumberNextPoint()].position - _moveBody.position;
		Quaternion rotation = Quaternion.LookRotation(direction);
		_moveBody.rotation = Quaternion.Lerp(_moveBody.rotation, rotation, _speedRotate * Time.deltaTime);
	}
	private void ReturnToPath(TypeDirection typeDirection)
	{
		switch (typeDirection)
		{
			case TypeDirection.up:
				_timeReturnToPathUp -= Time.deltaTime;
				Maneuver(TypeDirection.down);
				_moveBody.Translate(Vector3.down * _moveSpeed / _speedManeuver);
				break;
			case TypeDirection.down:
				_timeReturnToPathDown -= Time.deltaTime;
				Maneuver(TypeDirection.up);
				_moveBody.Translate(Vector3.up * _moveSpeed / _speedManeuver);
				break;
			case TypeDirection.left:
				_timeReturnToPathLeft -= Time.deltaTime;
				Maneuver(TypeDirection.left);
				_moveBody.Translate(Vector3.right * _moveSpeed / _speedManeuver);
				break;
			case TypeDirection.right:
				_timeReturnToPathRight -= Time.deltaTime;
				Maneuver(TypeDirection.right);
				_moveBody.Translate(Vector3.left * _moveSpeed / _speedManeuver);
				break;
			default:
				break;
		}
	}
	private void Maneuver(TypeDirection direction)
	{
		switch (direction)
		{
			case TypeDirection.up:
				_rotateBody.Rotate(-_rotateBody.right * .1f);
				break;
			case TypeDirection.down:
				_rotateBody.Rotate(_rotateBody.right * .1f);
				break;
			case TypeDirection.left:
				_rotateBody.Rotate(-_rotateBody.forward * .1f);
				break;
			case TypeDirection.right:
				_rotateBody.Rotate(_rotateBody.forward * .1f);
				break;
			default:
				break;
		}
	}
	private void Pause()
	{
		StopMoving = !StopMoving;
	}
	private void StartMoving()
	{
		StopMoving = false;
	}
	private int GetNumberNextPoint()
	{
		int number = 0;

		if (_numberPointsPassed < _points.Length)
			number = _numberPointsPassed + 1;
		else
		{
			number = 0;
			EventManager.CirclePassed();
		}

		return number;
	}
	private void Move()
	{
		_moveBody.position = Vector3.MoveTowards(_moveBody.position, _points[GetNumberNextPoint()].position, _moveSpeed * Time.deltaTime);

		if (_moveBody.position == _points[GetNumberNextPoint()].position)
			_numberPointsPassed = GetNumberNextPoint();
	}
	private void RotatePropeller()
	{
		_propeller1.Rotate(1000 * Time.deltaTime, 0, 0);
		_propeller2.Rotate(50 * Time.deltaTime, 0, 0);
	}
	public void Replay()
	{
		StopMoving = false;
		_numberPointsPassed = 0;

		_timeReturnToPathDown = 0;
		_timeReturnToPathLeft = 0;
		_timeReturnToPathRight = 0;
		_timeReturnToPathUp = 0;

		_moveSpeed = _startMoveSpeed;
		Speed = _moveSpeed;
		_durationFlyingToIncrease = 0;

		_moveBody.position = _startPosition;
		_moveBody.rotation = _startRotation;
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
