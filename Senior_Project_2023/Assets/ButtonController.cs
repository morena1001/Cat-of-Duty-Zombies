using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    public Image[] buttons; public Image[] slots;
    public GameObject player, prefab;
    public UI ui;
    public void OnMouseOver(PointerEventData pointerEventData) {
        Debug.Log("hi");
    }
public void OnPointerExit(PointerEventData pointerEventData) {
        Debug.Log("bye");
    }
    public void OnPointerDown(PointerEventData pointerEventData) {
        if (this.gameObject.transform.GetChild(0).gameObject.activeSelf) {
            this.gameObject.GetComponent<Image>().color = Color.grey;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData) {
        Transform t = player.transform.Find("center");
        // for (int i = 0; i < t.childCount; i++) {
        //     t.GetChild(i).GetComponent<
        // }
        if (t.childCount == 0) return;
        if (!this.gameObject.transform.GetChild(0).gameObject.activeSelf) {
            return;
        }
        if (this.gameObject == buttons[0].gameObject) {
            buttons[1].gameObject.GetComponent<Image>().color = Color.white;
        } else {
            buttons[0].gameObject.GetComponent<Image>().color = Color.white;
        }
        this.gameObject.GetComponent<Image>().color = Color.green;
        //Transform t = player.transform.Find("center");
        for (int i = 0; i < t.childCount; i++) {
            if (!t.GetChild(i).gameObject.activeSelf) {
                t.GetChild(i).gameObject.SetActive(true);
                GunController temp = t.GetChild(i).gameObject.GetComponent<GunController>();
                ui.Change(temp.bulletCount, temp.bulletCountTotal);
            } else if (t.GetChild(i).gameObject.tag == "Weapon" && t.GetChild(i).gameObject.activeSelf) {
                t.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}