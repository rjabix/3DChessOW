using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardEmptyScript : MonoBehaviour
{

    private GameObject selectedPiece;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            GameObject clickedObject = hit.transform.gameObject;

    //            // ��������, �� ������� �� ������
    //            if (clickedObject.tag == "ChessPiece")
    //            {
    //                // ���� ������ �� �� ���� �������, �������� ������� ������� ������ �� ������� ��
    //                if (selectedPiece == null)
    //                {
    //                    selectedPiece = clickedObject;
    //                    initialPosition = selectedPiece.transform.position;
    //                }
    //                // ���� ������ ��� ���� �������, ������ �� �� �������
    //                else
    //                {
    //                    selectedPiece.transform.position = clickedObject.transform.position;
    //                    selectedPiece = null; // �������� ������ ������ ���� �����������
    //                }
    //            }
    //        }
    //    }
    //}
}
