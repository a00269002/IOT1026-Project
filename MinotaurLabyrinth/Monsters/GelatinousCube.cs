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
            var heroLocation = hero.Location;
            int dx = heroLocation.Column - _location.Column;
            int dy = heroLocation.Row - _location.Row;

            Location? newLocation;
            if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
            {
                newLocation = SwapLocation(map, _location, heroLocation);
                Activate(hero, map);
            }
            //The gel is not adjacent to the player
            else
            {
                //If the gel is closer in the y direction--> lets move it closer in tge x direction
                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    if (dx > 0)
                    {
                        var swapLocation = new Location(_location.Row, _location.Column + 1);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                    else
                    {
                        var swapLocation = new Location(_location.Row, _location.Column - 1);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                }
                else
                {
                    if (dy > 0)
                    {
                        var swapLocation = new Location(_location.Row + 1, _location.Column);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                    else
                    {
                        var swapLocation = new Location(_location.Row - 1, _location.Column);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                }
                //This means the gel was not able to move
                if (newLocation == null)
                {
                    ConsoleHelper.WriteLine("You hear a frustated gurgling noise from somewhere within", ConsoleColor.White);
                }
            }
        }
        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            return false;
        }

        private Location? SwapLocation(Map map, Location currentLocation, Location newLocation)
        {
            if (map.IsOnMap(newLocation) && !map.GetRoomAtLocation(newLocation).IsActive)
            {
                map.GetRoomAtLocation(currentLocation).RemoveMonster();
                map.GetRoomAtLocation(newLocation).AddMonster(this);
                _location = newLocation;
                return newLocation;
            }
            return null;
        }

        public override DisplayDetails Display()
        {
            return new DisplayDetails("[G]", ConsoleColor.Green);
        }
    }
}
