using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
public class weapnvisualcontroller : MonoBehaviour
{
    [SerializeField] private weaponmodel[] weaponmodels;
    [SerializeField] BackupWeaponModel[] backpackWeaponModels;
    [Header("Left Hand")]
    [SerializeField] TwoBoneIKConstraint lefthandconstraint;
    [SerializeField] private float incresetime = 0.25f;
    [SerializeField] private float incresetimelefthand = 0.25f;
    [SerializeField] Transform lefthand;
    private Animator anim;
    Rig rig;
    private bool rigshouldbeincreased;
    private bool lefthandshouldbeincreased;
   
    player player;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rig = GetComponentInChildren<Rig>();
        player = GetComponent<player>();
        weaponmodels = GetComponentsInChildren<weaponmodel>(true);
        backpackWeaponModels = GetComponentsInChildren<BackupWeaponModel>(true);

    }

    private void Update()
    {
        adjusting_rig_and_left_hand();


    }
    public weaponmodel currentweaponmodel()
    {
        weaponmodel currentweaponmodel = null;
        WeaponType weaponType = player.playerweaponcontroller.Currentweapon().weaponType;
        for (int i = 0; i < weaponmodels.Length; i++)
        {
            if (weaponmodels[i].weaponType == weaponType)
            {
                currentweaponmodel = weaponmodels[i];

            }

        }
        return currentweaponmodel;
    }

    private void adjusting_rig_and_left_hand()
    {
        if (rigshouldbeincreased)
        {
            
            rig.weight += incresetime * Time.deltaTime;
            if (rig.weight >= 1)
            {
                Debug.Log("inside rig");
                rigshouldbeincreased = false;
            }
        }
        if (lefthandshouldbeincreased)
        {
            lefthandconstraint.weight += incresetimelefthand * Time.deltaTime;
            if (lefthandconstraint.weight >= 1)
            {
                Debug.Log("inside lefthand");
                lefthandshouldbeincreased = false;
            }
        }
    }

    public void playreloadanimations()
    {
        float reloadspeed = player.playerweaponcontroller.Currentweapon().Reloadspeed;
        Debug.Log(reloadspeed);
        anim.SetFloat("reloadspeed", reloadspeed);
        anim.SetTrigger("reload");
        
        pauserig();
    }

    private void pauserig()
    {
        rig.weight = 0;
    }

    public void weaponequipanimation()
    {
        GrabType type = currentweaponmodel().grabType;
        pauserig();
        lefthandconstraint.weight = 0;
        float Grabspeed= player.playerweaponcontroller.Currentweapon().Grabspeed;
        anim.SetFloat("weaponGrab", (float)type);
        anim.SetFloat("grabspeed", Grabspeed);
        anim.SetTrigger("Grab");


    }

    public void callriganimation() => rigshouldbeincreased = true;
    public void calllefthandanimation() => lefthandshouldbeincreased = true;



    public void switchoncurrent()
    {
        switchoffcurrent(); 
        switchoffbackup();
        if (player.playerweaponcontroller.onlyonebackup() == false)
        {
            switchonbackup();
        }
        switchanimations((int)currentweaponmodel().holdType);   
        currentweaponmodel().gameObject.SetActive(true);
        attachlefthad();
    }

    public void switchoffcurrent()
    {
        for (int i = 0; i < weaponmodels.Length; i++)
        {
            weaponmodels[i].gameObject.SetActive(false);
        }

    }
    public void switchoffbackup()
    {
        for (int i = 0; i < backpackWeaponModels.Length; i++)
        {
           backpackWeaponModels[i].Activate(false);
        }
    }
    public void switchonbackup()
    {
        switchoffbackup();
        BackupWeaponModel lowbackhung = null;
        BackupWeaponModel backhang = null;
        BackupWeaponModel sidehang = null;

        for (int i = 0; i < backpackWeaponModels.Length; i++)
        {
            if(backpackWeaponModels[i].weaponType == player.playerweaponcontroller.Currentweapon().weaponType)
            {
                continue;
            }
            if (player.playerweaponcontroller.hasbackupininventory(backpackWeaponModels[i].weaponType))
            {
                if (backpackWeaponModels[i].HangTypeIs(HangType.LowBackHang))
                {
                    lowbackhung = backpackWeaponModels[i];
                }
                if (backpackWeaponModels[i].HangTypeIs(HangType.BackHang))
                {
                    backhang = backpackWeaponModels[i];
                }
                if (backpackWeaponModels[i].HangTypeIs(HangType.SideHang))
                {
                    sidehang = backpackWeaponModels[i];
                }
            }

        }
        lowbackhung?.Activate(true);
        backhang?.Activate(true);
        sidehang?.Activate(true);

    }
    private void attachlefthad()
    {
        Transform targettransform = currentweaponmodel().holdpoint;
        lefthand.localPosition = targettransform.localPosition;
        lefthand.localRotation = targettransform.localRotation;
    }
    private void switchanimations(int layerindex)
    {
        for (int i = 1; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(layerindex, 1);
    }


}
