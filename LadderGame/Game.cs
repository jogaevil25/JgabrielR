using System;

namespace SnakeLadderGame
{
    class Game
    {
        Player currentPlayer;
        Cell[] board;
        Player[] playerQueue;
        int totalPlayers;
        public Game(int BoardSize, int NumberOfPlayers)
        {
            totalPlayers = NumberOfPlayers;
            board = CreateBoard(BoardSize);
            playerQueue = AssignPlayers(totalPlayers);
        }
        private Cell[] CreateBoard(int boardSize)
        {
            Cell[] board = new Cell[boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                Cell c = new Cell();
                c.NumeroCelda = i + 1;
                board[i] = c;
            }
            bool isSnakeCellLeft = true;
    
            

            bool isLadderCellLeft = true;
            while (isLadderCellLeft)
            {
                Console.WriteLine("Quiere definir la escalera?? (|-|-|-|-|-|-|) s/n");
                if (Console.ReadLine().ToLower() == "s")
                {
                    Console.WriteLine("Numero de celda donde se va a encontrar la escalera");
                    int ladderCellNumber = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Ventaja por el numero de celda");
                    int advantageCellNumber = Convert.ToInt32(Console.ReadLine());
                    LadderCell l = new LadderCell();
                    l.NumeroCelda = ladderCellNumber;
                    l.CeldaVentaja = advantageCellNumber;
                    board[ladderCellNumber - 1] = l;
                }
                else
                {
                    isLadderCellLeft = false;
                }
            }
            return board;
        }
        private Player[] AssignPlayers(int numberOfPlayers)
        {
            Player[] players = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i] = new Player();
                players[i].PosicionCeldaActual = 0;
                players[i].NumeroJugador = i + 1;
                Console.WriteLine("Ingrese el nombre");
                players[i].NombreJugador = Console.ReadLine();
            }
            return players;
        }
        private int RollDice()
        {
                Random rnd = new Random();
                return rnd.Next(1, 6);
        }
        private void NextChance()
        {
            if (currentPlayer.NumeroJugador< totalPlayers)
            {
                currentPlayer = playerQueue[(currentPlayer.NumeroJugador - 1) + 1];
            }
            else
            {
                currentPlayer = playerQueue[0];
            }
        }        
        private void CalculatePlayerPosition(int diceNumber)
        {
            Console.WriteLine(currentPlayer.NombreJugador + ", tu dado muestra " +diceNumber);
            int moveLocation = currentPlayer.PosicionCeldaActual;
            if ((moveLocation + diceNumber) <= board.Length)
            {
                moveLocation = moveLocation + diceNumber;
                Console.WriteLine(currentPlayer.NombreJugador + ", se mueve hacia " + moveLocation);
            }
            else
            {
                Console.WriteLine(currentPlayer.NombreJugador + ", se queda en " + moveLocation);
            }
            
         
                if (board[moveLocation - 1].GetType() == typeof(LadderCell))
                {
                    moveLocation = (board[moveLocation - 1] as LadderCell).CeldaVentaja;
                    Console.WriteLine(currentPlayer.NombreJugador + " escalera encontrada :D , se mueve " + moveLocation + " posciones");
                }
            
            currentPlayer.PosicionCeldaActual = moveLocation;
        }
        public void Play()
        {
            currentPlayer = playerQueue[0];
            bool isFirstMove = true;
            while (currentPlayer.PosicionCeldaActual != board.Length)
            {                
                if (!isFirstMove)
                {
                    NextChance();
                }
                isFirstMove = false;
                CalculatePlayerPosition(RollDice());
            }
            Console.WriteLine(currentPlayer.NombreJugador + " GANA");
            foreach (Player p in playerQueue)
            {
                Console.WriteLine(p.NombreJugador + " esta en "+ p.PosicionCeldaActual);
            }
            Console.WriteLine("Game Over!!!");
        }
    }
}
