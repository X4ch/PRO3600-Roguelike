using System;

class Program {
    static void Main(string[] args) {
        
        Floor floor = new Floor(13, "Basement", 0);
        floor.generateFloor(1, 0.99, 7);

        Console.WriteLine(floor.toString());

        /*List<Room> test = new List<Room>();
        Console.WriteLine(test.Count());*/


        
        /*Room room = new Room(0, 1, 1, 2, "default");
        room.neighbor[0] = true;
        room.neighbor[1] = true;
        Console.WriteLine(room.getScore());*/
        
        /*List<Room> list = new List<Room>();
        Room room1 = new Room(0, 1, 3, 2, "default");
        Room room2 = new Room(0, 5, 2, 2, "default");
        Room room3 = new Room(0, 4, 0, 2, "default");
        Room room4 = new Room(0, 2, 6, 2, "default");

        list.Add(room1);
        list.Add(room2);
        list.Add(room3);
        list.Add(room4);

        int i = 0;

        foreach(Room temp in list) {
            if (temp.x == 5) {
                i++;
            }
            else {
                Console.WriteLine(list[i].toString());
                i++;
            }
        }*/
    }
}

