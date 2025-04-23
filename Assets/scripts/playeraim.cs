
using UnityEngine;

public class playeraim : MonoBehaviour
{

    private player playerr;
    private Pllayermovement pllayermovement;
    playerweaponcontroller playerweaponcontroller;
    private Input inputActions;

    [Header("Aim info")]
    [SerializeField] Transform AimTarget;
    [SerializeField] LineRenderer aimlaser;
    private bool isaimingprecisely = true;
    [Header("camera info ")]
    [Space]
    private Vector2 aiminput;
    private RaycastHit lasthit;
    [SerializeField] LayerMask layerMask;
    private void Start()
    {
        playerr = GetComponent<player>();
        pllayermovement = GetComponent<Pllayermovement>();
        playerweaponcontroller = GetComponent<playerweaponcontroller>();
        applyinputtactions();
    }
    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse1))
        {
            isaimingprecisely = !isaimingprecisely;
        }
        updateaimposition();
        AimLaserPosition();
      

    }
    private void AimLaserPosition()
    {
        Vector3 gunPoint = playerweaponcontroller.returngunpoint();
        Vector3 direction = playerweaponcontroller.BulletDirection();

        aimlaser.SetPosition(0, gunPoint);

        Vector3 endPoint = gunPoint + direction * 25f;

        if (Physics.Raycast(gunPoint, direction, out RaycastHit hit, 25f))
        {
            endPoint = hit.point;
            Debug.Log("Laser Hit Point: " + endPoint);
        }

        aimlaser.SetPosition(1, endPoint);
    }
    private void updateaimposition()
    {
        AimTarget.position = applyaim().point;
        if (!isaimingprecisely)
        {
            AimTarget.position = new Vector3(AimTarget.position.x, transform.position.y + 1, AimTarget.position.z);
        }
    }

    public bool isaimingprecise()
    {
        if (isaimingprecisely)
        {
            return true;
        }
        return false;
    }
    private void applyinputtactions()
    {
        inputActions = playerr.inputactions;
        inputActions.Character.Aim.performed += context => aiminput = context.ReadValue<Vector2>();
        inputActions.Character.Aim.canceled += context => aiminput = Vector2.zero;
    }
    public RaycastHit applyaim()
    {
        Ray ray = Camera.main.ScreenPointToRay(aiminput);
        if (Physics.Raycast(ray, out var rayhit, Mathf.Infinity, layerMask))
        {
            lasthit = rayhit;
            return rayhit;
        }
        return lasthit;
    }
  

}

