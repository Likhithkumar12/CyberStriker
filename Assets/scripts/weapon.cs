
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
    [Header("Burst Fire")]
    public bool burstavailable;
    public bool isActive;
    public int bulletsperburst = 3;
    public float burstfirerate = 1;
    public float burstdelay = 0.1f;
    [Header("firerate")]
    public float defaltfirerate;
    public float firerate;
    private float lastfiretime;
    [Header("Types")]
    public WeaponType weaponType;
    public ShootType shootType;
    [Header("Speeds")]
    public float Reloadspeed = 1;
    [Range(1, 2)]
    public float Grabspeed=1;
    [Header("Spread")]
    private  float currentspread ;
    public float maxspread;
    public float basespread;
    private float spreadincrease = 0.1f;
    private float lastspreadtime;
    private float spreadcooldown = 1;


    public bool ISAvailable()
    {
        if (weaponType == WeaponType.Shotgun)
        {
            burstdelay = 0;
            return isActive && canshoot();
        }
        return isActive && canshoot();
    }
    public void toggleburstfire()
    {
        if (burstavailable == false)
        {
            return;
        }
        isActive = !isActive;
        if (isActive)
        {
            bulletspercount = bulletsperburst;
            firerate = burstfirerate;
        }
        else
        {
            bulletspercount = 1;
            firerate = defaltfirerate;
        }
         
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
    public bool canshoot()=> checkfirerate() && isenoughammo();
        
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
