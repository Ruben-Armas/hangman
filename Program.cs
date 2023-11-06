/*	juego del ahorcado
	Requisitos mínimos:
 a. No debes usar programación orientada a objetos.
 b. El juego se ejecutará en la terminal (consola).
 c. El jugador tendrá 6 oportunidades para averiguar la palabra.
 d. Las palabras posibles deben estar almacenadas en una lista de strings.
 e. El programa debe seleccionar una palabra aleatoria de 10 precargadas en la lista.
 f. Tras cada intento, se debe mostrar en la pantalla cuántas oportunidades quedan.
 g. Debes gestionar que el usuario no introduzca más de una letra por intento.
 h. Cada vez que se introduce una letra correcta se debe mostrar cómo se va formando
 la palabra.
 i. Si se acaban los intentos y no se ha acertado la palabra, se debe mostrar un mensaje por haber perdido.
 j. Si aciertas todas las letras de la palabra se debe mostrar un mensaje de victoria.
 k. Genera todas las funciones que estimes necesarias para programar el juego.
	Extra:
 l. Mejora el código anterior para que permita introducir una o más palabras a mano al principio de cada partida y que así otra persona pueda tratar de adivinar la palabra que has elegido.
*/




using System;
using System.Diagnostics.Metrics;
/**
 * 1. Crea una lista de tipo string.
 * 2. Crea una variable global de tipo integer con el número de intentos restantes.
 * 3. Crea la función principal del juego y llámala Hangman(). Está función debe contener el bucle de juego así como las llamadas al resto de funciones desarrolladas.
 * 4. Crea una función void denominada MostrarCabecera() que muestre información al comienzo del juego.
 * 5. Crea una función void denominada PrecargarPalabras() en la que guardes el listado de todas las palabras dentro de la lista de tipo string.
 * 6. Crea una función denominada SeleccionarPalabraAleatoria() que devuelva una palabra aleatoria (string) de la lista de palabras.
 * 7. Crea una función denominada OcultarPalabra() con un parámetro de entrada de tipo string que devuelva otra string en la que se hayan reemplazado todos los caracteres de la entrada por el símbolo “_”.
 * 8. Crea una función void denominada DibujarLineas() con un parámetro de entrada de tipo string que muestre en la pantalla la string recibida añadiendo un espacio después de cada carácter.
 * 9. Crea una función denominada IntentosRestantes() que devuelva el número de intentos almacenados en la variable global correspondiente.
 * 10. Crea una función denominada SolicitarLetra() que pida al usuario que teclee una única letra y una vez revisado que solo se ha puesto una letra, la devuelva en tipo char.
 * 11. Crea una función denominada ComprobarLetra() con un parámetro de entrada de tipo string y otro parámetro de entrada de tipo char. La función debe verificar si el carácter pasado como parámetro se encuentra contenido en la palabra contenida en la string. Si la encuentra devuelve true y si no, devuelve false.
 * 12. Crea una función void denominada DecrementarVidas() que decrementa en una unidad el contenido de la variable global que contiene el número de intentos restantes.
 * 13. Crea una función denominada ReemplazarLineas() con tres parámetros de entrada. El primer parámetro de tipo string representa la palabra que se desea adivinar, el segundo parámetro también de tipo string representa la palabra que se va adivinando poco a poco (la que contiene los caracteres “_”). El tercer parámetro es el carácter (tipo char) que se ha introducido. El propósito de la función es reemplazar las líneas “_” en las posiciones correctas. La función devuelve una string con las letras en las posiciones que se han adivinado. Por ejemplo: “_u_o”.
 * 14. Crea una función denominada HasGanado() con dos parámetros de entrada de tipo string que verifique si la palabra a adivinar es igual que la palabra sobre la que se han ido sustituyendo las letras. Si son iguales entonces la función debe devolver true. Si son diferentes devuelve false.
*/


string words = "";
int lives = 6;
string hideWord = "";

Hangman();

// 1.Crea una lista de tipo string.
// 2. Crea una variable global de tipo integer con el número de intentos restantes.
// 3. Crea la función principal del juego y llámala Hangman(). Está función debe contener el bucle de juego así como las llamadas al resto de funciones desarrolladas.
void Hangman() {

    // Bucle para llamar a las funciones
    MostrarCabecera();
    PrecargarPalabras();
    MostrarCabecera();
    string endWord = SeleccionarPalabraAleatoria();
    hideWord = OcultarPalabra(endWord);
    
    Console.WriteLine("-------------------\n" +
                      "PALABRA:\n");
    DibujarLineas(hideWord);
    char inputLetter = SolicitarLetra();

    while (lives > 1) {
        string ckLetter = "";
        if (ComprobarLetra(endWord, inputLetter)) {
            hideWord = ReemplazarLineas(endWord, hideWord, inputLetter);
            ckLetter += "-------------------\n" +
                "La letra '"+ inputLetter  + "' SÍ está en la palabra.";
        }
        else {
            DecrementarVidas();
            ckLetter += "-------------------\n" +
                "La letra '" + inputLetter + "' NO está en la palabra.";
        }

        int currentLives = IntentosRestantes();
        MostrarJugada(currentLives);    //Banner
        Console.WriteLine(ckLetter); //Letra SÍ o NO
        DibujarLineas(hideWord);        //Progreso
        Console.WriteLine("-------------------\n" +
               "VIDAS RESTANTES: " + currentLives);


        if (HasGanado(endWord, hideWord)) {
            Console.WriteLine("¡¡HAS GANADO!! :)");
            return;
        }
        
        if (currentLives > 0)
            inputLetter = SolicitarLetra();
    }
    Console.WriteLine("¡¡HAS PERDIDO!! :(");

}

