using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Coin, Heart, Exp};
    public Type type;
    public int value;
}
