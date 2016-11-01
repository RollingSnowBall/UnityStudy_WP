using UnityEngine;
using System.Collections;

public class NGUIScrollViewItem : MonoBehaviour {

	public UILabel label;

	public void setSort(int iSort){
		this.gameObject.name = iSort.ToString();
		if(null != label){
			label.text = iSort.ToString();
		}
	}
}
