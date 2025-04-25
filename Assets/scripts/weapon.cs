

using UnityEngine;

public enum WeaponType
{
    Pistol,
    Revolver,
    Rifle,
    Shotgun,
    SniperRifle,
}

[System.Serializable]
public class weapon
{
    public int inmagazineammo;
    public int reservedammo;
    public int magazinesize;
    public WeaponType weaponType;
    public bool canshoot()
    {
        return isenoughammo();
    }
    public bool isenoughammo()
    {
        if (inmagazineammo > 0)
        {
            inmagazineammo--;
            return true;
        }
        return false;
    }
    public bool weaponcanreload()
    {
        if (reservedammo > 0)
        {
            return true;
        }
        return false;

    }
    public void fillbullets()
    {
        Debug.Log("inside");
        int bulletstoreload = magazinesize;
        if (bulletstoreload > reservedammo)
        {
            bulletstoreload = reservedammo;
        }
        reservedammo -= bulletstoreload;
        inmagazineammo = bulletstoreload;

        if (reservedammo < 0)
        {
            reservedammo = 0;
        }

    }

}
