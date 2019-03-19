using UnityEngine;

public class Attack_Melee : MonoBehaviour {

    public Transform target;
	
    public GameObject targetThing = null;
    public float range = 3f;
    private float turnSpeed = 6f;
    public float damage = 1f;
    private string enemytag;
	private string towertag;
	private string enemyBase;
	public float updateShield = 0;
	public float updateWeapon = 0;
	private string walltag;
	public Transform rotate;
    private float hitRate = 1f;
    private float hitCountdown = 0f;
	public int i = 0;
	Animation sword;
	Animation shield;

	void Start()
    {
        if (tag == "Enemy")
        {
            enemytag = "Player";
			towertag = "Tower";
			walltag = "Wall";
		}
        else
        {
            enemytag = "Enemy";
			towertag = "TowerE";
			walltag = "WallE";
		}
		sword = gameObject.transform.Find("Sword").GetComponent<Animation>();
		shield = gameObject.transform.Find("Shield").GetComponent<Animation>();
		InvokeRepeating("UpdateTower", 0f, 0.5f);
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void Update () {
		updateWeapon = PlayerStats.UpgradeWeapon;
		updateShield = PlayerStats.UpgradeShield;

		if (target != null)
        {
			
			
			GetComponent<Unit_Movement>().enabled = false;
            Vector3 dire = target.position - transform.position;
            if (Vector3.Distance(transform.position, target.position) > 3.8f)
			{
				transform.Translate(dire.normalized * GetComponent<Unit_Movement>().speed / 1.5f * Time.deltaTime, Space.World);
			}
            
            if (target != null)
            {
                Vector3 dir = target.position - transform.position;
                Quaternion lookingRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = Quaternion.Lerp(rotate.rotation, lookingRotation, Time.deltaTime * turnSpeed).eulerAngles;
                rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
            if (hitCountdown <= 0f && targetThing != null && Vector3.Distance(transform.position, target.position) <= 3.5f)
            {
				sword.Play("SwordSwing");
                Hit();
                hitCountdown = 1f / hitRate;
			}

            hitCountdown -= Time.deltaTime;
        }
        else
        {
            GetComponent<Unit_Movement>().enabled = true;
			
        }
    }

    void Hit()
    {
        targetThing.GetComponent<Unit_Healt>().GetHit(damage * updateWeapon/2);  
    }

	void UpdateTower()
	{
		GameObject[] enemytowers = GameObject.FindGameObjectsWithTag(towertag);
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

		foreach (GameObject tower in enemytowers)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, tower.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = tower;
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

	void UpdateTarget()
    {
		if (targetThing == null && target == null)
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
			
			float shortestDistance = Mathf.Infinity;
			GameObject nearestEnemy = null;
			targetThing = null;

			
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
		else
		{
			return;
		}
    }
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