// 4. Muestra información al comienzo del juego
void MostrarCabecera() {
    Console.Clear();
    Console.WriteLine("+----------------------------------+\n" +
                      "|  +---+   --------------------    |\n" +
                      "|  |   |   JUEGO: Ahorcado         |\n" +
                      "|  o   |   LENGUAJE: C#            |\n" +
                      "| /|\\  |   AUTOR: Rubén            |\n" +
                      "|      |   --------------------    |\n" +
                      "|      |   PROGRAMACIÓN Y MOTORES  |\n" +
                      "+----------------------------------+\n");

    Console.WriteLine("-------------------\n" +
                      "JUEGO DEL AHORCADO");

    Console.WriteLine("-------------------\n" +
                      "Adivina la palabra introduciendo las posibles\n" +
                      "letras. Puedes fallar un máximo de 5 veces.");
}
void MostrarJugada(int currentLives) {
    string head = "";
    string torso = "";
    string legs = "";

    switch (currentLives) {
        case 0:
            head = "  o   ";
            torso = " /|\\  ";
            legs = " / \\  ";
            break;
        case 1:
            head = "  o   ";
            torso = " /|\\  ";
            legs = " /    ";
            break;
        case 2:
            head = "  o   ";
            torso = " /|\\  ";
            legs = "      ";
            break;
        case 3:
            head = "  o   ";
            torso = " /|   ";
            legs = "      ";
            break;
        case 4:
            head = "  o   ";
            torso = " /    ";
            legs = "      ";
            break;
        case 5:
            head = "  o   ";
            torso = "      ";
            legs = "      ";
            break;
        case 6:
            head = "      ";
            torso = "      ";
            legs = "      ";
            break;
        default:
            Console.WriteLine("El número no coincide con ningún caso");
            break;
    }


    Console.Clear();
    Console.WriteLine("+----------------------------------+\n" +
                      "|  +---+   --------------------    |\n" +
                      "|  |   |   JUEGO: Ahorcado         |\n" +
                      "|"+ head + "|   LENGUAJE: C#            |\n" +
                      "|"+ torso + "|   AUTOR: Rubén            |\n" +
                      "|"+ legs + "|   --------------------    |\n" +
                      "|      |   PROGRAMACIÓN Y MOTORES  |\n" +
                      "+----------------------------------+\n");
}



// 5. Guarda el listado de todas las palabras en la string
void PrecargarPalabras() {
    int salir = 0;
    int cont = 1;
    words = "";
    Console.WriteLine("-------------------\n" +
        "Escriba tantas palabras como quiera intentar adivinar.\n" +
        "Pulse ENTER para añadir la palabra\n" +
        "Para salir introduzca un 1\n" +
        "-------------------\n");
    while (salir != 1) {
        Console.Write("Introduzca la palabra "+ cont +":");
        cont++;
        string read = Console.ReadLine();
        if (read == "1")
            salir = 1;
        else
            words += read + " ";
    }
}

// 6. Devuelve una palabra aleatoria de la string
string SeleccionarPalabraAleatoria() {
    words = words.Trim();   // Evita errores (quita espacios en los extremos)
    string[] wordsSplit = words.Split(' ');

    Random random = new Random();
    int randInt = random.Next(words.Length);

    return wordsSplit[randInt];
}

// 7. Recibe una string y devuelve una string en la que se han reemplazados los caracteres por _
string OcultarPalabra(string word) {
    string res = "";
    foreach (char caracter in word) {
        res += "_";
    }
    return res;
}

// 8. Muestra la string con un espacio después de cada letra
void DibujarLineas(string word) {
    string res = "";
    foreach (char caracter in word) {
        res += caracter + " ";
    }
    Console.WriteLine(res);
}

// 9. Devuelve el nº de intentos almacenados en la variable global
int IntentosRestantes() {
    return lives;
}

// 10. Solicita y comprueba que el usuario introduce UNA letra y la devuelve en tipo CHAR
char SolicitarLetra() {
    char letter = ' ';
    bool checkLetter = false;

    while (!checkLetter) {
        Console.WriteLine("-------------------\n" +
                          "Introduce una letra: ");
        string input = Console.ReadLine();

        if (input.Length == 1 && char.IsLetter(input[0])) {
            letter = char.ToLower(input[0]); // Convertir a minúsculas
            checkLetter = true;
        }
        else {
            Console.WriteLine("Por favor, introduce una sola letra válida.");
        }
    }
    return letter;
}

// 11. Recibe la palabra y la letra introducida, devuelve true/false si la letra está en la palabra
bool ComprobarLetra(string word, char letter) {
    return word.Contains(letter);
}

// 12. Decrementa en 1 la variable global de intentos restantes
void DecrementarVidas() {
    lives--;
}

/**	13. Recibe 3 parámetros
 *			string	-> palabra a adivinar
 *			string	-> palabra que voy adivinando/progreso (la de _ _ _ _)
 *			char	-> letra introducida
 *	Devuelve la string de progreso sustituyendo _ por la letra si es correcta
*/
string ReemplazarLineas(string endWord, string progress, char inputChar) {
    char[] progressArray = progress.ToCharArray();

    for (int i = 0; i < endWord.Length; i++) {
        if (endWord[i] == inputChar) {
            // Reemplaza el guion bajo por la letra
            progressArray[i] = inputChar;
        }
    }

    // Convierte el array de caracteres a una string
    return new string(progressArray);
}

/**	14. Recibe 2 parámetros
 *			string	-> Palabra a adivinar
 *			string	-> Palabra de progreso (_ _ _ _)
 *	Devuelve true/false según si la palabra a adivinar coincide con la de progreso
*/
bool HasGanado(string endWord, string progress) {
    if (endWord.Equals(progress))
        return true;
    else
        return false;
}