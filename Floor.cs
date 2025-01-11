using System;

public class Floor {
    private int dim;
    public string name;
    public int[,] floor;
    int floorID;
    public List<Room> roomList;

    public Floor(int dim, string name, int floorID) {
        this.dim = dim;
        this.name = name;
        this.floorID = floorID;
        this.floor = new int[dim,dim];
        this.roomList = new List<Room>();
    }

    public int getDim() {
        return this.dim;
    }

    public string toString() {
        string str = "";
        str = str + this.name + ": \n";
        for (int i = 0; i < this.dim; i++) {
            str+= "[";
            for (int j = 0; j < this.dim; j++) {
                if (j == this.dim -1) {
                    str = str + this.floor.GetValue(i,j) + "] \n";
                }
                else {
                    str = str + this.floor.GetValue(i,j) + ", ";
                 }   
            }
        }
        return str;
    }

    public string listToString() {
        string str = "";
        foreach (Room temp in this.roomList) {
            str = str + temp.toString() + "\n";
        }
        return str;
    }

    private bool roomSpawnable(Room room, int direction) {
        int x = updateCords(room, direction)[0];
        int y = updateCords(room, direction)[1];
        if (x < 0 || y < 0 || x > dim - 1 || y > dim - 1) {
            return false;
        }
        else if (floor[x,y] != 0) {
            return false;
        }
        return true;
    }

    private int[] updateCords(Room room, int direction) {
        int[] result = new int[2];
        int testx = room.x;
        int testy = room.y;
        if (direction == 1) {testy -= 1;}
        if (direction == 2) {testx += 1;}
        if (direction == 3) {testy += 1;}
        if (direction == 4) {testx -= 1;}
        result[0] = testx;
        result[1] = testy;
        return result;
    }

    private void initialisation() {
        int mid = 0;
        if (this.dim % 2 == 1) {
            mid = (this.dim+1)/2 - 1;
        }
        else {
            mid = this.dim/2 - 1;
        }

        Random rng = new Random();
        int offsetx = rng.Next(-1, 2);
        int offsety = rng.Next(-1, 2);
        int x = mid + offsetx;
        int y = mid + offsety;
        floor.SetValue(1, x, y);

        Room spawn = new Room(floorID, x, y, 1, "spawn");
        roomList.Add(spawn);
        spawn.updateNeighbor(this);
    }

    private void generateRoom(Room room, int direction) {
        Random rng = new Random();

        int x = updateCords(room, direction)[0];
        int y = updateCords(room, direction)[1];
        floor.SetValue(2, x, y);

        Room newRoom = new Room(floorID, x, y, 1, "2_default");
        roomList.Add(newRoom);
        newRoom.updateNeighbor(this);
    }

    public void generateFloor(double roomChance, double decrease, int minRoomNumber) {
        this.initialisation();
        Random rng = new Random();
        Pile<Room> queue = new Pile<Room>();

        for (int i = 1; i < 5; i++) {
            if (rng.Next(0, 100) * 0.01 < roomChance*roomList[0].getScore()) {
                generateRoom(roomList[0], i);
                queue.push(roomList[roomList.Count() - 1]);
            }
        }

        while (queue.isEmpty() == false) {
            Room tempRoom = queue.pop();
            if (rng.Next(0, 100) * 0.01 < roomChance*tempRoom.getScore()) {
                int dir = rng.Next(1,5);
                if (roomSpawnable(tempRoom, dir)) {
                    generateRoom(tempRoom, dir);
                    queue.push(roomList[roomList.Count() - 1]);
                    roomChance = roomChance*decrease;
                }
            }
        }

        List<Room> specialList = this.getSpecialRoomList();
        if (roomList.Count() < minRoomNumber || specialList.Count() < 2) {
            Console.WriteLine("L'étage n'a pas assez de salle");
            this.reset();
            this.generateFloor(roomChance, decrease, minRoomNumber);
        }
        else {
            Console.WriteLine("~ UwU ~");
            this.generateSpecial(specialList);
        }
    }

    private void reset() {
        for (int i = 0; i < dim; i++ ) {
            for (int j = 0; j < dim; j++) {
                floor[i,j] = 0;
            }
        }
        roomList = new List<Room>();
    }

    private List<Room> getSpecialRoomList() {
        List<Room> result = new List<Room>();
        for (int i = 0; i < dim; i++) {
            for (int j = 0; j < dim; j++) {
                if (i-1 < 0 || j-1 < 0 || i+1 > dim - 1 || j+1 > dim - 1) {}
                else {
                    if (floor[i,j] == 0) {
                        int count = 0;
                        if (floor[i, j - 1] == 1 || floor[i, j - 1] == 2) {count++;}
                        if (floor[i + 1, j] == 1 || floor[i + 1, j] == 2) {count++;}
                        if (floor[i, j + 1] == 1 || floor[i, j + 1] == 2) {count++;}
                        if (floor[i - 1, j] == 1 || floor[i - 1, j] == 2) {count++;}
                        if (count == 1) {
                            Room tempSpecialRoom = new Room(floorID, i, j, 7, "special");
                            result.Add(tempSpecialRoom);
                            floor.SetValue(7, i, j);
                        }
                    }
                    
                }
            }
        }
        return result;
    }

    public void generateSpecial(List<Room> list) {
        Random rng = new Random();
        int index = rng.Next(0, list.Count());
        Room tempRoom = list[index];
        Room special = new Room(floorID, tempRoom.x, tempRoom.y, 3, "special");
        roomList.Add(special);
        floor.SetValue(3, tempRoom.x, tempRoom.y);
    }
    
}