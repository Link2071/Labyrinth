using System;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
