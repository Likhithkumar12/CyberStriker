using UnityEngine;

public class animationevents : MonoBehaviour
{
    weapnvisualcontroller weapnvisualcontroller;
    playerweaponcontroller playerweaponcontroller;
    private void Start()
    {
        weapnvisualcontroller = GetComponentInParent<weapnvisualcontroller>();
        playerweaponcontroller = GetComponentInParent<playerweaponcontroller>();
    }
    public void Reloadisover()
    {
        weapnvisualcontroller.callriganimation();
        playerweaponcontroller.Currentweapon().fillbullets();
        playerweaponcontroller.setweaponready(true);

    }
    public void rigisover()
    {
        weapnvisualcontroller.callriganimation();
        weapnvisualcontroller.calllefthandanimation();
    }
    public void weapongrabisover()
    {
        playerweaponcontroller.setweaponready(true);
    }
    public void switchonweaponmodels(){
        Debug.Log("inside switchon");
        weapnvisualcontroller.switchoncurrent();
    }
}
