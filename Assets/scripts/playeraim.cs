
using UnityEngine;

public class playeraim : MonoBehaviour
{

    private player playerr;
    private Pllayermovement pllayermovement;
    private Input inputActions;
    [Header("Aim info")]
    [SerializeField] Transform AimTarget;
    [Header("camera info ")]
    [SerializeField] float maxcameradistance;
    [SerializeField] float camerasensi;
    [SerializeField] float mincameradistance;
    [SerializeField] Transform cameratarget;
    [Space]
    private Vector2 aiminput;
    private RaycastHit lasthit;
    [SerializeField] LayerMask layerMask;
    private void Start()
    {
        playerr = GetComponent<player>();
        pllayermovement = GetComponent<Pllayermovement>();
        applyinputtactions();
    }
    private void Update()
    {
        AimTarget.position = applyaim().point;
        AimTarget.position = new Vector3(AimTarget.position.x, transform.position.y + 1, AimTarget.position.z);
        cameratarget.position = Vector3.Lerp(cameratarget.position, cameralookahead(), Time.deltaTime * camerasensi);

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
            lasthit= rayhit;
            return rayhit;
        }
        return lasthit;
    }
    public Vector3 cameralookahead()
    {
        float actualcameramaxdistance = pllayermovement.moveinput.y < -0.5f ? mincameradistance : maxcameradistance;
        Vector3 desiredcameraposition = applyaim().point;
        Vector3 aimdirection = (desiredcameraposition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, desiredcameraposition);
        float clampedDistance = Mathf.Clamp(distance, mincameradistance, actualcameramaxdistance);
        desiredcameraposition = transform.position + aimdirection * clampedDistance;
        desiredcameraposition.y = transform.position.y + 1;
        return desiredcameraposition;
        


    }

}

