using UnityEngine;

public class Magic : MonoBehaviour
{

    public int DMG { set; get; }

    void Update()
    {
        Vector3 T = PlayerData.player.Position;
        T.y += 0.5f;
        Vector3 dir = T - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * 25.0f);
    }
    private void LateUpdate()
    {
        if (UtilLib.InRange(PlayerData.player.Position, transform.position, 1.0f))
        {
            PlayerData.player.TakeDMG(DMG);
            Destroy(gameObject);
        }
    }
}
