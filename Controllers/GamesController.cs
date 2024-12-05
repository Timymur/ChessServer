using ChessWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ChessWeb.Controllers
{
    public class GamesController : ApiController
    {
        private ModelChessDB db = new ModelChessDB();

        public Game GetGames()
        {
            Logic logic = new Logic();
            return logic.GetCurrentGame();

            
        }

        [ResponseType(typeof(Game))]
        public Game GetGame(int id)
        {
            Logic logic = new Logic();
            return logic.GetGame(id);
        }

        public Game GetMove(int id, string move)
        {
            Logic logic = new Logic();
            Game game = logic.MakeMove(id, move);
            
            return game;
        }


    }
}
