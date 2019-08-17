using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace MockTest
{
    [TestClass]
    public class UnitTest1
    {
        private List<int> _data;

        [TestInitialize]
        public void Init()
        {
            _data = new List<int>() { 1, 2, 3 };
        }

        [TestMethod]
        public void WaitingNullList()
        {

            var serviceMock = new Mock<IMeteringDataServiceAgent>();

            var result = serviceMock.Object.GetDataList();
            //Normal case because mock not defined (null by default) and return a List
            Assert.IsNull(result);
        }

        [TestMethod]
        public void WaitingNullIEnumerable()
        {
            var serviceMock = new Mock<IMeteringDataServiceAgent>();
            var result = serviceMock.Object.GetData();

            //Expected null because the mock it's not defined but it return an empty list
            //Maybe because the method return an IEnumerable
            Assert.IsNull(result);

        }

        [TestMethod]
        public void DateTimeOffset()
        {
            var serviceMock = new Mock<IMeteringDataServiceAgent>();
            serviceMock.Setup(s => s.GetDataList(It.IsAny<DateTimeOffset>())).Returns(_data);
            var result = serviceMock.Object.GetDataList(DateTime.Now);

            //Normal case
            Assert.AreEqual(_data, result);

        }

        [TestMethod]
        public void DateTimeNotWorking()
        {
            var serviceMock = new Mock<IMeteringDataServiceAgent>();
            serviceMock.Setup(s => s.GetDataList(It.IsAny<DateTime>())).Returns(_data);
            var result3 = serviceMock.Object.GetDataList(new DateTime(2019,1,1));

            //Expected the list but received null
            //Because during the setup I put It.IsAny<DateTime>() and not It.IsAny<DateTimeOffset>()
            Assert.AreEqual(_data, result3);

        }
    }

    public interface IMeteringDataServiceAgent
    {
        IEnumerable<int> GetData();
        List<int> GetDataList();
        List<int> GetDataList(DateTimeOffset date);
    }


}
