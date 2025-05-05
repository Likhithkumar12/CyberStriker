

using UnityEngine;

public enum WeaponType
{
    Pistol,
    Revolver,
    Rifle,
    Shotgun,
    SniperRifle,
}
public enum ShootType
{
    Single,
    Multiple,
}

[System.Serializable]
public class weapon
{
    public int inmagazineammo;
    public int reservedammo;
    public int magazinesize;
    public float firerate;
    private float lastfiretime;
    public WeaponType weaponType;
    public ShootType shootType;
    public float Reloadspeed = 1;
    [Range(1, 2)]
    public float Grabspeed=1;
    public bool canshoot()
    {
        return isenoughammo();
    }
    public bool checkfirerate()
    {
        if(Time.time>lastfiretime+1/firerate)
        {
            lastfiretime = Time.time;
            return true;
        }
        return false;

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
