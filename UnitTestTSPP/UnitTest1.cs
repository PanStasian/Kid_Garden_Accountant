using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coursach.DAL;
using Coursach;
using System.Collections.Generic;

namespace UnitTestTSPP
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetUnitsByID_Test()
        {
            PodrazdelenieRepository reposit = new PodrazdelenieRepository();
            Podrazdelenie unit = reposit.Read(4);
            Assert.IsNotNull(unit);
        }

        [TestMethod]
        public void GetAllUnits_Test()
        {
            PodrazdelenieRepository reposit = new PodrazdelenieRepository();
            List<Podrazdelenie> unit = reposit.Read();
            Assert.IsNotNull(unit);
        }

        [TestMethod]
        public void AddUnit_Test()
        {
            PodrazdelenieRepository reposit = new PodrazdelenieRepository();
            Podrazdelenie unit = new Podrazdelenie()
            {
                Podrazdelenie_Name = "Test"
            };
            reposit.Create(unit);
            Assert.IsNotNull(unit.Podrazdelenie_Code);
        }

        [TestMethod]
        public void DeleteUnit_Test()
        {
            PodrazdelenieRepository reposit = new PodrazdelenieRepository();
            Podrazdelenie unit = reposit.Read(27);
            reposit.Delete(unit.Podrazdelenie_Code);
            Assert.AreNotEqual(2,unit.Podrazdelenie_Code);            
        }

        [TestMethod]
        public void UpdateUnit_Test()
        {
            PodrazdelenieRepository reposit = new PodrazdelenieRepository();
            Podrazdelenie unit = reposit.Read(5);
            unit.Podrazdelenie_Name = "Test";
            reposit.Update(unit);
            Assert.AreNotEqual("Smile", "Test");
        }
    }
}
