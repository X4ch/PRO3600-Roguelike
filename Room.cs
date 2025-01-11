using System;

public class Room {
    int floorID;
    public int x;
    public int y;
    int type;
    string subtype;
    public bool[] neighbor;

    public Room(int floorID, int x, int y, int type, string subtype) {
        this.floorID = floorID;
        this.x = x;
        this.y = y;
        this.type = type;
        this.subtype = subtype;
        bool[] portes = new bool[] {false, false, false, false};
        this.neighbor = portes;
    }

    public string toString() {
        string str = "";
        str = "FloorId: " + this.floorID + ", Type: " + type + ", Sub " + subtype + ", X: " + this.x + ", Y: " + this.y;
        return str;
    }

    public string neighborToString() {
        string str= "";
        string[] list = {"Haut: ", "Droite: ", "Bas: ", "Gauche: "};
        for (int i = 0; i < 4; i++) {
            str = str + list[i] + this.neighbor[i] + "\n";
        }
        return str;
    }

    public void updateNeighbor(Floor floor) {
        foreach (Room temp in floor.roomList) {
            if (temp.x-1 < 0 || temp.y-1 < 0 || temp.x+1 > floor.getDim() - 1 || temp.y+1 > floor.getDim() - 1) {}
            else {
                if (floor.floor[temp.x, temp.y - 1] !=0) {temp.neighbor[0] = true;}
                if (floor.floor[temp.x + 1, temp.y] !=0) {temp.neighbor[1] = true;}
                if (floor.floor[temp.x, temp.y + 1] !=0) {temp.neighbor[2] = true;}
                if (floor.floor[temp.x - 1, temp.y] !=0) {temp.neighbor[3] = true;}
            }
        }
    }

    public int getScore() {
        int result = 0;
        for (int i = 0; i < 4; i++) {
            if (neighbor[i]) {
                result++;
            }
        }
        if (result != 0) {return 1 / result;}
        else {return 1;}
        
    }
}