using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chess;

namespace ChessWeb.Models
{
    public class Logic
    {
        private ModelChessDB db;
        

        public Logic()
        {
            db = new ModelChessDB();
            
        }

        public Game GetGame(int id)
        {
            return db.Games.Find(id);
        }


        public Game GetCurrentGame()
        {
            Game game = db.Games
                .Where(g => g.Status == "play")
                .OrderBy(g => g.ID)
                .FirstOrDefault() ;
            if (game == null)
            {
                game = CreateNewGame();
            }
            return game;
            
        }

        private Game CreateNewGame()
        {
            Chess.Chess chess = new Chess.Chess();

            Game game = new Game();
            game.FEN = chess.fen;
            game.Status = "play";

            db.Games.Add(game);
            db.SaveChanges();
            return game;
        }

        public Game MakeMove(int id, string move)
        {
            Game game =  GetGame(id);
            if (game == null) return game;

            if(game.Status != "play") return game;

            Chess.Chess chess = new Chess.Chess(game.FEN);
            Chess.Chess chessNext = chess.Move(move);

            if(chessNext.fen == game.FEN) return game;

            game.FEN = chessNext.fen;

            
            db.Entry(game).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return game;
             
        }

    }
}