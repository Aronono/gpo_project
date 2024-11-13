using System;

namespace DbClasses
{
    class Monster : DbClass
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MechDescription { get; set; }
        public int Level { get; set; }

        public Monster()
        {

        }

        public Monster(int id, string Name, string Description, string MechDescription, int Level)
        {
            this.id = id;
            this.Name = Name;
            this.Description = Description;
            this.MechDescription = MechDescription;
            this.Level = Level;
        }

        public static Monster ToMonster(object[] values)
        {
            int id = Convert.ToInt32(values[0]);
            string Name = values[1].ToString();
            string Description = values[2].ToString();
            string MechDescription = values[3].ToString();
            int Level = Convert.ToInt32(values[4]);
            return new Monster(id, Name, Description, MechDescription, Level);
        }        

        public override string ToString()
        {
            return Name;
        }
    }
}
