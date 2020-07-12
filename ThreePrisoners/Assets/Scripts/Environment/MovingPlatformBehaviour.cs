using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{
	public Transform movingPlatform;
	public Transform[] position;
	public float timeOffset;
	public float travelTime;
	public float waitTime;

	private int currentState;
	private Vector3 newPosition;
	private Vector3 oldPosition;
	private float timeSum;

	void Start()
	{
		currentState = -1;		//starting state
		changeTarget();			
		timeSum = timeOffset;	//start with predefined offset
	}


	void FixedUpdate()
	{
		timeSum = timeSum + Time.deltaTime;		//build up timer
		movingPlatform.position = oldPosition + (newPosition - oldPosition) * Mathf.Min(1, timeSum / travelTime);	//calculate appropriate midway between current two positions
		if (timeSum >= travelTime + waitTime)		//state has been reached and wait time has passed
		{
			changeTarget();
		}
	}


	void changeTarget()
	{
        
		currentState = (currentState+1)%position.Length;		//increment state, go back to 0 after reaching final state
		newPosition = position[(currentState + 1) % position.Length].position;	//define origin and destination position depending on state
		oldPosition = position[currentState].position;
		timeSum = timeSum - travelTime - waitTime;			//decrement timer


	}
}
