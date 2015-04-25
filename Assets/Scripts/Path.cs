using UnityEngine;
using System.Collections.Generic;

public static class Path
{
    public static class Bullets
    {
        public enum ID
        {
            Ballet3Way = 1,
            Homing2Way = 2,
            MachineGun = 3,
            Homing2WayUp = 4
        }

        private const string path = "Prefabs/Weapon/";
        private const string Ballet3Way  = "Ballet3Way";
        private const string Homing2Way  = "Homing2Way";
        private const string MachineGun  = "MachineGun";
        private const string Homing2WayUp  = "Homing2WayUp";

        public static string GetPathByID(ID id)
        {
            switch (id) {
            case ID.Ballet3Way:
                return path + Ballet3Way;
            case ID.Homing2Way:
                return path + Homing2Way;
            case ID.MachineGun:
                return path + MachineGun;
            case ID.Homing2WayUp:
                return path + Homing2WayUp;
            }

            Debug.LogError ("Path.Bullets.GetPathByID Error");
            return "";
        }
    }
}
