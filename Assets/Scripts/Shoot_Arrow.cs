using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Arrow : MonoBehaviour {

	public Transform target;
	public GameObject arrowPrefab;
	public Transform firePoint;
	public GameObject upgradeshops = null;
	public GameObject targetThing = null;
	public float range = 3f;
	private float turnSpeed = 6f;
	public float damage = 1f;
	private string enemytag;
	public string walltag;
	public Transform rotate;
	private float hitRate = 1;
	private float hitCountdown = 0f;
	public float update;
	private string towertag = "Tower";
	public Transform tower;


	void Start()
	{
		if (tag == "Enemy")
		{
			enemytag = "Player";
			towertag = "TowerE";
			walltag = "Wall";
}
		else
		{
			enemytag = "Enemy";
			towertag = "Tower";
			walltag = "WallE";
		}
		
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	void Update()
	{
		update = PlayerStats.UpgradeWeapon;
		if (tower == null)
		{
			UpdateTower();
		}
		

		if (target != null)
		{
			GetComponent<Unit_Movement>().enabled = false;

			if (target != null)
			{
				Vector3 dir = target.position - transform.position;
				Quaternion lookingRotation = Quaternion.LookRotation(dir);
				Vector3 rotation = Quaternion.Lerp(rotate.rotation, lookingRotation, Time.deltaTime * turnSpeed).eulerAngles;
				rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
			}
			if (hitCountdown <= 0f && targetThing != null)
			{
				Shoot();
				hitCountdown = 1f / hitRate;
			}

			hitCountdown -= Time.deltaTime;
		}
		else if (tower != null)
		{
			GetComponent<Unit_Movement>().enabled = false;
			GetComponent<Rigidbody>().isKinematic = false;
			Vector3 dire = (tower.position + tower.GetComponent<Tower>().positionOffset) - transform.position;
			float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
			if (distanceToTower >= 1.5f)
			{
				if (distanceToTower >= 3f)
				{
					transform.Translate(dire.normalized * GetComponent<Unit_Movement>().speed * Time.deltaTime, Space.World);
				}
				transform.Translate(dire.normalized * GetComponent<Unit_Movement>().speed/3 * Time.deltaTime, Space.World);	
			}
			else
			{
				return;
			}
			
		}

		else
		{
			GetComponent<Unit_Movement>().enabled = true;
			GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	void Shoot()
	{
		GameObject arrowFly = (GameObject)Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
		Arrow arrow = arrowFly.GetComponent<Arrow>();

		if (arrow != null)
		{
			arrow.Seek(target, targetThing);
		}
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
		GameObject[] walls = GameObject.FindGameObjectsWithTag(walltag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		targetThing = null;

		foreach (GameObject wall in walls)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, wall.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = wall;
			}
		}
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetThing = nearestEnemy;
		}
		else
		{
			target = null;
			targetThing = null;
		}
	}
	void UpdateTower()
	{

		GameObject[] Towers = GameObject.FindGameObjectsWithTag(towertag);
		
		float shortestDistance = Mathf.Infinity;
		GameObject nearestTower = null;

		
		foreach (GameObject tower in Towers)
		{
			if (tower.GetComponent<Tower>().Inside == false)
			{
				float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
				if (distanceToTower < shortestDistance)
				{
					shortestDistance = distanceToTower;
					nearestTower = tower;
				}
			}

		}

		if (nearestTower != null && shortestDistance <= range / 2.5)
		{
			tower = nearestTower.transform;
			tower.GetComponent<Tower>().Inside = true;
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
