using System;
using System.IO;

namespace Automatas
{
    public class Lexico : Token
    {
        StreamReader archivo;
        StreamWriter log;

        const int F = -1;
        const int E = -2;
        
        public Lexico()
        {
            archivo = new StreamReader("/Users/52442/Documents/Lenguajes y Automatas I/Automatas/Entrada.txt");
            log = new StreamWriter("/Users/52442/Documents/Lenguajes y Automatas I/Automatas/Salida.txt");
            log.AutoFlush = true;
        }

        public Lexico(string filename)
        {
            archivo = new StreamReader(filename);
            log = new StreamWriter("/Users/guillermofernandezromero/Documents/Proyectos/Automatas/Salida.txt");
            log.AutoFlush = true;
        }

        ~Lexico()
        {
            Console.WriteLine("Destructor");
        }

        private int Automata(int Estado, char t)
        {
            int SiguienteEstado = Estado;

            switch (Estado)
            {
                case 0:
                    if (char.IsWhiteSpace(t))
                    {
                        SiguienteEstado = 0;
                    }
                    else if (char.IsLetter(t))
                    {
                        SiguienteEstado = 1;
                    }
                    else if (char.IsDigit(t))
                    {
                        SiguienteEstado = 2;
                    }
                    

                    else
                    {
                        SiguienteEstado = 28;
                    }
                    break;
                case 1:
                    SETClasificacion(Tipos.Identificador);
                    if (char.IsLetterOrDigit(t))
                    {
                        SiguienteEstado = 1;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 2:
                    SETClasificacion(Tipos.Numero);
                    if (char.IsDigit(t))
                    {
                        SiguienteEstado = 2;
                    }
                    else if (t == '.')
                    {
                        SiguienteEstado = 3;
                    }
                    /*
                    else if (char.tolower(t)=='e')
                    {
                        SiguienteEstado = 5;
                    }
                    */
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 3:
                    if (char.IsDigit(t))
                    {
                        SiguienteEstado = 4;
                    }
                    else
                    {
                        SiguienteEstado = E;
                    }
                    break;
                case 4:
                    if (char.IsDigit(t))
                    {
                        SiguienteEstado = 4;
                    }
                    /*
                    else if (char.tolower(t)=='e')
                    {
                        SiguienteEstado = 5;
                    }
                    */
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 5: 
                    if(t == '+' || t == '-')
                    {
                        SiguienteEstado = 6;
                    }
                    else if (char.IsDigit(t))
                    {
                        SiguienteEstado = 7;
                    }
                    else
                    {
                        SiguienteEstado = E;
                    }
                    break;
                case 6: 
                    if(char.IsDigit(t))
                    {
                        SiguienteEstado = 7;
                    }
                    else
                    {
                        SiguienteEstado = E;
                    }
                    break;
                case 7:
                    if(char.IsDigit(t))
                    {
                        SiguienteEstado = 7;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 8:
                    SETClasificacion(Tipos.Asignacion);
                    if (t == '=')
                    {
                        SiguienteEstado = 9;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 9:
                    SETClasificacion(Tipos.OperadorRelacional);
                    SiguienteEstado = F;
                    break;
                case 10:
                    SETClasificacion(Tipos.Caracter);
                    if(t == '=')
                    {
                        SiguienteEstado = 11;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 11:
                    SETClasificacion(Tipos.Inicializacion);
                    SiguienteEstado = F;
                    break;
                case 12:
                    SETClasificacion(Tipos.FinSentencia);
                    SiguienteEstado = F;
                    break;
                case 13:
                    SETClasificacion(Tipos.Caracter);
                    if(t == '&')
                    {
                        SiguienteEstado = 14;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 14:
                    SETClasificacion(Tipos.OperadorLogico);
                    SiguienteEstado = F;
                    break;
                case 15:
                    SETClasificacion(Tipos.Caracter);
                    if(t == '|')
                    {
                        SiguienteEstado = 14;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 16:
                    SETClasificacion(Tipos.OperadorLogico);
                    if(t == '=')
                    {
                        SiguienteEstado = 18;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 17:
                    SETClasificacion(Tipos.OperadorRelacional);
                    if(t == '=')
                    {
                        SiguienteEstado = 18;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 18:
                    SETClasificacion(Tipos.OperadorRelacional);
                    SiguienteEstado = F;
                    break;
                case 19:
                    SETClasificacion(Tipos.OperadorRelacional);
                    if(t == '=' || t == '>')
                    {
                        SiguienteEstado = 18;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 20:
                    SETClasificacion(Tipos.OperadorTermino);
                    if(t == '+' || t == '=')
                    {
                        SiguienteEstado = 22;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 21:
                    SETClasificacion(Tipos.OperadorTermino);
                    if(t == '-' || t == '=')
                    {
                        SiguienteEstado = 22;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 22:
                    SETClasificacion(Tipos.IncrementoTermino);
                    SiguienteEstado = F;
                    break;
                case 23:
                    SETClasificacion(Tipos.OperadorFactor);
                    if(t == '=')
                    {
                        SiguienteEstado = 24;
                    }
                    else
                    {
                        SiguienteEstado = F;
                    }
                    break;
                case 24:
                    SETClasificacion(Tipos.IncrementoFactor);
                    SiguienteEstado = F;
                    break;
                case 25:
                    SETClasificacion(Tipos.Ternario);
                    SiguienteEstado = F;
                    break;
                case 26:

                    break;
                case 27:
                    SETClasificacion(Tipos.Cadena);
                    SiguienteEstado = F;
                    break;
                case 28:
                    SETClasificacion(Tipos.Caracter);
                    SiguienteEstado = F;
                    break;
            }
            return SiguienteEstado;
        }

        public void nextToken()
        {
            string  buffer = "";
            char    transicion;

            int Estado = 0;

            while (Estado >= 0)
            {
                transicion = (char) archivo.Peek();

                Estado = Automata(Estado, transicion);

                if (Estado >= 0)
                {
                    if (Estado > 0)
                    {
                        buffer += transicion;
                    }
                    archivo.Read();
                }
            }

            SETContenido(buffer);

            if (GETClasificacion() == Tipos.Identificador)
            {
                switch (GETContenido())
                {
                    case "char":
                    case "int":
                    case "float": SETClasificacion(Tipos.TipoDato); break;

                    case "private":
                    case "protected":
                    case "public": SETClasificacion(Tipos.Zona); break;

                    case "if":
                    case "else":
                    case "switch": SETClasificacion(Tipos.Condicion); break;

                    case "while":
                    case "do": 
                    case "for": SETClasificacion(Tipos.Ciclo); break;
                }
            }
            log.WriteLine(GETContenido() + " = " + GETClasificacion());  
        }	

        public void cerrarArchivos()
        {
            log.Close();
            archivo.Close();
        }

        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}