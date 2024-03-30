using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.ComponentModel.Design;
using System.Text;

//Holds A List Of All Characters For The Application.
string[] characterNames = ["Luke", "Jamie", "Manon", "Kimberly", "Marisa", "Lily", "JP", "Juri", "Dee Jay", "Cammy", "Ryu", "E. Honda", "Blanka", "Guile", "Ken", "Chun-Li", "Zangief", "Dhalsim", "Rashid", "A.K.I", "Ed"];

//Creates A 2D Array For Characters To Put Corresponding Data
string[,] characters = new string[characterNames.Length, 5];

//Holds A Dynamic List Of Terms Used In Fighting Games. Allows User To Input New Terms
List<List<string>> fightingTerms = new List<List<string>>()
{
    new List<string> {"Meaty", "An Attack That Is Perfectly Timed On To Hit An Opponent On Their First Possible Frame. Meaties Often Time Result In Huge Damage Being Dealt."},
    new List<string> {"DP", "DP (Or Dragon Punch) Is A Uppercut Move Used By Ryu & Ken. A DP Is Often Used To Describe An Anti-Air Special Move That Beats Air Attacks And When Amped Is Invincible On Wakeup."},
    new List<string> {"Wakeup", "Refers To A Player Who Recovers From A Knocked Down State. Getting Up From This Is Referred To As A \"Wakeup\"."},
    new List<string> {"Chicken Block", "This Refers To Avoiding An Attack By Neutral Jumping."},
    new List<string> {"Footsies", "Refers To The Battle Of Controlling The Space In Front Of You By Using Good Neutral Or Poke Tools."},
    new List<string> {"Okizeme", "This Is The Strategy Of Maintaining Pressure On An Knocked Down Opponents Using Attacks, Mixups, Or Setups To Limit Opponents Options. Derived From The Japanese Term (\u8D77\u653B) Which Mean \"Wakeup\" & \"Attack\" Respectively"},
    new List<string> {"Frame Trap", "Is A Strategy To Create A Situation In Which The Opponent Thinks They Have An Opening By Leaving A Tiny Gap In Attacks, But Is This Punished For It."},
    new List<string> {"Reversal", "A Reveresal Is A Defensive Technique Where An Attack OR Defensive Action Is Used On The First Possible Frame Of Action After Recovering From Knockdown State."}, 
    new List<string> {"Tech", "Refers To Performing A Defensive Action To Escape Or Reduce An Opponent's Attack Severity. Often Used To Describe Escaping Potential Enemy Throws."},
    new List<string> {"Cancel", "A Technique Where You Interupt The Animation Of An Attack To Immediately Start The Animation Of Another. Cancelling Is A Huge Part Of Fighting Game Combos."}
};

// Dictionary to store character types and their descriptions
Dictionary<string, string> characterTypes = new Dictionary<string, string>
{
    { "standard", "Standard Characters Are Characters That Excel At Everything But Aren't Particularly Good At One Thing!\n\nADVANTAGES\n\n\t1: It's Traditional For These Characters To Have Straightforward Tools & Gameplan.\n\t2: Most Characters In This Category Excel At Medium Range.\n\t3: Tools Consist Of A Projectile, Decent Walk Speeds, Great Anti-Air, & Good Normals.\n\nDISADVANTAGES\n\n\t1: Their Straightforward Gameplan Can Make Them Predictable.\n\t2: Because They Are Well Rounded, They Can't Excel At Things Other Character Types Can." },
    { "speed", "Speed Characters Are Characters That Excel At Pressuring The Opponent At Close Range With Fast Attacks And Movements\n\nADVANTAGES\n\n\t1: Amazing Walk And/Or Dash Speed And Lots Of Movement Options\n\t2: Possess Multiple Ways At Applying Pressure To Opponents\n\t3: Access To Quick Normals Compared\n\nDISADVANTAGES\n\n\t1: Lack Projectile Option Or Lack Good Projectiles\n\t2: Can Struggle Against Characters Who Excel At Long Range." },
    { "power", "Power Characters Are Characters That Excel At Dealing Heavy Amount Of Damage.\n\nADVANTAGES\n\n\t1: This Category Possess Characters That Span Different Archetypes Such As Grapplers\n\t2: Access To Heavier Hitting Combos And Special Moves\n\t3: Have Excellent Combos To Punish Enemies On Mistakes\n\nDISADVANTAGES\n\n\t1: Most Lack Good Walk And/Or Dash Speeds\n\t2: Some Characters In This Category Don't Have Lots Of Ways To Get In On Opponents." },
    { "tricky", "Tricky Characters Are Fighters That Provide Multiple Ways To Confuse And Trip Up Opponents.\n\nADVANTAGES\n\n\t1: Traditionally, Tricky Characters Are Characters With Tools To Mix-Up Opponents With Highs & Lows\n\t2: Can Easily Catch Opponents Of Guard With Your Hard To Deal With Tools\n\t3: Can Easily Condition Opponents To Certain Behaviors\n\nDISADVANTAGES\n\n\t1: Requires High Execution On Setups And Combos\n\t2: Along With High Execution, These Characters Skill Ceiling Are Normally Higher Than Others\n\t3: Can Risk Being Predictable If You Aren't Careful." }
};

