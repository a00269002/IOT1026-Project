namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a Slimy gel monster in the game.
    /// </summary>
    public class GelatinousCube : Monster, IMoveable
    {
        private Location _location;

        /// <summary>
        /// Initializes a new instance of the GelatinousCube class with the specified location.
        /// </summary>
        /// <param name="location">The location of the GelatinousCube.</param>
        public GelatinousCube(Location location)
        {
            _location = location;
        }

        /// <summary>
        /// Retrieves the location of the object.
        /// </summary>
        /// <returns>The location of the object.</returns>
        public Location GetLocation()
        {
            return _location;
        }

        /// <summary>
        /// Activates the fearsome monster, causing it to prowl through the darkness and unleash a deadly strike on the hero.
        /// </summary>
        /// <param name="hero">The hero object that the monster interacts with.</param>
        /// <param name="map">The map object representing the game map.</param>
        public override void Activate(Hero hero, Map map)
        {
            ConsoleHelper.WriteLine("The fearsome monster prowl through the darkness, its monstrous form shifting and morphing in response to the agile movements of the hero", ConsoleColor.Red);
            hero.Kill("In a swift and devastating strike, the monstrous embodiment claimed victory, extinguishing the hero's last breath.");
        }

        /// <summary>
        /// Moves the gelatinous cube towards the hero on the game map.
        /// </summary>
        /// <param name="hero">The hero object that the gelatinous cube interacts with.</param>
        /// <param name="map">The map object representing the game map.</param>
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

        /// <summary>
        /// Displays the sense feedback based on the distance between the hero and the monstrous creature.
        /// </summary>
        /// <param name="hero">The hero object.</param>
        /// <param name="heroDistance">The distance between the hero and the monster.</param>
        /// <returns>Always returns false.</returns>
        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            if (heroDistance == 0)
            {
                ConsoleHelper.WriteLine("A shiver runs down your spine as the hero's gaze meets the monstrous creature before them", ConsoleColor.DarkRed);
            }
            else if (heroDistance == 1)
            {
                ConsoleHelper.WriteLine("Your heart races as you sense the monster's presence drawing near.", ConsoleColor.DarkBlue);
            }
            return false;
        }

        /// <summary>
        /// Swaps the current location of the gelatinous cube with a new location on the map, if the new location is valid and the corresponding room is inactive.
        /// </summary>
        /// <param name="map">The map object representing the game map.</param>
        /// <param name="currentLocation">The current location of the gelatinous cube.</param>
        /// <param name="newLocation">The new location to which the gelatinous cube is to be moved.</param>
        /// <returns>The new location if the swap is successful, otherwise null.</returns>
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
