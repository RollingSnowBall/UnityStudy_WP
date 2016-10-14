﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SnakeDirection{}

public class SnakeHandle : MonoBehaviour {

	public List<SnakeChild> listChild;

	public void front(){

	}

	public void left(){

	}

	public void right(){

	}

	public void addChild(){
		GameObject goChild = Instantiate<GameObject>(Resources.Load("Game/Snake/Child") as GameObject);
		if(null != goChild){
			SnakeChild child = goChild.GetComponent<SnakeChild>();
			if(null != child){
				listChild.Add(child);
				goChild.transform.SetParent(this.transform);
				goChild.transform.position = listChild[listChild.Count - 2].transformNext.position;
				goChild.transform.rotation = listChild[listChild.Count - 2].transform.rotation;
			}
		}
	}

	public void UpdateSnakeChildren(){
		for(int i = 1; i < listChild.Count; i++){
			listChild[i].transform.LookAt(listChild[i - 1].transform);
		}
		for(int i = listChild.Count - 1; i > 0; i--){
			listChild[i].gameObject.transform.position = listChild[i - 1].transformNext.gameObject.transform.position;
		}
	}

	void Update(){
#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			addChild();
		}
#endif
	}

	void SnakeMove(){

	}
}
