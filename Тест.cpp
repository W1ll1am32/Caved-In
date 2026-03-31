# include <iostream>
# include <cmath>
using namespace std;


int main(){
    for(int i=1; i<=2020; i++)
        for(int j=2003; j<=2020; j++)
            if(pow(i,j) + pow(j,i) == 2019) cout << i << " " << j << endl;
    return 0;
}
