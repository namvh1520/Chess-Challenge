﻿using ChessChallenge.API;
using System;

public class MyBot : IChessBot
{

    int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };

    public Move Think(Board board, Timer timer)
    {

        Move[] allMoves = board.GetLegalMoves();

        Random rng = new();
        Move moveToPlay = allMoves[rng.Next(allMoves.Length)];
        int highestValueCapture = 0;

        foreach (Move move in allMoves)
        {
            // Always play checkmate in one
            if (MoveIsCheckmate(board, move))
            {
                moveToPlay = move;
                break;
            }

            // Find highest value capture
            Piece capturedPiece = board.GetPiece(move.TargetSquare);
            int capturedPieceValue = pieceValues[(int)capturedPiece.PieceType];

            if (capturedPieceValue > highestValueCapture)
            {
                moveToPlay = move;
                highestValueCapture = capturedPieceValue;
            }
        }

        return moveToPlay;
    }

    bool MoveIsCheckmate(Board board, Move move)
    {
        board.MakeMove(move);
        bool isMate = board.IsInCheckmate();
        board.UndoMove(move);
        return isMate;
    }
}