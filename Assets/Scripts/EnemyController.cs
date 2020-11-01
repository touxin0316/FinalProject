using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData eData;
    public GameObject FoVobject;
    private FieldOfView FoVscript;

    private Transform GFx;

    public GameObject VFx;

    private Vector3 lastPos = Vector3.zero;
    private Vector3 currentPos = Vector3.zero;

    [SerializeField] private int baseDMG;
    [SerializeField] private int fireRate;

    private float fireCD = 0;

    private int x = 0;
    private int d = 1;

    void Start()
    {
        EnemyControllerMaster.ListOfEnemies.Add(gameObject);
        GFx = transform.GetChild(0);

        GameObject f = Instantiate(FoVobject, transform.position, Quaternion.identity, transform);
        FoVscript = f.GetComponent<FieldOfView>();
        FoVscript.SetController(this);
    }

    void Update()
    {/*
        if (Input.GetKey(KeyCode.RightArrow))
            gameObject.transform.Translate(Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            gameObject.transform.Translate(-Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
            gameObject.transform.Translate(0, 0, Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            gameObject.transform.Translate(0, 0, -Time.deltaTime);
        */
        if(x < 600)
        {
            gameObject.transform.Translate(d * Time.deltaTime, 0, 0);
            x += 1;
        }
        else
        {
            d *= -1;
            x = 0;
        }
    }

    public void Attack()
    {
        if (fireCD < Time.time)
        {
            fireCD = Time.time + 1.0f / fireRate;
            GameObject v = Instantiate(VFx, transform.position, Quaternion.identity);
            v.GetComponent<Magic>().DMG = baseDMG;
        }
    }

    void LateUpdate()
    {
        currentPos = transform.position;
        CalculateDirection();
        lastPos = transform.position;
    }

    private void CalculateDirection()
    {
        if (currentPos.x > lastPos.x)    // Going Right
        {
            FoVscript.SetDirection(transform.right);
            GFx.localScale = new Vector3(1, 1, 1);
        }
        else if (currentPos.x < lastPos.x)    // Going Left
        {
            FoVscript.SetDirection(-transform.right);
            GFx.localScale = new Vector3(-1, 1, 1);
        }
        else if (currentPos.z > lastPos.z)    // Going Up
        {
            FoVscript.SetDirection(transform.forward);
        }
        else if (currentPos.z < lastPos.z)    // Going Down
        {
            FoVscript.SetDirection(-transform.forward);
        }
        else
            FoVscript.SetDirection(Vector3.zero);
    }

}
