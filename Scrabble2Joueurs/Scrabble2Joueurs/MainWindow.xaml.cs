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

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        Joueur j1;
        Joueur j2;
        string lettresDisponibles = "";
        int nbTours = 0;
        public MainWindow()
        {

            InitializeComponent();
        }

        private void btnCommencer_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomJ1.Text != "" && txtNomJ2.Text != "")
            {
                if (txtNomJ1.Text != txtNomJ2.Text)
                {
                    j1 = new Joueur(txtNomJ1.Text);
                    j2 = new Joueur(txtNomJ2.Text);
                    DebutPartie.IsEnabled = false;
                    lblNomJoueur1.Content = j1.getnom();
                    lblNomJoueur2.Content = j2.getnom();
                    int choice = rnd.Next(0, 2);
                    if (choice == 0)
                    {
                        BordureJoueur2.IsEnabled = false;
                    }
                    else
                    {
                        BordureJoueur1.IsEnabled = false;
                    }

                }
                else
                {
                    MessageBox.Show("Les deux joueurs doivent avoir des noms différents.","ERREUR");

                }


            }
            else
            {
                MessageBox.Show("les deux joueurs doivent saisir un nom","ERREUR");
            }

        }
        private void GenererLettres()
        {
            string alphabet = "AAABBBCCDDEEEEEEFFFGGHHIIIJJKKLLLLMMNNNOOOOPPQRRRRSSSSTTTTUUUVVWXYYZ";
            char[] tirage = new char[7];
            for (int i = 0; i < 7; i++)
            {
                tirage[i] = alphabet[rnd.Next(alphabet.Length)];
            }
            lettresDisponibles = new string(tirage);
            txtLettres.Text = string.Join(" ", tirage);
        }
        private void btnNouvellesLettres_Click(object sender, RoutedEventArgs e)
        {
            GenererLettres();
        }
        private bool MotValideAvecLettres(string mot)
        {
            List<char> dispo = lettresDisponibles.ToUpper().ToList();
            foreach (char c in mot.ToUpper())
            {
                if (!dispo.Contains(c))
                    return false;
                dispo.Remove(c);
            }
            return true;
        }


        private void btnOkbleu_Click(object sender, RoutedEventArgs e)
        {
            string mot = txtNombleu.Text.Trim();

            if (mot == "")
            {
                MessageBox.Show("Veuillez entrer un mot.", "ERREUR");
                return;
            }

            foreach (char c in mot)
            {
                if (!char.IsLetter(c))
                {
                    MessageBox.Show("Le mot est incorrect. Il ne doit contenir que des lettres." );
                    txtNombleu.Clear();
                    return;
                }
            }

            if (!MotValideAvecLettres(mot))
            {
                MessageBox.Show("Le mot doit être formé uniquement avec les lettres disponibles !", "Mot invalide");
                txtNombleu.Clear();
                return;
            }

            j1.AjouterMot(mot);
            GenererLettres();
            AfficherHistoriqueBleu();
            nbTours++;
            lblToursJ1.Content = $"Tours : {j1.GetNbMots()}/5";

            txtNombleu.Clear();
            BordureJoueur1.IsEnabled = false;
            if (nbTours >= 10)
            {
                FinDePartie();
                return;
            }

            BordureJoueur2.IsEnabled = true;
        }

        private void btnOkrouge_Click(object sender, RoutedEventArgs e)
        {
            string mot = txtNomrouge.Text.Trim();

            if (mot == "")
            {
                MessageBox.Show("Veuillez entrer un mot.");
                return;
            }

            foreach (char c in mot)
            {
                if (!char.IsLetter(c))
                {
                    MessageBox.Show("Le mot est incorrect. Il ne doit contenir que des lettres.");
                    txtNomrouge.Clear();
                    return;
                }
            }

            if (!MotValideAvecLettres(mot))
            {
                MessageBox.Show("Le mot doit être formé uniquement avec les lettres disponibles !", "Mot invalide");
                txtNomrouge.Clear();
                return;
            }

            j2.AjouterMot(mot);
            GenererLettres();
            AfficherHistoriqueRouge();
            nbTours++;
            lblToursJ2.Content = $"Tours : {j2.GetNbMots()}/5";

            txtNomrouge.Clear();
            BordureJoueur2.IsEnabled = false;
            if(nbTours >= 10)
{
                FinDePartie();
                return;
            }
            BordureJoueur1.IsEnabled = true;
        }

        private void AfficherHistoriqueBleu()
        {
            string texte = $"{j1.getnom()} :\n";
            foreach (string mot in j1.GetLesMots())
            {
                texte += $"  {mot} → {Utilitaire.PointsMot(mot)} pts\n";
            }
            texte += $"  Total : {j1.GetTotalPoints()} pts";
            txtHistoriqueBleu.Text = texte;
        }

        private void AfficherHistoriqueRouge()
        {
            string texte = $"{j2.getnom()} :\n";
            foreach (string mot in j2.GetLesMots())
            {
                texte += $"  {mot} → {Utilitaire.PointsMot(mot)} pts\n";
            }
            texte += $"  Total : {j2.GetTotalPoints()} pts";
            txtHistoriqueRouge.Text = texte;
        }

        private void FinDePartie()
        {
            BordureJoueur1.IsEnabled = false;
            BordureJoueur2.IsEnabled = false;
            btnNouvellesLettres.IsEnabled = false;

            string gagnant;
            if (j1.GetTotalPoints() > j2.GetTotalPoints())
                gagnant = $"{j1.getnom()} gagne avec {j1.GetTotalPoints()} pts !";
            else if (j2.GetTotalPoints() > j1.GetTotalPoints())
                gagnant = $"{j2.getnom()} gagne avec {j2.GetTotalPoints()} pts !";
            else
                gagnant = "Égalité !";

            MessageBox.Show($"Partie terminée ! {gagnant}","VICTOIRE");
        }
    }
}
