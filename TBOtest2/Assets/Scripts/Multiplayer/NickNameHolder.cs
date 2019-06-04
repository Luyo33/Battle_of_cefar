using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickNameHolder : MonoBehaviour
{
    static string nickName = string.Empty;
    public void SetNickName(string nN)
    {
        nickName = nN;
    }
    public string GetNickName()
    {
        return nickName;
    }
}
