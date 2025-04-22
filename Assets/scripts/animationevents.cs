using UnityEngine;

public class animationevents : MonoBehaviour
{
    weapnvisualcontroller weapnvisualcontroller;
    private void Start()
    {
        weapnvisualcontroller = GetComponentInParent<weapnvisualcontroller>();
    }
    public void Reloadisover()
    {
        weapnvisualcontroller.callriganimation();

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
