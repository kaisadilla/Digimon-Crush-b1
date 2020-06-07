using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridChar : MonoBehaviour {
    [SerializeField] private Animator charAnim;
    [SerializeField] private Animator borderAnim;

    public void SetHovered(int player) {
        borderAnim.SetInteger("player", player);
        if (player == -1) {
            charAnim.SetFloat("selected", 0f);
        }
        else {
            charAnim.SetFloat("selected", 1f);
        }
    }

    public void SetSelected(int player) {
        if (player == 0) {
            borderAnim.enabled = false;
            borderAnim.gameObject.GetComponent<Image>().color = Color.blue;
        }
        else if (player == 1) {
            borderAnim.enabled = false;
            borderAnim.gameObject.GetComponent<Image>().color = Color.red;
        }
        else {
            borderAnim.enabled = true;
            borderAnim.gameObject.GetComponent<Image>().color = Color.white;
        }

    }
}
