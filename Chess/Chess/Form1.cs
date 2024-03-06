using Chess.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public partial class Form1 : Form
    {
        bool first_click = true;
        int first_i, first_j; // click place
        int kingw_i = 7, kingw_j = 4;
        int kingb_i = 0, kingb_j = 4;
        bool kingWL = true, kingWR = true , kingBL = true, kingBR = true;// to Check is king Castling allowed or not ?

        public Form1()
        {
            InitializeComponent();
        }

        Button[,] bt = new Button[8, 8];
        int[,] color = new int[8, 8];   // 0->black  1->white  2->empty
        char[,] place = new char[8, 8]; // p->pown r->rook  b->bishop  n->knight  q->queen  k->king
        int[,] valid = new int[8, 8];   // 0->not valid  1->empty valid  2->valid attack 3->En Passant (eat by excess) 5->king Castling
        int to_paly = 1;
        char promo_to = '\0';
        bool promo = false;
        bool passant = false;
        int valid_pass_i = -10, valid_pass_j = -10;
        private void Form1_Load(object sender, EventArgs e)
        {
            mk_buttons();
            mk_piece();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int send_i = i;
                    int send_j = j;
                    bt[i, j].Click += (temp_i, temp_j) => click(send_i, send_j);
                }
            }
        }
        void click(int i, int j)
        {
            if (first_click && color[i, j] == to_paly)
            { 
                first_i = i;
                first_j = j;
                first_click = false;
                show_path(i, j);
            }
            else
            {
                first_click = true;
                if (valid[i, j] != 0) // valid click
                {
                    passant = false;
                    if (place[first_i, first_j] == 'k')// Make Castling invalid
                    {
                        if (to_paly == 0)
                        {
                            kingb_i = i;
                            kingb_j = j;
                            kingBL = false;
                            kingBR = false;
                        }
                        else
                        {
                            kingw_i = i;
                            kingw_j = j;
                            kingWL = false;
                            kingWR = false;
                        }
                    }
                    if (place[first_i, first_j] == 'r')// Make Castling invalid
                    {
                        if (kingBL && first_i == 0 && first_j == 0) kingBL = false;
                        if (kingBR && first_i == 0 && first_j == 7) kingBR = false;
                        if (kingWL && first_i == 7 && first_j == 0) kingWL = false;
                        if (kingWR && first_i == 7 && first_j == 7) kingWR = false;
                    }
                    if (place[first_i, first_j] == 'p')//Pown Promotion
                    {
                        if ((to_paly == 0 && i == 7) || (to_paly == 1 && i == 0))
                        {
                            Pawn_promotion pp = new Pawn_promotion();
                            pp.ShowDialog();
                            promo_to = pp.selected_item;
                            promo = true;
                        }
                        if (to_paly == 0)
                        {
                            if (first_i == 1 && i == 3)
                            {
                                passant = true;
                                valid_pass_j = j;
                                valid_pass_i = i;
                            }
                        }
                        else
                        {
                            if (first_i == 6 && i == 4)
                            {
                                passant = true;
                                valid_pass_j = j;
                                valid_pass_i = i;
                            }
                        }
                    }
                    if (valid[i, j] == 5)// king Castling (tabyit)
                    {
                        int rook_i = 0, rook_j = 0, rook_ni = 0, rook_nj = 0;
                        if (to_paly == 0)
                        {
                            if (j == 2)
                            {
                                rook_i = 0;
                                rook_j = 0;
                                rook_ni = 0;
                                rook_nj = 3;
                            }
                            else // 6
                            {
                                rook_i = 0;
                                rook_j = 7;
                                rook_ni = 0;
                                rook_nj = 5;
                            }
                        }
                        else
                        {
                            if (j == 2)
                            {
                                rook_i = 7;
                                rook_j = 0;
                                rook_ni = 7;
                                rook_nj = 3;
                            }
                            else // 6
                            {
                                rook_i = 7;
                                rook_j = 7;
                                rook_ni = 7;
                                rook_nj = 5;
                            }
                        }
                        delet_path();
                        color[i, j] = (i == 0 ? 0 : 1);
                        color[rook_ni, rook_nj] = (i == 0 ? 0 : 1);
                        color[first_i, first_j] = 2;
                        color[rook_i, rook_j] = 2;//rook
                        place[i, j] = 'k';
                        place[rook_ni, rook_nj] = 'r';
                        place[first_i, first_j] = '\0';
                        place[rook_i, rook_j] = '\0';
                        bt[i, j].BackgroundImage = bt[first_i, first_j].BackgroundImage;//king
                        bt[first_i, first_j].BackgroundImage = null;
                        bt[rook_ni, rook_nj].BackgroundImage = bt[rook_i, rook_j].BackgroundImage;
                        bt[rook_i, rook_j].BackgroundImage = null;
                    }
                    else
                    {
                        color[i, j] = color[first_i, first_j];
                        color[first_i, first_j] = 2;
                        place[i, j] = place[first_i, first_j];
                        place[first_i, first_j] = '\0';//empty
                        if (valid[i, j] == 3)
                        {
                            color[valid_pass_i, valid_pass_j] = 2;
                            place[valid_pass_i, valid_pass_j] = '\0';
                            bt[valid_pass_i, valid_pass_j].BackgroundImage = null;
                        }
                        delet_path();
                        if (promo)// if there if pown Promotion
                        {
                            promo = false;
                            place[i, j] = promo_to;
                            switch (promo_to)
                            {
                                case 'q':
                                    if (to_paly == 0)
                                        bt[i, j].BackgroundImage = Resources.QueenB;
                                    else
                                        bt[i, j].BackgroundImage = Resources.QueenW;
                                    break;
                                case 'r':
                                    if (to_paly == 0)
                                        bt[i, j].BackgroundImage = Resources.RookB;
                                    else
                                        bt[i, j].BackgroundImage = Resources.RookW;
                                    break;
                                case 'b':
                                    if (to_paly == 0)
                                        bt[i, j].BackgroundImage = Resources.BishopB;
                                    else
                                        bt[i, j].BackgroundImage = Resources.BishopW;
                                    break;
                                case 'n':
                                    if (to_paly == 0)
                                        bt[i, j].BackgroundImage = Resources.KnightB;
                                    else
                                        bt[i, j].BackgroundImage = Resources.KnightW;
                                    break;
                            }
                        }
                        else
                        {
                            bt[i, j].BackgroundImage = bt[first_i, first_j].BackgroundImage;
                        }
                        bt[first_i, first_j].BackgroundImage = null;
                    }

                    to_paly = (to_paly + 1) % 2;// swap 0 and 1

                    bt[kingw_i, kingw_j].BackgroundImage = Resources.KingW;
                    bt[kingb_i, kingb_j].BackgroundImage = Resources.KingB;

                    if (there_Check())
                    {
                        if (to_paly == 0)
                        {
                            bt[kingb_i, kingb_j].BackgroundImage = Resources.eat_KingB;
                        }
                        else
                        {
                            bt[kingw_i, kingw_j].BackgroundImage = Resources.eat_KingW;
                        }
                    }
                }
                else
                {
                    if (color[i, j] == to_paly)//Change piece (click on other piece to start it path)
                    {
                        delet_path();
                        click(i, j);
                    }
                    else // remove path
                    {
                        delet_path();
                    }
                }
            }
        }

        #region King Check
        bool there_Check()
        {
            int clrDash, x, y;// king point

            if (to_paly == 1)
            {
                x = kingw_i;
                y = kingw_j;
                clrDash = 0;
            }
            else
            {
                x = kingb_i;
                y = kingb_j;
                clrDash = 1;
            }

            if (knight_Check(x + 2, y + 1, clrDash) || knight_Check(x + 2, y - 1, clrDash) || knight_Check(x - 2, y + 1, clrDash) ||
                knight_Check(x - 2, y - 1, clrDash) || knight_Check(x + 1, y + 2, clrDash) || knight_Check(x - 1, y + 2, clrDash) ||
                knight_Check(x + 1, y - 2, clrDash) || knight_Check(x - 1, y - 2, clrDash)) return true;
        
            for (int i = x + 1; i < 8; i++)
            {
                if (color[i, y] != 2)
                {
                    if ((place[i, y] == 'r' || place[i, y] == 'q') && color[i, y] == clrDash)
                        return true;
                    else
                        break;
                }
            }// down rook or queen
            for (int i = x - 1; i >= 0; i--)
            {
                if (color[i, y] != 2)
                {
                    if ((place[i, y] == 'r' || place[i, y] == 'q') && color[i, y] == clrDash)
                        return true;
                    else
                        break;
                }
            }// up rook or queen
            for (int i = y + 1; i < 8; i++)
            {
                if (color[x, i] != 2)
                {
                    if ((place[x, i] == 'r' || place[x, i] == 'q') && color[x, i] == clrDash)
                        return true;
                    else
                        break;
                }
            }// right rook or queen
            for (int i = y - 1; i >= 0; i--)
            {
                if (color[x, i] != 2)
                {
                    if ((place[x, i] == 'r' || place[x, i] == 'q') && color[x, i] == clrDash)
                        return true;
                    else
                        break;
                }
            }// left rook or queen
            for (int i = x + 1, j = y + 1; i < 8 && j < 8; i++, j++)
            {
                if (color[i, j] != 2)
                {
                    if ((place[i, j] == 'b' || place[i, j] == 'q') && color[i, j] == clrDash)
                        return true;
                    else
                        break;
                }
            }// diagonal dowm \
            for (int i = x + 1, j = y - 1; i < 8 && j >= 0; i++, j--)
            {
                if (color[i, j] != 2)
                {
                    if ((place[i, j] == 'b' || place[i, j] == 'q') && color[i, j] == clrDash)
                        return true;
                    else
                        break;
                }
            }// diagonal dowm /
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (color[i, j] != 2)
                {
                    if ((place[i, j] == 'b' || place[i, j] == 'q') && color[i, j] == clrDash)
                        return true;
                    else
                        break;
                }
            }// diagonal up \
            for (int i = x - 1, j = y + 1; i >= 0 && j < 8; i--, j++)
            {
                if (color[i, j] != 2)
                {
                    if ((place[i, j] == 'b' || place[i, j] == 'q') && color[i, j] == clrDash)
                        return true;
                    else
                        break;
                }
            }// diagonal up /
            if (to_paly == 0)
            {
                if (pown_Check(x + 1, y + 1, clrDash)) return true;
                if (pown_Check(x + 1, y - 1, clrDash)) return true;
            }
            else
            {
                if (pown_Check(x - 1, y + 1, clrDash)) return true;
                if (pown_Check(x - 1, y - 1, clrDash)) return true;
            }

            for (int r = -1; r < 2; r++)
            {
                int i = x + r;
                for (int c = -1; c < 2; c++)
                {
                    int j = y + c;
                    if (in_range(i, j) && place[i, j] == 'k' && color[i, j] == clrDash)
                        return true;
                }
            }// Check there is no king beside him
            return false;
        }
        bool pown_Check(int i, int j, int clr)
        {
            if (!in_range(i,j)) return false;
            if (color[i, j] == clr && place[i, j] == 'p') return true;
            return false;
        }
        bool knight_Check(int i, int j, int clr)
        {
            if (!in_range(i,j)) return false;
            if (color[i, j] == clr && place[i, j] == 'n') return true;
            return false;
        }
        #endregion

        void delet_path()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (valid[i, j] == 1 || valid[i, j] == 3 || valid[i, j] == 5)
                    {
                        bt[i, j].BackgroundImage = null;
                        valid[i, j] = 0;
                    }
                    else if (valid[i, j] == 2)
                    {
                        valid[i, j] = 0;
                        int clr = color[i, j];
                        switch (place[i, j])
                        {
                            case 'p':
                                if (clr == 1)
                                    bt[i, j].BackgroundImage = Resources.PawnW;
                                else
                                    bt[i, j].BackgroundImage = Resources.PawnB;
                                break;
                            case 'r':
                                if (clr == 1)
                                    bt[i, j].BackgroundImage = Resources.RookW;
                                else
                                    bt[i, j].BackgroundImage = Resources.RookB;
                                break;
                            case 'b':
                                if (clr == 1)
                                    bt[i, j].BackgroundImage = Resources.BishopW;
                                else
                                    bt[i, j].BackgroundImage = Resources.BishopB;
                                break;
                            case 'n':
                                if (clr == 1)
                                    bt[i, j].BackgroundImage = Resources.KnightW;
                                else
                                    bt[i, j].BackgroundImage = Resources.KnightB;
                                break;
                            case 'q':
                                if (clr == 1)
                                    bt[i, j].BackgroundImage = Resources.QueenW;
                                else
                                    bt[i, j].BackgroundImage = Resources.QueenB;
                                break;
                        }
                    }
                }
            }
        }

        #region valid Point
        void show_path(int i, int j)
        {
            switch (place[i, j])
            {
                case 'p':
                    move_pown(i, j);
                    break;
                case 'r':
                    move_rook(i, j);
                    break;
                case 'b':
                    move_bishop(i, j);
                    break;
                case 'n':
                    move_knight(i, j);
                    break;
                case 'q':
                    move_rook(i, j);
                    move_bishop(i, j);
                    break;
                case 'k':
                    move_king(i, j);
                    break;
            }
        }
        bool point(int i, int j, int type = 2) // mark blue and return true when i,j is empty else mark with red and return fasle 
        {// type 0->pown attack  1->pown move  2->eny move else
            int clr = to_paly;
            if (i < 0 || i >= 8 || j < 0 || j >= 8) return false;
            if (color[i, j] == clr) return false;
            if (type == 1)
                goto move;// can not attack 
            switch (place[i, j])
            {
                case 'p':
                    if (clr == 0)
                        bt[i, j].BackgroundImage = Resources.eat_PawnW;
                    else
                        bt[i, j].BackgroundImage = Resources.eat_PawnB;
                    valid[i, j] = 2;
                    break;
                case 'r':
                    if (clr == 0)
                        bt[i, j].BackgroundImage = Resources.eat_RookW;
                    else
                        bt[i, j].BackgroundImage = Resources.eat_RookB;
                    valid[i, j] = 2;
                    break;
                case 'b':
                    if (clr == 0)
                        bt[i, j].BackgroundImage = Resources.eat_BishopW;
                    else
                        bt[i, j].BackgroundImage = Resources.eat_BishopB;
                    valid[i, j] = 2;
                    break;
                case 'n':
                    if (clr == 0)
                        bt[i, j].BackgroundImage = Resources.eat_KnightW;
                    else
                        bt[i, j].BackgroundImage = Resources.eat_KnightB;
                    valid[i, j] = 2;
                    break;
                case 'q':
                    if (clr == 0)
                        bt[i, j].BackgroundImage = Resources.eat_QueenW;
                    else
                        bt[i, j].BackgroundImage = Resources.eat_QueenB;
                    valid[i, j] = 2;
                    break;
                default:
                    if (type == 0) return false;
                    bt[i, j].BackgroundImage = Resources.empty;
                    valid[i, j] = 1;
                    return true;
            }
        move:
            if (type == 1 && place[i, j] == '\0') // move
            {
                bt[i, j].BackgroundImage = Resources.empty;
                valid[i, j] = 1;
                return true;
            }
            return false;
        }
        void move_pown(int i, int j)
        {
            int clrDash = (to_paly + 1) % 2;
            if (to_paly == 0)//black  // attack
            {
                if (in_range(i + 1, j + 1) && color[i + 1, j + 1] == clrDash && is_valid(i + 1, j + 1, 0)) ;
                if (in_range(i + 1, j - 1) && color[i + 1, j - 1] == clrDash && is_valid(i + 1, j - 1, 0)) ;
            }
            else
            {
                if (in_range(i - 1, j + 1) && color[i - 1, j + 1] == clrDash && is_valid(i - 1, j + 1, 0)) ;
                if (in_range(i - 1, j - 1) && color[i - 1, j - 1] == clrDash && is_valid(i - 1, j - 1, 0)) ;
            }

            if ((to_paly == 0 && i == 1) || (to_paly == 1 && i == 6))
            {
                if (to_paly == 0)//black
                {
                    if (in_range(i + 1, j) && color[i + 1, j] == 2 && is_valid(i + 1, j, 1))
                        if (in_range(i + 2, j) && color[i + 2, j] == 2 && is_valid(i + 2, j, 1)) ;
                }
                else
                {
                    if (in_range(i - 1, j) && color[i - 1, j] == 2 && is_valid(i - 1, j, 1))
                        if (in_range(i - 2, j) && color[i - 2, j] == 2 && is_valid(i - 2, j, 1)) ;
                }
            }
            else
            {
                if (to_paly == 0)//black
                {
                    if (in_range(i + 1, j) && color[i + 1, j] == 2 && is_valid(i + 1, j, 1)) ;
                }
                else
                {
                    if (in_range(i - 1, j) && color[i - 1, j] == 2 && is_valid(i - 1, j, 1)) ;
                }
            }
            if (passant)
            {
                if ((j + 1 == valid_pass_j || j - 1 == valid_pass_j) && i == valid_pass_i)
                {
                    if (to_paly == 0)
                    {
                        valid[valid_pass_i + 1, valid_pass_j] = 3;
                        bt[valid_pass_i + 1, valid_pass_j].BackgroundImage = Resources.empty;
                    }
                    else
                    {
                        valid[valid_pass_i - 1, valid_pass_j] =3;
                        bt[valid_pass_i-1, valid_pass_j].BackgroundImage = Resources.empty;
                    }
                }
            }
        }
        void move_knight(int i, int j)
        {
            is_valid(i + 2, j + 1);// Down move
            is_valid(i + 2, j - 1);

            is_valid(i - 2, j + 1);// Up move
            is_valid(i - 2, j - 1);

            is_valid(i + 1, j + 2);// Right move
            is_valid(i - 1, j + 2);

            is_valid(i + 1, j - 2);// Left move
            is_valid(i - 1, j - 2);
        }
        void move_rook(int i, int j)
        {
            int x;
            x = i + 1;
            while (is_valid(x, j)) x++;//Down
            x = i - 1;
            while (is_valid(x, j)) x--;//Up
            x = j + 1;
            while (is_valid(i, x)) x++;// Right
            x = j - 1;
            while (is_valid(i, x)) x--;// Left
        }
        void move_bishop(int i, int j)
        {
            int a, b;

            a = i + 1;
            b = j + 1;
            while (is_valid(a, b)) { a++; b++; }// \ Dowm

            a = i - 1;
            b = j - 1;
            while (is_valid(a, b)) { a--; b--; }//  \ Up

            a = i - 1;
            b = j + 1;
            while (is_valid(a, b)) { a--; b++; }//  / Up

            a = i + 1;
            b = j - 1;
            while (is_valid(a, b)) { a++; b--; }//  / Down
        }
        void move_king(int i, int j)
        {
            bool le = false, ri = false;// Check Right and left Castling
            int a = i - 1, b = j - 1;
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (r == 1 && c == 0)
                        le = is_valid(a + r, b + c, 5);// left Castling
                    else if (r == 1 && c == 2)
                        ri = is_valid(a + r, b + c, 5);// Right Castling
                    else
                        is_valid(a + r, b + c);
                }
            }
            if (!there_Check())
            {
                if (to_paly == 0)
                {
                    if (kingBL)
                    {
                        if (le && color[0, 1] == 2 && color[0, 2] == 2 && color[0, 3] == 2)
                        {
                            if (is_valid(0, 2, 5))
                                valid[0, 2] = 5;
                        }
                    }
                    if (kingBR)
                    {
                        if (ri && color[0, 5] == 2 && color[0, 6] == 2)
                        {
                            if (is_valid(0, 6, 5))
                                valid[0, 6] = 5;
                        }
                    }
                }
                else
                {
                    if (kingWL)
                    {
                        if (le && color[7, 1] == 2 && color[7, 2] == 2 && color[7, 3] == 2)
                        {
                            if (is_valid(7, 2, 5))
                                valid[7, 2] = 5;
                        }
                    }
                    if (kingWR)
                    {
                        if (ri && color[7, 5] == 2 && color[7, 6] == 2)
                        {
                            if (is_valid(7, 6, 5))
                                valid[7, 6] = 5;
                        }
                    }
                }
            }
        }
        bool is_valid(int i, int j, int type = 2)// try the move and check is there king check or not and call "Point()" Function Which was mentioned above
        { // do move -> check is there king check -> undo move -> call "Point()"
            if (i < 0 || i >= 8 || j < 0 || j >= 8) return false;
            if (color[i, j] == to_paly) return false;
            int first_clr = color[first_i, first_j];
            char first_plase = place[first_i, first_j];
            int cur_clr = color[i, j];
            char cur_place = place[i, j];
            int firstk_i = 0, firstk_j = 0;
            if (first_plase == 'k')
            {
                if (to_paly == 1)
                {
                    firstk_i = kingw_i;
                    firstk_j = kingw_j;
                    kingw_i = i;
                    kingw_j = j;
                }
                else
                {
                    firstk_i = kingb_i;
                    firstk_j = kingb_j;
                    kingb_i = i;
                    kingb_j = j;
                }
            }

            color[i, j] = first_clr;
            color[first_i, first_j] = 2;

            place[i, j] = first_plase;
            place[first_i, first_j] = '\0';//empty

            if (there_Check())
            {
                if (first_plase == 'k')
                {
                    if (to_paly == 1)
                    {
                        kingw_i = firstk_i;
                        kingw_j = firstk_j;
                    }
                    else
                    {
                        kingb_i = firstk_i;
                        kingb_j = firstk_j;
                    }
                }
                color[i, j] = cur_clr;
                color[first_i, first_j] = first_clr;
                place[i, j] = cur_place;
                place[first_i, first_j] = first_plase;// undo
                if (type == 5) return false;// king tabyit
                return (cur_clr == 2);
            }
            else
            {
                if (first_plase == 'k')
                {
                    if (to_paly == 1)
                    {
                        kingw_i = firstk_i;
                        kingw_j = firstk_j;
                    }
                    else
                    {
                        kingb_i = firstk_i;
                        kingb_j = firstk_j;
                    }
                }
                color[i, j] = cur_clr;
                color[first_i, first_j] = first_clr;
                place[i, j] = cur_place;
                place[first_i, first_j] = first_plase;// undo
                return point(i, j, type);
            }
        }// try the move and check is there check or not and call "Point()" Function Which was mentioned above 
        bool in_range(int i, int j)
        {
            if (i < 0 || i >= 8 || j < 0 || j >= 8) return false;
            return true;
        }
        #endregion
        void mk_piece()
        {
            for (int i = 0; i < 8; i++)
            {
                place[1, i] = 'p';
                bt[1, i].BackgroundImage = Resources.PawnB;
            }
            for (int i = 0; i < 8; i++)
            {
                place[6, i] = 'p';
                bt[6, i].BackgroundImage = Resources.PawnW;
            }
            bt[0, 0].BackgroundImage = bt[0, 7].BackgroundImage = Resources.RookB;
            place[0, 0] = place[0, 7] = 'r';

            bt[7, 0].BackgroundImage = bt[7, 7].BackgroundImage = Resources.RookW;
            place[7, 0] = place[7, 7] = 'r';

            bt[0, 1].BackgroundImage = bt[0, 6].BackgroundImage = Resources.KnightB;
            place[0, 1] = place[0, 6] = 'n';

            bt[7, 1].BackgroundImage = bt[7, 6].BackgroundImage = Resources.KnightW;
            place[7, 1] = place[7, 6] = 'n';

            bt[0, 2].BackgroundImage = bt[0, 5].BackgroundImage = Resources.BishopB;
            place[0, 2] = place[0, 5] = 'b';

            bt[7, 2].BackgroundImage = bt[7, 5].BackgroundImage = Resources.BishopW;
            place[7, 2] = place[7, 5] = 'b';


            bt[0, 4].BackgroundImage = Resources.KingB;
            place[0, 4] = 'k';

            bt[0, 3].BackgroundImage = Resources.QueenB;
            place[0, 3] = 'q';

            bt[7, 4].BackgroundImage = Resources.KingW;
            place[7, 4] = 'k';

            bt[7, 3].BackgroundImage = Resources.QueenW;
            place[7, 3] = 'q';

            for (int i = 0; i < 8; i++) color[0, i] = 0;// 0 -> black
            for (int i = 0; i < 8; i++) color[1, i] = 0;
            for (int i = 0; i < 8; i++) color[6, i] = 1;// 1 -> White
            for (int i = 0; i < 8; i++) color[7, i] = 1;

            for (int i = 2; i < 6; i++)
                for (int j = 0; j < 8; j++)
                    color[i, j] = 2;// empty place

        }
        void mk_buttons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bt[i, j] = new Button();
                    Controls.Add(bt[i, j]);
                    bt[i, j].Anchor = AnchorStyles.None;

                    if ((i + j) % 2 == 0)
                        bt[i, j].BackColor = Color.WhiteSmoke;
                    else
                        bt[i, j].BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));

                    bt[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    bt[i, j].FlatStyle = FlatStyle.Flat;
                    bt[i, j].ForeColor = SystemColors.ActiveCaptionText;
                    bt[i, j].Location = new Point(61 + (90 * j), 10 + (90 * i));
                    bt[i, j].Size = new Size(90, 90);
                    bt[i, j].UseVisualStyleBackColor = false;
                }
            }
        }
    }
}
