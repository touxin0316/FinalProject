using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController controller;

    public float movementSpeed;

    private Rigidbody rb;
    private Transform GFx;

    private Vector3 movement;
    private GameObject currentlySelected;

    void Start()
    {
        controller = this;
        rb = gameObject.GetComponent<Rigidbody>();
        GFx = gameObject.transform.GetChild(1);
        currentlySelected = null;
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
    }

    public void Deselect()
    {
        currentlySelected = null;
    }

    private void MouseCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            RaycastHit hit2;
            Vector3 dir = hit.transform.position - transform.position;
            dir.y = 0;
            if (Physics.Raycast(transform.position, dir, out hit2, PlayerData.player.activeRange))
            {
                GameObject target = hit2.transform.gameObject;
                if (currentlySelected == null && target.GetComponent<SelectableObject>() != null)
                {
                    if (target.GetComponent<SelectableObject>().isAvailable)
                    {
                        currentlySelected = target;
                        target.GetComponent<SelectableObject>().MouseEnter();
                    }
                }
                if (currentlySelected != null && target != currentlySelected)
                {
                    currentlySelected.GetComponent<SelectableObject>().MouseExit();
                    if (target.GetComponent<SelectableObject>() != null && target.GetComponent<SelectableObject>().isAvailable)
                    {
                        currentlySelected = target;
                        target.GetComponent<SelectableObject>().MouseEnter();
                    }
                    else
                        currentlySelected = null;
                }
            }
            else if (currentlySelected != null)
            {
                currentlySelected.GetComponent<SelectableObject>().MouseExit();
                currentlySelected = null;
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
