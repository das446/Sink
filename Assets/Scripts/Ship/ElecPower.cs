using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {

    [System.Serializable]
    public class ElecPower {
        public float curPow;
        public static float min = 0;
        public static float max = 100;
        public PowerBar bar;

        public ElecPower() {
            curPow = 60;

        }

        public void Adjust(int amnt) {
            curPow += amnt;
            bar.update();
        }

        public float percent() {
            return curPow / max;
        }

    }
}