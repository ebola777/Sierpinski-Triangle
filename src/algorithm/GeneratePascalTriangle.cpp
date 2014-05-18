#include <iostream>
#include <iomanip>
#include <cmath>
using namespace std;

#define SIZE    100
#define SETW    3

int P[SIZE];

/** P 2D structure
 *
 * Index of P if from left to right, top to down
 *
 * ┌────────┬─┬─┬──┬──┬───┬──────────────┐
 * │line\col│0│1│2 │3 │dec│index of group│
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * │0       │1│ │  │  │-0 │0             │
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * │1       │1│ │  │  │-1 │              │
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * │2       │1│2│  │  │-1 │1             │
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * │3       │1│3│  │  │-2 │              │
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * │4       │1│4│6 │  │-2 │2             │
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * │5       │1│5│10│  │-3 │              │
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * │6       │1│6│15│20│-3 │3             │
 * ├────────┼─┼─┼──┼──┼───┼──────────────┤
 * │7       │1│7│21│35│-4 │              │
 * └────────┴─┴─┴──┴──┴───┴──────────────┘
 */

/** Sorted by index of P
 *
 * ┌──────────┬─────┬───┬────┬─────────────┬────────────────┬─────────┬──────┬────────┐
 * │index of P│value│col│line│lowerLineHalf│lowerLineHalfInc│lowerLine│ofsInd│ofsGroup│
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │0         │1    │0  │0   │0            │1               │0        │0     │0       │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │1         │1    │0  │1   │             │                │         │1     │1       │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │2         │1    │0  │2   │1            │2               │2        │0     │0       │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │3         │2    │1  │2   │             │                │         │1     │        │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │4         │1    │0  │3   │             │                │         │2     │1       │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │5         │3    │1  │3   │             │                │         │3     │        │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │6         │1    │0  │4   │2            │3               │4        │0     │0       │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │7         │4    │1  │4   │             │                │         │1     │        │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │8         │6    │2  │4   │             │                │         │2     │        │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │9         │1    │0  │5   │             │                │         │3     │1       │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │10        │5    │1  │5   │             │                │         │4     │        │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │11        │10   │2  │5   │             │                │         │5     │        │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │12        │1    │0  │6   │3            │4               │6        │0     │0       │
 * ├──────────┼─────┼───┼────┼─────────────┼────────────────┼─────────┼──────┼────────┤
 * │...       │     │   │    │             │                │         │      │        │
 * └──────────┴─────┴───┴────┴─────────────┴────────────────┴─────────┴──────┴────────┘
 */

class POS {
public:
    POS() { }

    POS(int vcol, int vline) {
        col = vcol;
        line = vline;
    }

    int col;
    int line;
};

POS getPos(int ind) {
    int lower, lowerLine, lowerLineHalf, lowerLineHalfInc, ofsInd, ofsGroup;
    int col, line;

    lowerLineHalf = floor(sqrt(ind + 0.25) - 0.5);
    lowerLine = lowerLineHalf * 2;
    lowerLineHalfInc = lowerLineHalf + 1;

    lower = lowerLineHalf * lowerLineHalfInc;
    ofsInd = ind - lower;
    ofsGroup = ofsInd / lowerLineHalfInc;
    col = ofsInd - lowerLineHalfInc * ofsGroup;
    line = lowerLine + ofsGroup;

    return POS(col, line);
}

int getLower(int ind) {
    return ind * (ind + 1);
}

/// \brief Fill number into each group of two lines
///
/// \param int beginLine    Beginning index of group
/// \param int endLine      Ending index of group
///
void genLines(int beginLine, int endLine) {
    POS pos;
    int ind, indEnd;
    int col, line, dec;

    // convert line group number to index
    ind = getLower(beginLine);
    indEnd = getLower(endLine);

    // convert index to 2d position
    pos = getPos(ind);

    col = pos.col;
    line = pos.line;

    if (ind <= 1) {
        P[0] = 1;
        P[1] = 1;

        col = 0;
        line = 2;
        ind = 2;
    }

    dec = (line + 1) / 2;
    while (ind != indEnd) {
        // even line
        P[ind++] = 1;
        col = 1;

        for ( ; col < line / 2; ++col) {
            P[ind] = P[ind - dec - 1] + P[ind - dec];
            ++ind;
        }

        P[ind] = 2 * P[ind - dec - 1];
        ++ind;

        // entering next line
        ++line;
        ++dec;

        // odd line
        P[ind++] = 1;
        col = 1;

        for ( ; col <= line / 2; ++col) {
            P[ind] = P[ind - dec - 1] + P[ind - dec];
            ++ind;
        }

        // entering next line
        ++line;
    }
}

/// \brief Print data
///
/// \param int endLine      Ending index of group
///
void printData(int endLine) {
    int ind = 0, indEnd;
    int col, line = 0;

    indEnd = getLower(endLine);

    while (ind != indEnd) {
        // even line
        col = 0;
        for ( ; col <= line / 2; ++col) {
            cout << setw(SETW) << P[ind + col] << " ";
        }

        col -= 2;
        for ( ; col >= 0; --col) {
            cout << setw(SETW) << P[ind + col] << " ";
        }

        // entering next line
        cout << endl;
        ind += (line / 2) + 1;
        ++line;

        // odd line
        col = 0;
        for ( ; col <= line / 2; ++col) {
            cout << setw(SETW) << P[ind + col] << " ";
        }

        --col;
        for ( ; col >= 0; --col) {
            cout << setw(SETW) << P[ind + col] << " ";
        }

        // entering next line
        cout << endl;
        ind += (line / 2) + 1;
        ++line;
    }
}

int main() {
    P[0] = 1;
    P[1] = 1;
    P[2] = 1;
    P[3] = 2;
    P[4] = 1;
    P[5] = 3;

    // P[6] comes first in group 2 (index of group)

    // generate groups from 2 to 5
    genLines(2, 5);

    // print data from 0 to 5
    printData(5);

    return 0;
}
