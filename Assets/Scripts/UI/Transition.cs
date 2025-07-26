using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private AnimationClip _animationEnd;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        _animator.SetTrigger("Start");

        yield return new WaitForSeconds(_animationEnd.length);
    }
}
