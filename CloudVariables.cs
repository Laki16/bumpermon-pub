using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudVariables : MonoBehaviour {

    public static int[] SystemValues { get; set; }

    private void Awake()
    {
        SystemValues = new int[130]; // 3 for system, 17 for characters, 100 for items
    }
}

/* 0: coin
 * 1: gem
 * 2: score
 * 3: golemLv
 * 4: ghostLv
 * 5: santaLv
 * 6: skelLv
 * 7 ~ 19 : empty
 * 20: equipment number
 * 21 ~ 120 : items
 */
