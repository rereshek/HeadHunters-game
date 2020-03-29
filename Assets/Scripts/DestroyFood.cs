using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFood : MonoBehaviour
{
   public void BinFood ()
    {
        Destroy(this.gameObject);
    }
    public void unparent()
    {
        Vector3 dropPos = this.gameObject.transform.position;
        this.gameObject.transform.parent = null;
        this.gameObject.transform.position = dropPos;
    }
}
