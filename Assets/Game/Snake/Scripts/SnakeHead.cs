using UnityEngine;
using System.Collections;

public class SnakeHead : SnakeChild {

	public Transform transformRight;
	public Transform transformLeft;
	public Transform transformFront;

	private float fTime;

	public float fSpeed;

	private Transform transformNextDirection;

	void Awake(){
		transformNextDirection = transformFront;
	}

	void Update(){
		fTime += Time.deltaTime;
		if(fTime > fSpeed){
			fTime = 0;
			this.transform.position = transformNextDirection.position;
		}

	}

	void Handel(){
		if(Input.GetKeyDown(KeyCode.W)){
		}else if(Input.GetKeyDown(KeyCode.A)){
			
		}else if(Input.GetKeyDown(KeyCode.D)){
			
		}
	} 
}
