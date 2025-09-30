#include <iostream>
#include <vector>
#include <algorithm>
#include <cstdlib>

using namespace std;

int partition(vector<int>& a, int l, int h) {
    int pvt = a[h];
    int j = l - 1;
    for (int k = l; k < h; ++k) {
        if (a[k] < pvt) {
            ++j;
            swap(a[j], a[k]);
        }
    }
    swap(a[j + 1], a[h]);
    return j + 1;
}

void quickSort(vector<int>& a, int l, int h) {
    if (l < h) {
        int pi = partition(a, l, h);
        quickSort(a, l, pi - 1);
        quickSort(a, pi + 1, h);
    }
}

int main() {
    vector<int> a = {10, 7, 8, 9, 1, 5};
    cout << "El arreglo antes de ordenarlo:\n";
    for (int v : a) cout << v << " ";
    cout << "\n";

    quickSort(a, 0, (int)a.size() - 1);

    cout << "El arreglo despues de ordenarlo:\n";
    for (int v : a) cout << v << " ";
    cout << "\n";
    system("pause");
    return 0;
}
