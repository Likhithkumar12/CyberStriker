

using UnityEngine;
using UnityEngine.InputSystem;
public class Pllayermovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movespeed = 5f;
    [SerializeField] float rotationspeed = 10f;
    [SerializeField] float runspeed = 10f;
    [SerializeField] float turnspeed;
    Vector3 inputdir;
    private bool isrunning;
    private float speed;
    private Animator animator;

    private float verticalvelocity;
    private Input inputactions;
    public Vector2 moveinput;

    private Vector3 movedirection;
    private CharacterController controller;
    player player;



    void Start()
    {
        player = GetComponent<player>();
        applyinputactions();
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        speed = movespeed;
    }
    void Update()
    {
        applymovement();
        applyrotation();
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
        movedirection = inputdir.x * transform.right + inputdir.z * transform.forward;
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
    private void applyrotation()
    {
        Vector3 lookindirection = player.playeraimm.applyaim().point - transform.position;
        lookindirection.y = 0f;
        lookindirection.Normalize();
        Quaternion targetrotation = Quaternion.LookRotation(lookindirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, turnspeed * Time.deltaTime);
        

    }
    private void applyinputactions()
    {
        inputactions = player.inputactions;
        inputactions.Character.Movement.performed += context => moveinput = context.ReadValue<Vector2>();
        inputactions.Character.Movement.canceled += context => moveinput = Vector2.zero;

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
 
    
