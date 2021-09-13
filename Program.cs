using System;

namespace lab_1
{
	/*
В первой части необходимо разработать класс, согласно индивидуальному варианту, содержащий:
    • элементы разного уровня доступа (public и private);  
    • не менее 4 свойств;  
    • не менее 3 методов;  
    • перегрузку метода toString;  
    • статический метод;  
    • константное или поле только для чтения;  
    • не менее 3 конструкторов; 
    Рекомендуемые поля и методы указаны в варианте. Также необходимо написать программу с меню, позволяющую протестировать разработанный класс. Обязательные пункты меню:
    • Задание параметров конструируемого объекта
    • Вывод свойств объекта
    • Выполнение статического метода
    • Выполнение методов объекта
	*/

    class Program
    {
        static void Main(string[] args)
        {
			int option = 0;
			COVID19 virus = new COVID19 ();

			Console.WriteLine("1.set strain");
			Console.WriteLine("2. set sick");
			Console.WriteLine("3. set dead");
			Console.WriteLine("4. set spread probability");
			Console.WriteLine("5. print all");
			Console.WriteLine("6. set g5");
			Console.WriteLine("7. spread");
			Console.WriteLine("8. cure");
			Console.WriteLine("9. mutate");
			Console.WriteLine("0. exit");
			do {

				Console.Write("Choose option: ");
				Int32.TryParse(Console.ReadLine(), out option);
				switch(option){
				case 1:
					Console.Write("Enter strain label: ");
					virus.Strain = Console.ReadLine();
					Console.Write("Done.");
					break;
				case 2:
					Console.Write("Enter new sick count: ");
					int newSick = 0;
					if(Int32.TryParse(Console.ReadLine(), out newSick)){
						virus.Sick = newSick;
						Console.Write("Done.");
					}
					else 
						Console.Write("Failed.");
					break;
				case 3:					
					Console.Write("Enter new dead count: ");
					int newDead = 0;
					if(Int32.TryParse(Console.ReadLine(), out newDead)) {
						virus.Dead = newDead;
						Console.Write("Done.");
					}
					else 
						Console.Write("Failed.");
					break;
				case 4:					
					Console.Write("Enter new spread probability(%, 0 to 100, like 71 or 29): ");
					int newSpreadProbability = 0;
					if(Int32.TryParse(Console.ReadLine(), out newSpreadProbability)) {
						virus.SpreadProbability = newSpreadProbability / 100.0f;
						Console.Write("Done.");
					} else 
						Console.Write("Failed.");
					break;
				case 5:
					Console.WriteLine(virus.ToString());
					break;
				case 6:
					if(COVID19.turnOnG5())
						Console.WriteLine("G5 turned on. Be ready to die.");
					else 
						Console.WriteLine("G5 turned off. Mankind saved.");
					break;
				case 7:
					Console.Write("Enter desired time for spread in seconds: ");
					int time = 0;
					if(Int32.TryParse(Console.ReadLine(), out time)) {
						virus.spread(time);
						Console.WriteLine("Virus spreads. Use \"print all\" to see results.");
					} else 
						Console.Write("Failed, unacceptable time value.");
					break;
				case 8:
					Console.WriteLine("Applying cure");
					virus.cure();
					break;
				case 9:
					virus.mutate();
					Console.WriteLine("Virus mutating. Use \"print all\" to see results.");
					break;
				default: 
					Console.WriteLine("No such option");
					break;
				}
			} while (option != 0);
        }
    }
}
