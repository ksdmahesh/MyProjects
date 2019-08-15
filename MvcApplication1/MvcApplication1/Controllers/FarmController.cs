using Management;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1.Controllers
{
    public class FarmController : Controller
    {
        
        // GET: Farm
        public ActionResult DashBoard(string condition)
        {
            if (Request.IsAjaxRequest())
            {
                Session["URL"] = Request.Url.AbsolutePath;
                return PartialView();
            }
            return View();
        }

        public ActionResult LandManagement()
        {
            if (Request.IsAjaxRequest())
            {
                Session["URL"] = Request.Url.AbsolutePath;
                return PartialView();
            }
            return View();
        }

        public ActionResult PetManagement(string req = "")
        {
            PetManagement petManagement = OnLoad();
            if (Request.IsAjaxRequest())
            {
                return OnPetAjaxRequest(petManagement, req);
            }
            Session["PetManagement"] = petManagement;
            return View(petManagement);
        }

        private ActionResult OnPetAjaxRequest(PetManagement petManagement, string req = "")
        {
            if (req.Contains("id"))
            {
                petManagement.PetBreeds = petManagement.PetBreedList[Convert.ToInt32(req.Substring(req.IndexOf('[') + 1).Replace("]", "")) - 1];
                Session["PetManagement"] = petManagement;
                return PartialView("_PetDetails", petManagement.PetBreeds);
            }
            else if (req.Contains("delete"))
            {
                foreach (string index in req.Substring(req.IndexOf('[') + 1).Replace(",]", "").Split(','))
                {
                    petManagement.PetBreedList.Where(item => item.Id == index).ToList().ForEach(delegate(PetBreeds petBreeds)
                    {
                        petBreeds.DeleteFlag = 'D';
                    });
                }
                Session["PetManagement"] = petManagement;
                return PartialView("_PetList", petManagement.PetBreedList);
            }
            else if (req.Contains("newPet"))
            {
                petManagement.PetBreeds = petManagement.PetBreedList.FirstOrDefault(item => 
                    item.Type.ToString() == req.Substring(req.IndexOf('[') + 1).Replace("]", "")
                    ) ?? new PetBreeds();
                Session["PetManagement"] = petManagement;
                return PartialView("_PetDetails", petManagement.PetBreeds);
            }
            else if (req.Contains("newItem"))
            {
                petManagement.PetBreeds = new PetBreeds();
                Session["PetManagement"] = petManagement;
                return PartialView("_PetDetails", petManagement.PetBreeds);
            }            
            else
            {
                Session["URL"] = Request.Url.AbsolutePath;
                return PartialView("PetManagement", petManagement);
            }
        }

        public ActionResult OnResourceAjaxRequest(PetManagement petManagement, string req = "")
        {
            if (req.Contains("id"))
            {
                petManagement.PetBreeds = petManagement.PetBreedList[Convert.ToInt32(req.Substring(req.IndexOf('[') + 1).Replace("]", "")) - 1];
                Session["PetResources"] = petManagement;
                return PartialView("_PetResource", petManagement.PetBreeds);
            }
          else  if (req.Contains("sold"))
            {
                petManagement.PetBreedList.Where(item => item.Id == req.Substring(req.IndexOf('[') + 1).Replace("]", "")).ToList().ForEach(delegate(PetBreeds petBreeds)
                {
                    petBreeds.DeleteFlag = 'D';
                });
                Session["PetResources"] = petManagement;
                return PartialView("_PetResourceList", petManagement.PetBreeds);
            }
            Session["PetResources"] = petManagement;
            return PartialView(petManagement); 
        }

        private PetManagement OnLoad()
        {
            PetManagement petManagement;
            if (Session["PetManagement"] == null)
            {
                petManagement = new PetManagement()
                {
                    PetBreedList = new List<PetBreeds>() 
                {
                    
                   #region IndigenousDairyBreedOfCattle
		            
                    #region

                    new PetBreeds()
                    {
                        Id="1",
                        //Pet=Pet.Cattle,
                        //Type=PetType.Gir,
                        //Breed=Breed.IndigenousDairyBreedOfCattle,
                        Details=new Details()
                            {
                                Name="Y",
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                           //     Colors="white with dark red or chocolate-brown patches / sometimes black / purely red",
                                Color="white with dark red",
                                //Originates="Gir forests of South Kathiawar in Gujarat.",
                               // FirstCalf="45-54 months",
                                //InterCalf="515 to 600 days",
                                About="<img src='/Images/Gir.png' /><br /><ul><li>Horns are peculiarly curved, giving a ‘half moon’ appearance.</li><li>Milk yield ranges from 1200-1800 kgs.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                              //  AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },
                    
                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="2",
                        Pet=Pet.Cattle,
                        Type=PetType.RedSindhi,
                        Breed=Breed.IndigenousDairyBreedOfCattle,
                        Details=new Details()
                            {
                                Name="Z",
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="red with shades varying from dark red to light, strips of white",
                                Color="red with shades varying from dark red to light, strips of white",
                                Originates="Karachi and Hyderabad district of Pakistan.",
                                FirstCalf="39-50 months",
                                InterCalf="425-540 days",
                                About="<img src='/Images/redsindhi.png' /><br /><ul><li>Milk yield ranges from 1100-2600 kgs.</li><li>Widely used in crossbreeding programmes.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="3",
                        Pet=Pet.Cattle,
                        Type=PetType.Sahiwal,
                        Breed=Breed.IndigenousDairyBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="reddish dun / pale red / sometimes flashed with white patches",
                                Color="pale red",
                                Originates="Montgomery district in present Pakistan.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/Sahiwal.jpg' /><br /><ul><li>The average milk yield of this breed is between 2,725 and 3,175 kgs in lactation period of 300 days</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="4",
                        Pet=Pet.Cattle,
                        Type=PetType.Deoni,
                        Breed=Breed.IndigenousDairyBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="spotted black and white",
                                Color="spotted black and white",
                                Originates="Marathwada region of Maharashtra state and adjoining part of Karnataka and western Andhra Pradesh states.",
                                FirstCalf="894 to 1540 days with an average of 1391 days",
                                InterCalf="447 days",
                                About="<img src='/Images/deoni.png' /><br /><ul><li>Milk yield ranges from 636 to 1230 kgs with an average of 940 days.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

	                #endregion
                    
                   #region IndigenousDraughtBreedOfCattle

		            #region
                    
                    new PetBreeds()
                    {
                        Id="5",
                        Pet=Pet.Cattle,
                        Type=PetType.Hallikar,
                        Breed=Breed.IndigenousDraughtBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="grey",
                                Color="grey",
                                Originates="former princely state of Vijayanagarm, presently part of Karnataka.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/hallikar.jpg' /><br /><ul><li>They are compact, muscular and medium size animal.</li><li>The breed is best known for its draught capacity and especially for its trotting ability.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="6",
                        Pet=Pet.Cattle,
                        Type=PetType.Amritmahal,
                        Breed=Breed.IndigenousDraughtBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="grey ( shade white to black )",
                                Color="grey ( shade white to black )",
                                Originates="Hassan, Chikmagalur and Chitradurga district of Karnataka.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/amrithmahal.jpg' /><br /><ul><li>Horns are long and end in sharp black points.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="7",
                        Pet=Pet.Cattle,
                        Type=PetType.Khillari,
                        Breed=Breed.IndigenousDraughtBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="Grey-white",
                                Color="Grey-white",
                                Originates="Sholapur and Sitapur districts of Maharashtra.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/Khilari.png' />"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="8",
                        Pet=Pet.Cattle,
                        Type=PetType.Kangayam,
                        Breed=Breed.IndigenousDraughtBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="grey / white / red / black / fawn / broken colours",
                                Color="grey",
                                Originates="Kangayam, Dharapuram, Perundurai, Erode, Bhavani and part of Gobichettipalayam taluk of Erode and Coimbatore district.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/Kangayam.png' />"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="9",
                        Pet=Pet.Cattle,
                        Type=PetType.Bargur,
                        Breed=Breed.IndigenousDraughtBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="brown with white markings / white / dark brown",
                                Color="grey",
                                Originates="Bargur hills in Bhavani taluk of Erode district.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/bargur.png' />"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="10",
                        Pet=Pet.Cattle,
                        Type=PetType.Umblachery,
                        Breed=Breed.IndigenousDraughtBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="red or brown with white marking on the face, on limbs and tail",
                                Color="red or brown with white marking on the face, on limbs and tail",
                                Originates="Thanjavur, Thiruvarur and Nagappattinam districts of Tamil Nadu.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/umblacheri.png' />"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="11",
                        Pet=Pet.Cattle,
                        Type=PetType.Pullikulam_Alambadi,
                        Breed=Breed.IndigenousDraughtBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="grey / white",
                                Color="grey",
                                Originates="Salem and Coimbatore district of Tamil Nadu and part of Bangalore district of Karnataka.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/pullikulam.png' />"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

	                #endregion
                    
                   #region IndigenousDualPurposeBreedOfCattle
		
                    #region
                    
                    new PetBreeds()
                    {
                        Id="12",
                        Pet=Pet.Cattle,
                        Type=PetType.Tharparkar,
                        Breed=Breed.IndigenousDualPurposeBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="white / light grey",
                                Color="white",
                                Originates="Tharparkar district of southeast Sind in Pakistan.",
                                FirstCalf="38-42 months",
                                InterCalf="430 to 460 days",
                                About="<img src='/Images/tharparkerbull.jpg' />"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="13",
                        Pet=Pet.Cattle,
                        Type=PetType.Hariana,
                        Breed=Breed.IndigenousDualPurposeBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="white / light grey",
                                Color="white",
                                Originates="Rohtak, Hisar, Jind and Gurgaon districts of Haryana.",
                                FirstCalf="40-60 months",
                                InterCalf="-",
                                About="<img src='/Images/haryanabull.jpg' /><br /><li>Hariana cows are good milkers yielding on an average 1.5 kg/cow/day in a lactation period of 300 days.</li>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="14",
                        Pet=Pet.Cattle,
                        Type=PetType.Kankrej,
                        Breed=Breed.IndigenousDualPurposeBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="silver-grey to iron-grey / steel black",
                                Color="steel black",
                                Originates="Southeast Rann of Kutch of Gujarat and adjoining Rajasthan (Barmer and Jodhpur district).",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/kankrej.png' /><li>The cows are good milkers, yielding about 1360 kgs.</li>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="15",
                        Pet=Pet.Cattle,
                        Type=PetType.Ongole,
                        Breed=Breed.IndigenousDualPurposeBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="-",
                                Color="-",
                                Originates="Ongole taluk in Gantur district of Andhra Pradesh.",
                                FirstCalf="38-45 months",
                                InterCalf="470 days",
                                About="<img src='/Images/Ongole.png' /><li>Average milk yield is 1000 kgs.</li>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion

                    #region
                    
                    new PetBreeds()
                    {
                        Id="16",
                        Pet=Pet.Cattle,
                        Type=PetType.KrishnaValley,
                        Breed=Breed.IndigenousDualPurposeBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="whitish",
                                Color="whitish",
                                Originates="black cotton soil of the water shed of the river Krishna in Karnataka.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/Krishna_valley.png' /><li>The cows are fair milkers, average yield being about 916 kgs during the lactation period.</li>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 
	
                   #endregion
                    
                   #region ExoticDairyBreedOfCattle

                    #region
                    
                    new PetBreeds()
                    {
                        Id="17",
                        Pet=Pet.Cattle,
                        Type=PetType.Jersey,
                        Breed=Breed.ExoticDairyBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="reddish fawn",
                                Color="reddish fawn",
                                Originates="Jersey, U.K.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/jersey.png' /><li>These are economical producers of milk with 5.3% fat and 15% SNF.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="18",
                        Pet=Pet.Cattle,
                        Type=PetType.HolsteinFriesian,
                        Breed=Breed.ExoticDairyBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="marking of black and white",
                                Color="marking of black and white",
                                Originates="northern parts of Netherlands, especially in the province of Friesland.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/holstien friesian.png' /><li>They are the largest dairy breed and mature cows weigh as much as 700kg.</li><li>The average production of cow is 6000 to 7000 kgs per lactation. However, the fat content in their milk is rather low (3.45 per cent).</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="19",
                        Pet=Pet.Cattle,
                        Type=PetType.BrownSwiss,
                        Breed=Breed.ExoticDairyBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="-",
                                Color="-",
                                Originates="mountainous region of Switzerland.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/brownswiss.png' />"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="20",
                        Pet=Pet.Cattle,
                        Type=PetType.RedDane,
                        Breed=Breed.ExoticDairyBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="red / reddish brown / dark brown",
                                Color="red",
                                Originates="Danish.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/reddane.png' /><br /><li>It is also a heavy breed; mature males weighing up to 950 kgs and mature female weigh 600 kgs.</li><li>The lactation yield of Red Dane cattle varies from 3000 to 4000 kgs with a fat content of 4 per cent and above.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="21",
                        Pet=Pet.Cattle,
                        Type=PetType.Ayrshire,
                        Breed=Breed.ExoticDairyBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="-",
                                Color="-",
                                Originates="Scotland.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/ayshire.png' /><li>They do not produce as much milk or butter fat (only 4%) as some of the other dairy breeds.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="22",
                        Pet=Pet.Cattle,
                        Type=PetType.Guernsey,
                        Breed=Breed.ExoticDairyBreedOfCattle,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="-",
                                Color="-",
                                Originates="Guernsey (France).",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/guernsey.png' /><li>The milk has a golden colour due to an exceptionally high content of beta carotene which may help to reduce the risks of certain cancers.</li><li>The milk also has a high butterfat content of 5% and a high protein content of 3.7%.</li><li>Guernsey cows produce around 6000 litres per cow per annum.</li><li>The Guernsey cow has many notable advantages for the dairy farmer over other breeds includes high efficiency of milk production, low incidence of calving difficulty and longevity.</li>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion  

	                #endregion
                    
                   #region IndigenousBuffaloBreed

                	#region
                    
                    new PetBreeds()
                    {
                        Id="23",
                        Pet=Pet.Buffalo,
                        Type=PetType.Murrah,
                        Breed=Breed.IndigenousBuffaloBreed,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="black with white",
                                Color="black with white",
                                Originates="Rohtak, Hisar and Jind of Haryana, Nabha and Patiala districts of Punjab and southern parts of Delhi state.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/murrah.png' /><li>Butter fat content is 7%. Average lactation yield is varying from 1500-2500 kgs and the average milk yield is 6.8 kgs /day.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="24",
                        Pet=Pet.Buffalo,
                        Type=PetType.Surti,
                        Breed=Breed.IndigenousBuffaloBreed,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="rusty brown to silver-grey",
                                Color="rusty brown to silver-grey",
                                Originates="Kaira and Baroda district of Gujarat.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/surti.png' /><li>The milk yield ranges from 900 to 1300 kgs.</li><li>The peculiarity of this breed is very high fat percentage in milk (8-12per cent).</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="25",
                        Pet=Pet.Buffalo,
                        Type=PetType.Jaffrabadi,
                        Breed=Breed.IndigenousBuffaloBreed,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="-",
                                Color="-",
                                Originates="Gir forests, Kutch and Jamnagar districts of Gujarat.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/jaffrabadi.png' /><li>The average milk yield is 100 to 1200 kgs.</li><li>The bullocks are heavy and used for ploughing and carting.</li>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="26",
                        Pet=Pet.Buffalo,
                        Type=PetType.Bhadawari,
                        Breed=Breed.IndigenousBuffaloBreed,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="light or copper colored",
                                Color="light or copper coloured",
                                Originates="Agra and Etawah district of Uttar Pradesh and Gwalior district of Madhya Pradesh.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/bhadawari.png' /><li>The average milk yield is 800 to 1000 kgs.</li><li>The bullocks are good draught animal with high heat tolerance.</li><li>The fat content varies from 6 to 12.5 per cent. This breed is an efficient converter of coarse feed into butterfat and is known for its high butter fat content.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="27",
                        Pet=Pet.Buffalo,
                        Type=PetType.NiliRavi,
                        Breed=Breed.IndigenousBuffaloBreed,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="-",
                                Color="-",
                                Originates="Sutlej valley in Ferozpur district of Punjab and in the Sahiwal district of Pakistan.",
                                FirstCalf="45-50 months",
                                InterCalf="500-550 days",
                                About="<img src='/Images/niliravi.png' /><li>The milk yield is 1500-1850 kgs per lactation.</li>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="28",
                        Pet=Pet.Buffalo,
                        Type=PetType.Mehsana,
                        Breed=Breed.IndigenousBuffaloBreed,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="-",
                                Color="-",
                                Originates="Mehsana town in Gujarat and adjoining Maharashtra state.",
                                FirstCalf="-",
                                InterCalf="450-550 days",
                                About="<img src='/Images/mehsana.png' /><li>The milk yield is 1200-1500 kgs. The breed is supposed to have good persistency.</li></ul>"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="29",
                        Pet=Pet.Buffalo,
                        Type=PetType.Nagpuri,
                        Breed=Breed.IndigenousBuffaloBreed,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                Colors="black with white",
                                Color="black with white",
                                Originates="Nagpur, Akola and Amrawati districts of Maharashtra.",
                                FirstCalf="45-50 months",
                                InterCalf="450-550 days",
                                About="<img src='/Images/nagpuri.png' />"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion 

                    #region
                    
                    new PetBreeds()
                    {
                        Id="30",
                        Pet=Pet.Buffalo,
                        Type=PetType.Toda,
                        Breed=Breed.IndigenousBuffaloBreed,
                        Details=new Details()
                            {
                                Age=4,
                                Health=100,
                                Remarks="Healthy",
                                 Colors="fawn and ash-grey",
                                Color="fawn and ash-grey",
                                Originates="Nilgiris.",
                                FirstCalf="-",
                                InterCalf="-",
                                About="<img src='/Images/toda.png' />"
                            },
                        MilkSheds=new MilkSheds()
                            {
                                TodaysMilk=5,
                                TotalMilk=5,
                                AverageMilk=5
                            },
                        MoneyManagement=new MoneyManagement()
                            {
                                ForFood=500,
                                ForHealth=0,
                                Credit=1000,
                                Debit=500,
                                TotalCredited=1000,
                                TotalDebited=500
                            }
                    },

                    #endregion  

	                #endregion

                }
                };
            }
            else
            {
                petManagement = Session["PetManagement"] as PetManagement;
            }
            return petManagement;
        }

        public ActionResult Resources(string req = "")
        {
            PetManagement petResources = OnLoad();
            if (Request.IsAjaxRequest())
            {
                Session["URL"] = Request.Url.AbsolutePath;
                return OnResourceAjaxRequest(petResources, req);
            }
            Session["PetResources"] = petResources;
            return View(petResources);
        }

        public ActionResult Links()
        {
            if (Request.IsAjaxRequest())
            {
                Session["URL"] = Request.Url.AbsolutePath;
                return PartialView();
            }
            return View();
        }

        public ActionResult Status()
        {
            if (Request.IsAjaxRequest())
            {
                Session["URL"] = Request.Url.AbsolutePath;
                return PartialView();
            }
            return View();
        }

    }
}