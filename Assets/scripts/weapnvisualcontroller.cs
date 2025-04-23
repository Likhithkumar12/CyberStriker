using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
public class weapnvisualcontroller : MonoBehaviour
{
    [SerializeField] Transform[] weapons;
    [Header("Left Hand")]
    [SerializeField] TwoBoneIKConstraint lefthandconstraint;
    [SerializeField] private float incresetime = 0.25f;
    [SerializeField] private float incresetimelefthand = 0.25f;
    [SerializeField] Transform lefthand;
    private Animator anim;
    private Transform currentgun;
    Rig rig;
    private bool rigshouldbeincreased;
    private bool lefthandshouldbeincreased;
    public  bool weponisbusy;
    
    private void Start()
    {
        weponisbusy = false;
        anim = GetComponentInChildren<Animator>();
        rig = GetComponentInChildren<Rig>();
        switchonguns(weapons[0]);
    }

    private void Update()
    {
        applyinputswitch();
        if (UnityEngine.Input.GetKeyDown(KeyCode.R) &&  weponisbusy==false)
        {
            anim.SetTrigger("reload");
            pauserig();

        }
        if (rigshouldbeincreased)
        {
            rig.weight += incresetime * Time.deltaTime;
            if (rig.weight >= 1)
            {
                rigshouldbeincreased = false;
            }
        }
        if (lefthandshouldbeincreased)
        {
            lefthandconstraint.weight += incresetimelefthand * Time.deltaTime;
            if (lefthandconstraint.weight >= 1)
            {
                lefthandshouldbeincreased = false;
            }
        }

    }

    private void pauserig()
    {
        rig.weight = 0;
    }

    private void weaponGrab(GrabType type)
    {
        pauserig();
        lefthandconstraint.weight = 0;
        anim.SetFloat("weaponGrab", (float)type);
        setweapobusy(true);
        anim.SetTrigger("Grab");
      
        
    }
    public  void setweapobusy(bool busy)
    {
        weponisbusy = busy;
        anim.SetBool("weaponisbusy",weponisbusy);
    }
    public void callriganimation() => rigshouldbeincreased = true;
    public void calllefthandanimation() => lefthandshouldbeincreased = true;

    private void applyinputswitch()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchonguns(weapons[0]);
            switchanimations(1);
            weaponGrab(GrabType.sideGrab);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchonguns(weapons[1]);
            switchanimations(1);
            weaponGrab(GrabType.sideGrab);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchonguns(weapons[2]);
            switchanimations(1);
            weaponGrab(GrabType.BackGrab);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchonguns(weapons[3]);
            switchanimations(2);
            weaponGrab(GrabType.BackGrab);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha5))
        {
            switchonguns(weapons[4]);
            switchanimations(3);
            weaponGrab(GrabType.BackGrab);
        }
    }

    private void switchonguns(Transform gun)
    {
        switchoffguns();
        gun.gameObject.SetActive(true);
        currentgun = gun;
        attachlefthad();
    }

    private void switchoffguns()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

    }
    private void attachlefthad()
    {
        Transform targettransform = currentgun.GetComponentInChildren<left_hand_target_transform>().transform;
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

    public enum GrabType { sideGrab, BackGrab };
}
