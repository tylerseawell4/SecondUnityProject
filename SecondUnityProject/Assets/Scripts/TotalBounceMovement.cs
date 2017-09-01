using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalBounceMovement : MonoBehaviour {
    public Rigidbody2D thePlayerRigidBody;
    public int numberOfBounces;
    public float playerSpeed;
    public float moveSpeed;
    public float nextBounceIncrease;
    
    private int _bounceHits = 0;
    private float _highestYCoordinate;
    private float _currentYCoordinate;
    

    // Use this for initialization
    void Start () {
        numberOfBounces = 3;
        playerSpeed = 2;
        _highestYCoordinate = thePlayerRigidBody.position.y;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {        
        _currentYCoordinate = thePlayerRigidBody.position.y;

        if (_currentYCoordinate > _highestYCoordinate)
        {
            _highestYCoordinate = thePlayerRigidBody.position.y;
        }      
        
    }
    private Vector3 nextYVector;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_bounceHits < numberOfBounces)
        {
            nextYVector = new Vector3(transform.position.x, (transform.position.y + (_highestYCoordinate += nextBounceIncrease)), 0f);
            transform.position = Vector3.Lerp(transform.position, nextYVector, Time.deltaTime * moveSpeed);
        }
        _bounceHits++;
        Debug.Log(nextYVector.y);
        Debug.Log(nextBounceIncrease);
    }
}
