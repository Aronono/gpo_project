using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbClasses
{
    class Hazard : DbClass
    {
        public int id {  get; set; }
        public string Name { get; set; }
        public string Complexity { get; set; }
        public string Description { get; set; }
        public string MechDescription { get; set; }
        public int Level { get; set; }
        public string Stealth {  get; set; }
        public Hazard() { }
        public Hazard(int id, string name, string complexity, string description, string mechDescription, int level, string stealth)
        {
            this.id = id;
            Name = name;
            Complexity = complexity;
            Description = description;
            MechDescription = mechDescription;
            Level = level;
            Stealth = stealth;
        }

        public static Hazard ToHazard(object[] values)
        {
            int id = Convert.ToInt32(values[0]);
            string name = values[1].ToString();
            string complexity = values[2].ToString();
            string description = values[3].ToString();
            string mechDescription = values[4].ToString();
            int level = Convert.ToInt32(values[5]);
            string stealth = values[6].ToString();
            return new Hazard(id, name, complexity, description, mechDescription, level, stealth);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
