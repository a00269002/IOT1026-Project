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
        private readonly bool _isLocked;
        private readonly bool _isOpen;
        //private string contents;

        static Chest()
        {
            RoomFactory.Instance.Register(RoomType.Chest, () => new Chest());
        }
        /// <inheritdoc/>
        public override RoomType Type { get; } = RoomType.Chest;
        /// <inheritdoc/>
        public override bool IsActive { get; protected set; } = true;
        public override void Activate(Hero hero, Map map)
        {
            if (IsActive)
            {
                Console.WriteLine("Open your eyes! Chest room is active!!!");
                hero.CommandList.AddCommand(new List<string>() { "open" }, new OpenChestCommand());
                IsActive = false;
            }
        }
        /// <inheritdoc/>
        public override DisplayDetails Display()
        {
            return IsActive ? new DisplayDetails($"[{Type.ToString()[0]}]", ConsoleColor.Red)
                   : base.Display();
        }
        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            if (heroDistance == 0)
            {
                if (hero.HasChest) ConsoleHelper.WriteLine("This is the chest room but you've already picked up the chest!", ConsoleColor.DarkCyan);
                else ConsoleHelper.WriteLine("You can sense that the chest is nearby!", ConsoleColor.DarkCyan);
                return true;
            }
            else if (heroDistance == 1)
            {
                ConsoleHelper.WriteLine("Look carfully and use all your senses the chest is nearby!", ConsoleColor.DarkCyan);
                return true;
            }
            return false;
        }
    }
}


/*public class Room
{
    private bool isActive;
    private Chest chest;

    public Room()
    {
        isActive = false;
        chest = new Chest("Gold coins and a magical amulet");
    }

    public void Activate()
    {
        isActive = true;
        Console.WriteLine("The room is now active. There is a chest in the room.");
    }

    public void Deactivate()
    {
        isActive = false;
        Console.WriteLine("The room is now inactive.");
    }

    public void Interact()
    {
        if (isActive)
        {
            Console.WriteLine("You approach the chest in the room.");
            chest.Unlock();
            chest.Open();
        }
        else
        {
            Console.WriteLine("The room is not active. You need to activate it first.");
        }
    }
}

public class Chest
{
    private bool isLocked;
    private bool isOpen;
    private string contents;

    public Chest(string contents)
    {
        isLocked = true;
        isOpen = false;
        this.contents = contents;
    }

    public void Unlock()
    {
        if (isLocked)
        {
            Console.WriteLine("You unlock the chest.");
            isLocked = false;
        }
        else
        {
            Console.WriteLine("The chest is already unlocked.");
        }
    }

    public void Open()
    {
        if (!isLocked)
        {
            if (!isOpen)
            {
                Console.WriteLine("You open the chest and find: " + contents);
                isOpen = true;
            }
            else
            {
                Console.WriteLine("The chest is already open.");
            }
        }
        else
        {
            Console.WriteLine("The chest is locked. You need to unlock it first.");
        }
    }
}


public void Unlock()
        {
            if (_state == State.Locked)
            {
                _state = State.Closed;
            }
            else if (_state == State.Closed)
            {
                Console.WriteLine("The chest cannot unlocked because it is closed.");
            }
            else if (_state == State.Open)
            {
                Console.WriteLine("The chest cannot be unlocked because it is open.");
            }
        }

        /// <summary>
        /// Locks the treasure chest.
        /// </summary>
        /// <remarks>
        /// If the treasure chest is currently closed, it will be locked and its state will change to <see cref="State.Locked"/>.
        /// If the treasure chest is already locked, a message will be printed to the console indicating that the chest is already locked.
        /// If the treasure chest is open, a message will be printed to the console indicating that the chest cannot be locked because it is open.
        /// </remarks>
        public void Lock()
        {
            if (_state == State.Closed)
            {
                _state = State.Locked;
            }
            else if (_state == State.Locked)
            {
                Console.WriteLine("The chest is already locked!");
            }
            else if (_state == State.Open)
            {
                Console.WriteLine("The chest cannot be locked because it is open.");
            }
        }

        /// <summary>
        /// Opens the treasure chest.
        /// </summary>
        /// <remarks>
        /// If the treasure chest is currently closed, it will be opened and its state will change to <see cref="State.Open"/>.
        /// If the treasure chest is already open, a message will be printed to the console indicating that the chest is already open.
        /// If the treasure chest is locked, a message will be printed to the console indicating that the chest cannot be opened because it is locked.
        /// </remarks>
        public void Open()
        {

            if (_state == State.Closed)
            {
                _state = State.Open;
            }
            else if (_state == State.Open)
            {
                Console.WriteLine("The chest is already open!");
            }
            else if (_state == State.Locked)
            {
                Console.WriteLine("The chest cannot be opened because it is locked.");
            }
        }

        /// <summary>
        /// Closes the treasure chest.
        /// </summary>
        /// <remarks>
        /// If the treasure chest is currently open, it will be closed and its state will change to <see cref="State.Closed"/>.
        /// If the treasure chest is already closed, a message will be printed to the console indicating that the chest is already closed.
        /// If the treasure chest is locked, a message will be printed to the console indicating that the chest cannot be closed because it is locked.
        /// </remarks>
        public void Close()
        {
            if (_state == State.Open)
            {
                _state = State.Closed;
            }
            else if (_state == State.Closed)
            {
                Console.WriteLine("The chest is already close!");
            }
            else if (_state == State.Locked)
            {
                Console.WriteLine("The chest cannot be close because it is locked.");
            }
        }
*/
