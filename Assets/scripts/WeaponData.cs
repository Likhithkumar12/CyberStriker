using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Magazine")]
    public int inmagazineammo;
    public int bulletspercount;
    public int reservedammo;
    public int magazinesize;
    [Header("Types")]
    public WeaponType weaponType;
    public ShootType shootType;
    public float firerate;
    [Header("spread")]
    public float maxspread;
    public float basespread;
    public float spreadincrease = 0.1f;
    [Header("Speeds")]
    public float Reloadspeed = 1;
    [Range(1, 2)]
    public float Grabspeed = 1;


}
