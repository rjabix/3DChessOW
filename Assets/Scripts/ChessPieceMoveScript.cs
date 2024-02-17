using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMoveScropt : MonoBehaviour
{
    private bool isPieceSelected = false;
    private Vector3 targetPosition;
    private Material originalMaterial;
    public Material highlightMaterial; // ϳ���������� �������

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        // �������� ���������� ������� ��� ������� ����'����
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = originalMaterial;
        }
    }

    private void OnMouseDown()
    {
        isPieceSelected = true;
        HighlightPiece();
    }

    private void Update()
    {
        if (isPieceSelected && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ChessCube"))
                {
                    targetPosition = hit.collider.bounds.center; // �������� ����� ����
                    TeleportPiece();
                    isPieceSelected = false;
                    RestoreMaterial(); // ��������� ������� �� ����������� ���� ������������
                }
            }
        }
    }

    private void TeleportPiece()
    {
        transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
    }

    private void HighlightPiece()
    {
        // �������� �� ������ ��'���� ������
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>(true);

        // ������ ������� ��� ����� ������� ������
        foreach (Renderer renderer in childRenderers)
        {
            renderer.material = highlightMaterial;
        }
    }

    private void RestoreMaterial()
    {
        // ��������� ������� �� ����������� ��� ������� ����'����
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = originalMaterial;
        }
    }
}
