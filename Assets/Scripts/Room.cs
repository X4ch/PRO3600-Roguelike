using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Room
{
    int floorID;
    public int x;
    public int y;
    public int type;
    string subtype;
    public bool[] neighbor;
    public int distance;
    public string roomStatus;

    // Constructeur d'une salle
    // Les voisins sont dans l'orde suivant : [haut, droite, bas, gauche]
    public Room(int floorID, int x, int y, int type, string subtype, int distance, string roomStatus)
    {
        this.floorID = floorID;
        this.x = x;
        this.y = y;
        this.type = type;
        this.subtype = subtype;
        bool[] portes = new bool[] { false, false, false, false };
        this.neighbor = portes;
        this.distance = distance;
        this.roomStatus = roomStatus;
    }

    public string toString()
    {
        string str = "";
        str = "FloorId: " + this.floorID + ", Type: " + type + ", Sub " + subtype + ", X: " + this.x + ", Y: " + this.y;
        return str;
    }

    // Affiche la liste des voisins d'une salle
    public string neighborToString()
    {
        string str = "X: " + this.x + ", Y: " + this.y + "\n";
        string[] list = { "Haut: ", "Droite: ", "Bas: ", "Gauche: " };
        for (int i = 0; i < 4; i++)
        {
            str = str + list[i] + this.neighbor[i] + "\n";
        }
        return str;
    }

    // Permet de mettre à jour la liste des voisins d'une salle spécifiée, cette fonction est utilisé après avoir généré de nouvelle salles
    public void updateNeighbor(Floor floor)
    {
        int dim = floor.getDim();
        foreach (Room temp in floor.roomList)
        {
            // Cas 1
            if (temp.x - 1 < 0 && temp.y - 1 < 0)
            {
                if (floor.floor[temp.x + 1, temp.y].type != 0) { temp.neighbor[2] = true; }
                if (floor.floor[temp.x, temp.y + 1].type != 0) { temp.neighbor[1] = true; }
            }
            // Cas 2
            else if (temp.x + 1 < 0 && temp.y - 1 > dim - 1)
            {
                if (floor.floor[temp.x - 1, temp.y].type != 0) { temp.neighbor[0] = true; }
                if (floor.floor[temp.x, temp.y + 1].type != 0) { temp.neighbor[1] = true; }
            }
            // Cas 3
            else if (temp.x + 1 > dim - 1 && temp.y + 1 > dim - 1)
            {
                if (floor.floor[temp.x - 1, temp.y].type != 0) { temp.neighbor[0] = true; }
                if (floor.floor[temp.x, temp.y - 1].type != 0) { temp.neighbor[3] = true; }
            }
            // Cas 4
            else if (temp.x - 1 > dim - 1 && temp.y + 1 < 0)
            {
                if (floor.floor[temp.x + 1, temp.y].type != 0) { temp.neighbor[2] = true; }
                if (floor.floor[temp.x, temp.y - 1].type != 0) { temp.neighbor[3] = true; }
            }
            // Cas 5
            else if (temp.y - 1 < 0)
            {
                if (floor.floor[temp.x + 1, temp.y].type != 0) { temp.neighbor[2] = true; }
                if (floor.floor[temp.x - 1, temp.y].type != 0) { temp.neighbor[0] = true; }
                if (floor.floor[temp.x, temp.y + 1].type != 0) { temp.neighbor[1] = true; }
            }
            // Cas 6
            else if (temp.x + 1 > dim - 1)
            {
                if (floor.floor[temp.x - 1, temp.y].type != 0) { temp.neighbor[0] = true; }
                if (floor.floor[temp.x, temp.y + 1].type != 0) { temp.neighbor[1] = true; }
                if (floor.floor[temp.x, temp.y - 1].type != 0) { temp.neighbor[3] = true; }
            }
            // Cas 7
            else if (temp.y + 1 > dim - 1)
            {
                if (floor.floor[temp.x + 1, temp.y].type != 0) { temp.neighbor[2] = true; }
                if (floor.floor[temp.x - 1, temp.y].type != 0) { temp.neighbor[0] = true; }
                if (floor.floor[temp.x, temp.y - 1].type != 0) { temp.neighbor[3] = true; }
            }
            // Cas 8
            else if (temp.x - 1 < 0)
            {
                if (floor.floor[temp.x + 1, temp.y].type != 0) { temp.neighbor[2] = true; }
                if (floor.floor[temp.x, temp.y + 1].type != 0) { temp.neighbor[1] = true; }
                if (floor.floor[temp.x, temp.y - 1].type != 0) { temp.neighbor[3] = true; }
            }
            // Cas général
            else
            {
                if (floor.floor[temp.x + 1, temp.y].type != 0) { temp.neighbor[2] = true; }
                if (floor.floor[temp.x - 1, temp.y].type != 0) { temp.neighbor[0] = true; }
                if (floor.floor[temp.x, temp.y + 1].type != 0) { temp.neighbor[1] = true; }
                if (floor.floor[temp.x, temp.y - 1].type != 0) { temp.neighbor[3] = true; }
            }
        }
    }

    // Permet de renvoyer le nombre de voisin d'une salle
    public int numberOfNeighbor()
    {
        int result = 0;
        for (int i = 0; i < 4; i++)
        {
            if (neighbor[i] == true)
            {
                result++;
            }
        }
        return result;
    }
}