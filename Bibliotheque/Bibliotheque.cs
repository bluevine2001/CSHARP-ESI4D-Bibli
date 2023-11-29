using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Bibliotheque
{

    // classe qui réunit les listes de livres, auteurs et commandes de la bibliothèque pour les sauvegarder dans un fichier json
    class BibliothequeData
    {
        public List<Livre> Livres { get; set; } = new List<Livre>();
        public List<Auteur> Auteurs { get; set; } = new List<Auteur>();
        public List<Commande> Commandes { get; set; } = new List<Commande>();

        public BibliothequeData(List<Livre> livres, List<Auteur> auteurs, List<Commande> commandes)
        {
            Livres = livres;
            Auteurs = auteurs;
            Commandes = commandes;
        }

        public BibliothequeData()
        {
        }
    }

    // classe abstraite qui définit les propriétés communes aux livres et aux commandes
    abstract class Item
    {
        public string Titre { get; set; }
        public string Langue { get; set; }
        public int Stock { get; set; }
        public decimal Prix { get; set; }

        protected Item(string titre, string langue, int stock, decimal prix)
        {
            Titre = titre;
            Langue = langue;
            Stock = stock;
            Prix = prix;
        }

        public abstract void AfficherDetails();
    }

    // Classe qui gère les interactions avec l'utilisateur et la bibliothèque
    class Client
    {
        public Bibliotheque bibliotheque { get; set; }
        public Client()
        {
            bibliotheque = Bibliotheque.GetInstance();
        }
        
        public void relancer(){
            Console.WriteLine("retour au menu ? (Y/N)");
            string retour = Console.ReadLine() ?? string.Empty;
            if (retour == "Y" || retour == "y"){
                AfficherMenu();
                ChoisirMenu();
            }else{
                Console.WriteLine("Au revoir");
            }
        }
         public void AjouterLivre()
        {
            Console.WriteLine("Titre du livre :");
            string titre = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Auteur du livre :");
            string auteur = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Date du livre :");
            string date = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Genre du livre :");
            string genre = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Langue du livre :");
            string langue = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Prix du livre :");
            int prix = Convert.ToInt32(Console.ReadLine() ?? "0");
            Console.WriteLine("Stock du livre :");
            int stock = Convert.ToInt32(Console.ReadLine());
            Auteur auteur1 = new Auteur(auteur);
            Livre livre = new Livre(titre, auteur1, date, genre, langue, prix, stock);

            bibliotheque.AjouterLivre(livre);
            bibliotheque.AjouterAuteur(auteur1);

            relancer();
        }

        public void SupprimerLivre()
        {
            Console.WriteLine("Titre du livre :");
            string titre = Console.ReadLine() ?? string.Empty;

            bibliotheque.SupprimerLivre(titre);
            relancer();
        }

        public void AfficherLivres()
        {
            bibliotheque.AfficherLivres();
            relancer();
        }

        public void AfficherAuteurs()
        {
            bibliotheque.AfficherAuteurs();
            relancer();
        }

        public void AfficherLivresParAuteur()
        {
            Console.WriteLine("Nom de l'auteur :");
            string nom = Console.ReadLine() ?? string.Empty;
            Auteur auteur = new Auteur(nom);

            bibliotheque.AfficherLivresParAuteur(auteur);
            relancer();
        }

        public void AfficherLivresParGenre()
        {
            Console.WriteLine("Genre du livre :");
            string genre = Console.ReadLine() ?? string.Empty;

            bibliotheque.AfficherLivresParGenre(genre);

            relancer();
        }

        public void AfficherLivresParLangue()
        {
            Console.WriteLine("Langue du livre :");
            string langue = Console.ReadLine() ?? string.Empty;

            bibliotheque.AfficherLivresParLangue(langue);

            relancer();
        }

        public void AfficherCommandes()
        {
            bibliotheque.AfficherCommandes();

            relancer();
        }

        public void AfficherCommandesParStatut()
        {
            Console.WriteLine("Statut de la commande :");
            string statut = Console.ReadLine() ?? string.Empty;

            bibliotheque.AfficherCommandesParStatut(statut);

            relancer();
        }

        public void ModifierStock()
        {
            Console.WriteLine("Titre du livre :");
            string titre = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Nouveau stock :");
            int stock = Convert.ToInt32(Console.ReadLine());
            
            Livre? livre = bibliotheque.GetLivre(titre);
            if(livre != null){
                bibliotheque.ModifierStock(livre, stock);
            }
            

            relancer();
        }

        public void ModifierStatutCommande()
        {
            Console.WriteLine("Nom du client :");
            string nomClient = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Nouveau statut :");
            string statut = Console.ReadLine() ?? string.Empty;
            Commande commande = new Commande(statut, nomClient, "", "", "");
            bibliotheque.ModifierStatutCommande(commande, statut);

            relancer();
        }

        public void AjouterCommande()
        {
            Console.WriteLine("Nom du client :");
            string nomClient = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Adresse du client :");
            string adresseClient = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Date de la commande :");
            string dateCommande = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Date de la livraison :");
            string dateLivraison = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Statut de la commande :");
            string statut = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Sélectionnez les livres à ajouter à la commande :");
            AfficherLivres();
            List<Item> items = new List<Item>();
            bool stilladding = true;
            string titre = Console.ReadLine() ?? string.Empty;
            Livre? livre = bibliotheque.GetLivre(titre);
            if(livre != null){
                items.Add(livre);
            }
            while(stilladding){
                Console.WriteLine("Ajouter un livre  à la commande? (Y/N)");
                string add = Console.ReadLine() ?? string.Empty;
                if (add == "Y" || add == "y"){
                    Console.WriteLine("Titre du livre :");
                    titre = Console.ReadLine() ?? string.Empty;
                    livre = bibliotheque.GetLivre(titre);
                    if(livre != null){
                        items.Add(livre);
                    }
                }else{
                    stilladding = false;
                }
            }
            
            Commande commande = new Commande(statut, nomClient, adresseClient, dateCommande, dateLivraison);

            bibliotheque.AjouterCommande(commande);

            relancer();
        }

        public void AfficherMenu()
        {
            Console.WriteLine("1. Ajouter un livre");
            Console.WriteLine("2. Supprimer un livre");
            Console.WriteLine("3. Afficher les livres");
            Console.WriteLine("4. Afficher les auteurs");
            Console.WriteLine("5. Afficher les livres par auteur");
            Console.WriteLine("6. Afficher les livres par genre");
            Console.WriteLine("7. Afficher les livres par langue");
            Console.WriteLine("8. Afficher les commandes");
            Console.WriteLine("9. Afficher les commandes par statut");
            Console.WriteLine("10. Modifier le stock d'un livre");
            Console.WriteLine("11. Modifier le statut d'une commande");
            Console.WriteLine("12. Ajouter une commande");
            Console.WriteLine("13. Quitter");
        }

        public void ChoisirMenu(){
            string choix = Console.ReadLine() ?? string.Empty;
            switch(choix){
                case "1":
                    AjouterLivre();
                    break;
                case "2":
                    SupprimerLivre();
                    break;
                case "3":
                    AfficherLivres();
                    break;
                case "4":
                    AfficherAuteurs();
                    break;
                case "5":
                    AfficherLivresParAuteur();
                    break;
                case "6":
                    AfficherLivresParGenre();
                    break;
                case "7":
                    AfficherLivresParLangue();
                    break;
                case "8":
                    AfficherCommandes();
                    break;
                case "9":
                    AfficherCommandesParStatut();
                    break;
                case "10":
                    ModifierStock();
                    break;
                case "11":
                    ModifierStatutCommande();
                    break;
                case "12":
                    AjouterCommande();
                    break;
                case "13":
                    break;
            }
        }
    
        public void TestJsonAuteur(){
            try{
                Auteur auteur = new Auteur("test");
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(auteur, options);
                File.WriteAllText("auteur.txt", json);
                Console.WriteLine(json);
            }catch (Exception ex){
                Console.WriteLine("Erreur lors de la sauvegarde des données : "+ ex.Message );
            }
            
        }
    }
    class Bibliotheque
    {
        // instance unique de la classe Bibliotheque (singleton pattern)
        private static Bibliotheque? _instance;
        // lien vers le fichier de sauvegarde des données
        private const string FilePath = "bibliotheque.txt";
        // listes de livres, auteurs et commandes
        public List<Livre> Livres = new List<Livre>();
        public List<Auteur> Auteurs = new List<Auteur>();
        public List<Commande> Commandes = new List<Commande>();

        // objet qui réunit les listes de livres, auteurs et commandes de la bibliothèque pour les sauvegarder dans le fichier json
        public BibliothequeData Listes = new BibliothequeData();

        // constructeur privé pour empêcher la création d'instances de la classe Bibliotheque et rrécuperer les données du fichier json
        private Bibliotheque()
        {
            if (File.Exists(FilePath))
            {
                ChargerDonnees();
            }
            else
            {
                File.Create(FilePath).Dispose();
            }
        }

        // méthode qui retourne l'instance unique de la classe Bibliotheque
        public static Bibliotheque GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Bibliotheque(); 
            }
            return _instance;
        }

        // méthode qui récupère les données du fichier json
        private void ChargerDonnees()
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                var bibliothequeData = JsonSerializer.Deserialize<BibliothequeData>(json);
                if (bibliothequeData != null)
                {
                    Console.WriteLine("Chargement des données...");
                    Console.WriteLine("data : " + bibliothequeData);
                    Livres = bibliothequeData.Livres;
                    Auteurs = bibliothequeData.Auteurs;
                    Commandes = bibliothequeData.Commandes;
                    Listes = bibliothequeData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des données : {ex.Message}");
            }
        }
        
        // méthode qui sauvegarde les données dans le fichier json
        public void SauvegarderDonnees()
        {
            try{
                Listes = new BibliothequeData(Livres, Auteurs, Commandes);
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(Listes, options);
                File.WriteAllText(FilePath, json);
            }catch(Exception ex){
                Console.WriteLine($"Erreur lors de la sauvegarde des données : {ex.Message}");
            }
            
        }

        
        /* public Bibliotheque(List<Livre> livres, List<Auteur> auteurs, List<Commande> commandes)
        {
            Livres = livres;
            Auteurs = auteurs;
            Commandes = commandes;
        } */
        
        public void AjouterLivre(Livre livre)
        {
            Livres.Add(livre);
            File.WriteAllText(FilePath, livre.Titre);
            Console.WriteLine("Livre \""+livre.Titre+"\" ajouté");
            SauvegarderDonnees();
        }

        public void SupprimerLivre(string titre)
        {
            foreach (Livre livre in Livres)
            {
                if (livre.Titre == titre)
                {
                    Livres.Remove(livre);
                    Console.WriteLine("Livre \"" + livre.Titre + "\" supprimé");
                }
            }
            SauvegarderDonnees();
        }

        public void AfficherLivres()
        {
            foreach (Livre livre in Livres)
            {
                livre.AfficherDetails();
            }
        }

        public Livre? GetLivre(string titre)
        {
            foreach (Livre livre in Livres)
            {
                if (livre.Titre == titre)
                {
                    return livre;
                }
            }
            return null;
        }
        public void AfficherAuteurs()
        {
            foreach (Auteur auteur in Auteurs)
            {
                Console.WriteLine(auteur.Nom);
            }
        }

        public void AfficherLivresParAuteur(Auteur auteur)
        {
            foreach (Livre livre in Livres)
            {
                if (livre.Auteur.Nom == auteur.Nom)
                {
                    livre.AfficherDetails();
                }
            }
        }

        public void AfficherLivresParGenre(string genre)
        {
            foreach (Livre livre in Livres)
            {
                if (livre.Genre == genre)
                {
                    livre.AfficherDetails();
                }
            }
        }

        public void AfficherLivresParLangue(string langue)
        {
            foreach (Livre livre in Livres)
            {
                if (livre.Langue == langue)
                {
                    livre.AfficherDetails();
                }
            }
        }

        public void AfficherCommandes()
        {
            foreach (Commande commande in Commandes)
            {
                Console.WriteLine(commande.NomClient);
            }
        }

        public void AfficherCommandesParStatut(string statut)
        {
            foreach (Commande commande in Commandes)
            {
                if (commande.Statut == statut)
                {
                    Console.WriteLine(commande.NomClient);
                }
            }
        }

        public void ModifierStock(Livre livre, int stock)
        {
            livre.Stock = stock;
            SauvegarderDonnees();
        }

        public void ModifierStatutCommande(Commande commande, string statut)
        {
            commande.Statut = statut;
            SauvegarderDonnees();
        }

        public void AjouterCommande(Commande commande)
        {
            Commandes.Add(commande);
            SauvegarderDonnees();
        }

        public void AjouterAuteur(Auteur auteur)
        {
            Auteurs.Add(auteur);
            SauvegarderDonnees();
        }

        public void SupprimerAuteur(Auteur auteur)
        {
            Auteurs.Remove(auteur);
            SauvegarderDonnees();
        }

        public void SupprimerCommande(Commande commande)
        {
            Commandes.Remove(commande);
            SauvegarderDonnees();
        }

        public void SupprimerCommandeParNom(string nom)
        {
            foreach (Commande commande in Commandes)
            {
                if (commande.NomClient == nom)
                {
                    Commandes.Remove(commande);
                }
            }
            SauvegarderDonnees();
        }
    }

    // classe qui implémente la classe abstraite Item et qui définit les propriétés propres aux livres
    class Livre : Item
    {
        public Auteur Auteur { get; set; }
        public string Date { get; set; }
        public string Genre { get; set; }

        public Livre(string titre, Auteur auteur, string date, string genre, string langue, decimal prix, int stock)
            : base(titre, langue, stock, prix)
        {
            Auteur = auteur;
            Date = date;
            Genre = genre;
        }

        // méthode qui affiche les détails d'un livre
        public override void AfficherDetails()
        {
            Console.WriteLine($"{Titre}, {Auteur.Nom}, {Date}, {Genre}, {Langue}, {Prix}€, {Stock} en stock");
        }
    }

    // classe qui définit les propriétés propres aux commandes
    class Commande
    {
        public string Statut { get; set; }
        public string NomClient { get; set; }
        public string AdresseClient { get; set; }
        public string DateCommande { get; set; }
        public string DateLivraison { get; set; }
        public decimal Prix { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

        public Commande(string statut, string nomClient, string adresseClient, string dateCommande, string dateLivraison)
        {
            Statut = statut;
            NomClient = nomClient;
            AdresseClient = adresseClient;
            DateCommande = dateCommande;
            DateLivraison = dateLivraison;
            Prix = CalculerTotal();
        }

        public void AjouterItem(Item item)
        {
            Items.Add(item);
        }

        public decimal CalculerTotal()
        {
            return Items.Sum(item => item.Prix);
        }

        public void AfficherCommande()
        {
            Console.WriteLine($"Commande pour {NomClient} à {AdresseClient} - Total: {CalculerTotal()}€");
            foreach (var item in Items)
            {
                item.AfficherDetails();
            }
        }
    }
    class Auteur
    {
        public string? Nom { get; set; }

        public Auteur() { }
        public Auteur(string nom)
        {
            Nom = nom;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.AfficherMenu();
            client.ChoisirMenu();


        }
    }
}
