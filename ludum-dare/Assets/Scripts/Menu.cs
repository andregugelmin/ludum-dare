using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    [SerializeField]
    private GameObject MainMenu;
    [SerializeField]
    private GameObject CreditsMenu;

    private void Start()
    {
        SetActive(MainMenu);
        SetInactive(CreditsMenu);
    }
    public void SetActive(GameObject menu)
    {
        menu.SetActive(true);
    }
    public void SetInactive(GameObject menu)
    {
        menu.SetActive(false);
    }

}
