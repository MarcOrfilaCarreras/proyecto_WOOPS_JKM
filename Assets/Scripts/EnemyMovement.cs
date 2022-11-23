using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	Rigidbody2D enemyRigidBody2D;
	public int UnitsToMove = 5;
	public float EnemySpeed = 1;
	public bool _isFacingLeft;
	private float _startPos;
	private float _endPos;

	public bool _moveRight = true;


    // Use this for initialization
	public void Awake()
	{
		Turn180Degrees();
		enemyRigidBody2D = GetComponent<Rigidbody2D>();
		_startPos = transform.position.x;
		_endPos = _startPos + (UnitsToMove/2);
		_isFacingLeft = transform.localScale.x > 0;
	}
    // Update is called once per frame
	public void Update()
	{
		if (_moveRight){
			enemyRigidBody2D.AddForce(Vector2.right * EnemySpeed * Time.deltaTime);
			if (!_isFacingLeft){
				Turn180Degrees();
			}
		}

		if (enemyRigidBody2D.position.x >= _endPos-(_endPos*10/100)){
			_moveRight = false;
		}

		if (!_moveRight){
			enemyRigidBody2D.AddForce(-Vector2.right * EnemySpeed * Time.deltaTime);
			if (_isFacingLeft){
				Turn180Degrees();
			}
		}

		if (enemyRigidBody2D.position.x <= _startPos){
			_moveRight = true;
		}

	}

	public void Turn180Degrees()
	{
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		_isFacingLeft = transform.localScale.x > 0;
	}


}
