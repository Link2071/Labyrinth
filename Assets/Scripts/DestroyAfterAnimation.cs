using System;
using UnityEditor.Timeline;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
