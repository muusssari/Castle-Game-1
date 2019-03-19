using UnityEngine;

public class Tower : MonoBehaviour {

	public bool Inside = false;
	public Vector3 positionOffset;
	public GameObject unit;
	[Header("Switch Side")]
	public bool player = true;
	public int unitcol = 0;

	public void OnCollisionEnter(Collision col)
	{
		positionOffset = new Vector3(0f, 3f, 0f);
		if (col.collider.tag == "Player" && col.collider.name == "Player_Unit_Archer(Clone)")
		{
			unit = col.collider.gameObject;
			unit.GetComponent<Shoot_Arrow>().range = 25;
			unitcol = 1;
		}
		if (col.collider.tag == "Enemy" && col.collider.name == "Enemy_Unit_Archer(Clone)")
		{
			unit = col.collider.gameObject;
			unit.GetComponent<Shoot_Arrow>().range = 25;
			unitcol = 1;
		}

	}
	private void Update()
	{
		if (unit == null && unitcol == 1)
		{
			Inside = false;
			unitcol = 0;
		}
	}
}
