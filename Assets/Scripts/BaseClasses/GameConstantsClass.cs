using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstantsClass
{
    //Layers
    public const int PLAYER_LAYER = 6;
    public const int ENEMY_LAYER = 7;

    //Tags
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";

    //AI Directive Text
    public const string ALLY_DEFAULT = "Following";
    public const string ALLY_MELEE = "Attacking!";
    public const string ALLY_RANGED = "Throwing Projectiles!";
    public const string ALLY_HEALING = "Providing Healing Cookies!";
    public const string ALLY_SHIELD = "Making Armour Pudding!";
    public const string ALLY_PROJ_SHIELD = "Setting Projectile Barrier!";
    public const string ALLY_DECOY = "Diversionary Tactics!";

}
