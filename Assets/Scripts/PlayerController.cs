using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController controller;

    public float movementSpeed;
    public GameObject targeting;

    private Transform GFx;
    private Vector3 movement;
    private Rigidbody rb;

    void Start()
    {
        controller = this;
        rb = gameObject.GetComponent<Rigidbody>();
        GFx = gameObject.transform.GetChild(1);
        targeting.SetActive(false);
    }

    void Update()
    {
        MouseCheck();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement.y = 0;
        if (Input.mousePosition.x > Screen.width / 2)
            GFx.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else
            GFx.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void MouseCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            RaycastHit hit2;
            Vector3 dir = hit.transform.position - transform.position;
            Physics.Raycast(transform.position, dir, out hit2, PlayerData.player.activeRange);

            if (hit2.transform != hit.transform)
            {
                targeting.SetActive(false);
                return;
            }

            if (hit.transform.tag != "Targetable")
            {
                targeting.SetActive(false);
                return;
            }

            targeting.SetActive(true);
            Vector3 pos = hit.transform.position;
            pos.y = 0.01f;
            targeting.transform.position = pos;

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.GetComponent<EnemyController>() != null)
                {
                    Destroy(hit.transform.gameObject);
                }
                if (hit.transform.GetComponent<BreakableObject>() != null)
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire3"))
            rb.MovePosition(transform.position + movement.normalized * (movementSpeed / 5) * Time.fixedDeltaTime);
        else
            rb.MovePosition(transform.position + movement.normalized * movementSpeed * Time.fixedDeltaTime);
    }
}
