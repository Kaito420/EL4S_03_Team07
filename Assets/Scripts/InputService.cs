using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum AlphabetList
//{
//    Q, W, E, R, T, Y, U, I, O, P,
//     A, S, D, F, G, H, J, K, L,
//      Z, X, C, V, B, N, M,
//};


public class InputService : MonoBehaviour
{
    static readonly KeyCode[] Keys =
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
        KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
        KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
        KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
        KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y,
        KeyCode.Z
    };

    static bool[] keyStates;
    static KeyCode[] pushKeysBuffer;
    static int pushKeyCount;

    void Awake()
    {
        keyStates = new bool[Keys.Length];
        pushKeysBuffer = new KeyCode[Keys.Length];
    }

    void Update()
    {
        pushKeyCount = 0;

        for (int i = 0; i < Keys.Length; i++)
        {
            bool down = Input.GetKeyDown(Keys[i]);
            keyStates[i] = down;

            if (down)
                pushKeysBuffer[pushKeyCount++] = Keys[i];
        }


    }

    /// <summary>
    /// 押しているキーをまとめて取得する
    /// </summary> 
    /// <code>
    /// int count;
    /// var keys = InputService.GetPushKeys(out count);
    /// for (int i = 0; i < count; i++)
    /// {
    ///     KeyCode key = keys[i];
    ///     Debug.Log(key);
    /// }
    /// </code>
    /// </example> 
    /// <param name="count">配列に入っているキーコード数</param>
    /// <returns>押しているキーコードの配列</returns>
    public static KeyCode[] GetPushKeys(out int count)
    {
        count = pushKeyCount;
        return pushKeysBuffer;
    }
}

