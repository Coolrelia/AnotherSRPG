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

        if(initiatorDamage >= 1)
        {
            outgoingText.text = initiator.name + " attacked    " + initiatorDamage.ToString() + " Damage";
            TextBox();
            Instantiate(explosion, receiver.transform.position, Quaternion.identity);
            receiver.stat.health -= initiatorDamage;
        }
    }

    void TextBox()
    {
        TextBoxEnter();
        Invoke("TextBoxExit", 5);
    }

    void TextBoxEnter()
    {
        anim.SetTrigger("Enter");
    }

    void TextBoxExit()
    {
        anim.SetTrigger("Exit");
        outgoingText.text = "";
    }
}
