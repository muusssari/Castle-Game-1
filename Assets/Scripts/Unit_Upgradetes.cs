using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Upgradetes : MonoBehaviour {

	private float updateShield = 0;
	private float updateWeapon = 0;
	

	void Update () {
		updateWeapon = PlayerStats.UpgradeWeapon;
		updateShield = PlayerStats.UpgradeShield;


	}
}
