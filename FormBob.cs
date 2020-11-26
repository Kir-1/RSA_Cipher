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
    public partial class FormBob : Form
    {
        BigInteger e1;
        BigInteger n;
        BigInteger m;
        BigInteger mShifr;
        public FormBob()
        {
            InitializeComponent();
        }




        private void СanselButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        public static BigInteger ConvertNumber(string str)
        {
            BigInteger m1 = 0;
            for (int i = 0; i < str.Length; i++)
            {
                m1 += (Convert.ToInt32(str[i]) - 97) * Convert.ToInt32(Math.Pow(26, (str.Length - 1) - i));
            }

            return m1;
        }
        public static string ConvertNumber(BigInteger num)
        {
            string result1 = "";
            string result2 = "";
            result2 = num.ToString();
            int num1 = Int32.Parse(result2);
            result2 = "";
            while (num1 != 0)
            {
                result1 += Convert.ToChar((num1 % 26) + 97);
                num1 /= 26;
            }
            for (int i = result1.Length - 1; i >= 0; i--)
            {
                result2 += result1[i];
            }
            return result2;
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
        private bool ChekN()
        {
            if (nTextBox.Text.Length == 0)
            {
                MessageBox.Show("N некорректно", "Ошибка");
                return false;
            }
            if (BigInteger.TryParse(nTextBox.Text, out n) == false)
            {
                MessageBox.Show("N некорректно", "Ошибка");
                return false;
            }
            /*if(eA >= p)
            {
                MessageBox.Show("e<a> некорректно", "Ошибка");
                return false;
            }*/
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
        private bool Chek()
        {
            if (ChekE() && ChekN() && ChekM())
            {
                return true;
            }
            return false;
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (Chek() == true)
            {
                eTextBox.Enabled = false;
                nTextBox.Enabled = false;
                mTextBox.Enabled = false;
                m = ConvertNumber(mTextBox.Text);
                mShifr = BigInteger.ModPow(m, e1, n);
                mShifrTextBox.Text = ConvertNumber(mShifr);
                mShifrTextBox.Enabled = true;
                label6.Text = "Передайте |m| Принимающему";
            }
        }

    }
}
