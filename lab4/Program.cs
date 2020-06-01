using System;

namespace lab4
{
    class Program
    {
        class Grammar
        {
            string sentence;
            int position;

            public Grammar(string sentence)
            {
                this.sentence = sentence.Replace(" ", String.Empty);
                this.position = 0;
            }

            public bool Check()
            {
                if (S() && sentence.Length == position)
                    return true;
                else
                    return false;
            }

            // S -> if C then P else P;
            public bool S()
            {
                try
                {
                    if (sentence[position] == 'i' && sentence[++position] == 'f')
                    {
                        ++position;
                        C();

                        if (sentence.Substring(position, 4) == "then")
                        {
                            position += 4;
                            P();

                            if (sentence.Substring(position, 4) == "else")
                            {
                                position += 4;
                                P();

                                if (sentence[position] == ';')
                                    ++position;
                                 else return false;  
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }

                return true;
            }

            // C -> VB
            private bool C()
            {
                V();
                B();

                return true;
            }

            // B -> >= V | =< V
            private bool B() 
            { 
                try
                {
                    if (sentence.Substring(position, 2) == ">=" || sentence.Substring(position, 2) == "=<")
                    {
                        position += 2;
                        V();
                    }
                    else return false;
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }

                return true;
            }

            // V -> aD | bD | cD
            private bool V() 
            { 
                try
                {
                    if ("abc".Contains(sentence[position]))
                    {
                        ++position;
                        D();
                    }
                    else return false;
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }

                return true;
            }

            // D -> 1{D} | 2{D} | 3{D}
            private bool D() 
            { 
                try
                {
                    if ("123".Contains(sentence[position]))
                    {
                        ++position;
                        while ("123".Contains(sentence[position]))
                        {
                            ++position;
                            D();
                        }
                    }
                    else return false;
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }

                return true;
            }

            // P -> V := F | S
            private bool P() 
            {
                int pos = position;
                if (V())
                {
                    try
                    {
                        if (sentence.Substring(position, 2) == ":=")
                        {
                            position += 2;
                            if (F())
                                return true;
                            else
                            {
                                position = pos;
                                return S();
                            }
                        }
                        else
                        {
                            position = pos;
                            return S();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return false;
                    }
                    
                }
                else
                {
                    position = pos;
                    return S();
                }
            }

            // F -> V | D
            private bool F() 
            {
                int pos = position;
                if (V())
                    return true;
                else
                {
                    position = pos;
                    return D();
                }
            }
        }

        static void Main(string[] args)
        {
            /* Examples
            string ex1 = "if a1 >= b2 then c3 := a1 else c3 := 3;";
            string ex2 = "if a1 =< b2 then c3 := a12 else c3 := 333;";
            string ex3 = "if a1 >= b2 then if a1 =< a2 then b1 := 1 else a3 := c3; else c3 := 333;";
            string ex4 = "if a1 >= b2 then if a1 =< a2 then b1 := 1 else a3 := c3; else if b123 >= b321 then b123 := 123 else b321 := 321;;";
            */

            Console.WriteLine("Enter the sentence: ");
            string givenSentence = Console.ReadLine();
            Grammar gr = new Grammar(givenSentence);

            if (gr.Check())
                Console.WriteLine("\nThe given sentence is a part of the grammar.");
            else
                Console.WriteLine("\nThe given sentence isn't a part of the grammar.");
        }
    }
}
