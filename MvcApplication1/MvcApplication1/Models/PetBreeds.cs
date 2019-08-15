using Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Management
{

    public enum Breed
    {
        [Description("Indigenous Dairy Breed")]
        IndigenousDairyBreedOfCattle,
        [Description("Indigenous Draught Breed")]
        IndigenousDraughtBreedOfCattle,
        [Description("Indigenous Dual Purpose Breed")]
        IndigenousDualPurposeBreedOfCattle,
        [Description("Exotic Dairy Breed")]
        ExoticDairyBreedOfCattle,
        [Description("Indigenous Buffalo Breed")]
        IndigenousBuffaloBreed
    }

    public enum PetType
    {
        [Description("Gir / Desan / Gujarati / Kathiawari / Sorthi / Surati / Maldhari")]
        Gir,
        [Description("Red Sindhi / Red Karachi / Sindhi")]
        RedSindhi,
        [Description("Sahiwal / Lola (loose skin) / Lambi Bar / Montgomery / Multani / Teli")]
        Sahiwal,
        [Description("Deoni / Dongerpati / Dongari / Wannera / Waghyd / Balankya / Shevera")]
        Deoni,
        [Description("Hallikar")]
        Hallikar,
        [Description("Amritmahal")]
        Amritmahal,
        [Description("Khillari")]
        Khillari,
        [Description("Kangayam")]
        Kangayam,
        [Description("Bargur")]
        Bargur,
        [Description("Umblachery / Jathi madu / Mottai madu / Molai madu / Therkathi madu")]
        Umblachery,
        [Description("Pullikulam / Alambadi")]
         Pullikulam_Alambadi,
        [Description("Tharparkar / White Sindhi / Gray Sindhi / Thari")]
         Tharparkar,
        [Description("Hariana")]
         Hariana,
        [Description("Kankrej / Wadad / Waged / Wadhiar")]
         Kankrej,
        [Description("Ongole / Nellore")]
         Ongole,
        [Description("Krishna Valley")]
        KrishnaValley,
        [Description("Jersey")]
        Jersey,
        [Description("Holstein Friesian")]
        HolsteinFriesian,
        [Description("Brown Swiss")]
        BrownSwiss,
        [Description("Red Dane")]
        RedDane,
        [Description("Ayrshire / Dunlop / Cunningham")]
        Ayrshire,
        [Description("Guernsey")]
        Guernsey,
        [Description("Murrah / Delhi / Kundi / Kali")]
        Murrah,
        [Description("Surti")]
        Surti,
        [Description("Jaffrabadi")]
        Jaffrabadi,
        [Description("Bhadawari")]
        Bhadawari,
        [Description("Nili Ravi")]
        NiliRavi,
        [Description("Mehsana")]
        Mehsana,
        [Description("Nagpuri / Elitchpuri / Barari")]
        Nagpuri,
        [Description("Toda")]
        Toda
    }

    public enum Pet
    {
        Cattle,
        Buffalo
    }

    public class PetBreeds
    {
        public PetBreeds()
        {
            MilkSheds = new MilkSheds();
            MoneyManagement = new MoneyManagement();
            Details = new Details();
            Resources = new Resources();
        }
        public string Id { get; set; }
        public char DeleteFlag { get; set; }
        public Pet Pet { get; set; }
        public PetType Type { get; set; }
        public Breed Breed { get; set; }
        public MilkSheds MilkSheds { get; set; }
        public Resources Resources { get; set; }
        public MoneyManagement MoneyManagement { get; set; }
        public Details Details { get; set; }
    }

    public class Resources
    {
        public double TotalDung { get; set; }
        public double TotalPee { get; set; }
    }

    public class MilkSheds
    {
        public double TodaysMilk { get; set; }
        public double TotalMilk { get; set; }
        public double AverageMilk { get; set; }
    }

    public class MoneyManagement
    {
        public double ForFood { get; set; }
        public double ForHealth { get; set; }
        public double Credit { get; set; }
        public double TotalCredited { get; set; }
        public double Debit { get; set; }
        public double TotalDebited { get; set; }
    }

    public class Details
    {
        public double Age { get; set; }
        public string Originates { get; set; }
        public string Color { get; set; }
        public string Colors { get; set; }
        public string Name { get; set; }
        public double Health { get; set; }
        public string Remarks { get; set; }
        public string About { get; set; }
        public string FirstCalf { get; set; }
        public string InterCalf { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
    }

}

namespace Models 
{
    public class PetManagement
    {
        public PetManagement()
        {
            PetBreeds = new PetBreeds();
            PetBreedList = new List<PetBreeds>();
        }
        public PetBreeds PetBreeds { get; set; }
        public List<PetBreeds> PetBreedList { get; set; }
    }
}