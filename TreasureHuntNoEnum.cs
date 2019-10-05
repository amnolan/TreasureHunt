using System;

// Explorer game
public class Explorer
{
	// Enum representing simplified bearings
	public enum CompassDirection
	{
		North = 0,
		East = 1,
		South = 2,
		West = 3
	}
	
  // Enum representing dual purpose, type of location, water or land
  // similar to how unions work in C, it also allows for ExploredArea which just
  // indicates we already know what is in that location
	public enum Element
	{
		Water = 0,
		Land = 1,
		ExploredArea = 2,
    	Treasure = 3
	}
	
  // init static variables
	static Element[,] gridMap = new Element[7,7];
	static String input = "";
	static int currX = 3;
	static int currY = 3;
	// static int waterFound = 0;
	// static int landFound = 0;
	// static int exploredArea = 1;
	
  // entry point
	public static void Main()
	{
	  printIntroduction();
    // generate the map for the run and print it out
		initGridMap();
		PrintMap();
    // iterate until an event occurs to cause a break
		while(true){
		  printInstructions();
			input = Console.ReadLine();
      if(validateInput(input)){
        int intInput = Int32.Parse(input);
        if(intInput==-1){
          stopProgram("See you later!");
        }
        CheckBoundsSetVals((CompassDirection)Int32.Parse(input));
		  	PrintMap();
      }
			
		}	
	}
	
  // loop through multidimensional array printing based on element type
	public static void PrintMap(){
		for(int i = 0; i < gridMap.GetLength(0); i++){
			for(int j = 0; j < gridMap.GetLength(1); j++){
				// this is land
				if(gridMap[i,j] == Element.Land){
					Console.Write("O  ");
				// this is water
				}else if(gridMap[i,j] == Element.Water){
					Console.Write("~  ");
				// this is explored area
				}else if(gridMap[i,j] == Element.ExploredArea){
					Console.Write("X  ");
				// this is treasure (looks like land)
				}else if(gridMap[i,j] == Element.Treasure){
          			Console.Write("O  ");
        }
				if(j==gridMap.GetLength(1)-1){
					Console.WriteLine();
				}
			}
		}
	}
	
	public static void CheckBoundsSetVals(CompassDirection bearing){
		int futureX = currX;
		int futureY = currY;
		if(bearing == CompassDirection.North){
			futureY--;
		}else if(bearing == CompassDirection.East){
			futureX++;
		}else if(bearing == CompassDirection.South){
			futureY++;
		}else if(bearing == CompassDirection.West){
			futureX--;
		}

		if(futureX < 0 || futureX+1 > gridMap.GetLength(1)){
      // the program should end
      stopProgram("You've reached the edge!");
		}else if(futureY < 0 || futureY+1 > gridMap.GetLength(0)){
      // the program should end
		  stopProgram("You've reached the edge!");
		}else{
      // continue
			Console.WriteLine("You crossed over " + gridMap[futureY,futureX]);
      if(gridMap[futureY,futureX]==Element.Treasure){
        // congrats
        stopProgram("You've found treasure, time to retire!\n\n"+fetchTreasureString());
      }
			currX = futureX;
			currY = futureY;
			gridMap[futureY,futureX] = Element.ExploredArea;
			Console.WriteLine();
      checkVisitedWholeWorld();
		}
	}

	
  // fills the map with Element enums using a call to a random number generator
	public static void initGridMap(){
		Random rand = new Random();
		for(int i = 0; i < gridMap.GetLength(0); i++){
			for(int j = 0; j < gridMap.GetLength(1); j++){
				gridMap[i,j] = getNewRandomElem(rand);
			}
		}
		gridMap[3,3] = Element.ExploredArea;
    // create a single treasure chest
    int randX = rand.Next(0, 7);
    int randY = rand.Next(0, 7);
    gridMap[randX,randY] = Element.Treasure;
	}
	
  // random element generator
	public static Element getNewRandomElem(Random rand){
		Element retElem;
    // water is more common than land
		if(rand.Next(0, 10) > 7){
			retElem = Element.Land;
		}else{
			retElem = Element.Water;
		}
    	return retElem;
	}
	
  public static bool validateInput(String input){
    int temp;
    if(Int32.TryParse(input, out temp)){
      return true;
    }
    return false;
  }

   public static void checkVisitedWholeWorld(){

     // check if all are "X" if so, break out
     for(int i = 0; i < gridMap.GetLength(0); i++){
			for(int j = 0; j < gridMap.GetLength(1); j++){
				if(gridMap[i,j] != Element.ExploredArea){
          return;
        }
			}
		}
    // if you reached here, there were no free spots
    Console.WriteLine("You've traveled the entire world!\n");
    PrintMap();
    Environment.Exit(0);
  }

  public static String fetchShipImage(){
    return @"              |    |    |
             )_)  )_)  )_)
            )___))___))___)\
           )____)____)_____)\\
         _____|____|____|____\\\__
---------\                   /---------
  ^^^^^ ^^^^^^^^^^^^^^^^^^^^^
    ^^^^      ^^^^     ^^^    ^^
         ^^^^      ^^^";
  }

  public static String fetchTreasureString(){
    return @"        /\____;;___\
       | /         /
       `. ())oo() .
        |\(%()*^^()^\
       %| |-%-------|
      % \ | %  ))   |
      %  \|%________|
ejm97  %%%%";
  }

  public static void printIntroduction(){
    Console.WriteLine("You are an explorer...");
		Console.WriteLine("This is your map...\n");
		Console.WriteLine(fetchShipImage()+"\n");
  }

  public static void printInstructions(){
    Console.WriteLine("\nPlease enter a bearing...");
    Console.WriteLine("North = 0, East = 1, South = 2, West = 3 or -1 to quit\n");
  }


  public static void stopProgram(String msg){
    Console.WriteLine(msg);
		Environment.Exit(0);
  }

  public static compassDirectionMapping(int numericDirection){

  }

  // ascii art credits to http://www.ascii-art.de/
}