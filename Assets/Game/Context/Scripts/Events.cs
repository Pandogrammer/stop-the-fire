namespace Game.Context.Scripts
{
    public static class Events
    {
        public static Defeat Defeat()
        {
            return new Defeat();
        }
    }

    public class Defeat
    {
    }
}