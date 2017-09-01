using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileMovement : MonoBehaviour
{
    public Rigidbody2D thePlayerRigidBody;
    private bool _facingRight;
    public int _numberOfBounces;
    public float _playerSpeed;
    public float _moveSpeed;
    public float _nextBounceIncrease;
    private int _bounceHits = 0;
    private float _highestYCoordinate;
    private float _currentYCoordinate;
    private Vector3 _nextYVector;
    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        //_numberOfBounces = 3;
        //_playerSpeed = 2;
        _highestYCoordinate = thePlayerRigidBody.position.y;
        _facingRight = true;
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.UpArrow))
            thePlayerRigidBody.velocity = new Vector3(thePlayerRigidBody.velocity.x, _moveSpeed, 0f);

        if (Input.GetKey(KeyCode.LeftArrow))
            thePlayerRigidBody.velocity = new Vector3(-_moveSpeed, thePlayerRigidBody.velocity.y, 0f);

        if (Input.GetKey(KeyCode.RightArrow))
            thePlayerRigidBody.velocity = new Vector3(_moveSpeed, thePlayerRigidBody.velocity.y, 0f);

#endif
        //creating neutral zone for character movements
        if (Input.acceleration.x > .025f)
        {
            thePlayerRigidBody.velocity = new Vector3(10f * Input.acceleration.x, thePlayerRigidBody.velocity.y, 0f);
        }
        //thePlayerRigidBody.AddRelativeForce(Vector3.right * 4f);
        else if (Input.acceleration.x < -.025f)
            thePlayerRigidBody.velocity = new Vector3(10f * Input.acceleration.x, thePlayerRigidBody.velocity.y, 0f);
        // thePlayerRigidBody.AddRelativeForce(Vector3.left * 4f);
    }
    // Update is called once per frame
    void Update()
    {
        _currentYCoordinate = thePlayerRigidBody.position.y;

        if (_currentYCoordinate > _highestYCoordinate)
            _highestYCoordinate = thePlayerRigidBody.position.y;

        //creating neutral zone for flip
        if (thePlayerRigidBody.velocity.x < -.025f && _facingRight)
        {
            _facingRight = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }
        else if (thePlayerRigidBody.velocity.x > .025f && !_facingRight)
        {
            _facingRight = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("RightCamera"))
        {
            thePlayerRigidBody.position = new Vector3(-2.80f, thePlayerRigidBody.position.y, 0f);
        }
        else if (collision.gameObject.tag.Equals("LeftCamera"))
        {
            thePlayerRigidBody.position = new Vector3(2.80f, thePlayerRigidBody.position.y, 0f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_bounceHits < _numberOfBounces)
        {
            _nextYVector = new Vector3(transform.position.x, (transform.position.y + (_highestYCoordinate += _nextBounceIncrease)), 0f);
            transform.position = Vector3.Lerp(transform.position, _nextYVector, Time.deltaTime * _moveSpeed);
        }
        _bounceHits++;
    }
}

