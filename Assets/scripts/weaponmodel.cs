using UnityEngine;
public enum GrabType { sideGrab, BackGrab };
public enum HoldType { common=1, low, high };
public class weaponmodel : MonoBehaviour
{
    public WeaponType weaponType;
    public GrabType grabType;
    public HoldType holdType;
    public Transform gunpoint;
    public Transform holdpoint;

    
}
