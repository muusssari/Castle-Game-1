using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public Transform target;
	public float explosionRadius = 0f;
	public float speed = 5f;
	public float damage = 0.5f;
	public GameObject effect;
	public GameObject targetThing;
	private Transform targetpoint;

	public void Seek(Transform getTarget, GameObject getTargetThing)
	{
		targetThing = getTargetThing;
		target = getTarget;
		targetpoint = target;
	}

	private void Update()
	{
		if (targetThing == null || target == null)
		{
			Destroy(gameObject);
			return;
		}

		if (explosionRadius <= 0f)
		{
			Vector3 dir = target.position - transform.position;
			float distanceThisFrame = speed * Time.deltaTime;

			if (dir.magnitude <= distanceThisFrame)
			{
				HitTarget();
				return;
			}

			transform.Translate(dir.normalized * distanceThisFrame, Space.World);
			transform.LookAt(target.position);
		}
		else
		{
			Vector3 dir = targetpoint.position - transform.position;
			
			float distanceThisFrame = speed * Time.deltaTime;

			if (dir.magnitude <= distanceThisFrame)
			{
				HitTarget();
				return;
			}
			transform.Translate(dir.normalized * distanceThisFrame, Space.World);
			transform.LookAt(targetpoint);
		}
		
	}

	void HitTarget()
	{

		if (explosionRadius > 0f)
		{
			Explode();
		}
		else
		{
			Damage(target);
		}

		Destroy(gameObject);
	}

	void Damage (Transform enemy)
	{
		targetThing.GetComponent<Unit_Healt>().GetShot(damage);
	}
	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		Debug.Log(colliders);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "TowerE" || collider.tag == "Tower" || collider.name == "EnemyBase" || collider.name == "PlayerBase" ||collider.tag == "Wall" || collider.tag == "WallE")
			{
				collider.GetComponent<Unit_Healt>().GetHit(damage);
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}

}
