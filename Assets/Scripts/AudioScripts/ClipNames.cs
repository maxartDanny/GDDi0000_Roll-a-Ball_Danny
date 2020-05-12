
namespace Audio {

    public static class Enemy {
        public static string HIT => Mortal.HIT_SOUND;

        public const string SHOOT = "ProjectileSound";
    }

    public static class Player {
        public const string HIT = "PlayerHit";

        public const string DASH = "DashSound";

        public const string SLASH = "SlashSound";
    }

    public static class Mortal {
        public const string HIT_SOUND = "HitSound";

        public const string DEFLECT_SOUND = "DeflectSound";

    }

    public static class Game {
        public const string PICKUP_SOUND = "PickupSound";
    }

}