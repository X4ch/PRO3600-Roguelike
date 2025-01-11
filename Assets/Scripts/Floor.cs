using System;
using System.Collections.Generic;

public class Floor
{
    private int dim;
    public string name;
    public Room[,] floor;
    int floorID;
    public List<Room> roomList;
    private Random rng;

    // Constructeur d'un étage
    public Floor(int dim, string name, int floorID)
    {
        this.dim = dim;
        this.name = name;
        this.floorID = floorID;
        this.floor = new Room[dim, dim];
        this.roomList = new List<Room>();
        this.rng = new Random();
    }
    // Renvoie la dimension de l'étage
    public int getDim()
    {
        return this.dim;
    }

    // Permet d'afficher la matrice qui représente l'étage
    public string toString()
    {
        string str = "";
        str = str + this.name + ": \n";
        for (int i = 0; i < this.dim; i++)
        {
            str += "[";
            for (int j = 0; j < this.dim; j++)
            {
                if (j == this.dim - 1)
                {
                    str = str + this.floor[i, j].type + "] \n";
                }
                else
                {
                    str = str + this.floor[i, j].type + ", ";
                }
            }
        }
        return str;
    }

    // Permet d'afficher la liste des salles présentes dans l'étage
    public string listToString()
    {
        string str = "";
        foreach (Room temp in this.roomList)
        {
            str = str + temp.toString() + "\n";
        }
        return str;
    }

    // Fonction générique permettant d'actualiser les coordonées d'une salle en vue de faire apparaitre une salle à cette endroit
    public (int, int) updateCords((int, int) currentCoords, int direction)
    {
        (int, int) result = (0, 0);
        int testx = currentCoords.Item1;
        int testy = currentCoords.Item2;
        if (direction == 1 && testy - 1 >= 0) { testy -= 1; }
        if (direction == 2 && testx + 1 < dim) { testx += 1; }
        if (direction == 3 && testy + 1 < dim) { testy += 1; }
        if (direction == 4 && testx - 1 >= 0) { testx -= 1; }
        result = (testx, testy);
        return result;
    }

    /* Permet d'initialiser l'étage, en faisant apparaitre une salle de départ
    dans une carrée de 3x3 centré au milieu de la matrice de l'étage */
    private (int, int) initialisation()
    {
        int mid = 0;
        if (this.dim % 2 == 1)
        {
            mid = (this.dim + 1) / 2 - 1;
        }
        else
        {
            mid = this.dim / 2 - 1;
        }

        for (int i = 0; i < this.dim; i++)
        {
            for (int j = 0; j < this.dim; j++)
            {
                Room empty = new Room(floorID, i, j, 0, "empty", -1, "EP");
                floor.SetValue(empty, i, j);
            }
        }

        int offsetx = rng.Next(-1, 2);
        int offsety = rng.Next(-1, 2);
        int x = mid + offsetx;
        int y = mid + offsety;

        Room spawn = new Room(floorID, x, y, 1, "spawn", 0, "VI");
        floor.SetValue(spawn, x, y);

        roomList.Add(spawn);
        spawn.updateNeighbor(this);
        return (x, y);
    }

    // Fonction centrale de cette classe, elle permet de générer un étage complet, cette fonction ce base sur le système de marche alétaoire pour générer les étages
    public void generateFloor(int randomWalkerNumber, int minRoomNumber, int maxRoomNumber)
    {
        /* On commence par faire apparaitre la salle du début de l'étage */
        (int, int) spawnCoords = this.initialisation();

        ((int, int), int)[] randomWalkerList = new ((int, int), int)[randomWalkerNumber];
        for (int i = 0; i < randomWalkerNumber; i++)
        {
            randomWalkerList[i].Item1 = spawnCoords;
        }

        // Tant que le nombre de salle voulue n'est pas atteint, les marcheurs aléatoires continue leurs marches.
        while (roomList.Count < this.rng.Next(minRoomNumber, maxRoomNumber + 1))
        {
            for (int i = 0; i < randomWalkerNumber; i++)
            {
                randomWalkerList[i].Item1 = updateCords(randomWalkerList[i].Item1, rng.Next(1, 5));
                if (floor[randomWalkerList[i].Item1.Item1, randomWalkerList[i].Item1.Item2].type != 0 && floor[randomWalkerList[i].Item1.Item1, randomWalkerList[i].Item1.Item2].distance < randomWalkerList[i].Item2)
                {
                    randomWalkerList[i].Item2 = floor[randomWalkerList[i].Item1.Item1, randomWalkerList[i].Item1.Item2].distance;
                }
                else 
                {
                    randomWalkerList[i].Item2++;
                }

                Console.WriteLine(randomWalkerList[i]);
                if (floor[randomWalkerList[i].Item1.Item1, randomWalkerList[i].Item1.Item2].type == 0)
                {
                    Room tempRoom = new Room(floorID, randomWalkerList[i].Item1.Item1, randomWalkerList[i].Item1.Item2, 2, "default", randomWalkerList[i].Item2, "NS");
                    floor[randomWalkerList[i].Item1.Item1, randomWalkerList[i].Item1.Item2] = tempRoom;
                    roomList.Add(tempRoom);
                }
            }
        }
        generateSpecial();
        updateAllNeighbors();
    }

