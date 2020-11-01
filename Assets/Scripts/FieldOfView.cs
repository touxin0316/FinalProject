using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FieldOfView : MonoBehaviour
{
    private LayerMask mask;
    [SerializeField] private float fov = 90.0f;
    private int rayCount = 256;
    [SerializeField] private float viewDistance = 0.0f;

    private EnemyController controller;

    private float currentAngle = 0.0f;
    private float angleStep;
    private Mesh mesh;
    private Vector3 direction;
    private bool isMoving;

    private void Start()
    {
        angleStep = fov / rayCount;
        mesh = new Mesh();
        mask = LayerMask.GetMask("Wall");
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.transform.Translate(0, 0.05f, 0);
    }

    public void SetController(EnemyController c)
    {
        controller = c;
    }

    void Update()
    {
        if (isMoving)
            CalculateFoV();
    }

    private void CalculateFoV()
    {
        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit hit;
            Physics.Raycast(transform.position, UtilLib.GetVectorFromAngle(currentAngle), out hit, viewDistance, mask);
            if (hit.collider == null)
                vertex = UtilLib.GetVectorFromAngle(currentAngle) * viewDistance;
            else
                vertex = hit.point - transform.position;
            vertices[vertexIndex] = vertex;
            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            currentAngle -= angleStep;
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    void LateUpdate()
    {
        CheckPlayer();
    }

    public void SetDirection(Vector3 dir)
    {
        if (dir == Vector3.zero)
        {
            isMoving = false;
            return;
        }
        isMoving = true;
        direction = dir;
        float angle = UtilLib.GetAngleFromDirection(direction);
        if (angle < 0)
            angle += 360;
        currentAngle = angle + fov / 2;
    }

    private void CheckPlayer()
    {
        if (!UtilLib.InRange(transform.position, PlayerData.player.Position, viewDistance))
            return;

        Vector3 dir = PlayerData.player.Position - transform.position;
        if (Vector3.Angle(direction, dir) < fov / 2)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, dir, out hit, viewDistance);
            if (hit.transform.name == "Player")
                controller.Attack();
        }
    }

}


