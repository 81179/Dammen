using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    public bool isWhite;
    public bool isKing;

	public bool IsFocedToMove(Piece[,] board, int x, int y)
	{
		if (isWhite || isKing) {
			//Top left
			if (x >= 2 && y <= 5) {
				Piece p = board [x - 1, y + 1];
				//If there is a piece, and its not the same color
				if (p != null && p.isWhite != isWhite) {
					//Check if its possible to land after the jump
					if (board [x - 2, y + 2] == null)
						return true;
				}
			}
			//Top right
			if (x <= 5 && y <= 5) {
				Piece p = board [x + 1, y + 1];
				//If there is a piece, and its not the same color
				if (p != null && p.isWhite != isWhite) {
					//Check if its possible to land after the jump
					if (board [x + 2, y + 2] == null)
						return true;
				}
			}
		} 
		if(!isWhite || isKing){
			//Bottom left
			if (x >= 2 && y >= 2) {
				Piece p = board [x - 1, y - 1];
				//If there is a piece, and its not the same color
				if (p != null && p.isWhite != isWhite) {
					//Check if its possible to land after the jump
					if (board [x - 2, y - 2] == null)
						return true;
				}
			}
			//Bottom right
			if (x <= 5 && y >= 2) {
				Piece p = board [x + 1, y - 1];
				//If there is a piece, and its not the same color
				if (p != null && p.isWhite != isWhite) {
					//Check if its possible to land after the jump
					if (board [x + 2, y - 2] == null)
						return true;
				}
			}
		}
		return false;

	}

    public bool ValidMove(Piece[,] board, int x1, int y1, int x2, int y2)
    {
        //If you moving on top of a other piece
        if (board[x2, y2] != null)
            return false;

        //Get amout of boxes moved
        int deltaMove = Mathf.Abs(x1 - x2);
        int deltaMoveY = y2 - y1;
        if (isWhite || isKing)
        {
            //If piece moves 1 box
            if(deltaMove == 1)
            {
                if (deltaMoveY == 1)
                    return true;
            }
            //If piece moves 2 boxes
            else if(deltaMove == 2)
            {
                if(deltaMoveY == 2)
                {
                    Piece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (p != null && p.isWhite != isWhite)
                        return true;
                }
            }
        }

        if (!isWhite || isKing)
        {
            //If piece moves 1 box
            if (deltaMove == 1)
            {
                if (deltaMoveY == -1)
                    return true;
            }
            //If piece moves 2 boxes
            else if (deltaMove == 2)
            {
                if (deltaMoveY == -2)
                {
                    Piece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (p != null && p.isWhite != isWhite)
                        return true;
                }
            }
        }
        return false;
    }
}
