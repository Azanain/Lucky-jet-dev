using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour 
{
	[SerializeField] private Transform _body;
	[SerializeField] private Transform _propeller1;
	[SerializeField] private Transform _propeller2;

	[Header("Parametrs")]
	[SerializeField] private float _moveSpeed;
	[Range(0, 10)][SerializeField] private float _speedRotate;
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
	private bool _stopMoving;

	private void Start()
    {
		StartCoroutine(Moving());
		StartCoroutine(Countdown());
	}
	void Update()
	{
		RotatePropeller();

		if (Input.GetKey(_turnLeft))
		{
			RotateToDirection(-_body.right);
			RotateSlant(_body.forward);
		}
		else if (Input.GetKey(_turnRight))
		{
			RotateToDirection(_body.right);
			RotateSlant(-_body.forward);
		}
		else
			RotateToDirection(_points[GetNumberNextPoint()].position);

		if (Input.GetKey(_rise))
			RotateSlant(_body.right);
		else if (Input.GetKey(_lower))
			RotateSlant(-_body.right);
		else
			RotateToDirection(_points[GetNumberNextPoint()].position);

		if (Input.GetKeyDown(KeyCode.Space) && !_stopMoving)
			_stopMoving = true;
		else if
			(Input.GetKeyDown(KeyCode.Space) && _stopMoving)
			_stopMoving = false;
	}
	private int GetNumberNextPoint()
	{
		return _numberPointsPassed < _points.Length ? _numberPointsPassed + 1 : 0;
	}
	private IEnumerator Moving()
	{
        while (!_stopMoving)
        {
			_body.position = Vector3.MoveTowards(_body.position, _points[GetNumberNextPoint()].position, _moveSpeed * Time.deltaTime);

			if (_body.position == _points[GetNumberNextPoint()].position)
				_numberPointsPassed = GetNumberNextPoint();

			yield return null;
        }
	}
	private IEnumerator Countdown()
	{
		yield return new WaitForSeconds(_speedIncreaseInterval);
		int percent = _percentIncreaseSpeed / 100 + 1;
		_moveSpeed *= percent;
		StartCoroutine(Countdown());
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
        _body.Rotate(slantDirection * 100 * Time.deltaTime);
    }
}
