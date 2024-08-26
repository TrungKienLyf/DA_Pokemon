using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDB : MonoBehaviour
{
    static Dictionary<string, MoveBase> moves;

    public static void Init()
    {
        moves = new Dictionary<string, MoveBase>();

        var moveList = Resources.LoadAll<MoveBase>("");
        foreach (var move in moveList)
        {
            if (moves.ContainsKey(move.Name))
            {
                Debug.LogError($"Có 2 kỹ năng có tên {move.Name} trong CSDL");
                continue;
            }

            moves[move.Name] = move;
        }
    }

    public static MoveBase GetMoveByName(string name)
    {
        if (!moves.ContainsKey(name))
        {
            Debug.LogError($"Kỹ năng có tên {name} không tìm thấy trong CSDL");
            return null;
        }

        return moves[name];
    }
}
