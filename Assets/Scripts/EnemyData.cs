using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy")]
public class EnemyData : ScriptableObject
{
    public enum Axis { LeftRight, UpDown }

    [Header("Basic Data")]
    public string enemyName;
    public int baseDMG;
    public int fireRate;
    public int maxHP;
    public int movementSpeed;

    [Header("Movement")]
    public Axis moveAxis;
    public float moveDistance;

    [Header("Detection")]
    public int FoV;
    public float viewDistance;
}