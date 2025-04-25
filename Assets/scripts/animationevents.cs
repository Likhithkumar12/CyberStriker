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
        Debug.Log("inside reload");
        weapnvisualcontroller.callriganimation();
        playerweaponcontroller.Currentweapon().fillbullets();

    }
    public void rigisover()
    {
        weapnvisualcontroller.callriganimation();
        weapnvisualcontroller.calllefthandanimation();
    }
    public void weapongrabisover()
    {
        weapnvisualcontroller.setweapobusy(false);
    }
}
