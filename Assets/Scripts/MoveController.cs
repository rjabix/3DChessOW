using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private static MoveController instance;

    // �����, �� ������ �������� ���
    private string currentMove = "White";

    public static MoveController Instance
    {
        get
        {
            // ���� ��������� �� ����, ��������� ����
            if (instance == null)
            {
                instance = new GameObject("MoveController").AddComponent<MoveController>();
            }
            return instance;
        }
    }

    // ����� ��� ���� ����
    public void ChangeMove()
    {
        currentMove = (currentMove == "White") ? "Black" : "White";
        Debug.Log("Now is " + currentMove + "'s move");
    }

    // ����� ��� ��������� ��������� ����
    public string GetCurrentMove()
    {
        return currentMove;
    }
}
