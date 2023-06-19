namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a Slimy gel monster in the game.
    /// </summary>
    public class GelatinousCube : Monster, IMoveable
    {
        private Location _location;

        public GelatinousCube(Location location)
        {
            _location = location;
        }

        public Location GetLocation()
        {
            return _location;
        }
        public override void Activate(Hero hero, Map map)
        {
            ConsoleHelper.WriteLine("Exiting text", ConsoleColor.Red);
            hero.Kill("You got gooed!");
        }
        public void Move(Hero hero, Map map)
        {
            var potentialLocation = new Location(_location.Row + 1, _location.Column);
            //If the potential location is valid (on the map & room location is inactive)
            if (potentialLocation.Row < map.Rows && !map.GetRoomAtLocation(potentialLocation).IsActive)
            {
                //Then do our previous logic, otherwise do nothing
                map.GetRoomAtLocation(_location).RemoveMonster();
                _location = new Location(_location.Row + 1, _location.Column);
                map.GetRoomAtLocation(_location).AddMonster(this);
            }
        }

        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            return false;
        }

        public override DisplayDetails Display()
        {
            return new DisplayDetails("[G]", ConsoleColor.Green);
        }
    }
}