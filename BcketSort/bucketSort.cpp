#include <iostream>
#include <cstdlib>
#include <vector>
#include <string>

void insertionSort(std::vector<float>& a) {
    int n = a.size();
    for (int i = 1; i < n; i++) {
        float temp = a[i]; // float en lugar de int
        int j = i - 1;
        while (j >= 0 && temp < a[j]) {
            a[j + 1] = a[j];
            j--;
        }
        a[j + 1] = temp;
    }
}

void printArr(const std::vector<float>& arr) {
    for (size_t i = 0; i < arr.size(); ++i) {
        std::cout << arr[i] << (i == arr.size() - 1 ? "" : " ");
    }
    std::cout << std::endl;
}

void bucket_sort(std::vector<float>& inputArr) {
    int s = inputArr.size();
    
    std::vector<std::vector<float>> bucketArr(s);

    for (float j : inputArr) {
        int bi = static_cast<int>(s * j);
        bucketArr[bi].push_back(j);
    }

    for (auto& bukt : bucketArr) {
        insertionSort(bukt); 
    }

    int idx = 0;
    for (const auto& bukt : bucketArr) {
        for (float j : bukt) {
            inputArr[idx] = j;
            idx += 1;
        }
    }
}

int main() {
    std::vector<float> test_array = {0.77f, 0.16f, 0.28f, 0.25f, 0.71f, 0.93f, 0.22f, 0.11f, 0.24f, 0.67f};
    
    std::cout << "Arreglo antes de ordenar:" << std::endl;
    printArr(test_array);
    
    bucket_sort(test_array);
    
    std::cout << "Arreglo despues de ordenar:" << std::endl;
    printArr(test_array);
    system("pause");
    return 0;
}
