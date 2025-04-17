

using UnityEngine;
using UnityEngine.InputSystem;
public class Pllayermovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movespeed = 5f;
    [SerializeField] float rotationspeed = 10f;
    [SerializeField] float runspeed = 10f;
    Vector3 inputdir;
    private bool isrunning;
    private float  speed;
    private Animator animator;

    [Header("aim")]
   
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform sphere;
    private Vector3 lookindirection;
    private float verticalvelocity;
    private Input inputactions;
    private Vector2 moveinput;
    private Vector2 aiminput;
    private Vector3 movedirection;
    private CharacterController controller;
    player player;



    void Start()
    {
        player= GetComponent<player>();
        applyinputactions();
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        speed = movespeed;
    }
    void Update()
    {
        applymovement();
        applyaim();
        animationcontroller();

    }
    void animationcontroller()
    {
        float xvelocity = Vector3.Dot(movedirection.normalized, transform.right);
        float zvelocity = Vector3.Dot(movedirection.normalized, transform.forward);
        animator.SetFloat("xvelocity", xvelocity, .1f, Time.deltaTime);
        animator.SetFloat("zvelocity", zvelocity, .1f, Time.deltaTime);
        bool playrunaniimation = isrunning && inputdir.magnitude > 0;
        animator.SetBool("isrunning", playrunaniimation);
    }
    void applymovement()
    {
        inputdir = new Vector3(moveinput.x, 0, moveinput.y);
        movedirection= inputdir.x*transform.right + inputdir.z*transform.forward;
        if (controller.isGrounded == false)
        {
            verticalvelocity -= 9.81f * Time.deltaTime;
            movedirection.y = verticalvelocity;
        }
        else
        {
            verticalvelocity = 0f;
        }
        controller.Move(movedirection * Time.deltaTime * speed);

    }
    void applyaim()
    {
        Ray ray = Camera.main.ScreenPointToRay(aiminput);
        if (Physics.Raycast(ray, out var rayhit, Mathf.Infinity, layerMask))
        {
            lookindirection = rayhit.point - transform.position;
            float distance = lookindirection.magnitude;
            if (distance > 3f)
            {
                Quaternion targetrotation = Quaternion.LookRotation(lookindirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, Time.deltaTime * rotationspeed);
                sphere.position = new Vector3(rayhit.point.x, rayhit.point.y, rayhit.point.z);

            }

        }
        else
        {
            
            Vector3 aimPoint = ray.origin + ray.direction * 10f;
            aimPoint.y = transform.position.y + 1.0f; 

            lookindirection = aimPoint - transform.position;
            Quaternion targetrotation = Quaternion.LookRotation(lookindirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, Time.deltaTime * rotationspeed);
            sphere.position = aimPoint; 

        }
 
    }
 
    private void applyinputactions()
    {
        inputactions=player.inputactions;
        inputactions.Character.Movement.performed += context => moveinput = context.ReadValue<Vector2>();
        inputactions.Character.Movement.canceled += context => moveinput = Vector2.zero;
        inputactions.Character.Aim.performed += context => aiminput = context.ReadValue<Vector2>();
        inputactions.Character.Aim.canceled += context => aiminput = Vector2.zero;
        inputactions.Character.run.performed += contex =>
        {
            isrunning = true;
            speed = runspeed;
        };
        inputactions.Character.run.canceled += contex =>
        {
            speed = movespeed;
            isrunning = false;
        };
    }
}
