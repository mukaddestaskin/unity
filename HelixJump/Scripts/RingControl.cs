using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingControl : MonoBehaviour
{
    public List<GameObject> pieces;

    public void PrepareSimple(int nonactiveAmount)
    {
        int index = Random.Range(0, pieces.Count);

        for (int i = 0; i < nonactiveAmount; i++)
        {
            var item = pieces[index];
            item.SetActive(false);
            index++;
            if (index >= pieces.Count)
                index = 0;
        }
    }
}
