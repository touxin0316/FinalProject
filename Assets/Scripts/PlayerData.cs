using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public static PlayerData player;

    private int maxHP = 10;
    private int currentHP = 10;

    private int ammoCount = 8;

    public float activeRange { get; } = 2.5f;
    /*
    [Header("UI")]
    public Image barHP;
    public Text barAmmo;
    */
    void Start()
    {
        player = this;
        //UpdateUI();
    }

    void Update()
    {
        
    }

    public void TakeDMG(int amount)
    {
        currentHP -= amount;
        //UpdateUI();
    }

    public int GetDMG()
    {
        return 1;
    }

    /*
    private void UpdateUI()
    {
        barHP.fillAmount = (float)currentHP / maxHP;
        string ammo = "";
        for (int i = 0; i < ammoCount; i++)
            ammo += "I";
        barAmmo.text = ammo;
    }
    */
    public Vector3 Position
    {
        get
        {
            return gameObject.transform.position;
        }
    }

}
