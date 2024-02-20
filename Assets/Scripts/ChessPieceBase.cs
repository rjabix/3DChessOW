using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPieceBase : MonoBehaviour
{
    // Метод, який повертає команду фігури
    public abstract string GetTeam();

    public abstract void SetTeam(string Team);

    public abstract void RestoreMaterial();

    public void DeselectAllPieces()
    {
        ChessPieceBase[] allPieces = FindObjectsOfType<ChessPieceBase>();
        foreach (ChessPieceBase piece in allPieces)
        {
            piece.RestoreMaterial();
        }
    }
}
