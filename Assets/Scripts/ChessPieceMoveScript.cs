using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMoveScropt : MonoBehaviour
{
    private bool isPieceSelected = false;
    private Vector3 targetPosition;
    private Material originalMaterial;
    public Material highlightMaterial; // Підсвічуваний матеріал

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        // Збережіть початковий матеріал для кожного підоб'єкту
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
                    targetPosition = hit.collider.bounds.center; // Отримуємо центр куба
                    TeleportPiece();
                    isPieceSelected = false;
                    RestoreMaterial(); // Повертаємо матеріал до початкового після телепортації
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
        // Отримати всі дочірні об'єкти фігури
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>(true);

        // Змінити матеріал для кожної частини фігури
        foreach (Renderer renderer in childRenderers)
        {
            renderer.material = highlightMaterial;
        }
    }

    private void RestoreMaterial()
    {
        // Повертаємо матеріал до початкового для кожного підоб'єкту
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = originalMaterial;
        }
    }
}
