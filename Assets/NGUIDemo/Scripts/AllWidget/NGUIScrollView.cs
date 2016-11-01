using UnityEngine;
using System.Collections;

public class NGUIScrollView : NGUIWidgetShowScreen {

	public UIGrid[] grid;

	private int count;

	public void AddItem(){
		foreach(var root in grid){
			GameObject goItem = Instantiate(Resources.Load<GameObject>("NGUIDemo/AllWidget/ScrollViewItem"));
			if(null != goItem){
				goItem.transform.SetParent(root.transform);
				goItem.transform.localScale = Vector3.one;
				NGUIScrollViewItem item = goItem.gameObject.GetComponent<NGUIScrollViewItem>();
				if(null != item){
					item.setSort(count);
					count++;
				}

				root.Reposition();
				UIScrollView scrollview = root.GetComponent<UIScrollView>();
				if(null != scrollview){
					UIProgressBar bar = scrollview.verticalScrollBar;
					bar.value = 0;
				}
			}
		}
	}

	public void DeleteOldestItem(){
		foreach(var root in grid){
			int t = -1;
			int count = root.transform.childCount;
			for(int i = 0; i < count; i++){
				Transform tf = root.GetChild(i);
				int n = int.Parse(tf.gameObject.name);
				t = t < n ? n : t;
			}
			DeleteItem(t);
		}
	}

	void DeleteItem(int index){
		foreach(var root in grid){
			int count = root.transform.childCount;
			for(int i = 0; i < count; i++){
				Transform tf = root.GetChild(i);
				if(null != tf && tf.gameObject.name.Equals(index.ToString())){
					Destroy(tf.gameObject);
				}
			}
			root.Reposition();
		}
	}
}
