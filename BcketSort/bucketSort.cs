using System;
using System.Collections.Generic; 
public class BucketSort
{
    public static void InsertionSort(List<float> a) 
    {
        int n = a.Count; 
        for (int i = 1; i < n; i++)
        {
            float temp = a[i]; 
            int j = i - 1;
            while (j >= 0 && temp < a[j])
            {
                a[j + 1] = a[j];
                j--;
            }
            a[j + 1] = temp;
        }
    }

    public static void DoBucketSort(float[] inputArr)
    {
        int s = inputArr.Length;
        
        List<List<float>> bucketArr = new List<List<float>>(s);
        for (int i = 0; i < s; i++)
        {
            bucketArr.Add(new List<float>());
        }

        foreach (float j in inputArr)
        {
            int bi = (int)(s * j);
            bucketArr[bi].Add(j);
        }

        foreach (List<float> bukt in bucketArr)
        {
            InsertionSort(bukt); 
        }

        int idx = 0;
        foreach (List<float> bukt in bucketArr)
        {
            foreach (float j in bukt)
            {
                inputArr[idx] = j;
                idx += 1;
            }
        }
    }

    public static void Main(string[] args)
    {
        float[] test_array = { 0.77f, 0.16f, 0.28f, 0.25f, 0.71f, 0.93f, 0.22f, 0.11f, 0.24f, 0.67f };

        Console.WriteLine("Arreglo antes de ordenar:");
        Console.WriteLine(string.Join(" ", test_array));

        DoBucketSort(test_array);

        Console.WriteLine("Arreglo despues de ordenar:");
        Console.WriteLine(string.Join(" ", test_array));
    }
}
