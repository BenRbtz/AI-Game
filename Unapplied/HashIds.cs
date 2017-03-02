using UnityEngine;
using System.Collections;

public class HashIds : MonoBehaviour
{
    public int speedFloat;
    public int openBool;

    void Awake()
    {
        speedFloat = Animator.StringToHash("Speed");
        openBool = Animator.StringToHash("Open");
    }
}
