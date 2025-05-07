using UnityEngine;
public enum HangType
{
    LowBackHang,
    BackHang,
    SideHang
}

public class BackupWeaponModel : MonoBehaviour
{
    public WeaponType weaponType;
    [SerializeField] private HangType hangType;

    public void Activate(bool value)=>gameObject.SetActive(value);

    public bool HangTypeIs(HangType type) => this.hangType == type;
    

    
}
