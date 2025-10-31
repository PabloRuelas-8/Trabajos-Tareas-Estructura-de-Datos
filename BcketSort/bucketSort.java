import java.util.ArrayList;
import java.util.List;

public class bucketSort {

    public static void insertionSort(List<Float> a) { 
        int n = a.size(); 
        for (int i = 1; i < n; i++) {
            float temp = a.get(i); 
            int j = i - 1;
            while (j >= 0 && temp < a.get(j)) { 
                a.set(j + 1, a.get(j)); 
                j--;
            }
            a.set(j + 1, temp);
        }
    }


    public static void BucketSort(float[] inputArr) {
        int s = inputArr.length;

        List<List<Float>> bucketArr = new ArrayList<>(s);
        for (int i = 0; i < s; i++) {
            bucketArr.add(new ArrayList<Float>());
        }

        for (float j : inputArr) {
            int bi = (int) (s * j);
            bucketArr.get(bi).add(j);
        }

        for (List<Float> bukt : bucketArr) {
            insertionSort(bukt); 
        }

        int idx = 0;
        for (List<Float> bukt : bucketArr) {
            for (float j : bukt) {
                inputArr[idx] = j;
                idx += 1;
            }
        }
    }

    public static void printArray(float[] arr) {
        String[] strings = new String[arr.length];
        for (int i = 0; i < arr.length; i++) {
            strings[i] = String.valueOf(arr[i]);
        }
        System.out.println(String.join(" ", strings));
    }

    public static void main(String[] args) {
        float[] test_array = {0.77f, 0.16f, 0.28f, 0.25f, 0.71f, 0.93f, 0.22f, 0.11f, 0.24f, 0.67f};

        System.out.println("Arreglo antes de ordenar:");
        printArray(test_array);

        BucketSort(test_array);

        System.out.println("Arreglo despues de ordenar:");
        printArray(test_array);
    }
}