//Dictionary To Store Character Names And Descriptions
Dictionary<string, string> characterDescriptions = new Dictionary<string, string>
{
    {"luke", "Luke Possesses Strong Mid-Range Pressure And Whiff Punishing, Which Transitions To Excellent Pressure Once He's Gotten In."},
    {"jamie", "Jamie Is A Close-Range Fighter Specializing In Poking, Footsies, And Harrassment. This Playstyle Is Further Benefitted By His \"Drink Level\" Powerup System."},
    {"manon", "Manon's Playstyle Features Long, Far Reaching Normals That Control The Opponents Position And Enhance Her Strike/Throw Game. Her Lethality Is Increased by Her \"Medal Level\" Powerup System."},
    {"kimberly", "Kimberly Uses Her Plethora Of Unique Movement Options And Setplay To Get In And Keep Opppoonents Guessing. This Gameplan Is Further Developed Thanks To Her \"Shuriken Bombs\"."},
    {"marisa", "Marisa Is A High Damage Juggernaut With A Variety Of Far-Reaching Normals."},
    {"lily", "Lily Is A Poke-Focused Grappler Who Prods And Looks For Openings To Exploit Her Gap Closing Tool."},
    {"jp", "JP Is A Slow But Powerful Zoner Utlizing His Long Reaching Moves To Pester His Opponents And Impede Their Advances."},
    {"juri", "Juri Is An Explosive And Aggressive Rushdown Character That Possesses Strike/Throw Mix That Is Further Enhanced By Her \"Fuha Stocks\" Powerup System."},
    {"deejay", "Dee Jay Is A Pressure Oriented Rushdown Character With Many Ways To Setup Frame Traps And Other Mixups."},
    {"cammy", "Cammy Specializes In Quick Attacks And Movement To Apply High Pressure Offense And Whiff Punishment."},
    {"ryu", "Ryu Emphasizes A Patient Playstyle With Strong Defensive Tools."},
    {"e.honda", "E. Honda Is A Powerful Aggressive Rushdown Character With A Specialty In Long Range Advancing Offense."},
    {"blanka", "Blanka Is A Character That Focuses On Aggressive Mobility And A Variety Of Confusing Mind Games Supplmented By His \"Blanka Chan\" Setplay."},
    {"guile", "Guile Specializes In Pestering The Opponent With A Barrage Of Projectiles Before Analyzing Their Next Move And Punsihing Them Accordingly."},
    {"ken", "Ken Emphasizes Aggression And Close-Range Combat, Providing An Alternative To His Counterpart Ryu."},
    {"chunli", "Chun-Li Puts A Great Emphasis On Controlling Space And Punishing Her Opponents In The Neutral Game."},
    {"zangief", "Zangief Is the Original Grappler, Utilizing His Size And Long Reach To Intimidate The Opponents Into Sitting Stil Before Opening Them Up With Command Grabs."},
    {"dhalsim", "Dhalsim Is A Normals Zoner Who Dominates The Screen With His Long-Range Normals And Variety Of Projectiles."},
    {"rashid", "Rashid Provides A Unique Focus On Mobility Options With Unique Air Current Projectiles Which Furhter Enhance His Movement And Mixup Game."},
    {"a.k.i", "A.K.I Takes After Her Master F.A.N.G Possessing Strong Zoning Tools With An Emphasis On The Wake-Up Game Thanks To Her Poison Mechanic Which Enhances Her Special Moves."},
    {"ed", "Ed Is A Mid-Range Zoner With A Strong Emphasis On Hard Callouts And Looping Wakeup Game."}
};

