using System;

// Explorer game
public class Explorer
{	
  	// init static variables
	static int[,] gridMap = new int[7,7];
	static String input = "";
	static int currX = 3;
	static int currY = 3;

  	// entry point
	public static void Main()
	{
	  PrintIntroduction();
    	// generate the map for the run and print it out
		InitGridMap();
		PrintMap();
    	// iterate until an event occurs to cause a break
		while(true){
		  	PrintInstructions();
			input = Console.ReadLine();
			if(ValidateInput(input)){
	        	int intInput = Int32.Parse(input);
		        if(intInput==-1){
		          StopProgram("See you later!");
	        	}
		    	CheckBoundsSetVals(Int32.Parse(input));
			  	PrintMap();
	      	}	
		}	
	}
	
  	// loop through multidimensional array printing based on element type
	public static void PrintMap(){
		for(int i = 0; i < gridMap.GetLength(0); i++){
			for(int j = 0; j < gridMap.GetLength(1); j++){
				if(gridMap[i,j] == LookupElementNumber("Land")){
					Console.Write("O  ");
				}else if(gridMap[i,j] == LookupElementNumber("Water")){
					Console.Write("~  ");
				}else if(gridMap[i,j] == LookupElementNumber("ExploredArea")){
					Console.Write("X  ");
				}else if(gridMap[i,j] == LookupElementNumber("Treasure")){
          			Console.Write("O  ");
        		}
				if(j==gridMap.GetLength(1)-1){
					Console.WriteLine();
				}
			}
		}
	}
	
	public static void CheckBoundsSetVals(int bearing){
		int futureX = currX;
		int futureY = currY;
		// North, East, South, West
		if(bearing == LookupDirectionNumber("North")){
			futureY--;
		}else if(bearing == LookupDirectionNumber("East")){
			futureX++;
		}else if(bearing == LookupDirectionNumber("South")){
			futureY++;
		}else if(bearing == LookupDirectionNumber("West")){
			futureX--;
		}

		if(futureX < 0 || futureX+1 > gridMap.GetLength(1)){
      		// the program should end
      		StopProgram("You've reached the edge!");
		}else if(futureY < 0 || futureY+1 > gridMap.GetLength(0)){
      		// the program should end
		  	StopProgram("You've reached the edge!");
		}else{
      		// continue
			Console.WriteLine("You crossed over " + LookupElementName(gridMap[futureY,futureX]));
      	if(gridMap[futureY,futureX]==LookupElementNumber("Treasure")){
        	// congrats
        	StopProgram("You've found treasure, time to retire!\n\n"+FetchTreasureString());
      	}
		currX = futureX;
		currY = futureY;
		gridMap[futureY,futureX] = LookupElementNumber("ExploredArea");
		Console.WriteLine();
  		CheckVisitedWholeWorld();
		}
	}

	
  // fills the map with Element enums using a call to a random number generator
	public static void InitGridMap(){
		Random rand = new Random();
		for(int i = 0; i < gridMap.GetLength(0); i++){
			for(int j = 0; j < gridMap.GetLength(1); j++){
				gridMap[i,j] = GetNewRandomElem(rand);
			}
		}
		gridMap[3,3] = LookupElementNumber("ExploredArea");
	    // create a single treasure chest
	    int randX = rand.Next(0, 7);
	    int randY = rand.Next(0, 7);
	    gridMap[randX,randY] = LookupElementNumber("Treasure");
	}
	
  // random element generator
	public static int GetNewRandomElem(Random rand){
		int retElem;
    // water is more common than land
		if(rand.Next(0, 10) > 7){
			retElem = LookupElementNumber("Land");
		}else{
			retElem = LookupElementNumber("Water");
		}
    	return retElem;
	}
	
  public static bool ValidateInput(String input){
    int temp;
    if(Int32.TryParse(input, out temp)){
      return true;
    }
    return false;
  }

  	// theoretically this method should never actually stop the program
   	public static void CheckVisitedWholeWorld(){

     // check if all are "X" if so, break out
    for(int i = 0; i < gridMap.GetLength(0); i++){
		for(int j = 0; j < gridMap.GetLength(1); j++){
			if(gridMap[i,j] != LookupElementNumber("ExploredArea")){
          		return;
        	}
		}
	}
    // if you reached here, there were no free spots
    Console.WriteLine("You've traveled the entire world!\n");
    PrintMap();
    Environment.Exit(0);
  }

  public static String FetchShipImage(){
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

  public static String FetchTreasureString(){
    return @"        /\____;;___\
       | /         /
       `. ())oo() .
        |\(%()*^^()^\
       %| |-%-------|
      % \ | %  ))   |
      %  \|%________|
ejm97  %%%%";
  }

  public static void PrintIntroduction(){
	Console.WriteLine("You are an explorer...");
	Console.WriteLine("This is your map...\n");
	Console.WriteLine(FetchShipImage()+"\n");
  }

  public static void PrintInstructions(){
    Console.WriteLine("\nPlease enter a bearing...");
    Console.WriteLine("North = 0, East = 1, South = 2, West = 3 or -1 to quit\n");
  }


  public static void StopProgram(String msg){
    Console.WriteLine(msg);
	Environment.Exit(0);
  }

  public static String LookupDirectionName(int entry){
  	// could use int constants here but a small program so chose not to
 	String retString = "";
 	// North
  	if(entry==0){
  		retString = "North";
  	// East
	}else if(entry==1){
		retString = "East";
	// South
	}else if(entry==2){
		retString = "South";
	// West
	}else{
		retString = "West";
	}
	return retString;
  }

  public static int LookupDirectionNumber(String name){
  	// could use int constants here but a small program so chose not to
 	int retVal = 0;
 	// North
  	if(name.Equals("North")){
  		retVal = 0;
  	// East
	}else if(name.Equals("East")){
		retVal = 1;
	// South
	}else if(name.Equals("South")){
		retVal = 2;
	// West
	}else{
		retVal = 3;
	}
	return retVal;
  }

  public static String LookupElementName(int entry){
  	String retString = "";
 	// North
  	if(entry==0){
  		retString = "Water";
  	// East
	}else if(entry==1){
		retString = "Land";
	// South
	}else if(entry==2){
		retString = "ExploredArea";
	// West
	}else{
		retString = "Treasure";
	}
	return retString;
  }

  public static int LookupElementNumber(String name){
  	// could use int constants here but a small program so chose not to
 	int retVal = 0;
 	// North
  	if(name.Equals("Water")){
  		retVal = 0;
  	// East
	}else if(name.Equals("Land")){
		retVal = 1;
	// South
	}else if(name.Equals("ExploredArea")){
		retVal = 2;
	// West
	}else{
		retVal = 3;
	}
	  return retVal;
  }

  // ascii art credits to http://www.ascii-art.de/
}