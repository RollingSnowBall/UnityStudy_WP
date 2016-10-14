using UnityEngine;
using System.Collections;

public class SnakeHead : SnakeChild {

	public Transform transformRight;
	public Transform transformLeft;
	public Transform transformUp;
	public Transform transformDown;

	public Transform transformSurface;

	private float fTime;

	public float fSpeed;

	private Transform transformNextDirection;

	void Awake(){
		transformNextDirection = transformUp;
	}

	void Update(){
		Handel();
		fTime += Time.deltaTime;
		if(fTime > fSpeed){
			fTime = 0;
			this.transform.position = transformNextDirection.position;
			transformSurface.LookAt(transformNextDirection);
		}
	}

	void Handel(){
		if(Input.GetKeyDown(KeyCode.W)){
			setDirection(transformUp);
		}else if(Input.GetKeyDown(KeyCode.A)){
			setDirection(transformLeft);
		}else if(Input.GetKeyDown(KeyCode.D)){
			setDirection(transformRight);
		}else if(Input.GetKeyDown(KeyCode.S)){
			setDirection(transformDown);
		}
	}

	void setDirection(Transform transform){
		transformNextDirection = transform;
	}
}
