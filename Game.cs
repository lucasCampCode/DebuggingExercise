using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    struct Player
    {
        public string name;
        public int health;
        public int damage;
        public int defense;
        public item item;
    }
    struct item
    {
        public string name;
        public int statboost;
    }
    class Game
    {
        Player player1, player2;
        item sword,rappier,broadSword,dagger;

        bool _singlePlayer = true;
        bool _gameOver = false;
        int _levelScaleMax = 5;
        //Run the game
        public void Run()
        {
            Start();

            while(_gameOver == false)
            {
                Update();
            }

            End();
        }
        void initilizeItems()
        {
            sword.name = "sword";
            sword.statboost = 15;

            rappier.name = "rappier";
            rappier.statboost = 30;

            broadSword.name = "broadsword";
            broadSword.statboost = 40;

            dagger.name = "dagger";
            dagger.statboost = 5;
        }
        void initilizePlayers()
        {
            player1.name = "player1";
            player1.health = 100;
            player1.damage = 10;
            player1.defense = 10;


            player2.name = "player2";
            player2.health = 100;
            player2.damage = 10;
            player2.defense = 10;
        }

        void introductions(ref Player player)
        {
            Console.WriteLine("welcome " + player.name + " what's your true name");
            Console.Write("> ");
            player.name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("welcome " +player.name+" choose your weapon");
            char input = ' ';
            GetInput(out input,"sword","rappier","broadSword","dagger");
            if (input == '1')
            {
                Console.WriteLine("you choose a sword!");
                Console.WriteLine("the sword boost your damage by 15");
                player.item = sword;
                player.damage += sword.statboost;
            }
            else if (input == '2')
            {
                player.item = rappier;
                player.damage += rappier.statboost;
                Console.WriteLine("you choose a rappier!");
                Console.WriteLine("the rappier boost your damage by 30");
            }
            else if(input == '3')
            {
                player.item = broadSword;
                player.damage += broadSword.statboost;
                Console.WriteLine("you choose a broadSword!");
                Console.WriteLine("the broadSword boost your damage by 40");
            }
            else if(input == '4')
            {
                player.item = dagger;
                player.damage += dagger.statboost;
                Console.WriteLine("you choose a dagger!");
                Console.WriteLine("the dagger boost your damage by 5");
            }
            Console.WriteLine("press any key to continue");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }

        //This function handles the battles for our ladder. roomNum is used to update the our opponent to be the enemy in the current room. 
        //turnCount is used to keep track of how many turns it took the player to beat the enemy
        bool StartBattle(int roomNum, ref int turnCount)
        {
            //initialize default enemy stats
            int enemyHealth = 0;
            int enemyAttack = 0;
            int enemyDefense = 0;
            string enemyName = "";
            //Changes the enemy's default stats based on our current room number. 
            //This is how we make it seem as if the player is fighting different enemies
            switch (roomNum)
            {
                case 0:
                    {
                        enemyHealth = 100;
                        enemyAttack = 20;
                        enemyDefense = 5;
                        enemyName = "Wizard";
                        break;
                    }
                case 1:
                    {
                        enemyHealth = 80;
                        enemyAttack = 30;
                        enemyDefense = 5;
                        enemyName = "Troll";
                        break;
                    }
                case 2:
                    {
                        
                        enemyHealth = 200;
                        enemyAttack = 40;
                        enemyDefense = 10;
                        enemyName = "Giant";
                        break;
                    }
            }

            //Loops until the player or the enemy is dead
            while(player1.health > 0 && enemyHealth > 0)
            {
                //Displays the stats for both charactersa to the screen before the player takes their turn
                PrintStats(player1);
                PrintStats(enemyName, enemyHealth, enemyAttack, enemyDefense);

                //Get input from the player
                char input = ' ';
                input = GetInput(out input, "Attack", "Defend");
                //If input is 1, the player wants to attack. By default the enemy blocks any incoming attack
                if(input == '1')
                {
                    BlockAttack(ref enemyHealth,player1.damage, enemyDefense);
                    Console.WriteLine("You dealt " + (player1.damage - enemyDefense) + " damage.");
                    player1.health -= enemyAttack;
                    Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                    turnCount++;
                }
                //If the player decides to defend the enemy just takes their turn. However this time the block attack function is
                //called instead of simply decrementing the health by the enemy's attack value.
                else
                {
                    BlockAttack(ref player1.health, enemyAttack,player1.defense);
                    Console.WriteLine(enemyName + " dealt " + (enemyAttack - player1.defense) + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    turnCount++;
                    
                }

                //After the player attacks, the enemy takes its turn. Since the player decided not to defend, the block attack function is not called.
                
                
            }
            if (player1.health > 0 && roomNum < 2)
            {
                shop(roomNum, enemyName);
            }
            //Return whether or not our player died
            return player1.health != 0;

        }

        bool PVPBattle() 
        {
            while(player1.health > 0 && player2.health > 0)
            {
                PrintStats(player1);
                PrintStats(player2);
                char input = ' ';
                Console.WriteLine();
                Console.WriteLine(player1.name+"'s turn");
                GetInput(out input,"attack","defend");
                if (input == '1')
                {
                    BlockAttack(ref player2.health, player1.damage, player2.defense);
                    Console.WriteLine("You dealt " + (player1.damage - player2.defense) + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if(input == '2')
                {
                    Console.WriteLine("you can't defend");
                }
                PrintStats(player1);
                PrintStats(player2);
                char input2 = ' ';
                Console.WriteLine();
                Console.WriteLine(player2.name + "'s turn");
                GetInput(out input2, "attack", "defend");
                if (input == '1')
                {
                    BlockAttack(ref player1.health, player2.damage, player1.defense);
                    Console.WriteLine("You dealt " + (player2.damage - player1.defense) + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (input == '2')
                {
                    Console.WriteLine("you can't defend");
                }

            }
            return true;
        }
        //Decrements the health of a character. The attack value is subtracted by that character's defense
        void BlockAttack(ref int opponentHealth, int attackVal, int opponentDefense)
        {
            int damage = attackVal - opponentDefense;
            if(damage < 0)
            {
                damage = 0;
            }
            opponentHealth -= damage;
        }


        //Scales up the player's stats based on the amount of turns it took in the last battle
        void upgradeStats(int turnCount)
        {
            //Subtract the amount of turns from our maximum level scale to get our current level scale
            int scale = _levelScaleMax - turnCount;
            if(scale <= 0)
            {
                scale = 1;
            }
            player1.health += 10 * scale;
            player1.damage *= scale;
            player1.defense *= scale;
        }

        void upgradeStats(int amount,char stat)
        {
            if (stat == '1')
            {
                player1.health *= amount;
            }
            else if(stat == '2')
            {
                player1.damage *= amount;
            }
            else if(stat == '3')
            {
                player1.defense *= amount;
            }

        }
        //Gets input from the player
        //Out's the char variable given. This variables stores the player's input choice.
        //The parameters option1 and option 2 displays the players current chpices to the screen
        Char GetInput(out char input,string option1, string option2,string option3 = " ", string option4 = " ")
        {
            //Initialize input
            input = ' ';
            //Loop until the player enters a valid input
            
            while (input != '1' && input != '2' && input != '3' && input != '4')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                if(option3 != " ")
                {
                    Console.WriteLine("3." + option3);
                }
                if (option4 != " ")
                {
                    Console.WriteLine("4." + option4);
                }
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }
            return input;
        }

        //Prints the stats given in the parameter list to the console
        void PrintStats(string name, int health, int damage, int defense)
        {
            Console.WriteLine("\n" + name);
            Console.WriteLine("Health: " + health);
            Console.WriteLine("Damage: " + damage);
            Console.WriteLine("Defense: " + defense);
        }
        void PrintStats(Player player)
        {
            Console.WriteLine("\n" + player.name);
            Console.WriteLine("Health: " + player.health);
            Console.WriteLine("Damage: " + player.damage);
            Console.WriteLine("Defense: " + player.defense);
            Console.WriteLine("item: " + player.item.name);
        }
        void shop(int roomNum,string enemyName)
        {
            Console.WriteLine("you defeted the " + enemyName);
            Console.WriteLine("now you can chose what to upgrade");
            char input = ' ';
            GetInput(out input,"health","damage","defense");
            upgradeStats(2, input);
            if (input == '1')
            {
                Console.WriteLine("you health doubled");
                Console.Write("> ");
                Console.ReadKey();
                Console.Clear();
            }
            else if(input == '2')
            {
                Console.WriteLine("you damage doubled");
                Console.Write("> ");
                Console.ReadKey();
                Console.Clear();
            }
            else if(input == '3')
            {
                Console.WriteLine("you defense double");
                Console.Write("> ");
                Console.ReadKey();
                Console.Clear();
            }
        }

        //This is used to progress through our game. A recursive function meant to switch the rooms and start the battles inside them.
        void ClimbLadder(int roomNum)
        {
            int turnCount = 0;
            //Displays context based on which room the player is in
            switch (roomNum)
            {
                case 0:
                    {
                        Console.WriteLine("A wizard blocks your path");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("A troll stands before you");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("A giant has appeared!");
                        break;
                    }
                default:
                    {
                        _gameOver = true;
                        return;
                    }
            }
            
            //Starts a battle. If the player survived the battle, level them up and then proceed to the next room.
                if(StartBattle(roomNum, ref turnCount))
            {
                roomNum++;
                upgradeStats(turnCount);
                ClimbLadder(roomNum);
            }
            _gameOver = true;

        }

        //Displays the character selection menu. 
        void SelectCharacter()
        {
            char input = ' ';
            //Loops until a valid option is choosen
            while(input != '1' && input != '2' && input != '3')
            {
                //Prints options
                Console.WriteLine("Welcome! Please select a character.");
                Console.WriteLine("1.Sir Kibble");
                Console.WriteLine("2.Gnojoel");
                Console.WriteLine("3.Joedazz");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                //Sets the players default stats based on which character was picked
                switch (input)
                {
                    case '1':
                        {
                            player1.name = "Sir Kibble";
                            player1.health = 120;
                            player1.defense = 10;
                            player1.damage = 40;
                            break;
                        }
                    case '2':
                        {
                            player1.name = "Gnojoel";
                            player1.health = 40;
                            player1.defense = 2;
                            player1.damage = 70;
                            break;
                        }
                    case '3':
                        {
                            player1.name = "Joedazz";
                            player1.health = 200;
                            player1.defense = 5;
                            player1.damage = 25;
                            break;
                        }
                        //If an invalid input is selected display and input message and input over again.
                    default:
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.Write("> ");
                            Console.ReadKey();
                            break;
                        }
                }
                Console.Clear();
            }
            //Prints the stats of the choosen character to the screen before the game begins to give the player visual feedback
            PrintStats(player1);
            Console.WriteLine("Press any key to continue.");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }
        //Performed once when the game begins
        public void Start()
        {
            initilizePlayers();
            initilizeItems();

            char input = ' ';
            GetInput(out input,"singlePlayer","multiPlayer");
            if(input == '1')
            {
                SelectCharacter();
            }
            else if(input == '2')
            {
                _singlePlayer = false;
                introductions(ref player1);
                introductions(ref player2);
            }
        }

        //Repeated until the game ends
        public void Update()
        {
            if (_singlePlayer)
            {
                ClimbLadder(0);  
            }
            else
            {
                _gameOver = PVPBattle();
            }
             
        }

        //Performed once when the game ends
        public void End()
        {
            if (_singlePlayer)
            {
                //If the player died print death message
                if(player1.health <= 0)
                {
                    Console.WriteLine("Failure");
                    return;
                }
                //Print game over message
                Console.WriteLine("Congrats");
            }
            else
            {
                if(player1.health > 0)
                {
                    Console.WriteLine("congrats you won " + player1.name);
                }
                else
                {
                    Console.WriteLine("congrats you won " + player2.name);
                }
            }
        }
            
    }
}
