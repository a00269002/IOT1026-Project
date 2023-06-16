namespace MinotaurLabyrinth
{
    // Make your own unique and fun/creative room! There will be lots of marks for creativity if you aren't able to get your head around
    // how to program something extravagent.
    // 1. Add your Room to the list of enums on line 60 in the Utils.cs file
    // 2. Add the code 'AddRooms(RoomType.MyRoom, map);' to the LabyrinthCreator.cs file below where the Pit room is added (line 48)
    // 3. Implement the Activate, Display and DisplaySense methods.
    // 4. Add documentation and a description of your room (any maybe an image?) to the README.
    public class Chest : Room
    {
        /// <summary>
        /// Static constructor for the Chest class.
        /// </summary>
        static Chest()
        {
            RoomFactory.Instance.Register(RoomType.Chest, () => new Chest());
        }
        /// <inheritdoc/>
        public override RoomType Type { get; } = RoomType.Chest;
        /// <inheritdoc/>
        public override bool IsActive { get; protected set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user has entered the room.
        /// </summary>
        public bool UserEnterAtRoom { get; protected set; }

        /// <summary>
        /// Activates the functionality for the specified hero and map.
        /// </summary>
        /// <param name="hero">The hero object.</param>
        /// <param name="map">The map object.</param>
        public override void Activate(Hero hero, Map map)
        {
            if (IsActive)
            {
                hero.CommandList.AddCommand(new List<string>() { "c" }, new GetChestCommand());
                if (hero.HasChest)
                {
                    hero.CommandList.AddCommand(new List<string>() { "open" }, new OpenChestCommand());
                    IsActive = false;
                }
            }
        }
        /// <inheritdoc/>
        public override DisplayDetails Display()
        {
            return IsActive ? new DisplayDetails($"[{Type.ToString()[0]}]", ConsoleColor.Cyan)
                   : base.Display();
        }

        /// <summary>
        /// Displays the sense information for the specified hero and hero distance.
        /// </summary>
        /// <param name="hero">The hero object.</param>
        /// <param name="heroDistance">The distance of the hero from the chest.</param>
        /// <returns>True if the sense information is displayed; otherwise, false.</returns>
        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            if (heroDistance == 0)
            {
                UserEnterAtRoom = true;
                if (hero.HasShield)
                {
                    ConsoleHelper.WriteLine("Chest open successfully!!!", ConsoleColor.Green);
                    Console.WriteLine("You managed to open the chest! You find a strong sturdy shield");
                }
                else
                {
                    ConsoleHelper.WriteLine("You should pick the chest! Don't lost your opportunity", ConsoleColor.DarkCyan);
                }
                return true;
            }
            else if (heroDistance == 1)
            {
                if (IsActive)
                {
                    ConsoleHelper.WriteLine("Look carfully and use all your senses the chest is nearby!", ConsoleColor.DarkCyan);
                    if (UserEnterAtRoom && !hero.HasShield)
                    {
                        ConsoleHelper.WriteLine("Your lost the opportunity to catch the chest!", ConsoleColor.Red);
                        IsActive = false;
                        UserEnterAtRoom = false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
