using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private static MoveController instance;

    // Змінна, що зберігає поточний хід
    private string currentMove = "White";

    public static MoveController Instance
    {
        get
        {
            // Якщо екземпляр не існує, створюємо його
            if (instance == null)
            {
                instance = new GameObject("MoveController").AddComponent<MoveController>();
            }
            return instance;
        }
    }

    // Метод для зміни ходу
    public void ChangeMove()
    {
        currentMove = (currentMove == "White") ? "Black" : "White";
        Debug.Log("Now is " + currentMove + "'s move");
    }

    // Метод для отримання поточного ходу
    public string GetCurrentMove()
    {
        return currentMove;
    }
}
