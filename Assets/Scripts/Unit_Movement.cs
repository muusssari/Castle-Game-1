using UnityEngine;

public class Unit_Movement : MonoBehaviour {

    public float speed = 8f;

    private Transform wayp;
    private int wavepointIndex = 0;
    private string side;
    public string enemy;
	public Transform rotate;
	public GameObject target;
	public Vector3 offset;


    private void Start()
    {
        side = tag;
        if (side == "Enemy")
        {
            wayp = Waypoints.points[0];
        }
        else
        {
            wavepointIndex = Waypoints.points.Length - 1;
            wayp = Waypoints.points[Waypoints.points.Length - 1];
        }
    }

    private void Update()
    {
		if (wayp != null)
		{
			Vector3 dire = wayp.position - transform.position;
			Quaternion lookingRotation = Quaternion.LookRotation(dire);
			Vector3 rotation = Quaternion.Lerp(rotate.rotation, lookingRotation, Time.deltaTime * 8f).eulerAngles;
			rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
		}

		Vector3 dir = wayp.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance((transform.position + offset), wayp.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1 && side == "Enemy")
        {
            return;
        }
        else if (wavepointIndex <= 0 && side == "Player")
        {
            return;
        }
        else
        {
            if (side == "Enemy")
            {
                wavepointIndex++;
                wayp = Waypoints.points[wavepointIndex];
            }
            else
            {
                wavepointIndex--;
                wayp = Waypoints.points[wavepointIndex];
            }
        }

    }
}
