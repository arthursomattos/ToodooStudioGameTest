using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{
    public ThirdPersonController thirdPerson;
    public AnimationHandler animatonHandler;

    public void OnTriggerEnter(Collider c)
    {
        if (c.tag == "MagicOneGot")
        {
            animatonHandler.PlayAnimation("Victory", 1.2f);
            thirdPerson.GotMagic1 = true;
        }
        if (c.tag == "MagicTwoGot")
        {
            animatonHandler.PlayAnimation("Victory", 1.2f);
            thirdPerson.GotMagic2 = true;
        }
        if (c.tag == "EnemyAttack1" && thirdPerson.isDefending == false)
        {
            thirdPerson.HP--;
            animatonHandler.PlayAnimation("Damage", 1.0f);
            if (thirdPerson.HP <= 0)
            {
                animatonHandler.PlayDeathAnimation();
            }
        }
        else if (c.tag == "EnemyAttack2" && thirdPerson.isDefending == false)
        {
            thirdPerson.HP -= 2;
            animatonHandler.PlayAnimation("Damage", 1.0f);
            if (thirdPerson.HP <= 0)
            {
                animatonHandler.PlayDeathAnimation();
            }
        }

    }
}
