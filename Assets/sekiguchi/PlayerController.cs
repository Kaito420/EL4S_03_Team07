using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Wキーだけで歩く
        animator.SetBool("IsWalk", Input.GetKey(KeyCode.W));

        // Space押した瞬間にダンス
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Dance");
        }
    }
}