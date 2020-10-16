using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace MyUtility
{
    public static class Utility
    {
        public static Vector3 GetRandomDirection() => new Vector3(UnityEngine.Random.Range(-1, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized; // from CodeMonkey

        public static void AddInverse(int x) { x += (x * -1); }

        public static float CavityShape(float x) => x * x - 1f;
        public static float GetRimSteepness(float x, float rimWidth, float rimSteepness)
        {
            x = math.abs(x) - 1 - rimWidth;
            return rimSteepness * x * x;
        }

        public static float SmoothMin(float a, float b, float k)
        {
            float h = Mathf.Clamp01((b - a + k) / (2 * k));
            return a * h + b * (1 - h) - k * h * (1 - h);
        }

        public static float BiasFunction(float x, float bias)
        {
            float k = math.pow(1 - bias, 3);
            return (x * k) / (x * k - x + 1);
        }

        // A - B => -1(A - B) => B - A 

        public static void ParabolicTranslate(Transform objectToMove, float a, float b, float c, float speed, float delay, bool risingParabola, bool findVertexOfParabola)
        {
            if (Mathf.Sign(a) == 1f && risingParabola)
                a *= -1f; // so the parabola always goes up

            Vector2 currentposition = objectToMove.position;
            Vector2 endingPosition = new Vector2(5f, objectToMove.position.y); // hardcoded. You should resolve the parabola to know ending position

            while (currentposition.x < endingPosition.x && delay > 0f)
            {
                currentposition.y = a * currentposition.x * currentposition.x + b * currentposition.x + c;

                currentposition += new Vector2(currentposition.x + Time.fixedDeltaTime, currentposition.y) * speed;
                delay -= Time.fixedDeltaTime;

                objectToMove.Translate(currentposition, Space.World);
            }

            if (findVertexOfParabola)
            {
                float temp = Mathf.Abs(currentposition.x - endingPosition.x) * 0.5f;
                currentposition.y = a * temp * temp + b * temp + c;

                Debug.Log($"vertex of parabola is at Vector2({temp}, {currentposition.y}).");
            }
        }

        public static List<int> FindFactors(int numToFactor, bool debugResult)
        {
            List<int> resultList = new List<int>();
            int[] primesArray = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
                                            103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211,
                                            223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337,
                                            347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461,
                                            463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601,
                                            607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739,
                                            743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881,
                                            883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997
            };
            int numInLastPosition = primesArray[primesArray.Length - 1];

            if (numToFactor > numInLastPosition || numToFactor < 0)
            {
                Debug.LogWarning($"number must be smaller or equal to {numInLastPosition} and non-negative. Your number is {numToFactor}");
                return resultList;
            }

            for (int i = 0; i < primesArray.Length; i++)
            {
                while (numToFactor % primesArray[i] == 0 && numToFactor >= primesArray[i])
                {
                    int temp = numToFactor / primesArray[i];

                    resultList.Add(primesArray[i]);
                    numToFactor = temp;
                }
            }

            if (debugResult)
            {
                foreach (int item in resultList)
                {
                    Debug.Log($"added number is {item}");
                }
            }

            return resultList;
        }
    }

    namespace MyUtility.Checks
    {
        public static class Checks
        {
            public static bool ValueIsUnderTolerance(float value, float target, float tolerance) => Mathf.Abs(value - target) < tolerance;
            public static bool ValueIsBetweenMinAndMax(float value, float min, float max) => (value > (min + Mathf.Epsilon)) && (value < (max - Mathf.Epsilon));
        }
    }

    public class States
    {
        public IEnumerator Cooldown(bool value, float delay)
        {
            value = false;
            yield return new WaitForSeconds(delay);
            value = true;
        }
    }

}

