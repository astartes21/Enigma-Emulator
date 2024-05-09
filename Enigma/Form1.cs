using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace EnigmaCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // t1 - номер первого ротора, t2 - номер второго ротора, t3 - номер третьего ротора
        // i1 - номер буквы на i-ом роторе и т.д, LetterIn - Input
        int t1 = 0, t2 = 0, t3 = 0, i1 = 0, i2 = 0, i3 = 0, LetterIn = 0;
        // объявление алфавита, роторов и рефлектора
        char[] ALB = { 'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P',
            'Q','R','S','T','U','V','W','X','Y','Z'};

        char[] ROT1 = {'E','K','M','F','L','G','D','Q','V','Z','N','T','O','W','Y','H',
            'X','U','S','P','A','I','B','R','C','J'};

        char[] ROT2 = {'A','J','D','K','S','I','R','U','X','B','L','H','W','T','M','C',
            'Q','G','Z','N','P','Y','F','V','O','E'};

        char[] ROT3 = {'B','D','F','H','J','L','C','P','R','T','X','V','Z','N','Y','E',
            'I','W','G','A','K','M','U','S','Q','O'};

        char[] REFLECTOR = {'Y','R','U','H','Q','S','L','D','P','X','N','G','O','K','M','I',
            'E','B','F','Z','C','W','V','J','A','T'};

        // С помощью trackBar объявляем буквы ротора
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SoundR();
            t3 = trackBar1.Value;
            label4.Refresh();
            label4.Text = ROT3[t3].ToString();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            SoundR();
            t2 = trackBar2.Value;
            label5.Refresh();
            label5.Text = ROT2[t2].ToString();
        }
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            SoundR();
            t1 = trackBar3.Value;
            label6.Refresh();
            label6.Text = ROT1[t1].ToString();
        }
        // Функция для проигрывания звука роторов
        public void SoundR()
        {
            SoundPlayer simpleSound = new SoundPlayer("Rotor.wav");
            simpleSound.Play();
        }

        // Функция подсвечивания зашифрованной буквы
        public async void Spot(Button t)
        {
            t.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            t.FlatAppearance.BorderSize = 2;
            t.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            SoundPlayer simpleSound = new SoundPlayer("Click.wav");
            simpleSound.Play();
            await Task.Delay(400);
            t.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
        }

        // Функция определения индекса буквы в алфавите
        int SearchA(char Letter)
        {
            for (int i = 0; i <= 25; i++)
            {
                if (ALB[i] == Letter)
                {
                    LetterIn = i;
                    break;
                }
            }
            return LetterIn;
        }
        // Функция поиска буквы в роторе
        int SearchROT(int Value, char[] ROT)
        {
            if (Value >= 0)
            {
                if (Value > 25)
                {
                    for (int i = 0; i <= 25; i++)
                    {
                        if (ALB[Value - 26] == ROT[i])
                        {
                            LetterIn = i;
                            Letter = ROT[i];
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i <= 25; i++)
                    {
                        if (ALB[Value] == ROT[i])
                        {
                            LetterIn = i;
                            Letter = ROT[i];
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i <= 25; i++)
                {
                    if (ALB[Value + 26] == ROT[i])
                    {
                        LetterIn = i;
                        Letter = ROT[i];
                        break;
                    }
                }
            }
            return LetterIn;
        }
        
        // Объявление символьной переменной для хранения буквы 
        char Letter = ' ';
        // Основная функция шифрования
        void Encryption()
        {
            // Записываем индексы букв на роторах 
            for (int i = 0; i <= 25; i++)
            {
                if (Convert.ToChar(label4.Text) == ROT3[i])
                    i3 = i;
            }

            for (int i = 0; i <= 25; i++)
            {
                if (Convert.ToChar(label5.Text) == ROT2[i])
                    i2 = i;
            }

            for (int i = 0; i <= 25; i++)
            {
                if (Convert.ToChar(label6.Text) == ROT1[i])
                    i1 = i;
            }

            //Input -> ROT1

            if (i1 + LetterIn >= 0)
            {
                if (i1 + LetterIn > 25)
                    Letter = ROT1[i1 + LetterIn - 26];
                else
                    Letter = ROT1[i1 + LetterIn];
            }
            else
                Letter = ROT1[i1 + LetterIn + 26];
            SearchA(Letter);

            //ROT1 -> ROT2 

            if (LetterIn + (i2 - i1) >= 0)
            {
                if (LetterIn + (i2 - i1) > 25)
                    Letter = ROT2[LetterIn + (i2 - i1) - 26];
                else
                    Letter = ROT2[LetterIn + (i2 - i1)];
            }
            else
                Letter = ROT2[LetterIn + (i2 - i1) + 26];
            SearchA(Letter);

            //ROT2 -> ROT3 

            if (LetterIn + (i3 - i2) >= 0)
            {
                if (LetterIn + (i3 - i2) > 25)
                    Letter = ROT3[LetterIn + (i3 - i2) - 26];
                else
                    Letter = ROT3[LetterIn + (i3 - i2)];
            }
            else
                Letter = ROT3[LetterIn + (i3 - i2) + 26];
            SearchA(Letter);

            //ROT3 -> REFLECTOR 

            SearchA(Letter);
            if (LetterIn - i3 >= 0)
            {
                if (LetterIn - i3 > 25)
                {
                    Letter = REFLECTOR[LetterIn - i3 - 26];
                }
                else
                    Letter = REFLECTOR[LetterIn - i3];
            }
            else
                Letter = REFLECTOR[LetterIn - i3 + 26];
            SearchA(Letter);

            //REFLECTOR -> ROT3 
            SearchROT(LetterIn + i3, ROT3);
            //ROT3->ROT2 
            SearchROT(LetterIn - (i3 - i2), ROT2);
            //ROT2 -> ROT1 
            SearchROT(LetterIn - (i2 - i1), ROT1);
            //Rot1 -> OutPut 
            if (LetterIn - i1 >= 0)
            {
                if (LetterIn - i1 > 25)
                {
                    Letter = ALB[LetterIn - i1 - 26];
                }
                else
                    Letter = ALB[LetterIn - i1];
            }
            else
                Letter = ALB[LetterIn - i1 + 26];
            SearchA(Letter);
            
            if(LetterIn == 0) Spot(A0);if(LetterIn == 1) Spot(B0);if(LetterIn == 2) Spot(C0);if(LetterIn == 3) Spot(D0);if(LetterIn == 4) Spot(E0);if(LetterIn == 5) Spot(F0);if(LetterIn == 6) Spot(G0);
            if(LetterIn == 7) Spot(H0);if(LetterIn == 8) Spot(I0);if(LetterIn == 9) Spot(J0);if(LetterIn == 10) Spot(K0);if(LetterIn == 11) Spot(L0);if(LetterIn == 12) Spot(M0);if(LetterIn == 13) Spot(N0);
            if(LetterIn == 14) Spot(O0);if(LetterIn == 15) Spot(P0);if(LetterIn == 16) Spot(Q0);if(LetterIn == 17) Spot(R0);if(LetterIn == 18) Spot(S0);if(LetterIn == 19) Spot(T0);if(LetterIn == 20) Spot(U0);
            if(LetterIn == 21) Spot(V0);if(LetterIn == 22) Spot(W0);if(LetterIn == 23) Spot(X0);if(LetterIn == 24) Spot(Y0);if(LetterIn == 25) Spot(Z0);
            //Spot(Buttons(Letter));
            Letter = ALB[LetterIn];
            textBox4.Text += Letter.ToString();
            // Прокрутка роторов 
            if (i1 + 1 > 25)
            {
                i1 = 0;
                i2++;
            }
            else
            {
                i1++;
            }

            if (i2 > 25)
            {
                i2 = 0;
                i3++;
            }

            if (i3 > 25)
            {
                i3 = 0;
            }

            trackBar1.Value = i3;
            label4.Text = ROT3[i3].ToString();
            trackBar2.Value = i2;
            label5.Text = ROT2[i2].ToString();
            trackBar3.Value = i1;
            label6.Text = ROT1[i1].ToString();
        }

        // Объявляем все 25 кнопок,соответствующих буквам алфавита 
        private void A_Click(object sender, EventArgs e)
        {
            LetterIn = 0;
            Encryption();
        }
        private void B_Click(object sender, EventArgs e)
        {
            LetterIn = 1;
            Encryption();
        }
        private void C_Click(object sender, EventArgs e)
        {
            LetterIn = 2;
            Encryption();
        }
        private void D_Click(object sender, EventArgs e)
        {
            LetterIn = 3;
            Encryption();
        }
        private void E_Click(object sender, EventArgs e)
        {
            LetterIn = 4;
            Encryption();
        }
        private void F_Click(object sender, EventArgs e)
        {
            LetterIn = 5;
            Encryption();
        }
        private void G_Click(object sender, EventArgs e)
        {
            LetterIn = 6;
            Encryption();
        }
        private void H_Click(object sender, EventArgs e)
        {
            LetterIn = 7;
            Encryption();
        }
        private void I_Click(object sender, EventArgs e)
        {
            LetterIn = 8;
            Encryption();
        }
        private void J_Click(object sender, EventArgs e)
        {
            LetterIn = 9;
            Encryption();
        }
        private void K_Click(object sender, EventArgs e)
        {
            LetterIn = 10;
            Encryption();
        }
        private void L_Click(object sender, EventArgs e)
        {
            LetterIn = 11;
            Encryption();
        }
        private void M_Click(object sender, EventArgs e)
        {
            LetterIn = 12;
            Encryption();
        }
        private void N_Click(object sender, EventArgs e)
        {
            LetterIn = 13;
            Encryption();
        }
        private void O_Click(object sender, EventArgs e)
        {
            LetterIn = 14;
            Encryption();
        }
        private void P_Click(object sender, EventArgs e)
        {
            LetterIn = 15; 
            Encryption();
        }
        private void Q_Click(object sender, EventArgs e)
        {
            LetterIn = 16;
            Encryption();
        }
        private void R_Click(object sender, EventArgs e)
        {
            LetterIn = 17;
            Encryption();
        }
        private void S_Click(object sender, EventArgs e)
        {
            LetterIn = 18;
            Encryption();
        }

        private void T_Click(object sender, EventArgs e)
        {
            LetterIn = 19;
            Encryption();
        }

        private void U_Click(object sender, EventArgs e)
        {
            LetterIn = 20;
            Encryption();
        }
        private void V_Click(object sender, EventArgs e)
        {
            LetterIn = 21;
            Encryption();
        }
        private void W_Click(object sender, EventArgs e)
        {
            LetterIn = 22;
            Encryption();
        }
        private void X_Click(object sender, EventArgs e)
        {
            LetterIn = 23;
            Encryption();
        }
        private void Y_Click(object sender, EventArgs e)
        {
            LetterIn = 24;
            Encryption();
        }
        private void Z_Click(object sender, EventArgs e)
        {
            LetterIn = 25;
            Encryption();
        }
        //=======================================================================================//
    }
}
