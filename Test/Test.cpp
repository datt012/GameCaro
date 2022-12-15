#include <iostream>
#include<string>

int main()
{
    std::string s = "scott tiger mushroom ";
    std::string delimiter = " ";

    size_t pos = 0;
    std::string token;
    while ((pos = s.find(delimiter)) != std::string::npos) {
        token = s.substr(0, pos);
        std::cout << token;
        s.erase(0, pos + delimiter.length());
    }
}

