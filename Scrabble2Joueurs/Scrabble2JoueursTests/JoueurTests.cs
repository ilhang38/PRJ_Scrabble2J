using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble2Joueurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2Joueurs.Tests
{
    [TestClass()]
    public class JoueurTests
    {
        [TestMethod()]
        public void JoueurTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AjouterMotTest()
        {
            Joueur E1 = new Joueur("p");
            E1.AjouterMot("poulet");
            Assert.AreEqual(1,E1.GetNbMots());
            Assert.AreEqual(8, E1.GetTotalPoints());
            E1.AjouterMot("nugget");
            Assert.AreEqual("nugget", E1.GetLesMots());
            Assert.AreEqual(8, E1.GetTotalPoints());
        }
        

        [TestMethod()]
        public void GetTotalPointsTest()
        {
             Joueur E1 = new Joueur("p");
            E1.AjouterMot("poulet");
            Assert.AreEqual("poulet",E1.GetLesMots());
            Assert.AreEqual(8,E1.GetTotalPoints());
           
        }

        [TestMethod()]
        public void GetNbMotsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetLesMotsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MotMeilleurTest()
        {
            Joueur E1 = new Joueur("p");
            E1.AjouterMot("mots");
            E1.AjouterMot("poulet");
            E1.AjouterMot("m");
            E1.AjouterMot("mot");
            Assert.AreEqual("poulet", E1.MotMeilleur());

        }
    }
}