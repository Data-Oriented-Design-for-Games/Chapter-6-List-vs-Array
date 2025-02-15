using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace DOD_BOOK
{

    struct StructExample
    {
        public int TestInt;

        public StructExample(int testInt)
        {
            TestInt = testInt;
        }
    }

    public class Main : MonoBehaviour
    {
        public TextMeshProUGUI ResultText;
        public TextMeshProUGUI ButtonText;
        public int NumIterations = 10000;
        public int ArraySize = 10000;

        public GameObject UI;

        int m_runTest = 0;

        private void Awake()
        {
            ResultText.text = "Ready\n";
        }

        private void Start()
        {
            if (Screen.width > Screen.height)
                UI.GetComponent<CanvasScaler>().matchWidthOrHeight = 1.0f;
            else
                UI.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.0f;
        }

        public void RunTest()
        {
            m_runTest = 1;
        }

        private void Update()
        {
            if (m_runTest == 1)
            {
                ResultText.text = "Running\n";
                m_runTest++;
            }
            else if (m_runTest == 2)
            {
                ResultText.text = "Test 1 of 2: \n";

                List<int> IntList = new List<int>(ArraySize);
                int[] IntArray = new int[ArraySize];

                for (int i = 0; i < ArraySize; i++)
                {
                    IntList.Add(Mathf.FloorToInt(UnityEngine.Random.value * 1024));
                    IntArray[i] = Mathf.FloorToInt(UnityEngine.Random.value * 1024);
                }

                Double listTime = 0d;
                Double arrayTime = 0d;

                for (int t = 0; t < NumIterations; t++)
                {
                    double time = 0d;

                    time = Time.realtimeSinceStartupAsDouble;
                    for (int i = 0; i < ArraySize; i++)
                        IntList[i]++;
                    listTime += Time.realtimeSinceStartupAsDouble - time;

                    time = Time.realtimeSinceStartupAsDouble;
                    for (int i = 0; i < ArraySize; i++)
                        IntArray[i]++;
                    arrayTime += Time.realtimeSinceStartupAsDouble - time;
                }

                ResultText.text += "List Increment " + listTime.ToString("G0") + "\n";
                ResultText.text += "Array Increment " + arrayTime.ToString("G0") + "\n";
                if (listTime > arrayTime)
                    ResultText.text += "Array is faster than List by " + (listTime / arrayTime).ToString("G0") + "\n";
                else
                    ResultText.text += "Array is slower than List by " + (listTime / arrayTime).ToString("G0") + "\n";

                m_runTest++;
            }
            else if (m_runTest == 3)
            {
                ResultText.text += "\nTest 2 of 2: \n";

                List<StructExample> IntList1 = new List<StructExample>(ArraySize);
                List<StructExample> IntList2 = new List<StructExample>(ArraySize);
                List<StructExample> IntList3 = new List<StructExample>(ArraySize);
                StructExample[] IntArray1 = new StructExample[ArraySize];
                StructExample[] IntArray2 = new StructExample[ArraySize];
                StructExample[] IntArray3 = new StructExample[ArraySize];

                for (int i = 0; i < ArraySize; i++)
                {
                    IntList1.Add(new StructExample(Mathf.FloorToInt(UnityEngine.Random.value * 1024)));
                    IntList2.Add(new StructExample(Mathf.FloorToInt(UnityEngine.Random.value * 1024)));
                    IntList3.Add(new StructExample(0));
                    IntArray1[i] = new StructExample(Mathf.FloorToInt(UnityEngine.Random.value * 1024));
                    IntArray2[i] = new StructExample(Mathf.FloorToInt(UnityEngine.Random.value * 1024));
                    IntArray3[i] = new StructExample(0);
                }

                Double listTime = 0d;
                Double arrayTime = 0d;

                for (int t = 0; t < NumIterations; t++)
                {
                    double time = 0d;

                    time = Time.realtimeSinceStartupAsDouble;
                    for (int i = 0; i < ArraySize; i++)
                        IntList3[i] = new StructExample(IntList1[i].TestInt + IntList2[i].TestInt);
                    listTime += Time.realtimeSinceStartupAsDouble - time;

                    time = Time.realtimeSinceStartupAsDouble;
                    for (int i = 0; i < ArraySize; i++)
                        IntArray3[i].TestInt = IntArray1[i].TestInt + IntArray2[i].TestInt;
                    arrayTime += Time.realtimeSinceStartupAsDouble - time;
                }

                ResultText.text += "List Addition " + listTime.ToString("G0") + "\n";
                ResultText.text += "Array Addition " + arrayTime.ToString("G0") + "\n";
                if (listTime > arrayTime)
                    ResultText.text += "Array is faster than List by " + (listTime / arrayTime).ToString("G0") + "\n";
                else
                    ResultText.text += "Array is slower than List by " + (listTime / arrayTime).ToString("G0") + "\n";

                m_runTest++;
            }
        }
    }
}