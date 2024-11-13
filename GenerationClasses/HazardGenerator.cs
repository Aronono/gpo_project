using DbClasses;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GenerationClasses
{
    class HazardGenerator : ObjectGenerator
    {
        private List<Hazard> hazards;
        
        public HazardGenerator(string _dbName)
        {
            this.dbLink = _dbName;
        }

        private List<Hazard> GetHazardsTByLevelComplexity(int level, int complexity)
        {
            string tempComplexity;
            if (complexity == 0) tempComplexity = "Простая";
            else tempComplexity = "Комплексная";
            return GetObjectsByQuery(
                "SELECT * FROM Hazards WHERE Level = " + level + " AND Complexity = \"" + tempComplexity + "\"",
                Hazard.ToHazard
            );
        }

        //X опыта стоит ловушка, если она отличается от группы на Y уровней
        readonly Dictionary<int, int> simpleExpCostList = new Dictionary<int, int>()
            {
                {2, -4 }, 
                {3, -3 },
                {4, -2 },
                {6, -1},
                {8, 0 },
                {12, 1 },
                {16, 2 },
                {24, 3 },
                {32, 4 }
            };

        readonly Dictionary<int, int> complexExpCostList = new Dictionary<int, int>()
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

        Dictionary<int, List<Hazard>> simple_hazards_by_lvl = new Dictionary<int, List<Hazard>>();
        Dictionary<int, List<Hazard>> complex_hazards_by_lvl = new Dictionary<int, List<Hazard>>();
        Hazard hazard;

        public List<Hazard> BuildHazard(int xpbudget, int party_level)
        {
            hazards = new List<Hazard>();
            Debug.Log($"----Всего опыта на ловушки: - {xpbudget}\n");
            System.Random rnd = new System.Random();
            //Для групп уровней 1 и 2 ниже бюджета в 3 и 4 опыта невозможно найти ловушки
            //TODO: инициировать генерацию комплексных ловушек
            while (xpbudget >= 2)
            {
                xpbudget = BuildSimpleHazard(xpbudget, party_level);
                if (party_level == 1 && xpbudget < 4) break;
                if (party_level == 2 && xpbudget < 3) break;
            }
            return hazards;
           
        }

        public int BuildSimpleHazard(int xpbudget, int party_level)
        {
            System.Random rnd = new System.Random();
            int hazcostindex;
            while (true) {
                switch (party_level)
                {
                    case 1: //Для группы уровня "1"
                        hazcostindex = rnd.Next(2, 8);
                        break;
                    case 2://Для группы уровня "2"
                        hazcostindex = rnd.Next(1, 8);
                        break;
                    default://Для остальных разницы нет
                        if (xpbudget == 2) hazcostindex = 0;
                        else hazcostindex = rnd.Next(0, 8);
                        break;
                        //TODO: обработать случаи для уровней, близких к 20.
                }
                int hazcost = simpleExpCostList.ElementAt(hazcostindex).Key; // стоимость ловушки по бюджету
                if (hazcost > xpbudget) continue;

                int hazlvl = party_level + simpleExpCostList.ElementAt(hazcostindex).Value; // лвл исходя из уровня пати
                if (!simple_hazards_by_lvl.ContainsKey(hazlvl)) //Проверка, сохраняли ли список ловушек этого уровня
                {
                    List<Hazard> lvl_list = GetHazardsTByLevelComplexity(hazlvl, 0);
                    simple_hazards_by_lvl[hazlvl] = lvl_list;
                }
                int max_haz_amount = simple_hazards_by_lvl[hazlvl].Count;
                hazard = simple_hazards_by_lvl[hazlvl][rnd.Next(0, max_haz_amount)]; //Случайная ловушка этого уровня
                hazards.Add(hazard);
                xpbudget -= hazcost;
                Debug.Log($"Сгенерировали ловушку: {hazard.Name} / {hazard.Level}; Осталось опыта на ловушки: {xpbudget}");

                return xpbudget;
            }
            
        }
    }
}