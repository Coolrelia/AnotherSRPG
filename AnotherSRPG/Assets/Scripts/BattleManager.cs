using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public GameObject explosion;

    public Text outgoingText;
    public Text incomingText;

    public Animator anim;

    public void Combat(Unit initiator, Unit receiver)
    {
        initiator.hasAttacked = true;

        int initiatorDamage = initiator.stat.attack - receiver.stat.defense;
        int counterAttackDamage = receiver.stat.attack - initiator.stat.defense;

        if (initiator.transform.tag == "Ranged" && receiver.tag != "Ranged")
            //Check if you're ranged
        {
            if (Mathf.Abs(initiator.transform.position.x - receiver.transform.position.x) + Mathf.Abs(initiator.transform.position.y - receiver.transform.position.y) <= 1)
                //If you are ranged, check if you're within melee distance your target
            {
                if (initiatorDamage >= 1)
                    //You're in melee range, attack goes out and the enemy is given the chance to counter attack
                {
                    outgoingText.text = initiator.name + " attacked ... They dealt " + initiatorDamage.ToString() + " Damage";
                    TextBox();
                    Instantiate(explosion, receiver.transform.position, Quaternion.identity);
                    receiver.stat.health -= initiatorDamage;
                    initiator.stat.experience += receiver.stat.attackedExperience;
                }
            }
            else
            {
                if (initiatorDamage >= 1)
                    //You're not in melee range, the enemy is not given the chance to counter attack
                {
                    outgoingText.text = initiator.name + " attacked ... They dealt " + initiatorDamage.ToString() + " Damage";
                    TextBox();
                    Instantiate(explosion, receiver.transform.position, Quaternion.identity);
                    receiver.stat.health -= initiatorDamage;
                    initiator.stat.experience += receiver.stat.attackedExperience;
                    return;
                }
            }
        }
        else
        {
            if (initiatorDamage >= 1)
                //you're not ranged, attack proceeds as normal
            {
                outgoingText.text = initiator.name + " attacked ... They dealt " + initiatorDamage.ToString() + " Damage";
                TextBox();
                Instantiate(explosion, receiver.transform.position, Quaternion.identity);
                receiver.stat.health -= initiatorDamage;
                initiator.stat.experience += receiver.stat.attackedExperience;
            }
        }

        if (initiator.stat.health <= 0 || receiver.stat.health <= 0)
            //checks if either you or the enenmy is dead and ends the combat if true
        {
            return;
        }

        if (receiver.stat.health >= 1)
            //The enemy is stil alive, they counter attcak
        {
            if (counterAttackDamage >= 1)
            {
                incomingText.text = receiver.name + " retaliated ... They dealt " + counterAttackDamage.ToString() + " Damage";
                TextBox();
                Instantiate(explosion, initiator.transform.position, Quaternion.identity);
                initiator.stat.health -= counterAttackDamage;
            }
        }
    }

    void TextBox()
    {
        TextBoxEnter();
        Invoke("TextBoxExit", 2.5f);
    }

    void TextBoxEnter()
    {
        anim.SetTrigger("Enter");
    }

    void TextBoxExit()
    {
        anim.SetTrigger("Exit");
        outgoingText.text = "";
        incomingText.text = "";
    }
}

