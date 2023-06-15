using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void ReturnMenu()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }
}