    // Permet d'obtenir une liste de toutes les salles ayant un seul et unique voisin dans le but de faire apparaitre la salle de fin de l'étage
    private List<Room> getSpecialRoomList()
    {
        List<Room> result = new List<Room>();
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                if (i - 1 < 0 || j - 1 < 0 || i + 1 > dim - 1 || j + 1 > dim - 1) { }
                else
                {
                    if (floor[i, j].type == 0)
                    {
                        int count = 0;
                        if (floor[i, j - 1].type == 1 || floor[i, j - 1].type == 2) { count++; }
                        if (floor[i + 1, j].type == 1 || floor[i + 1, j].type == 2) { count++; }
                        if (floor[i, j + 1].type == 1 || floor[i, j + 1].type == 2) { count++; }
                        if (floor[i - 1, j].type == 1 || floor[i - 1, j].type == 2) { count++; }
                        if (count == 1)
                        {
                            Room tempSpecialRoom = new Room(floorID, i, j, 7, "special", -1, "EP");
                            /* La valeur 7 est utilisé pour indiqué que c'est une salle qui respecte les conditions des salles scpéciales */
                            result.Add(tempSpecialRoom);
                            //floor.SetValue(7, i, j);
                        }
                    }

                }
            }
        }
        return result;
    }

    // Choisit une salle aléatoire dans la liste de salle générer par getSpecialRoomList et fait apparaitre la salle de fin de l'étage
    // Le nombre 3 correspond à une salle de fin d'étage et le nombre 4 correspond à une salle contenant un objet
    private void generateSpecial()
    {
        List<Room> specialList = getSpecialRoomList();
        (int, int) specialRoomLocation = (0, 0);
        while (specialRoomLocation.Item1 == specialRoomLocation.Item2 || areNeighbor(specialRoomLocation, specialList) == true)
        {
            specialRoomLocation = (rng.Next(0, specialList.Count), rng.Next(0, specialList.Count));
        }
        Room bossRoom = new Room(floorID, specialList[specialRoomLocation.Item1].x, specialList[specialRoomLocation.Item1].y, 3, "default", -1, "NS");
        Room itemRoom = new Room(floorID, specialList[specialRoomLocation.Item2].x, specialList[specialRoomLocation.Item2].y, 4, "default", -1, "NS");
        floor[bossRoom.x, bossRoom.y] = bossRoom;
        floor[itemRoom.x, itemRoom.y] = itemRoom;
        roomList.Add(bossRoom);
        roomList.Add(itemRoom);
    }

    private bool areNeighbor((int,int) specialRoomLocation, List<Room> specialList)
    {
        if (specialRoomLocation.Item1 == specialRoomLocation.Item2)
        {
            return true;
        }
        else
        {
            Room room1 = specialList[specialRoomLocation.Item1];
            Room room2 = specialList[specialRoomLocation.Item2];
            if (room1.x == room2.x + 1 || room1.x == room2.x - 1 || room1.y == room2.y + 1 || room1.y == room2.y - 1)
            {
                return true;
            }
            return false;
        }  
    }

    private void updateAllNeighbors()
    {
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                floor[i, j].updateNeighbor(this);
            }
        }
    }

}