using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeBody : MonoBehaviour {

	int lineHeigth = 60;
	public GameObject AddMinionAbi;

	public int life,attack;
	public float value;

	public bool taunt=false;
	public bool shield=false;
	public bool charge=false;
	public bool windfury=false;
	public bool poison=false;
	public bool lifesteal=false;
	public bool stealth=false;
	public bool rush=false;
	public bool sp1=false;
	public bool sp2=false;

	GameObject GameControl;
	public GameObject MinionAbi;

	// Use this for initialization
	void Awake () {
		GameControl = GameObject.Find ("MainCamera");
		AddMinionAbi = GameControl.GetComponent<GameControl> ().AddMinionAbi;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetInfo(int setLife, int setAttack){
		life = setLife;
		attack = setAttack;

		CountBody ();
		this.GetComponent<NodeControl> ().CountTotalValue();
	}

	public void AddDeath(){
		GameObject tmp = GetComponent<NodeControl> ().AddNode ();
		//GameControl.GetComponent<GameControl> ().nowEdit = tmp;
		tmp.GetComponent<NodeControl> ().nodeType = "Death";
		tmp.transform.Find("ShowText").GetComponent<Text>().text= "亡语:";
	}

	public void OnPressAddMinionAbi(){
		AddMinionAbi.SetActive (true);
		GameControl.GetComponent<GameControl> ().nowEdit = this.gameObject;
	}
		

	float CountCharge(){
		float tmpValue;
		if (shield)
			tmpValue = attack * 0.5f;
		else
			tmpValue = attack * 0.35f;
		return tmpValue;
	}

	public void CountBody(){
		value = Mathf.Sqrt (life * attack);
		float chargeValue;
		this.transform.Find("ShowText").GetComponent<Text> ().text = "生命:" + life.ToString () + ",攻击:" + attack.ToString ()+"("+value.ToString()+");";
		if (taunt) {
			double tmpValue;
			int cost = GameControl.GetComponent<GameControl> ().cost;
			tmpValue = Mathf.RoundToInt((float)life / (cost + 1)+0.001f) * 0.5f;
			this.transform.Find("ShowText").GetComponent<Text> ().text+="嘲讽("+tmpValue.ToString()+");";
			value += (float)tmpValue;
		}
		if (shield) {
			float tmpValue = Mathf.Sqrt (attack);
			this.transform.Find("ShowText").GetComponent<Text> ().text+="圣盾("+tmpValue.ToString()+");";
			value += (float)tmpValue;
		}
		if (charge) {
			float tmpValue = CountCharge ();
			this.transform.Find ("ShowText").GetComponent<Text> ().text += "冲锋(" + tmpValue.ToString () + ");";
			value += (float)tmpValue;
			chargeValue = tmpValue;
		} else
			chargeValue = 0;
		if (windfury) {
			float tmpValue = attack * 0.25f;
			this.transform.Find("ShowText").GetComponent<Text> ().text+="风怒("+tmpValue.ToString()+");";
			value += (float)tmpValue;
		}
		if (poison) {
			int cost = GameControl.GetComponent<GameControl> ().cost;
			float tmpValue = (cost+1+life) * 0.5f;
			this.transform.Find("ShowText").GetComponent<Text> ().text+="剧毒("+tmpValue.ToString()+");";
			value += (float)tmpValue;
		}
		if (lifesteal) {
			float tmpValue = attack * 0.5f;
			this.transform.Find("ShowText").GetComponent<Text> ().text+="吸血("+tmpValue.ToString()+");";
			value += (float)tmpValue;
		}
		if (stealth) {
			double tmpValue;
			int cost = GameControl.GetComponent<GameControl> ().cost;
			tmpValue =Mathf.RoundToInt((float)attack / (cost + 1)+0.001f) * 0.5f;
			/*if (chargeValue > tmpValue)
				tmpValue = 0;
			else
				tmpValue -= chargeValue;
				*/
			this.transform.Find("ShowText").GetComponent<Text> ().text+="潜行("+tmpValue.ToString()+");";
			value += (float)tmpValue;
		}
		if (rush) {
			float tmpValue = attack * 0.25f;
			this.transform.Find("ShowText").GetComponent<Text> ().text+="突袭("+tmpValue.ToString()+");";
			value += (float)tmpValue;
		}
		if (sp1) {
			float tmpValue = 0.5f;
			this.transform.Find("ShowText").GetComponent<Text> ().text+="法伤+1("+tmpValue.ToString()+");";
			value += (float)tmpValue;
		}
		if (sp2) {
			float tmpValue = 3f;
			this.transform.Find("ShowText").GetComponent<Text> ().text+="法伤+2("+tmpValue.ToString()+");";
			value += (float)tmpValue;
		}
	}

	public bool ShouldMinusDeathValue(){
		if (taunt)
			return false;
		if (CountCharge () > (life * 0.75f))
			return false;
		return true;
	}
}
