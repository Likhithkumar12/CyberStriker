
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
    [Header("Magazine")]
    public int inmagazineammo;
    public int bulletspercount ;
    public int reservedammo;
    public int magazinesize;
    [Header("firerate")]
    public  float firerate;
    private float lastfiretime=0;
    [Header("Types")]
    public WeaponType weaponType;
    public ShootType shootType;
    [Header("Speeds")]
    public float Reloadspeed { get; private set; } = 1;
    [Range(1, 2)]
    public float Grabspeed { get; private set; } = 1;
    [Header("Spread")]
    private  float currentspread ;
    private float maxspread;
    private float basespread;
    private float spreadincrease = 0.1f;
    private float lastspreadtime;
    private float spreadcooldown = 1;

    public weapon(WeaponData weaponData)
    {
        this.firerate = weaponData.firerate;
        this.weaponType = weaponData.weaponType;
        this.shootType = weaponData.shootType;
        this.maxspread = weaponData.maxspread;
        this.basespread = weaponData.basespread;
        this.spreadincrease = weaponData.spreadincrease;
        this.Reloadspeed = weaponData.Reloadspeed;
        this.Grabspeed = weaponData.Grabspeed;
        this.reservedammo = weaponData.reservedammo;
        this.inmagazineammo = weaponData.inmagazineammo;
        this.magazinesize = weaponData.magazinesize;
        this.bulletspercount = weaponData.bulletspercount;

    }

    public Vector3 ApplySpread(Vector3 direction)
    {
        UpdateSpread();
        float x = Random.Range(-currentspread, currentspread);
        Quaternion spread = Quaternion.Euler(0, x, 0);
        return spread * direction;
    }
    public void IncreaseSpread()
    {
        currentspread=Mathf.Clamp(currentspread + spreadincrease, basespread, maxspread);
    }
    public void UpdateSpread()
    {
        if (Time.time > lastspreadtime + spreadcooldown)
        {
            currentspread = basespread;
        }
        else
        {
            IncreaseSpread();
        }
        lastspreadtime=Time.time;

    }
    public bool canshoot()
    {
        if (checkfirerate() && isenoughammo())
        {
            return true;
        }
        return false;
    }
        
    public bool checkfirerate()
    {
        if (Time.time > lastfiretime + 1 / firerate)
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
