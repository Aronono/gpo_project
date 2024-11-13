using DbClasses;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GenerationClasses
{
    class MonsterGenerator : ObjectGenerator
    {
        private List<Monster> monsters;
        public MonsterGenerator(string _dbName)
        {
            this.dbName = _dbName;
        }
        Monster monster;
        readonly Dictionary<int, int> expcostlist = new Dictionary<int, int>()
            {
                {10, -4 },
                {15, -3 },
                {20, -2 },
                {30, -1},
                {40, 0 },
                {60, 1 },
                {80, 2 },
                {120, 3 },
                {160, 4 }
            };
        Dictionary<int, List<Monster>> monsters_by_lvl = new Dictionary<int, List<Monster>>();
        public List<Monster> BuildCombat(int xpbudget, int party_level)
        {
            monsters = new List<Monster>();
            Random rnd = new Random();
            while (xpbudget >= 10)
            {
                int moncostindex;
                switch (party_level)
                {
                    case 1: //Для группы уровня "1"
                        moncostindex = rnd.Next(2, 8);
                        break;
                    case 2://Для группы уровня "2"
                        moncostindex = rnd.Next(1, 8);
                        break;
                    default://Для остальных разницы нет
                        moncostindex = rnd.Next(0, 8);
                        break;
                        //TODO: обработать случаи для уровней, близких к 20.
                }


                int moncost = expcostlist.ElementAt(moncostindex).Key; // стоимость по бюджету
                if (moncost > xpbudget) continue;

                int monlvl = party_level + expcostlist.ElementAt(moncostindex).Value; // лвл исходя из уровня пати
                if (!monsters_by_lvl.ContainsKey(monlvl)) //Проверка, сохраняли ли список монстров этого уровня
                {
                    monsters_by_lvl[monlvl] = GetMonstersTByLevel(monlvl);
                }
                int max_mon_amount = monsters_by_lvl[monlvl].Count;
                monster = monsters_by_lvl[monlvl][rnd.Next(0, max_mon_amount)]; //Случайный монстр этого уровня
                monsters.Add(monster);
                xpbudget -= moncost;
                if (party_level <= 2 && xpbudget <= 10) break;
            }
            return monsters;
        }

        private List<Monster> GetMonstersTByLevel(int level)
        {
            return GetObjectsByQuery(
                "SELECT * FROM Monsters WHERE Level = " + level.ToString(),
                Monster.ToMonster
            );
        }
    }
}