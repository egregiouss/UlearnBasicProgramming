using System;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName() => "Terrain.png";

        public int GetDrawingPriority() => 5;

        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject)
            => conflictedObject is Player;
    }

    public class Player : ICreature
    {
        public string GetImageFileName() => "Digger.png";

        public int GetDrawingPriority() => 3;

        public CreatureCommand Act(int x, int y)
        {
            var diggerAct = new CreatureCommand();
            if (Game.KeyPressed == System.Windows.Forms.Keys.Down)
                diggerAct.DeltaY++;
            if (Game.KeyPressed == System.Windows.Forms.Keys.Up)
                diggerAct.DeltaY--;
            if (Game.KeyPressed == System.Windows.Forms.Keys.Left)
                diggerAct.DeltaX--;
            if (Game.KeyPressed == System.Windows.Forms.Keys.Right)
                diggerAct.DeltaX++;

            if (!(x + diggerAct.DeltaX < 0 || 
			    x + diggerAct.DeltaX >= Game.MapWidth ||
                y + diggerAct.DeltaY < 0 || 
				y + diggerAct.DeltaY >= Game.MapHeight ||
                Game.Map[x + diggerAct.DeltaX, y + diggerAct.DeltaY] is Sack))
                return diggerAct;

            diggerAct.DeltaX = 0;
            diggerAct.DeltaY = 0;
            return diggerAct;
        }

        public bool DeadInConflict(ICreature conflictedObject)
            => conflictedObject is Sack ||
               conflictedObject is Monster;
    }

    public class Gold : ICreature
    {
        public string GetImageFileName() => "Gold.png";

        public int GetDrawingPriority() => 4;

        public CreatureCommand Act(int x, int y) => new CreatureCommand();

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                Game.Scores += 10;
                return true;
            }
            return conflictedObject is Monster;
        }
    }

    public class Sack : ICreature
    {
        private int drop;
		
        public string GetImageFileName() => "Sack.png";

        public int GetDrawingPriority() => 2;

        public CreatureCommand Act(int x, int y)
        {
            var sackAct = new CreatureCommand();

            if (y + 1 < Game.MapHeight)
            {
                if (drop > 0 && (Game.Map[x, y + 1] is Player ||
                    Game.Map[x, y + 1] is Monster) || 
					Game.Map[x, y + 1] is null)
                {
                    drop++;
                    sackAct.DeltaY++;
                    return sackAct;
                }
            }
            if (drop > 1 && (y + 1 == Game.MapHeight || 
			    (y + 1 < Game.MapHeight &&
                (!(Game.Map[x, y + 1] is null) || 
				 !(Game.Map[x, y + 1] is Player)))))
                sackAct.TransformTo = new Gold();
            drop = 0;

            return sackAct;
        }

        public bool DeadInConflict(ICreature conflictedObject) => false;
    }

    public class Monster : ICreature
    {
        public string GetImageFileName() => "Monster.png";

        public int GetDrawingPriority() => 1;

        public CreatureCommand Act(int x, int y)
        {
            var monsterAct = new CreatureCommand();
            var diggerCoord = DiggerOnTheMap();
            if (diggerCoord.Item1 == int.MinValue &&
				diggerCoord.Item2 == int.MinValue)
                return monsterAct;
            var offsetX = Math.Sign(diggerCoord.Item1 - x);
            var offsetY = Math.Sign(diggerCoord.Item2 - y);
            if (IsMovedPossible(x + offsetX, y))
            {
                monsterAct.DeltaX += offsetX;
                return monsterAct;
            }
            if (IsMovedPossible(x, y + offsetY))
            {
                monsterAct.DeltaY += offsetY;
                return monsterAct;
            }

            return monsterAct;
        }

        public bool DeadInConflict(ICreature conflictedObject)
            => conflictedObject is Sack || conflictedObject is Monster;

        private Tuple<int, int> DiggerOnTheMap()
        {
            for (var x = 0; x < Game.MapWidth; x++)
                for (var y = 0; y < Game.MapHeight; y++)
                    if (Game.Map[x, y] is Player)
                        return Tuple.Create(x, y);
            return Tuple.Create(int.MinValue, int.MinValue);
        }

        private bool IsMovedPossible(int x, int y)
        	=> !(x >= Game.MapWidth  || x < 0) &&
                   !(y >= Game.MapHeight || y < 0) &&
               		(Game.Map[x, y] is null   ||
                	 Game.Map[x, y] is Player ||
                	 Game.Map[x, y] is Gold);
    }
}
