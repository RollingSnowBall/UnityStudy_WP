using UnityEngine;
using System.Collections;

public class ClothTrigger : MonoBehaviour {

//	void OnTriggerEnter(Collider collider){
//		Debug.Log("123");
////		Debug.Log(collider.ClosestPointOnBounds);
//	}

//	void OnCollisionEnter(Collision ctl){
//		ContactPoint contact = ctl.contacts[0];  
//		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);  
//		Vector3 pos = contact.point; 
//		Debug.Log(pos);
//	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		Debug.Log("碰撞到的物体的名字是：" + collisionInfo.gameObject.name);
	}
}
