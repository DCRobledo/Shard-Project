using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManagement : MonoBehaviour
{
    [SerializeField]
    private bool playStartAnimation = true;

    [SerializeField]
    private GameObject blackPanel;
    private Animator animator;


    private void Awake() {
        this.animator = blackPanel.GetComponent<Animator>();

        if (!playStartAnimation) animator.Play("idle_transparent");
    }
}