//Dictionary To Store Game Systems Definition
Dictionary<string, string> gameSystems = new Dictionary<string, string>
{
    {"drivegauge", @"

Arguably A Meter More Important Than The Healthbar, The Drive Gauge Is The Main Mechanic Available To All Fighters In Street Fight 6.
    
Every Player Has 6 Bars Of This Meter And Can Expend To Enhance And Execute Certain Moves.

Drive Gauge Increases By:
1: Attacking Opponent (Hit/Block/Parry/Armor)
2: Passive Regeneration Over Time
3: Successful Drive Parry
4: Walking Forward

Drive Gauge Decreases By:
1: Blocking Attacks
2: Getting Hit BY Super, Drive Impact, Or Punish Counter
3: Using Drive Moves

If You Expend All Of Your Drive Gauge You Enter A State Known As Burnout.

During Burnout:
1: All Bkocked Attacks Have Additional 4 Frames Blockstun
2: Blocking Special Moves Or Supers Result In Chip Damage
3: Enduring A Drive Impact In The Corner Results In A Stun, Leaving You Open To Attack
4: Can No Longer Use Any Drive Options Until Gauge Is Replenished
    "},

    {"drivemoves", @"The Drive Gauge Can Be Used To Execute Drive Moves Which Can Be Utilized To Gain Different Offensive Or Defensive Advantages
    
    Drive Impact (1 Drive Bar): A Forward Lunging Strike With 2 Hits Of Armor. This Allows You To Launch A Counter Attack Agaisnt Your Opponent. If It Absorbs An Attack Or Hits As A Punish Counter, The Opponent Is Left Wide Open For An Attack.
    Drive Parry (1/2 Bar Held): Upon Activation, Drains 1/2 Bar And Continues To drain While Held. Automatically Repels Incoming Attacks. If Attack Is Absorbed Within It's First 2 Starting Frames, It Becomes A Perfect Parry.
    Drive Rush (1/2 Bar): Peform A Quick Rush Forward From A Drive Parry. Allows You To Close The Gap Between You & Your Opponent. Any Move Used During The Rush Gain +4 Frames On Hit Or Block.
    Drive Rush Cancel (3 Drive Bars): Cancel A Move Into A Drive Rush For Extra Combo Potential. Shares The Same Benefits As Drive Rush.
    Overdrive Specials (2 Bars): Pressing 2 Punches Or 2 Kicks While Inputting A Special Move Enhances The Moves. Overdriven Moves Gain Properties Such As Extra Damage, Faster Startup, Better Frame Advantage, Invincibility, Or Armor.
    Drive Reversal (2 Bars): Peform A Counter Attack While Blocking Your Opponents Attacking. Useful For Getting Out Of Tight Situations.
    "},

    {"supergauge", @"The Super Gauge Is Independent From The Drive Gauge And It's Sole Purpose Is Used To Execute Special Moves.
    
Super Gauge Is Built On Block And On Hit. Each Player Can Store Up To 3 Bars Of Super Gauge. A General Rule Of Thumb Is The Higher The Super Art, The Stronger It's Effectiveness.
    
Super Arts Can Also Be Canceled From Other Moves:
1: Level 1 Super Can Be Canceled From Normals Only
2: Level 2 Super Can Be Canceled From Normals And OD Specials
3: Level 3 Supers/CA Can Be Canceled From Normals & Specials.
    "},

    {"counters", @"A Counterhit Occurs When You Strike Your Opponent During Their Startup Or Active Frames. There Are 2 Forms Of Counters In Street Fighter 6.
    
Counterhit: A Counterhit Appears Yellow And Occurs When Attacking An Opponent On Their Startup Frames. Gives +2 Frame Advantage Of The Hit Move And Deal 20% Increased Damage.
Punish Counter: A Punish Counter Appears Orange And Adds +4 Frames Of Advantage, 20% Increased Damage (Throws Gain 70%), Depletes Opponents Drive Gauge. Often times, Heavy Attacks Gain Extra Properties On Counterhit.
    "},

    {"characterpowers", @"Certain Characters In Street Fighter Possess Their Own Unique Power System. These Systems Or Powers Normally Enhance Their Gameplay And Create Opportunities For Additional Power.

Some Examples Include:
Denjin Charge (Ryu)
Drink Level (Jamie)
Fuha Stocks (Juri)
    "}

};

//Creates An Array Of 3 Items For Searching Criteria
string[] searchCriteria = {"", "", ""};

//Valid Search Types
string[] validType = {"standard", "power", "speed", "tricky"};
string[] validRange = {"close-range", "mid-range", "long-range"};
string[] validEaseOfUse = {"easy", "normal", "hard"};

//Creates An Empty List Of Strings For Search Results
List<string> searchResults = new List<string>();

//List All Characters To Be Removed From Strings.
char[] charToRemove = {'!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=', ',', '<', '.', '>', '/','?', ';', ':', '"', '{', '[', ']', '}', '-'};

//Variables To Assist With User Input
string? readResult;
string newTerm = "";
string newDefinition = "";

//Calls A Function To Assing character Types
AssignCharacterInfo();

// Set console output encoding to UTF-8
Console.OutputEncoding = Encoding.UTF8;

//Calls Main Menu Method
MainMenu();

//Holds Main Menu Options DEBUGGED
void MainMenu()
{
    //Prompts User
    Console.WriteLine("\nWelcome To The Street Fighter Console Database. Here Are The Following Menu Options\n");
    Console.WriteLine("ALL DATA PROVIDED IS THANKS TO SUPERCOMBO WIKI");
    
    //Ensures That The Main Menu Is Displayed At Least Once
    do
    {
        //Prompts User With menu Options
        Console.WriteLine("1: Character Types");
        Console.WriteLine("2: Fighting Game Terminology");
        Console.WriteLine("3: Game Systems & Mechanics");
        Console.WriteLine("4: Character Search");
        Console.WriteLine("5: List All Characters");
        Console.WriteLine("\nPlease Enter The Numeral Of The Menu You Are Trying To Access");

        //Waits For User Input
        readResult = Console.ReadLine();

        //Checks User Input. Makes Sures Value Isn't Null
        if(readResult != null)
        {
            //Converts Any Integer String To An Integer DEBUG
            try
            {
                //Attempts To Convert An Read Result To An Integer
                int.TryParse(readResult, out int menuNumber);     

                //Run Switch Statement To Compare To Which Menu Option
                switch(menuNumber)
                {
                    case 1:
                    //Calls Character Type Menu
                    CharacterTypesMenu();
                    break;

                    case 2:
                    //Calls Fighting Game Terminology
                    Terminology();
                    break;

                    case 3:
                    //Calls Game Systems Menu
                    GameSystemsMenu();
                    break;

                    case 4:
                    //Calls The Character Search Menu
                    CharacterSearchMenu();
                    break;

                    case 5:
                    //Calls Character Menu
                    CharacterList();
                    break;

                    default:
                    Console.WriteLine("Invalid Menu Option. Please Re-Enter An Available Menu Option");
                    break;
                }           
            }
            catch (FormatException)
            {
                //If A Format Exception Is Called. See If We Can Turn The ReadResult From A Word To An Integer
                Console.WriteLine("The Input You Entered Is Not In Numerical Format. Please Input The Numeral Of The Menu Option You Wish To Select");
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Your Input Does Not Contain A Value. Please Re-Enter A Valid Input");
            }
        }
    } while (readResult != "exit");

    //Returns Us To Main Menu
    MainMenu();
}

//Holds Values For Character Type Menu. DEBUGGED
void CharacterTypesMenu()
{
    //Prompt User
    Console.WriteLine("\nStreet Fighter 6 Boast 4 Character Types That Have Variations That Represent Several Different Character Archetypes\n");

    do
    {
        //Prompt User
        Console.WriteLine("Here Are The List Of The Character Types Present In The Game:\n");
        Console.WriteLine("1: Standard");
        Console.WriteLine("2: Power");
        Console.WriteLine("3: Speed");
        Console.WriteLine("4: Tricky");
        Console.WriteLine("\nPlease Enter The Name Of The Character Type To Learn More. Or Type \"Exit\" To Return To The Main Menu");

        //Pauses Code
        readResult = Console.ReadLine()?.ToLower().Trim().Replace(" ", "");   
        
        try
        {
            //Checks For Null Value
            if(readResult != null)
            {
                //If The Read Result Contains A Value In The Dictionary
                if(characterTypes.ContainsKey(readResult))
                {
                    //Print Out The String Associated With The Value
                    Console.WriteLine(characterTypes[readResult]);
                    Console.WriteLine("\nPress Enter Or Type \"Exit\" To Return To Main Menu");

                    //Loops For Empty Input Or "Exit" Input
                    do
                    {
                        EmptyOrExit();
                    } while (readResult != "" && readResult != "exit");
                }
            }
            else
            {
                //Prompt User For Incorrect Input
                Console.WriteLine("The Value You Entered Was Invalid. Please Re-Enter A Valid Type.");
            }
        }
        catch(KeyNotFoundException) //If The Key Isn't Found In The Dictionary
        {
            Console.WriteLine("Invalid Character Type. Please Enter A Valid Character Type");
        }
        catch(NullReferenceException)
        {
            Console.WriteLine("Your Input Does Not Contain A Value. Please Re-Enter A Valid Input");
        }

    } while (readResult != "exit");

    //If The While Loop Gets Broken, This Will Return Us To The Main Menu
    MainMenu();
}

//Responsible For Informing User Of Fighting Game Terminology. Allows For Users To Enter New Terminology
void Terminology()
{
    Console.WriteLine("Across Fighting Games, There Are Universal Terms Used That All Have Their Own Meanings");

    do
    {
        //Prompts Users With Options
        Console.WriteLine("\nHere Are Some Of The Terms Most Commonly Used In Fighting Games");
        for(int i = 0; i < fightingTerms.Count; i++)
        {
            //Prints The List Of Terms Inside The List
            Console.WriteLine($"{i + 1}: {fightingTerms[i][0]}");
        }
        
        Console.WriteLine("\nPlease Enter The Name Of The Terms You'd Like To Find More Information About Or Type \"New Term\" To Enter A New Term In The Database");
        Console.WriteLine("Or Type \"Exit\" To Return To Main Menu");

        //Pauses The Code. Lowercases, Trims, And Replaces Whitespace
        readResult = Console.ReadLine()?.ToLower()?.Trim()?.Replace(" ", "");

        //If Read Result Isn't Null, Run The Switch
        if(readResult != null)
        {
            if(readResult == "newterm")
            {
                //Calls Method Responsible For Input Of New Term
                InputNewTerm();
            }
            else
            {
                //Creates Local Boolean
                bool termFound = false;

                for(int i = 0; i < fightingTerms.Count; i++)
                {
                    //Lowercases The First Term To Do Matching
                    string term = fightingTerms[i][0].ToLower();
                    if(readResult == term)
                    {
                        //We Found The Bool. Therefore, This Is True
                        termFound = true;

                        //Prompts User
                        Console.WriteLine("\nHere Is What You Searched For.\n");
                        Console.WriteLine($"Term: {fightingTerms[i][0]}");
                        Console.WriteLine($"Definition: {fightingTerms[i][1]}");
                        Console.WriteLine($"\nPress Enter To Continue Or Type \"Exit\" To Return To Main Menu.");

                        do
                        {
                            //Goes On To Validate Input If It's Empty Or Exit
                            EmptyOrExit();

                        } while (readResult != "" && readResult != "exit");

                        break;
                    }
                }

                if(!termFound)
                {
                    Console.WriteLine("The Term You Entered Was Not Found.");
                }
            }
        }
    } while (readResult != "exit");

    MainMenu();
}

//Responsible For Game Systems Menu. DEBUGGED
void GameSystemsMenu()
{
    Console.WriteLine("\nIn Street Fighter 6, There Main Mechanics Lie In The \"Drive System\". However, There Are Other Aspects Of The Game That Are Just As Important.");
    do
    {
        //Prompts User
        Console.WriteLine("Here Are All Game Systems And Mechanics Present Throughout The Game.\n");
        Console.WriteLine("Drive Gauge");
        Console.WriteLine("Drive Moves");
        Console.WriteLine("Super Gauge");
        Console.WriteLine("Character Powers");
        Console.WriteLine("Counters");
        Console.WriteLine("\nPlease Enter The Numeral Of The Option You Would Like To Learn More About, Or Type \"Exit\" To Return To The Main Menu.");

        //Reads Input From User
        readResult = Console.ReadLine()?.ToLower()?.Trim()?.Replace(" ", "");
        try
        {
            if(readResult != null)
            {
                if(gameSystems.ContainsKey(readResult))
                {
                    Console.WriteLine(gameSystems[readResult]);

                    Console.WriteLine("\nPlease Press Enter Or Type \"Exit\" To Return To Main Menu");

                    do
                    {
                        EmptyOrExit();
                    } while (readResult != "" && readResult != "exit");
                }
                else
                {
                    Console.WriteLine("Invalid Input. Please Re-Enter A Valid Input To Continue");
                }
            }
        }
        catch(KeyNotFoundException)
        {
            Console.WriteLine("Invalid Input. Please Re-Enter A Valid Input");
        }
        catch(NullReferenceException)
        {
            Console.WriteLine("Your Read Result Does Not Contain A Value. Please Re-Enter A Valid Input");
        }
    } while (readResult != "exit");

    MainMenu();
}

//Handles Character Searching
void CharacterSearchMenu()
{
    do
    {
        Console.WriteLine("You Can Search For Every Character Apart Of The Roster Based On The Following Criteria.");
        Console.WriteLine("\n\tType: Standard, Power, Speed, Tricky");
        Console.WriteLine("\tEase Of Use: Easy, Normal, Hard");
        Console.WriteLine("\tRange: Close-Range, Mid-Range, Long-Range");

        Console.WriteLine("\nPlease Enter The Type, Range, And Ease Of Use (Seperated By A \",\") To Begin A Search Or Type \"Exit\" To Return To Main Menu");

        //Pauses Code
        readResult = Console.ReadLine();

        //If The Result Isn't Null
        if(readResult != null && readResult != "exit")
        {
            //Lowercases, Trims, Then Splits The ReadResults
            searchCriteria = readResult.ToLower().Trim().Replace(" ", "").Split(",");

            if(searchCriteria.Length != 3)
            {
                Console.WriteLine("The Number Of Criteria You Entered Is Not Valid. Please Re-Enter 3 Criteria Seperated By \",\"");
                continue;
            }

            //Sets The Criteria Equal To Local Variables
            string type = searchCriteria[0];
            string easeOfUse = searchCriteria[1];
            string range = searchCriteria[2];

            //Checks For Inequalities & Reports Errors
            if(!validType.Contains(searchCriteria[0]))
            {
                Console.WriteLine("The Character Type You Entered Was No Valid. Please Try Again");
                continue;
            }
            else if(!validEaseOfUse.Contains(searchCriteria[1]))
            {
                Console.WriteLine("The Character Ease Of Use You Entered Was Not Valid. Please Try Again");
                continue;
            }
            else if(!validRange.Contains(searchCriteria[2]))
            {
                Console.WriteLine("The Character Range Was Not Valid. Please Try Again");
                continue;
            }
            else
            {
                //Clear Search Results Every Time
                searchResults.Clear();
                for(int i = 0; i < characters.GetLength(0); i++)
                {
                    if(characters[i,1].ToLower() == type && characters[i,2].ToLower() == easeOfUse && characters[i,3].ToLower() == range)
                    {
                        //Adds Them To The List If They Match
                        searchResults.Add(characters[i, 0]);
                    }
                }

                //Check If The List Is Empty
                if(searchResults.Count == 0)
                {
                    Console.WriteLine("\nThere Are No Characters On The Roster That Match This Search Criteria");
                    Console.WriteLine("\nPress Enter To Return To Character Search Or Type \"Exit\" To Return To Main Menu");

                    do
                    {
                        EmptyOrExit();
                    } while (readResult != "exit" && readResult != "");
                } else
                {
                    Console.WriteLine($"\nWe Have Found {searchResults.Count} Fighters That Match Your Search Criteria\n");
                    
                    foreach(string fighter in searchResults)
                    {
                        Console.WriteLine($"\t{fighter}");
                    }

                    Console.WriteLine("\nPress Enter To Return To Character Search Or Type \"Exit\" To Return To Main Menu"); 

                    do
                    {
                        EmptyOrExit();
                    } while(readResult != "" && readResult != "exit");
                }
            }
        }
    } while (readResult != "exit");

    MainMenu();
}

//Responsible For Character List DEBUGGED
void CharacterList()
{
    do
    {
        Console.WriteLine("Welcome To The Character List. Here Are All Available Fighters In Street Fighter 6\n");

        //Loops Through Each Name Then Prints It Out
        foreach(string name in characterNames)
        {
            Console.WriteLine($"\t{name}");
        } 

        //Prompts User
        Console.WriteLine("\nPlease Enter The Name Of The Character You'd Like To Find Out More About!");

        //Pauses Code To Read User Input
        readResult = Console.ReadLine()?.ToLower().Trim();
        try
        {
            //If The Result Isn't Null
            if(readResult != null)
            {
                //Check If The Dictionary Contains The Key
                if(characterDescriptions.ContainsKey(readResult))
                {
                    if(readResult == "a.k.i")
                    {
                        //Prints Name
                        Console.WriteLine($"\nName: A.K.I");
                    }
                    else if (readResult == "e.honda")
                    {
                        Console.WriteLine("\nName: E.Honda");
                    }
                    else
                    {
                        //Prints Name
                        Console.WriteLine($"\nName: {readResult.Substring(0,1).ToUpper() + readResult.Substring(1)}");
                    }
                    //Prints Description
                    Console.WriteLine($"Description: {characterDescriptions[readResult]}");

                    for(int i = 0; i < characters.GetLength(0); i++)
                    {
                        string fighterName = characters[i, 0].ToLower();

                        foreach (char character in charToRemove)
                        {
                            //Replaces Any Characters To Remove
                            fighterName.Replace(character.ToString(), "");
                        }

                        if(fighterName == readResult)
                        {
                            Console.WriteLine($"Type: {characters[i,1]}");
                            Console.WriteLine($"Ease Of Use: {characters[i,2]}");
                            Console.WriteLine($"Effective Range: {characters[i,3]}");
                            Console.WriteLine($"Character Power: {characters[i,4]}");
                            break;
                        }
                    }

                    //Prompts User
                    Console.WriteLine("\nPlease Press Enter Or Type \"Exit\" To Return To Main Menu.");

                    do
                    {
                        EmptyOrExit();
                    } while(readResult != "" && readResult != "exit");
                }
            }
            else
            {
                Console.WriteLine("Invalid Input. Please Enter A Valid Character Name.");
            }
        }
        catch(KeyNotFoundException) //If The Key Isn't Found In The Dictionary
        {
            Console.WriteLine("Invalid Character Type. Please Enter A Valid Character Type");
        }
        catch(NullReferenceException)
        {
            Console.WriteLine("Your Input Does Not Contain A Value. Please Re-Enter A Valid Input");
        }

    } while (readResult != "exit");

    MainMenu();
}

//Responsible For Assigning Types To Characters
void AssignCharacterInfo()
{
    for (int i = 0; i < characterNames.Length; i++)
    {
        //Assign The First Value Of 2D Array To First Name In Character
        characters[i,0] = characterNames[i];

        //Compares Name To Possible Outcomes
        switch(characterNames[i])
        {
            case "Ryu":
            case "Luke":
            characters[i,1] = "Standard";
            characters[i,2] = "Normal";
            characters[i,3] = "Mid-Range";
            break;

            case "Lily":
            characters[i,1] = "Standard";
            characters[i,2] = "Easy";
            characters[i,3] = "Mid-Range";
            break;

            case "A.K.I":
            case "Ed":
            characters[i,1] = "Tricky";
            characters[i,2] = "Hard";
            characters[i,3] = "Long-Range";
            break;

            case "Rashid":
            characters[i,1] = "Tricky";
            characters[i,2] = "Hard";
            characters[i,3] = "Mid-Range";
            break;

            case "Dhalsim":
            case "JP":
            characters[i,1] = "Tricky";
            characters[i,2] = "Hard";
            characters[i,3] = "Long-Range";
            break;

            case "Dee Jay":
            case "Jamie":
            characters[i,1] = "Tricky";
            characters[i,2] = "Normal";
            characters[i,3] = "Mid-Range";
            break;

            case "Zangief":
            characters[i,1] = "Power";
            characters[i,2] = "Hard";
            characters[i,3] = "Close-Range";
            break;

            case "Marisa":
            characters[i,1] = "Power";
            characters[i,2] = "Easy";
            characters[i,3] = "Mid-Range";
            break;

            case "Chun-Li":
            characters[i,1] = "Speed";
            characters[i,2] = "Hard";
            characters[i,3] = "Mid-Range";
            break;

            case "Blanka":
            case "Kimberly":
            characters[i,1] = "Speed";
            characters[i,2] = "Normal";
            characters[i,3] = "Mid-Range";
            break;

            case "Juri":
            characters[i,1] = "Speed";
            characters[i,2] = "Hard";
            characters[i,3] = "Close-Range";
            break;

            case "Guile":
            characters[i,1] = "Standard";
            characters[i,2] = "Normal";
            characters[i,3] = "Long-Range";
            break;

            case "Ken":
            case "E. Honda":
            case "Manon":
            characters[i,1] = "Power";
            characters[i,2] = "Normal";
            characters[i,3] = "Mid-Range";
            break;

            case "Cammy":
            characters[i,1] = "Speed";
            characters[i,2] = "Normal";
            characters[i,3] = "Close-Range";
            break;
        }

        //Goes On To Assign Powers Per Character
        AssignCharacterPower(i, characters[i,0]);
    }
}

//Responsible For Assigning Characters With Powers IF They Have Any
void AssignCharacterPower(int iteration, string characterName)
{
    switch(characterName)
    {
        case "Jamie":
        characters[iteration, 4] = "Drink Level";
        break;

        case "Manon":
        characters[iteration, 4] = "Medal Level";
        break;

        case "Kimberly":
        characters[iteration, 4] = "Shuriken Bombs";
        break;

        case "Lily":
        characters[iteration, 4] = "Windclad";
        break;

        case "Juri":
        characters[iteration, 4] = "Fuha";
        break;

        case "Ryu":
        characters[iteration, 4] = "Denjin Charge";
        break;

        case "E. Honda":
        characters[iteration, 4] = "Sumo Spirit";
        break;

        case "Blanka":
        characters[iteration, 4] = "Blanka-Chan";
        break;

        default:
        characters[iteration, 4] = "No Character Power";
        break;
    }
}

//Responsible For Handling Menus That Require Empty Strings Or The Term "Exit". DEBUGGED
void EmptyOrExit()
{
    //Pauses Code
    readResult = Console.ReadLine()?.ToLower()?.Trim()?.Replace(" ", "");
    try
    {
        if(readResult != null)
        {
            //If The Input Typed Doesn't Equal Exit Or An Empty String
            if(readResult != "" && readResult != "exit")
            {
                Console.WriteLine("INVALID RESPONSE: Please Press Enter Or Type \"Exit\" To Return To Main Menu");
            }
        }
    }
    catch(NullReferenceException)
    {
        Console.WriteLine("Your Read Result Does Not Contain A Value. Please Re-Enter A Valid Input");
    }
}

//Method Resposible For Inputting New Terminology DEBGGED
void InputNewTerm()
{
    //Prompts User
    Console.WriteLine($"Please Enter The Name Of The New Term You'd Like To Input. If The Term Requires Spaces Please Enter {"-"} Between Words");

    //Pauses Code
    readResult = Console.ReadLine();
    try
    {
        //If The Result Isn't Null
        if(readResult != null)
        {
            //Loops Through Each Character In Array And Removes It If Present
            foreach(char character in charToRemove)
            {
                if(character == '-')
                {
                    continue;
                }

                //Replcaes It With Nothing
                readResult = readResult.Replace(character.ToString(), "");
            }

            //Checks If The String Requires Spaces
            if(readResult.Contains('-'))
            {
                //Ensures All Dashes Are Gone And Turns The "-" To A Space
                readResult = readResult.Replace('-'.ToString(), " ");
            }
            else
            {
                //Ensures All Spaces Are Gone
                readResult = readResult.ToLower().Trim().Replace(" ", "");
            }

            //Uppercases First Letter, Lowercases The Rest, Puts It Together And Rids Remaining White Space
            readResult = readResult.Substring(0,1).ToUpper() + readResult.Substring(1).ToLower().Trim();

            //We Are Finished Cleaning And Can Safely Set The Variable
            newTerm = readResult;
        }

        //Prompt User
        Console.WriteLine("Please Enter The Definition Of The New Term You Input.");

        //Pauses Code
        readResult = Console.ReadLine();

        //If The Result Isn't Null
        if(readResult != null && readResult.Length >= 1)
        {
            //Loops Through Each Character In Array And Removes It If Present
            foreach(char character in charToRemove)
            {
                readResult = readResult.Replace(character.ToString(), "");
            }

            //Uppercases The First Letter, Lowercases The Rest. Trims White Space
            readResult = readResult.Substring(0,1).ToUpper() + readResult.Substring(1).ToLower().Trim();
            
            //Sets The Definition To The Read Result After Unwanted Variables Are Taken Out
            newDefinition = readResult;       
        }

        //Create A New List And Add A Term To It
        List<string> newTermDefinition = new List<string> {newTerm, newDefinition};

        fightingTerms.Add(newTermDefinition);
    }
    catch(NullReferenceException)
    {
        Console.WriteLine("Your Input Does Not Contain A Value. Please Re-Enter A Valid Input");
    }
}
