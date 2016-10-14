using UnityEngine;
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
