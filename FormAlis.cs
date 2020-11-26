using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace RSA
{
    public partial class FormAlis : Form
    {
        BigInteger p;
        BigInteger q;
        BigInteger m;
        BigInteger mShifr;
        BigInteger F;
        BigInteger n;
        BigInteger e1;
        BigInteger d;
        public FormAlis()
        {
            InitializeComponent();
        }

        private void СanselButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        public static int RevNum(BigInteger a, BigInteger mod)
        {
            List<int> listX = new List<int>();
            listX.Add(1);
            listX.Add(0);
            List<int> listY = new List<int>();
            listY.Add(0);
            listY.Add(1);
            int i = 2;
            BigInteger mod1 = mod;
            BigInteger a1 = a;
            BigInteger r = mod % a;
            BigInteger q;
            while (r != 1)
            {
                q = mod1 / a1;
                r = mod1 % a1;
                listX.Add(listX[i - 2] - (int)q * listX[i - 1]);
                listY.Add(listY[i - 2] -(int)q * listY[i - 1]);
                mod1 = a1;
                a1 = r;
                i++;
            }
            string str = (listX.Count - 1).ToString();
            int resX = listX[listX.Count - 1];
            int resY = listY[listY.Count - 1];
            if (resX <= 0)
            {
                resX = (int)(mod + resX);
            }
            if (resY <= 0)
            {
                resY = (int)(mod + resY);
            }
            if ((a * resX) % mod == 1)
            {
                return resX;
            }
            else
            {
                return resY;
            }


        }
        

        private bool Simple(BigInteger n)
        {

            if (n == 2)
            {
                return true;
            }
            if (n == 1 || n % 2 == 0)
            {
                return false;
            }
            double h = (double)n;
            for (int i = 3; i < Math.Sqrt(h); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private bool ChekP()
        {
            if (pTextBox.Text.Length == 0)
            {
                MessageBox.Show("P некорректно", "Ошибка");
                return false;
            }
            if (BigInteger.TryParse(pTextBox.Text, out p) == false)
            {
                MessageBox.Show("P некорректно", "Ошибка");
                return false;
            }
            if (p <= 0)
            {
                MessageBox.Show("P некорректно", "Ошибка");
                return false;
            }
            if (!Simple(p))
            {
                MessageBox.Show("P некорректно", "Ошибка");
                return false;
            }



            return true;
        }

        private bool ChekQ()
        {
            if (qTextBox.Text.Length == 0)
            {
                MessageBox.Show("q некорректно", "Ошибка");
                return false;
            }
            if (BigInteger.TryParse(qTextBox.Text, out q) == false)
            {
                MessageBox.Show("q некорректно", "Ошибка");
                return false;
            }
            if (q <= 0)
            {
                MessageBox.Show("q некорректно", "Ошибка");
                return false;
            }
            if(p==q)
            {
                MessageBox.Show("q некорректно", "Ошибка");
                return false;
            }
            if (!Simple(q))
            {
                MessageBox.Show("q некорректно", "Ошибка");
                return false;
            }



            return true;
        }
        private bool ChekM()
        {
            string resutl = "";
            for (int i = 0; i < mTextBox.Text.Length; i++)
            {
                if (Convert.ToInt32(mTextBox.Text[i]) < 97 || Convert.ToInt32(mTextBox.Text[i]) > 122)
                {
                    continue;
                }
                resutl += mTextBox.Text[i];
            }
            if (resutl.Length == 0)
            {
                MessageBox.Show("M некорректно", "Ошибка");
                return false;
            }
            mTextBox.Text = resutl;
            return true;
        }

        private bool ChekMS()
        {
            string resutl = "";
            for (int i = 0; i < mShifrTextBox.Text.Length; i++)
            {
                if (Convert.ToInt32(mShifrTextBox.Text[i]) < 97 || Convert.ToInt32(mShifrTextBox.Text[i]) > 122)
                {
                    continue;
                }
                resutl += mShifrTextBox.Text[i];
            }
            if (resutl.Length == 0)
            {
                MessageBox.Show("M некорректно", "Ошибка");
                return false;
            }
            mTextBox.Text = resutl;
            return true;
        }
        private bool ChekE()
        {
            if (eTextBox.Text.Length == 0)
            {
                MessageBox.Show("e некорректно", "Ошибка");
                return false;
            }
            if (BigInteger.TryParse(eTextBox.Text, out e1) == false)
            {
                MessageBox.Show("e некорректно", "Ошибка");
                return false;
            }
            /*if(eA >= p)
            {
                MessageBox.Show("e<a> некорректно", "Ошибка");
                return false;
            }*/
            return true;

        }
 
        private bool Chek()
        {
            if (ChekP() && ChekQ())
            {
                return true;
            }
            return false;
        }

        

        private void OkButton_Click(object sender, EventArgs e)
        {

            if (Chek() == true)
            {
                pTextBox.Enabled = false;
                qTextBox.Enabled = false;
                n = p * q;
                NTextBox.Text = n.ToString();
                F = (p - 1) * (q - 1);
                fNTextBox.Text = F.ToString();
                eTextBox.Enabled = true;
                label6.Text = "Выберите e и нажмите Ок";
                if(eTextBox.Text.Length != 0)
                {
                    eTextBox.Enabled = false;
                    if(ChekE() == true)
                    {
                        d = RevNum(e1, F);
                        dTextBox.Text = d.ToString();
                        eTextBox.Enabled = true;
                        NTextBox.Enabled = true;
                        label6.Text = "Передайте значения e и N передающему и получите от него |m|";
                        mShifrTextBox.Enabled = true;
                    }
                }
                if(mShifrTextBox.Text.Length!=0)
                {
                    if (ChekMS() == true)
                    {

                        mShifr = FormBob.ConvertNumber(mShifrTextBox.Text);
                        m = BigInteger.ModPow(mShifr, d, n);
                        mTextBox.Text = FormBob.ConvertNumber(m);
                    }
                }

            }
        }


    }
}
