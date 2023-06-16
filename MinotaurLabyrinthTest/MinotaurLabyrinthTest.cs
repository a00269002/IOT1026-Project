using MinotaurLabyrinth;
namespace MinotaurLabyrinthTest
{
    [TestClass]
    public class RoomTests
    {
        [TestMethod]
        public void PitRoomTest()
        {
            // Seed the RandomNumberGenerator so the sequence of random numbers
            // is deterministic
            RandomNumberGenerator.SetSeed(1);

            Pit pitRoom = new ();
            Hero hero = new ();
            Map map = new (1, 1);

            pitRoom.Activate(hero, map);
            Assert.AreEqual(pitRoom.IsActive, false);
            Assert.AreEqual(hero.IsAlive, true);

            hero.HasSword = true;
            pitRoom.Activate(hero, map);
            // Hero should not die because pitRoom is inactive here
            Assert.AreEqual(hero.IsAlive, true);

            Pit newPitRoom = new ();
            newPitRoom.Activate(hero, map);
            Assert.AreEqual(hero.IsAlive, true);

            newPitRoom.Activate(hero, map);
            newPitRoom = new Pit();
            newPitRoom.Activate(hero, map);
            newPitRoom = new Pit();
            newPitRoom.Activate(hero, map);
            Assert.AreEqual(hero.IsAlive, false);
        }

        [TestMethod]
        public void ChestRoomTest()
        {
            Chest chestRoom = new ();
            Hero hero = new ();
            Map map = new (1, 1);

            chestRoom.Activate(hero, map);
            Assert.AreEqual(chestRoom.IsActive, false);
            Assert.AreEqual(hero.IsAlive, true);

            hero.HasSword = true;
            chestRoom.Activate(hero, map);
            // Hero should not die because pitRoom is inactive here
            Assert.AreEqual(hero.IsAlive, true);

            Chest newChestRoom = new ();
            newChestRoom.Activate(hero, map);
            Assert.AreEqual(hero.IsAlive, true);

            newChestRoom.Activate(hero, map);
            newChestRoom = new();
            newChestRoom.Activate(hero, map);
            newChestRoom = new();
            newChestRoom.Activate(hero, map);
            Assert.AreEqual(hero.IsAlive, false);
        }
    }

    [TestClass]
    public class MonsterTests
    {
        [TestMethod]
        public void MinotaurTest()
        {
            Hero hero = new ();
            Minotaur minotaur = new ();
            Map map = new (4, 4);
            hero.HasSword = true;
            Assert.AreEqual(hero.HasSword, true);

            minotaur.Activate(hero, map);
            // Charge moves the hero 1 room east and 2 rooms north
            // -1 is off the map so hero position should be (0, 2)
            Assert.AreEqual(hero.Location, new Location(0, 2));
            Assert.AreEqual(hero.HasSword, false);

            minotaur.Activate(hero, map);
            Assert.AreEqual(hero.Location, new Location(0, 3));

            hero.Location = new Location(3, 1);
            minotaur.Activate(hero, map);
            Assert.AreEqual(hero.Location, new Location(2, 3));
        }
    }
}
