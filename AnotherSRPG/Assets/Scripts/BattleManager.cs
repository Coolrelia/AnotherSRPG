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
        {
            if (Mathf.Abs(initiator.transform.position.x - receiver.transform.position.x) + Mathf.Abs(initiator.transform.position.y - receiver.transform.position.y) <= 1)
            {
                if (initiatorDamage >= 1)
                {
                    outgoingText.text = initiator.name + " attacked ... They dealt " + initiatorDamage.ToString() + " Damage";
                    TextBox();
                    Instantiate(explosion, receiver.transform.position, Quaternion.identity);
                    receiver.stat.health -= initiatorDamage;
                }
            }
            else
            {
                if (initiatorDamage >= 1)
                {
                    outgoingText.text = initiator.name + " attacked ... They dealt " + initiatorDamage.ToString() + " Damage";
                    TextBox();
                    Instantiate(explosion, receiver.transform.position, Quaternion.identity);
                    receiver.stat.health -= initiatorDamage;
                    return;
                }
            }
        }
        else
        {
            if (initiatorDamage >= 1)
            {
                outgoingText.text = initiator.name + " attacked ... They dealt " + initiatorDamage.ToString() + " Damage";
                TextBox();
                Instantiate(explosion, receiver.transform.position, Quaternion.identity);
                receiver.stat.health -= initiatorDamage;
            }
        }

        if (initiator.stat.health <= 0 || receiver.stat.health <= 0)
        {
            return;
        }

        if (receiver.stat.health >= 1)
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
        Invoke("TextBoxExit", 4);
    }

    void TextBoxEnter()
    {
        anim.SetTrigger("Enter");
    }

    void TextBoxExit()
    {
        anim.SetTrigger("Exit");
    }
}

