using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWall : MonoBehaviour
{
    [SerializeField]
   private EXPManager eXPManager;

    private void Update()
    {
        //指定レベルになったらボスエリアに行ける
        if (eXPManager.GetLevel >= 10)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}