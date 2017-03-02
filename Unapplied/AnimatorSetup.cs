using UnityEngine;
using System.Collections;

public class AnimatorSetup
{
    public float speedDampTime = 0.1f;   

    private Animator anim;                        
    private HashIds hash;                         

    public AnimatorSetup(Animator animator, HashIds hashIDs)
    {
        // instalise
        anim = animator;
        hash = hashIDs;
    }


    public void Setup(float speed, float angle)
    {
        anim.SetFloat(hash.speedFloat, speed, speedDampTime, Time.deltaTime);//setting up animator to be adjusted by the hashId with damping time
    }
}