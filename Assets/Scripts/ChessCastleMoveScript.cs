using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessCastleMoveScript : ChessPieceBase
{

    public string team;
    private bool isPieceSelected = false;
    private Vector3 targetPosition;
    private Material originalMaterial;
    public Material highlightMaterial; 

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = originalMaterial;
        }
    }

    private void OnMouseDown()
    {
        if (MoveController.Instance.GetCurrentMove() == team)
        {
            DeselectAllPieces();
            isPieceSelected = true;
            HighlightPiece();
        }
    }

    private void Update()
    {
        if (isPieceSelected && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("ChessCube") && IsLegalMove(transform.position, hit.collider.bounds.center))
                {
                    targetPosition = hit.collider.bounds.center; 
                    TeleportPiece();
                    isPieceSelected = false;
                    RestoreMaterial();
                    MoveController.Instance.ChangeMove();
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
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>(true);

        // ������ ������� ��� ����� ������� ������
        foreach (Renderer renderer in childRenderers)
        {
            renderer.material = highlightMaterial;
        }
    }


    private bool IsPositionOccupied(Vector3 position) 
    {

        Collider[] colliders = Physics.OverlapSphere(position, 0.55f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("ChessPiece"))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsLegalMove(Vector3 Castle_position, Vector3 Cube_position)
    {
        if (!IsOnTheSameLine(Castle_position, Cube_position) || IsPathBlockedForRook(Castle_position, Cube_position))
        {
            string s = (!IsOnTheSameLine(Castle_position, Cube_position)) ? "NotInTheSameLine" : "PathIsBlocked"; Debug.Log(s); return false;
        }
        if (IsPositionOccupied(Cube_position))
        {

            Debug.Log("Position Occupied, getting trying to find it....");
            ChessPieceBase[] allPieces = FindObjectsOfType<ChessPieceBase>();
            foreach (ChessPieceBase piece in allPieces)
            {
                if (ChessCoordinates.PositionToFile(piece.transform.position) == ChessCoordinates.PositionToFile(Cube_position) && ChessCoordinates.PositionToRow(piece.transform.position) == ChessCoordinates.PositionToRow(Cube_position))
                {
                    if (piece.GetTeam() == this.team) return false;
                    Destroy(piece.gameObject);
                    return true;
                }
            }

        }
        return true;

    }

    private bool IsOnTheSameLine(Vector3 Castle_Position, Vector3 Cube_Position)
    {
        int CastleRow = ChessCoordinates.PositionToRow(Castle_Position);
        char CastleFile = ChessCoordinates.PositionToFile(Castle_Position);
        int cubeRow = ChessCoordinates.PositionToRow(Cube_Position);
        char cubeFile = ChessCoordinates.PositionToFile(Cube_Position);

        if ((CastleRow == cubeRow && CastleFile != cubeFile) || (CastleRow != cubeRow && CastleFile == cubeFile))
            return true;

        return false;
    }

    private bool IsPathBlockedForRook(Vector3 start, Vector3 end)
    {
        int startRow = ChessCoordinates.PositionToRow(start);
        char startFile = ChessCoordinates.PositionToFile(start);
        int endRow = ChessCoordinates.PositionToRow(end);
        char endFile = ChessCoordinates.PositionToFile(end);

        int rowStep = (endRow > startRow) ? 1 : (endRow < startRow) ? -1 : 0;
        int fileStep = (endFile > startFile) ? 1 : (endFile < startFile) ? -1 : 0;

        // Перевірте кожну клітину на шляху
        for (int row = startRow + rowStep, file = startFile + fileStep; row != endRow || file != endFile; row += rowStep, file += fileStep)
        {
            Vector3 position = ChessCoordinates.RowFileToPosition(row, (char)file);
            // Перевірте, чи є перешкода на клітині, крім кінцевої позиції
            if (IsPositionOccupied(position) && position != end)
            {
                Debug.Log("Currently checking cube " + ChessCoordinates.PositionToFile(position) + ChessCoordinates.PositionToRow(position));
                return true;
            }
        }

        return false;
    }



    public override string GetTeam()
    {
        return team;
    }
    public override void SetTeam(string Team)
    {
        team = Team;
    }
    public override void RestoreMaterial()
    {
        isPieceSelected = false;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material = originalMaterial;
        }
    }
}
