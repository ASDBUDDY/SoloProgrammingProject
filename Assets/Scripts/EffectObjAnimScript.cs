using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjAnimScript : MonoBehaviour
{
    public FriendlyControllerScript FriendlyController;


    public void CookieAnimOver() => FriendlyController.HealingOver();

    public void PuddingAnimOver() => FriendlyController.ShieldingOver();
}
