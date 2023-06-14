using MinotaurLabyrinth;
namespace MinotaurLabyrinthTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void PitRoomTest()
        {
            Pit pitRoom = new Pit();
            Hero hero = new Hero();
            Map map = new Map(1, 1);
            pitRoom.Activate(hero, map);
            Assert.AreEqual(pitRoom.IsActive, false);
            Assert.AreEqual(hero.IsAlive, true);

            hero.HasSword = true;
            pitRoom.Activate(hero, map);
            // Hero should not die because pitRoom is inactive here
            Assert.AreEqual(hero.IsAlive, true);

            Pit newPitRoom = new Pit();
            newPitRoom.Activate(hero, map);
            Assert.AreEqual(hero.IsAlive, false);
        }
    }
}
