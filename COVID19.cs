using System;

namespace lab_1
{
    class COVID19 {
		public const string labname = "Разработка иерархии классов"; // делать вразумительную константу было лень
        private string strain;          // штамм      
        private int sick;               // кол-во больных данным вирусом
        private int dead;               // кол-во погибших от данного вируса
        private float spreadProbability;// вероятность распространения в секунду (в %)
		private int mutated = 0;
		private static bool G5isTurnedOn = false;

		public COVID19() {
			// дефолтный вирус
			Strain = "SarS-2";
			Sick = 10000;
			Dead = 5000; 
			SpreadProbability = 0.7f;
		}

		public COVID19(string strain, float spreadProbability) {
			//свойства использованы для того, чтобы не дать установить никакие 
			//значения в обход проверок, хоть технически и можно было установить значения полей напрямую
			this.Strain = strain;
			this.SpreadProbability = spreadProbability;
		}

		public COVID19(string strain, int sick, int dead, float spreadProbability) {
			this.Sick = sick;
			this.Dead = dead;
			this.Strain = strain;
			this.SpreadProbability = spreadProbability;
		}

        public float SpreadProbability {
            get { return spreadProbability; }
            set 
            { 
                if(value > 1) spreadProbability = 1;
                else if(value < 0) spreadProbability = 0;
				else spreadProbability = value;
            }
        }

        public float getMortality() {
            if(sick == 0) return 0;
			else return (float)dead / sick;    
            //мертвых не может быть больше чем больных, так что доп. проверки не требуются
            //за это отвечает геттер поля dead
        }

        public int Dead {
            get { return dead; }
            set 
            { 
                if(value < 0) dead = 0;  
                else if(value > sick) dead = sick;
                else dead = value;
                // сообщения о неверном вводе / проброс исключения опущен для
                // упрощения кода, в случае чего их несложно дописать
            }
        }

        public int Sick {
            get { return sick; }
            set 
            { 
				if (value <= 0)
					sick = dead;
				else if (value < dead)
					sick = dead; 	// будем считать всех мертвых заражёнными
									// в таком случае, кол-во заражённых не может быть меньше кол-ва мертвых
									// так как мертвые - заражены
                else sick = value;
            }
        }

        public string Strain {
            get { return strain; }
            set 
            {
				if (String.IsNullOrEmpty (value))
					return;
				else
					strain = value;
            }
        }

		/// <summary>
		/// Распространяться в течение определённого времени (в секундах).
		/// </summary>
		/// <param name="time">Время в секундах</param>
		/// <returns>Новое кол-во заболевших</returns>
		public int spread(int time){
			if(time < 0) return sick;
			if (G5isTurnedOn) {
				sick += time;
				dead += time;
			} else {
				Random ran = new Random ();
				for (int i = 0, isSpreaded = 0; i < time; i++) {
					isSpreaded = ran.Next (0, 100);
					if (isSpreaded < spreadProbability * 100) {
						sick++;
					}
				
				}
			}
			return sick;
		}

		public void cure(){
			if (!G5isTurnedOn) {
				spreadProbability /= 2;
				Console.WriteLine ("Spread probability lowered.");
			}
			else
				Console.WriteLine ("G5 is turned on, cure is ineffective.");
		}

		public void mutate(){
			Random ran = new Random();
			int newSpreadProbability = ran.Next(0, 100);
			spreadProbability = newSpreadProbability / 100.0f;
			mutated++;
		}

		/// <summary>
		/// Активирует/деактивирует G5. 
		/// Включение G5 делает вероятность распространения вируса 100%, а 
		/// все новые больные при распространении вируса гарантированно умирают. 
		/// </summary>
		public static bool turnOnG5(){
			return G5isTurnedOn = !G5isTurnedOn;
		}

		public override string ToString(){
			string mutationString = mutated > 0 ? $"-{mutated}" : "";
			return $"Strain: {strain}{mutationString}, sick: {sick}, dead: {dead}, spread probability: {G5isTurnedOn ? 100 : spreadProbability * 100}%, virus mortality is {getMortality()*100}%";
		}
    }
}
