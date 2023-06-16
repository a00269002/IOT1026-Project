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
                // This means the user is standing in the room and they have opened the chest (Room will be activated again after the hero executes the OpenChestCommand)
                if (hero.HasShield)
                {
                    ConsoleHelper.WriteLine("After you retrieve the shield from the chest, it disintegrates before your eyes!", ConsoleColor.DarkBlue);
                    hero.CommandList.RemoveCommand(typeof(OpenChestCommand));
                    IsActive = false;
                }
                else
                {
                    hero.CommandList.AddCommand(new List<string>() { "o", "open chest" }, new OpenChestCommand());
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
                if (IsActive)
                {
                    // If it is the first time the hero has entered the room & the room is active.
                    if (!UserEnterAtRoom)
                    {
                        ConsoleHelper.WriteLine("You should pick the chest! Don't lost your opportunity", ConsoleColor.DarkCyan);
                        UserEnterAtRoom = true;
                    }
                    // Else the user has previously entered the room, so deactivate the room and indicate they lost their opportunity.
                    else
                    {
                        ConsoleHelper.WriteLine("A wave of sadness passes over you as you watch the chest and all its contents disintegrate before your eyes.", ConsoleColor.Red);
                        IsActive = false;
                    }
                    return true;
                }
                else
                {
                    ConsoleHelper.WriteLine("The dusty remnants of a once sturdy chest are all that remain in this room", ConsoleColor.Gray);
                }
            }
            else if (heroDistance == 1)
            {
                if (IsActive)
                {
                    // If the room is active but the user has not yet entered the room
                    if (!UserEnterAtRoom)
                    {
                        ConsoleHelper.WriteLine("Look carfully and use all your senses the chest is nearby!", ConsoleColor.DarkCyan);
                    }
                    // Else the user entered the room and left without opening the chest
                    else
                    {
                        ConsoleHelper.WriteLine("A profound sense of regret passes over you as you feel you lost your opportunity to loot the chest!", ConsoleColor.DarkCyan);
                        IsActive = false;
                        hero.CommandList.RemoveCommand(typeof(OpenChestCommand));
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
