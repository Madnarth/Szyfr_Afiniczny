using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Szyfr_Afiniczny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        char[] alfabetPol = new char[32] { 'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'r', 's', 'ś', 't', 'u', 'w', 'y', 'z', 'ź', 'ż', };
        char[] alfabetAng = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', };
        int przesuniecie, a;
        char znak;

        private int SzukajAPol(int a)
        {
            int wynik = 0;
            for (int i = 1; i < 32; i++)
            {
                if (((a * i) %32) == 1)
                {
                    wynik = i;
                }
            }

            return wynik;
        }
        private int SzukajAAng(int a)
        {
            int wynik = 0;
            for (int i = 1; i < 26; i++)
            {
                if (((a * i) % 26) == 1)
                {
                    wynik = i;
                }
            }

            return wynik;
        }
        private void szyfruj(object sender, RoutedEventArgs e)
        {
            przesuniecie = 0;
            znak = '0';
            var wybor = ((ComboBoxItem)ComboBoxJezyk.SelectedItem).Content as string;

            try
            {
                if (Int32.Parse(TxtBoxA.Text) + Int32.Parse(TxtBoxB.Text) == 0)
                {
                    MessageBox.Show("Suma A i B musi być większa od 0");
                }
                else
                {
                    TxtBoxSzyf.Text = "";
                    if (wybor=="Polski")
                    {
                        foreach (char c in TxtBoxNieszyf.Text)
                        {
                            znak = c;

                            if (Char.IsLetter(znak))
                            {
                                przesuniecie = (Array.IndexOf(alfabetPol, znak) * Int32.Parse(TxtBoxA.Text) + Int32.Parse(TxtBoxB.Text)) % 32;
                                TxtBoxSzyf.Text += alfabetPol[przesuniecie];
                            }
                            else
                            {
                                TxtBoxSzyf.Text += znak;
                            }
                        } 
                    }
                    else
                    {
                        foreach (char c in TxtBoxNieszyf.Text)
                        {
                            znak = c;

                            if (Char.IsLetter(znak))
                            {
                                przesuniecie = (Array.IndexOf(alfabetAng, znak) * Int32.Parse(TxtBoxA.Text) + Int32.Parse(TxtBoxB.Text)) % 26;
                                TxtBoxSzyf.Text += alfabetAng[przesuniecie]; 
                            }
                            else
                            {
                                TxtBoxSzyf.Text += znak;
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
                
            }
        }
        private void deszyfruj(object sender, RoutedEventArgs e)
        {
            przesuniecie = 0;
            znak = '0';
            var wybor = ((ComboBoxItem)ComboBoxJezyk.SelectedItem).Content as string;

            if (wybor=="Polski")
            {
                a = SzukajAPol(Int32.Parse(TxtBoxA.Text)); 
            }
            else
            {
                a = SzukajAAng(Int32.Parse(TxtBoxA.Text));
            }

            try
            {
                if (Int32.Parse(TxtBoxA.Text) + Int32.Parse(TxtBoxB.Text) == 0)
                {
                    MessageBox.Show("Suma A i B musi być większa od 0");
                }
                else
                {
                    TxtBoxRoszyf.Text = "";
                    if (wybor=="Polski")
                    {
                        foreach (char c in TxtBoxZaszyf.Text)
                        {
                            znak = c;

                            if (Char.IsLetter(znak))
                            {
                                przesuniecie = (a * (Array.IndexOf(alfabetPol, znak) - Int32.Parse(TxtBoxB.Text))) % 32;
                                if (przesuniecie < 0)
                                {
                                    przesuniecie += 32;
                                }
                                TxtBoxRoszyf.Text += alfabetPol[przesuniecie];
                            }
                            else
                            {
                                TxtBoxRoszyf.Text += znak;
                            }
                        } 
                    }
                    else
                    {
                        foreach (char c in TxtBoxZaszyf.Text)
                        {
                            znak = c;

                            if (Char.IsLetter(znak))
                            {
                                przesuniecie = (a * (Array.IndexOf(alfabetAng, znak) - Int32.Parse(TxtBoxB.Text))) % 26;
                                if (przesuniecie < 0)
                                {
                                    przesuniecie += 26;
                                }
                                TxtBoxRoszyf.Text += alfabetAng[przesuniecie];
                            }
                            else
                            {
                                TxtBoxRoszyf.Text += znak;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void TxtBoxA_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TxtBoxA.Text, "[^0-9]"))
            {
                TxtBoxA.Text = TxtBoxA.Text.Remove(TxtBoxA.Text.Length - 1);
            }
            try
            {
                if (Int32.Parse(TxtBoxA.Text) == 0 || (Int32.Parse(TxtBoxA.Text) %2) == 0)
                {
                    MessageBox.Show("Wartość A nie może być równa 0 lub parzysta");
                    TxtBoxA.Text = TxtBoxA.Text.Remove(TxtBoxA.Text.Length - 1);
                }
            }
            catch (Exception)
            {

            }
            try
            {
                var wybor = ((ComboBoxItem)ComboBoxJezyk.SelectedItem).Content as string;
                if (wybor=="Angielski" && Int32.Parse(TxtBoxA.Text) == 13)
                {
                    MessageBox.Show("Wartość A nie może być równa 13 dla alfabetu Angielskiego");
                    TxtBoxA.Text = TxtBoxA.Text.Remove(TxtBoxA.Text.Length - 1);
                }
            }
            catch (Exception)
            {

            }
        }

        private void TxtBoxB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TxtBoxB.Text, "[^0-9]"))
            {
                TxtBoxB.Text = TxtBoxB.Text.Remove(TxtBoxA.Text.Length - 1);
            }
        }
    }
}
