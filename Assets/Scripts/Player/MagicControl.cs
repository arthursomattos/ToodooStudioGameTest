using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicControl : MonoBehaviour
{
    [Header("Components")]
    ThirdPersonController thirdPerson;

    [Header("Magics")]
    public GameObject MagicAttack1; //This is a standard magic
    public GameObject MagicAttack2; //This is a powerfull magic
    public Transform MagicsOut;  //This is the place where the magics will go out

    private void Start()
    {
        thirdPerson = GetComponentInParent<ThirdPersonController>();
    }

    public void MagicAttack1Out()
    {
        if (thirdPerson.GotMagic1 == true)
        {
            Instantiate(MagicAttack1, MagicsOut.position, MagicsOut.rotation);
        }
    }

    public void MagicAttack2Out()
    {
        if (thirdPerson.GotMagic2 == true)
        {
            Instantiate(MagicAttack2, MagicsOut.position, MagicsOut.rotation);
        }
    }
}
