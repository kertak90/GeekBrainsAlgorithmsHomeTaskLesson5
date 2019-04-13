using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Задание
            /*
             * Пинарин Олег
                1.Реализовать перевод из 10 в 2 систему счисления с использованием стека.
                2.Добавить в программу “реализация стека на основе односвязного списка” проверку на выделение памяти. Если память не выделяется, то выводится соответствующее сообщение.Постарайтесь создать ситуацию, когда память не будет выделяться(добавлением большого количества данных).
                3.Написать программу, которая определяет, является ли введенная скобочная последовательность правильной.Примеры правильных скобочных выражений: (), ([])(), { } (), ([{ }]), неправильных — )(, ())({), (, ])}), ([(]) для скобок[, (,{.
                Например: (2 + (2 * 2)) или[2 /{ 5 * (4 + 7)}]

                4. * Создать функцию, копирующую односвязный список(то есть создает в памяти копию односвязного списка, без удаления первого списка).
                5. * *Реализовать алгоритм перевода из инфиксной записи арифметического выражения в постфиксную.
                6. * Реализовать очередь.
            */
            #endregion Задание
            
            int answer;
            do
            {
                Console.Clear();
                mainMenu();
                Console.WriteLine("Введите номер задачи");
                try
                {
                    answer = Convert.ToInt16(Console.ReadLine());
                    switch (answer)
                    {
                        case 3:
                            Task3();
                            break;
                        case 5:
                            Task5();
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Неправильный ввод!!!");
                }   
            } while (true);
        }

        static void mainMenu()
        {
            Console.WriteLine("3.Написать программу, которая определяет, является ли введенная скобочная последовательность \n" +
                                "правильной.Примеры правильных скобочных выражений: (), ([])(), { } (), ([{ }]), \n" +
                                "неправильных — )(, ())({), (, ])}), ([(]) для скобок[, (,{. Например: (2 + (2 * 2)) или[2 /{ 5 * (4 + 7)}]\n\n");
            Console.WriteLine("5. **Реализовать алгоритм перевода из инфиксной записи арифметического выражения в постфиксную.");
        }

        static void Task3()
        {
            //3.Написать программу, которая определяет, является ли введенная скобочная последовательность правильной.Примеры правильных скобочных выражений: (), ([])(), { } (), ([{ }]), неправильных — )(, ())({), (, ])}), ([(]) для скобок[, (,{.
            //    Например: (2 + (2 * 2)) или[2 /{ 5 * (4 + 7)}]
            string example = "()()()(())";
            Console.WriteLine("Введите строку:");
            example = Console.ReadLine();
            char[] allowedChars = new char[] {'(', ')', '[', ']', '{', '}'};
            char[] stack = new char[1000];                          //Объявили и инициализировали массив char
            int index = 0;
            bool rightString = true;
            char c;
            if(example != null && example != "")
            {
                for (int i = 0; i < example.Length; i++)
                {
                    c = example[i];
                    if (allowedChars.Contains(c))                   //принадлежит ли данный символ массиву allowedChars, если да, то
                    {
                        if (c == '(' || c == '{' || c == '[')       //Если символ скобки открывающий, то пушим его в стек
                        {
                            stack[index] = c;
                            index++;
                        }                       
                        else if ((c == ')' || c == '}' || c == ']') && index >= 1)            //Если символ закрывающй, то сравниваем его с последним добавленным
                        {
                            if (stack[index - 1] == '[')
                            {
                                index--;
                                continue;
                            }
                                
                            if (stack[index - 1] == '{')
                            {
                                index--;
                                continue;
                            }
                                
                            if (stack[index - 1] == '(')            //Если закрывающий символ противоположен последнему добавленному, то инкрементируем индекс
                            {
                                index--;
                                continue;
                            }                                
                            else                                    //Если символы не противоположны друг другу, то строка неверная 
                                rightString = false;                //флаг правильности строки                             
                        }
                        else
                        {
                            rightString = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                rightString = false;
            }            

            if (index==0 && rightString == true)                    //Если после проверки строки, стек открывающих скобок остался пуст, то 
                Console.WriteLine("Строка верная!!!");
            else
            {
                Console.WriteLine("Строка не верная!!!");
            }                
            Console.ReadLine();
        }

        static void Task5()
        {
            //5. * *Реализовать алгоритм перевода из инфиксной записи арифметического выражения в постфиксную.
            string Example = "(2 + (2 * 2)) или[2 /{ 5 * (4 + 7)}]";
            Example = "(2+(2*2))";
            Example = "5*(4+7)";
            Example = "((1+2)*10-(8-5)*5)/10";
            Console.WriteLine("Введите арифметическую операцию:");
            var str = Console.ReadLine();
            Example = (str == null || str == "") ? Example : str;
            List<string> outputSeparated = new List<string>();
            List<string> ariphmeticChars = new List<string> { "(", ")", "+", "-", "*", "/", "^" };
            List<string> stack = new List<string>();

            foreach (var ch in GetVariable(Example))   
            {
                if(ariphmeticChars.Contains(ch))                                                        //Если строка является арифметической операцией, то
                {
                    if (stack.Count > 0 && !ch.Equals("("))                                             //Если в техасе есть вагоны, и данный вагон на стрелке не открывающий символ скобки, то
                    {
                        if (ch.Equals(")"))                                                             //если вагон на стрелке закрывающий символ скобки, то
                        {
                            string s = stack[stack.Count - 1];                                          //Вынимаем вагоны из техаса в обратном порядке, до тех пор пока не будет открывающей скобки
                            stack.RemoveAt(stack.Count - 1);
                            while (s != "(")
                            {
                                outputSeparated.Add(s);
                                s = stack[stack.Count - 1];
                                stack.RemoveAt(stack.Count - 1);
                            }
                        }
                        else                                                                            //Если вагон на стрелке не символ закрывающей скобки, то 
                        {
                            if (GetPriority(ch) > GetPriority(stack[stack.Count - 1]))                  //Если приоритет теущего арифметического вагона на стрелке выше чем последнего вагона в техасе
                                stack.Add(ch);                                                          //поместим вагон со стрелки в техас
                            else                                                                        //Если приоритет последнего вагона из техаса выше чем приоритет вагона на стрелке, то 
                            {
                                while (stack.Count > 0 && GetPriority(ch) <= GetPriority(stack[stack.Count - 1]))
                                {
                                    string s = stack[stack.Count - 1];                                  //извлечем вагоны из техаса в обратном порядке до тех пор пока приоритеты вагонов техаса выше вагона со стрелки
                                    stack.RemoveAt(stack.Count - 1);
                                    outputSeparated.Add(s);                                             //и отправим их в калифорнию
                                }
                                stack.Add(ch);                                                          //В техас поместим вагон со стрелки
                            }
                        }                        
                    }
                    else
                        stack.Add(ch);                                                                  //Если в техасе нет арифметических вагонов, поместим туда вагон со стрелки
                }
                else
                    outputSeparated.Add(ch);                                                            //Если строка не арифметическая операция, то просто отправим ее в калифорнию
            }
            if(stack.Count > 0)
                foreach(string c in stack)
                {
                    outputSeparated.Add(c);
                }
            foreach (var c in outputSeparated)
                Console.Write(c + " ");           
            Console.ReadLine();
        }

        static IEnumerable<string> GetVariable(string Example)
        {
            List<char> ariphmeticChars = new List<char> { '(', ')', '+', '-', '*', '/', '^' };          //Список арифметических операций и скобок между которыми находятся переменные
            int index = 0;
            while (index < Example.Length)                                                              //Пока инекс перебора символов строки меньше исходной строки
            {
                string s = Example[index].ToString();                                                   //Взяли символ по индексу
                if (!ariphmeticChars.Contains(Convert.ToChar(s)))                                       //если символ является арифметической операцией, то вернем символ арифметической операции, иначе вернем константы или названия переменных
                {
                    if (Char.IsDigit(Example[index]))                                                   //если символ является цифрой, то
                    {
                        for (int j = index + 1; j < Example.Length && Char.IsDigit(Example[j]); j++)    //продолжаем перебор строки, до тех пор пока идут цифры 
                        {
                            s += Example[j];                                                            //добавляем их в результирующую строку s, в итоге мы соберем целое число
                        }
                    }
                    else if (Char.IsLetter(Example[index]))                                             //Если символ является буквой, то проболжаем перебирать до тех пор пока буквы или буквы с цифрами: А или А5
                        for (int k = index + 1; k < Example.Length && (Char.IsLetter(Example[k]) || Char.IsDigit(Example[k])); k++)
                            s += Example[k];                                                            //Добавляем все буквы в результирующую строку, в итоге мы соберем  название переменной
                }
                yield return s;                                                                         //Возвращаем результат в виде названия названия переменной или последовательности цифр
                index += s.Length;                                                                      //Сдвинем текущий индекс на длину s
            }
        }

        static int GetPriority(string s)
        {
            switch (s)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                default:
                    return 4;
            }
        }
    }
}
