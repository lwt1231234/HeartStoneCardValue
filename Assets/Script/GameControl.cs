using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	//ui
	public GameObject AddNode;
	public GameObject AddMinionAbi;
	public GameObject EditBody;
	public GameObject EditRecover;
	public GameObject EditDamage;
	public GameObject EditDrawCard;
	public GameObject EditDestory;
	public GameObject EditBuff;
	public GameObject EditWeapon;
	public GameObject EditFrose;
	public GameObject EditOverload;
	//button

	//prefab
	public GameObject Node;

	public GameObject nowEdit;
	public GameObject Root;

	int lineHeigth = 60;

	public int cost = 0;

	// Use this for initialization
	void Start () {
		AddNode.SetActive (false);
		AddMinionAbi.SetActive (false);
		EditBody.SetActive (false);
		EditRecover.SetActive (false);
		EditDamage.SetActive (false);
		EditDrawCard.SetActive (false);
		EditDestory.SetActive (false);
		EditBuff.SetActive (false);
		EditWeapon.SetActive (false);
		EditFrose.SetActive (false);
		EditOverload.SetActive (false);

		nowEdit = Root;
		GameObject.Find ("CostInput/Text").GetComponent<Text> ().text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CostChanged(){
		string stringCost = GameObject.Find ("CostInput/Text").GetComponent<Text> ().text;
		if (stringCost.Length > 0)
			cost = int.Parse (stringCost);
		CountTotalValue ();
	}
		
	public void OnPressAddBody(){
		AddNode.SetActive (false);
		EditBody.SetActive (true);

		nowEdit=nowEdit.GetComponent<NodeControl> ().AddNodeBody();
	}

	public void OnEditBodyOK(){
		string stringLife = GameObject.Find ("EditBody/Life/Text").GetComponent<Text> ().text;
		string stringAttack = GameObject.Find ("EditBody/Attack/Text").GetComponent<Text> ().text;

		if (stringLife.Length > 0 && stringAttack.Length > 0) {

			int life = int.Parse (stringLife);
			int attack = int.Parse (stringAttack);
			if (life > 0 && attack > -1) {
				nowEdit.GetComponent<NodeBody> ().SetInfo (life, attack);

				CountTotalValue ();
				EditBody.SetActive (false);
			} else
				GameObject.Find ("EditBody/Warning").GetComponent<Text> ().text = "输入有误";

		}
		else
			GameObject.Find ("EditBody/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void AddDeath(){
		nowEdit.GetComponent<NodeBody> ().AddDeath ();
		AddMinionAbi.SetActive (false);
	}

	public void OnPressAddMinionAbiOK(){
		AddMinionAbi.SetActive (false);
	}

	public void OnMinionAbiChanged(){
		if (AddMinionAbi.transform.Find("Taunt").GetComponent<Toggle> ().isOn)
			nowEdit.GetComponent<NodeBody>().taunt = true;
		else
			nowEdit.GetComponent<NodeBody>().taunt = false;
		if (AddMinionAbi.transform.Find("Shield").GetComponent<Toggle> ().isOn)
			nowEdit.GetComponent<NodeBody>().shield = true;
		else
			nowEdit.GetComponent<NodeBody>().shield = false;
		if (AddMinionAbi.transform.Find("Charge").GetComponent<Toggle> ().isOn)
			nowEdit.GetComponent<NodeBody>().charge = true;
		else
			nowEdit.GetComponent<NodeBody>().charge = false;
		if (AddMinionAbi.transform.Find("Windfury").GetComponent<Toggle> ().isOn)
			nowEdit.GetComponent<NodeBody>().windfury = true;
		else
			nowEdit.GetComponent<NodeBody>().windfury = false;
		if (AddMinionAbi.transform.Find("Poison").GetComponent<Toggle> ().isOn)
			nowEdit.GetComponent<NodeBody>().poison = true;
		else
			nowEdit.GetComponent<NodeBody>().poison = false;
		if (AddMinionAbi.transform.Find("Lifesteal").GetComponent<Toggle> ().isOn)
			nowEdit.GetComponent<NodeBody>().lifesteal = true;
		else
			nowEdit.GetComponent<NodeBody>().lifesteal = false;
		if (AddMinionAbi.transform.Find("Stealth").GetComponent<Toggle> ().isOn)
			nowEdit.GetComponent<NodeBody>().stealth = true;
		else
			nowEdit.GetComponent<NodeBody>().stealth = false;
		if (AddMinionAbi.transform.Find("Rush").GetComponent<Toggle> ().isOn)
			nowEdit.GetComponent<NodeBody>().rush = true;
		else
			nowEdit.GetComponent<NodeBody>().rush = false;
		if (AddMinionAbi.transform.Find("SP1").GetComponent<Toggle> ().isOn) {
			nowEdit.GetComponent<NodeBody>().sp1 = true;
			AddMinionAbi.transform.Find("SP2").GetComponent<Toggle> ().isOn = false;
		}
		else
			nowEdit.GetComponent<NodeBody>().sp1 = false;
		if (AddMinionAbi.transform.Find("SP2").GetComponent<Toggle> ().isOn) {
			nowEdit.GetComponent<NodeBody>().sp2 = true;
			AddMinionAbi.transform.Find("SP1").GetComponent<Toggle> ().isOn = false;
		}
		else
			nowEdit.GetComponent<NodeBody>().sp2 = false;
		nowEdit.GetComponent<NodeBody>().CountBody ();
		nowEdit.GetComponent<NodeControl> ().CountTotalValue();
	}

	public void OnPressAddRecover(){
		AddNode.SetActive (false);
		EditRecover.SetActive (true);
		nowEdit=nowEdit.GetComponent<NodeControl> ().AddNode();
		nowEdit.transform.Find ("AddNode").gameObject.SetActive (false);
	}

	public void AddArmor(){
		string input = GameObject.Find ("EditRecover/Armor/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 0.5f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="获得"+num.ToString()+"点护甲("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditRecover.SetActive (false);
			}
			else
				GameObject.Find ("EditRecover/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditRecover/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void HealthOneUnit(){
		string input = GameObject.Find ("EditRecover/HealthOneUnit/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 0.5f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="为一个单位恢复"+num.ToString()+"点生命("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditRecover.SetActive (false);
			}
			else
				GameObject.Find ("EditRecover/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditRecover/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void OnPressAddDamage(){
		AddNode.SetActive (false);
		EditDamage.SetActive (true);
		nowEdit=nowEdit.GetComponent<NodeControl> ().AddNode();
		nowEdit.transform.Find ("AddNode").gameObject.SetActive (false);
	}

	public void DamageOneUnit(){
		string input = GameObject.Find ("EditDamage/DamageOneUnit/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 1f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="对任意单位造成"+num.ToString()+"点伤害("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditDamage.SetActive (false);
			}
			else
				GameObject.Find ("EditDamage/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditDamage/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void DamageOneMinion(){
		string input = GameObject.Find ("EditDamage/DamageOneMinion/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 0.75f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="对任意随从造成"+num.ToString()+"点伤害("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditDamage.SetActive (false);
			}
			else
				GameObject.Find ("EditDamage/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditDamage/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void DamageAllEnMinion(){
		string input = GameObject.Find ("EditDamage/DamageAllEnMinion/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 2f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="对所有敌方随从造成"+num.ToString()+"点伤害("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditDamage.SetActive (false);
			}
			else
				GameObject.Find ("EditDamage/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditDamage/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void DamageEnHero(){
		string input = GameObject.Find ("EditDamage/DamageEnHero/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 0.5f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="对敌方英雄造成"+num.ToString()+"点伤害("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditDamage.SetActive (false);
			}
			else
				GameObject.Find ("EditDamage/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditDamage/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void DamageYourHero(){
		string input = GameObject.Find ("EditDamage/DamageYourHero/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * -0.5f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="对你的英雄造成"+num.ToString()+"点伤害("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditDamage.SetActive (false);
			}
			else
				GameObject.Find ("EditDamage/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditDamage/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void OnPressAddDrawCard(){
		AddNode.SetActive (false);
		EditDrawCard.SetActive (true);
		nowEdit=nowEdit.GetComponent<NodeControl> ().AddNode();
		nowEdit.transform.Find ("AddNode").gameObject.SetActive (false);
	}

	public void DrawCard(){
		string input = GameObject.Find ("EditDrawCard/DrawCard/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 2f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="抽"+num.ToString()+"张牌("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditDrawCard.SetActive (false);
			}
			else
				GameObject.Find ("EditDrawCard/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditDrawCard/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void DrawSpecialCard(){
		string input = GameObject.Find ("EditDrawCard/DrawSpecialCard/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 2f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="抽"+num.ToString()+"张特定牌("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditDrawCard.SetActive (false);
			}
			else
				GameObject.Find ("EditDrawCard/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditDrawCard/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void DiscoverACard(){
		string input = GameObject.Find ("EditDrawCard/DiscoverACard/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			float num = float.Parse (input);
			float tmpValue = 2f + num;
			nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
			nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="发现一张超模"+num.ToString()+"的牌并置入手中("+tmpValue.ToString()+");";

			CountTotalValue ();
			EditDrawCard.SetActive (false);
		}
		else
			GameObject.Find ("EditDrawCard/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void GetARandomCard(){
		string input = GameObject.Find ("EditDrawCard/GetARandomCard/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			float num = float.Parse (input);
			float tmpValue = 2f + num;
			nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
			nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="将一张超模"+num.ToString()+"的随机牌置入手中("+tmpValue.ToString()+");";

			CountTotalValue ();
			EditDrawCard.SetActive (false);
		}
		else
			GameObject.Find ("EditDrawCard/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void GetASpecialCard(){
		string input = GameObject.Find ("EditDrawCard/GetASpecialCard/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			float num = float.Parse (input);
			float tmpValue = 1f + num;
			nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
			nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="将一张超模"+num.ToString()+"的牌置入手中("+tmpValue.ToString()+");";

			CountTotalValue ();
			EditDrawCard.SetActive (false);
		}
		else
			GameObject.Find ("EditDrawCard/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void OnPressAddDestory(){
		AddNode.SetActive (false);
		EditDestory.SetActive (true);
		nowEdit=nowEdit.GetComponent<NodeControl> ().AddNode();
		nowEdit.transform.Find ("AddNode").gameObject.SetActive (false);
	}

	public void DestoryAMinion(){
		float tmpValue = 6f;
		nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
		nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="消灭一个随从("+tmpValue.ToString()+");";

		CountTotalValue ();
		EditDestory.SetActive (false);
	}

	public void DestoryRandomEnMinion(){
		float tmpValue = 4f;
		nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
		nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="消灭一个随机敌方随从("+tmpValue.ToString()+");";

		CountTotalValue ();
		EditDestory.SetActive (false);
	}

	public void ChangeAMinion(){
		string input = GameObject.Find ("EditDestory/ChangeAMinion/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			float num = float.Parse (input);
			float tmpValue = 6f - num;
			nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
			nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="将一个随从变形成价值"+num.ToString()+"的随从("+tmpValue.ToString()+");";

			CountTotalValue ();
			EditDestory.SetActive (false);
		}
		else
			GameObject.Find ("EditDestory/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void OnPressAddBuff(){
		AddNode.SetActive (false);
		EditBuff.SetActive (true);
		nowEdit=nowEdit.GetComponent<NodeControl> ().AddNode();
		nowEdit.transform.Find ("AddNode").gameObject.SetActive (false);
	}

	public void AddMinionLife(){
		string input = GameObject.Find ("EditBuff/AddMinionLife/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 0.5f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="使一个随从获得+"+num.ToString()+"生命值("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditBuff.SetActive (false);
			}
			else
				GameObject.Find ("EditBuff/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditBuff/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void AddMinionAttack(){
		string input = GameObject.Find ("EditBuff/AddMinionAttack/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 0.5f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="使一个随从获得+"+num.ToString()+"攻击力("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditBuff.SetActive (false);
			}
			else
				GameObject.Find ("EditBuff/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditBuff/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void AddMinionTaunt(){
		float tmpValue = 0.5f;
		nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
		nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="使一个随从获得嘲讽("+tmpValue.ToString()+");";

		CountTotalValue ();
		EditBuff.SetActive (false);
	}

	public void OnPressAddWeapon(){
		AddNode.SetActive (false);
		EditWeapon.SetActive (true);
		nowEdit=nowEdit.GetComponent<NodeControl> ().AddNode();
		nowEdit.transform.Find ("AddNode").gameObject.SetActive (false);
	}

	public void GetWeapon(){
		string stringAttack = GameObject.Find ("EditWeapon/GetWeapon/Input1/Text").GetComponent<Text> ().text;
		string stringDurable = GameObject.Find ("EditWeapon/GetWeapon/Input2/Text").GetComponent<Text> ().text;

		if (stringAttack.Length > 0 && stringDurable.Length > 0) {

			int attack = int.Parse (stringAttack);
			int durable = int.Parse (stringDurable);
			if (attack > -1 && durable > 0) {
				float tmpValue = attack * durable * 0.5f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="装备一把"+attack.ToString()+"/"+durable.ToString()+"的武器("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditWeapon.SetActive (false);
			} else
				GameObject.Find ("EditWeapon/Warning").GetComponent<Text> ().text = "输入有误";

		}
		else
			GameObject.Find ("EditWeapon/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void AddHeroAttack(){
		string input = GameObject.Find ("EditWeapon/AddHeroAttack/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = num * 0.5f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="在本回合使你的英雄获得+"+num.ToString()+"攻击力("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditWeapon.SetActive (false);
			}
			else
				GameObject.Find ("EditWeapon/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditWeapon/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void OnPressAddFrose(){
		AddNode.SetActive (false);
		EditFrose.SetActive (true);
		nowEdit=nowEdit.GetComponent<NodeControl> ().AddNode();
		nowEdit.transform.Find ("AddNode").gameObject.SetActive (false);
	}

	public void FroseAUnit(){
		float tmpValue = 1f;
		nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
		nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="冻结一个单位("+tmpValue.ToString()+");";

		CountTotalValue ();
		EditFrose.SetActive (false);
	}

	public void OnPressAddOverload(){
		AddNode.SetActive (false);
		EditOverload.SetActive (true);
		nowEdit=nowEdit.GetComponent<NodeControl> ().AddNode();
		nowEdit.transform.Find ("AddNode").gameObject.SetActive (false);
	}

	public void AddOverload(){
		string input = GameObject.Find ("EditOverload/AddOverload/Input/Text").GetComponent<Text> ().text;
		if (input.Length > 0) {
			int num = int.Parse (input);
			if (num > 0) {
				float tmpValue = -num + 0.5f;
				nowEdit.GetComponent<NodeControl> ().thisValue = tmpValue;
				nowEdit.transform.Find("ShowText").GetComponent<Text> ().text="过载"+num.ToString()+"("+tmpValue.ToString()+");";

				CountTotalValue ();
				EditOverload.SetActive (false);
			}
			else
				GameObject.Find ("EditOverload/Warning").GetComponent<Text> ().text = "输入有误";
		}
		else
			GameObject.Find ("EditOverload/Warning").GetComponent<Text> ().text = "输入有误";
	}

	public void CountTotalValue(){
		Root.GetComponent<NodeControl> ().UpdateValue();
	}
}
