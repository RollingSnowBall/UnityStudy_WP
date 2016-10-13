using UnityEngine;
using System.Collections;

public class SnakeHead : SnakeChild {

	public Transform transformRight;
	public Transform transformLeft;
	public Transform transformFront;

	private float fTime;

	void Update(){
		fTime += Time.deltaTime;
		if(fTime > 1.0F){
			fTime = 0;
			this.transform.position = transformFront.position;
		}

	}

	void Handel(){
		if(Input.GetKeyDown(KeyCode.W)){
				
		}else if(Input.GetKeyDown(KeyCode.A)){
			
		}else if(Input.GetKeyDown(KeyCode.D)){
			
		}
	} 
}
