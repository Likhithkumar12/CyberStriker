using UnityEngine;
using UnityEngine.InputSystem;
public class weapnvisualcontroller : MonoBehaviour
{
    [SerializeField] Transform[] weapons;

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchonguns(weapons[0]);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchonguns(weapons[1]);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchonguns(weapons[2]);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchonguns(weapons[3]);
        }
        
    }
    private void switchonguns(Transform gun)
    {
        switchoffguns();
        gun.gameObject.SetActive(true);
    }

    private void switchoffguns()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

    }
}
