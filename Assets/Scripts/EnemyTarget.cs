using UnityEngine;

public class EnemyTarget : SelectableObject
{
    EnemyController controller;

    void Start()
    {
        controller = GetComponent<EnemyController>();
    }

    public override void Action()
    {
        controller.TakeDMG(PlayerData.player.GetDMG());
    }
}
