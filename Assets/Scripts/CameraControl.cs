using UnityEngine;

public class CameraControl : MonoBehaviour {


    public float panSpeed = 40f;
    public float panBorderThickness = 20f;
    private Vector3 pos;
    public float minY = 10f;
    public float maxY = 80f;
    public bool moveCamera = false;
	private float scrollSpeed;

	void Awake()
    {
        pos = transform.position;
    }
    void Update () {
      /*  if (Input.GetKey(KeyCode.Escape))
        {
            moveCamera = !moveCamera;
        }
        if (!moveCamera)
        { */

            if (Input.GetKey("space"))
            {
                transform.position = pos;
            }

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                if (transform.position.x > 10)
                {
                    transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
                }
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
            if (transform.position.x < 150)
                {
                    transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
                }
                
            }
            if (Input.GetKey("d") || Input.mousePosition.x > Screen.width - panBorderThickness)
            {
                if (transform.position.z < 100)
                {
                    transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
                }
                
            }
            if (Input.GetKey("a") || Input.mousePosition.x < panBorderThickness)
            {
                if (transform.position.z > -40)
                {
                    transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
                }
            
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            /*Vector3 poss = transform.position;

            poss.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
            poss.y = Mathf.Clamp(pos.y, minY, maxY);
            transform.position = poss; */
            if (scroll > 0 && transform.position.y > minY)
            {
                transform.Translate(Vector3.forward * 350f * Time.deltaTime, Space.Self);
            }
            if (scroll < 0 && transform.position.y < maxY)
            {
                transform.Translate(Vector3.back * 350f * Time.deltaTime, Space.Self);
            }
       // }
    }
}
