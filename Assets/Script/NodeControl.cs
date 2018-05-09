using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeControl : MonoBehaviour {

	//ui
	int lineHeigth=60;
	public int line = 0;

	public GameObject NodeBody;
	public GameObject Node;

	public float totalValue,thisValue;
	public string nodeType;
	public bool alive = true;
	GameObject GameControl;

	public List<GameObject> nodeList = new List<GameObject>();
	// Use this for initialization
	void Start () {
		GameControl = GameObject.Find ("MainCamera");

		/*if (nodeType == "Root") {
			GameObject tmp = Instantiate(AddLevel1); 
			tmp.transform.SetParent(this.transform); 
			tmp.GetComponent<RectTransform>().localPosition = new Vector3 (0, -lineHeigth ,0);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPressAddLevel1(){
		GameControl.GetComponent<GameControl>().nowEdit = this.gameObject;
		GameControl.GetComponent<GameControl>().AddNode.SetActive (true);
	}
		
	public GameObject AddNode(){
		GameObject tmp = Instantiate(Node); 
		nodeList.Add (tmp);
		tmp.transform.SetParent(this.transform); 
		tmp.GetComponent<NodeControl> ().nodeType = "Node";
		GameObject.Find ("Root").GetComponent<NodeControl> ().UpdateUI();
		return tmp;
	}

	public GameObject AddNodeBody(){
		GameObject tmp = Instantiate(NodeBody); 
		nodeList.Add (tmp);
		tmp.transform.SetParent(this.transform); 
		tmp.GetComponent<NodeControl> ().nodeType = "Body";
		GameObject.Find ("Root").GetComponent<NodeControl> ().UpdateUI();
		return tmp;
	}
		
	public void OnPressDelete(){
		transform.parent.gameObject.GetComponent<NodeControl> ().nodeList.Remove (this.gameObject);
		alive = false;
		Destroy (this.gameObject);
		GameObject.Find ("Root").GetComponent<NodeControl> ().UpdateUI();
		GameObject.Find ("Root").GetComponent<NodeControl> ().UpdateValue();

	}

	public void CountTotalValue(){
		totalValue = 0;
		if (nodeType == "Body")
			totalValue += this.gameObject.GetComponent<NodeBody> ().value;
		for (int i = 0; i < nodeList.Count; i++) {
			int cost = GameControl.GetComponent<GameControl> ().cost;
			if (nodeList [i].GetComponent<NodeControl> ().nodeType == "Death" && GetComponent<NodeBody> ().ShouldMinusDeathValue ()) {
				totalValue += nodeList [i].GetComponent<NodeControl> ().totalValue - Mathf.Sqrt (cost + 2) + 1f;
			}
			else
				totalValue += nodeList [i].GetComponent<NodeControl> ().totalValue;
			
		}

		if (nodeType == "Root")
			GameObject.Find ("TotalValue").GetComponent<Text> ().text = totalValue.ToString ();
		else
			transform.parent.gameObject.GetComponent<NodeControl> ().CountTotalValue ();
	}

	public int UpdateUI(){
		int line = 1;
		for (int i = 0; i < nodeList.Count; i++) {
			nodeList [i].GetComponent<RectTransform>().localPosition = new Vector3 (30, -line*lineHeigth ,0);
			line += nodeList [i].GetComponent<NodeControl> ().UpdateUI ();
		}
		return line;
	}

	public void UpdateValue(){
		if (nodeType == "Body") {
			this.GetComponent<NodeBody> ().CountBody ();
			totalValue = this.gameObject.GetComponent<NodeBody> ().value;
		} else
			totalValue = thisValue;
		for (int i = 0; i < nodeList.Count; i++) {
				nodeList [i].GetComponent<NodeControl> ().UpdateValue ();
				int cost = GameControl.GetComponent<GameControl> ().cost;
				if (nodeList [i].GetComponent<NodeControl> ().nodeType == "Death" && GetComponent<NodeBody> ().ShouldMinusDeathValue ()) {
					totalValue += nodeList [i].GetComponent<NodeControl> ().totalValue - Mathf.Sqrt (cost + 2) + 1f;
				} else
					totalValue += nodeList [i].GetComponent<NodeControl> ().totalValue;
		}
		print (totalValue);

		if(nodeType == "Root")
			GameObject.Find ("TotalValue").GetComponent<Text> ().text = totalValue.ToString ();
	}
}
