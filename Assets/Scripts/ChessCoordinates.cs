using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessCoordinates : MonoBehaviour
{
    // Статичний словник для відповідності між літерними і числовими координатами
    public static Dictionary<char, float> fileToPosition = new Dictionary<char, float>()
    {
        {'a', -3.5f},
        {'b', -2.5f},
        {'c', -1.5f},
        {'d', -0.5f},
        {'e',  0.5f},
        {'f',  1.5f},
        {'g',  2.5f},
        {'h',  3.5f}
    };

    public static Dictionary<int, float> rowToPosition = new Dictionary<int, float>()
    {
        {0, -3.5f},
        {1, -2.5f},
        {2, -1.5f},
        {3, -0.5f},
        {4,  0.5f},
        {5,  1.5f},
        {6,  2.5f},
        {7,  3.5f},
    };

    public static Dictionary<int, char> numberToLetterFile = new Dictionary<int, char>()
    {
        {0, 'a'},
        {1, 'b'},
        {2, 'c'},
        {3, 'd'},
        {4, 'e'},
        {5, 'f'},
        {6, 'g'},
        {7, 'h'}
    };

    // Функція для переведення літерних координат в числові
    public static float FileToPosition(char letter)
    {
        if (fileToPosition.ContainsKey(letter))
        {
            return fileToPosition[letter];
        }
        else
        {
            Debug.LogError("Invalid letter coordinate: " + letter);
            return -1;
        }
    }

    public static float RowToPosition(int letter)
    {
        if (rowToPosition.ContainsKey(letter))
        {
            return rowToPosition[letter];
        }
        else
        {
            Debug.LogError("Invalid letter coordinate: " + letter);
            return -1;
        }
    }

    public static Vector3 RowFileToPosition(int row, char file)
    {
        return new Vector3(FileToPosition(file), 1, RowToPosition(row));
    }

    public static char PositionToFile(Vector3 position)
    {
        foreach (KeyValuePair<char, float> entry in fileToPosition)
        {
            float roundedX = Mathf.Round(position.x * 100) / 100f;
            if (Mathf.Approximately(entry.Value, roundedX))
            {
                return entry.Key;
            }
        }
        Debug.LogError("Invalid float (x, file) coordinate: " + position.x);
        return '0';
    }

    public static int PositionToRow(Vector3 position)
    {
        foreach (KeyValuePair<int, float> entry in rowToPosition)
        {
            float roundedZ = Mathf.Round(position.z * 100) / 100f; // Округлюємо до двох знаків після коми
            if (Mathf.Approximately(roundedZ, entry.Value))
            {
                return entry.Key;
            }
        }
        Debug.LogError("Invalid float (z, row) coordinate: " + position.z);
        return 0;
    }
}
