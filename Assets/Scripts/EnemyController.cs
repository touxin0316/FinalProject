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

    private Vector3 originalPos;
    private Vector3 direction;

    private int baseDMG;
    private int fireRate;
    private int currentHP;

    private float fireCD = 0;
    public bool playerInRange { set; get; }

    void Start()
    {
        EnemyControllerMaster.ListOfEnemies.Add(gameObject);
        originalPos = transform.position;
        GFx = transform.GetChild(0);

        GameObject f = Instantiate(FoVobject, transform.position, Quaternion.identity, transform);
        FoVscript = f.GetComponent<FieldOfView>();
        FoVscript.SetData(this, eData.FoV, eData.viewDistance);

        baseDMG = eData.baseDMG;
        fireRate = eData.fireRate;
        currentHP = eData.maxHP;

        if (eData.moveAxis == EnemyData.Axis.LeftRight)
            direction = new Vector3(1, 0, 0);
        else
            direction = new Vector3(0, 0, 1);
    }

    void Update()
    {
        if (playerInRange)
            Attack();
        else
        {
            if (!UtilLib.InRange(currentPos, originalPos, eData.moveDistance))
                SwapDirection();
            transform.Translate(direction * eData.movementSpeed * Time.deltaTime);
        }
    }

    public void TakeDMG(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
            Destroy(gameObject);
    }

    private void SwapDirection()
    {
        direction = -direction;
        transform.Translate(direction * eData.movementSpeed * Time.deltaTime);
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 10)
        SwapDirection();
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
